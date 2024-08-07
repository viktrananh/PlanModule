using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DocumentFormat.OpenXml.Office2010.Excel;
using PLAN_MODULE.DTO.Planed;
using PLAN_MODULE.DTO.ProductionPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.PLAN
{
    public class ProductionPlanBUS  :BaseBUS
    {
        public bool CreatPlan(List<ProductionPlanCreat> planCreat, string Op)
        {
            string sql = string.Empty;
            foreach (var item in planCreat)
            {
                sql += $"INSERT INTO `MASTER`.`ProductionPlan` (`WorkId`, `ModelId`, `StartTime`, `EndTime`, `MFGDate`, `ClusterDetailId`, `PlanIndex`, `Count` , `Op`) " +
                    $" VALUES ('{item.WorkId}', '{item.ModelId}', '{item.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{item.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{item.MFGDate.ToString("yyyy-MM-dd")}', '{item.ClusterDetailId}',  '{item.PlanIndex}', '{item.Count}', '{Op}' );";
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;

        }



        public bool CancelPlan(ProductionPlanVM ProductionPlanVM, string Op)
        {
            string sql = $"UPDATE  MASTER.ProductionPlan SET State = -1 WHERE Id = '{ProductionPlanVM.Id}';";
            sql += $"INSERT INTO `MASTER`.`ProductionPlanHistory` (`ProductionPlanId`, `Time`, `Action`, `Op`) VALUES ('{ProductionPlanVM.Id}', now(), 'Cancel', '{Op}');";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
       
        public bool FinishPlan(ProductionPlanVM ProductionPlanVM) 
        {
            string sql = $"UPDATE  MASTER.ProductionPlan SET State = 2 WHERE Id = '{ProductionPlanVM.Id}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }


        public bool ClosePlan(ProductionPlanVM ProductionPlanVM)
        {
            string sql = $"UPDATE  MASTER.ProductionPlan SET State = 1, EndTime = now() WHERE Id = '{ProductionPlanVM.Id}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }


        public bool ClosePlan(PlanDetail planDetail, DateTime timeClose, string op)
        {

            var closeFromPlan = planDetail.TimeFrames.Where(x => x.StartTime <= timeClose && x.EndTime > timeClose);
            int SubID = closeFromPlan.FirstOrDefault().ID;
            double productionTime = planDetail.ProductionTime;
            string index = planDetail.IndexPlan;
            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING_HISTORY` (`WORK_ID`, `TIME_ACTION`, `ACTION`, `OP`, `NOTE`) VALUES " +
                $"('{planDetail.WorkID}', now(), 'CLOSE', '{op}', '{planDetail.IndexPlan}-{SubID}');";

            int sum = planDetail.TimeFrames.Where(x => x.ID < SubID).Sum(x=>x.Qty);

            foreach (var item in planDetail.TimeFrames)
            {
                if(item.ID == SubID)
                {
                    DateTime timeStart = item.StartTime;

                    TimeSpan timeSpan = timeClose.Subtract(timeStart);
                    double timePlan = timeSpan.TotalSeconds;
                    int Qty = (int)Math.Floor(timePlan / productionTime);
                    sum += Qty;

                    sql += $"UPDATE TRACKING_SYSTEM.WORK_PLANNING SET QTY_REAL = '{Qty}' , END_TIME = '{timeClose.ToString("yyyy-MM-dd HH:mm:ss")}'  WHERE PLAN_INDEX = '{index}' AND PLAN_ID ='{item.ID}';";
                    sql += $"UPDATE  TRACKING_SYSTEM.WORK_PLANNING_OVERVIEW SET END_TIME = '{timeClose.ToString("yyyy-MM-dd HH:mm:ss")}', QTY = '{Qty}' WHERE PLAN_INDEX = '{index}';";
                }
                else if(item.ID > SubID)
                {
                    sql += $"UPDATE TRACKING_SYSTEM.WORK_PLANNING SET `STATUS`= 0 WHERE PLAN_INDEX = '{index}' AND PLAN_ID ='{item.ID}';";
                }
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool CreatCycletimeSMT(List<CycleTime> cyc, List<StationModel> stationModels, string modelID, string process, string OP)
        {
            string sql = string.Empty;
            foreach (var item in cyc)
            {
                string stationName = stationModels.Any(x => x.Side == item.Side) ? stationModels.Find(x => x.Side == item.Side).StationName : "";
                sql += $"INSERT INTO `TRACKING_SYSTEM`.`PRODUCT_CYCLE_TIME` (`MODEL_ID`, `SIDE`, `LINE`, `PROCESS`, `CYCLE_TIME`, `STATION_NAME`, `OP`) " +
              $"VALUES ('{modelID}', '{item.Side}', 'S{item.LineID}', '{process}', '{item.Times}','{stationName}', '{OP}') ON DUPLICATE KEY UPDATE `CYCLE_TIME` = '{item.Times}', `OP` = '{OP}';";
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

    }
}
