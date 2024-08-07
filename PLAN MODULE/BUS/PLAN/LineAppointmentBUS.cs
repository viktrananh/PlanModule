using PLAN_MODULE.DTO.Planed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.PLAN
{
    internal class LineAppointmentBUS 
        :BaseBUS
    {
        public bool SavePlan(List<PlanDetail> planNomal, string op, string route,int times, bool isNew = true)
        {
            string sql = string.Empty;
            foreach (var item in planNomal)
            {
                string workID = item.WorkID;
                string side = item.Side;
                string _line = item.Line;
                int shift = 1;
                string index = item.IndexPlan;
                if (item.StartTime.Hour > 18 || item.StartTime.Hour < 6)
                    shift = 2;
                string line = route =="SMT" ? $"S{item.Line}" : $"{item.Line}";
                string action = isNew ? "CREATE" : "EDIT";
                sql += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING_OVERVIEW` (`WORK_ID`, `QTY`, `SIDE`, `START_TIME`, `END_TIME`, `LINE`," +
                    $" `MFG`, `PLAN_INDEX`, `QTY_REAL`, `CYCLE_TIME` , `PRODUCTION_TIME` , `PROCESS`) VALUES" +
                    $" ('{workID}', '{item.Qty}', '{side}', '{item.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{item.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{line}'," +
                    $" '{item.StartTime.ToString("yyyy-MM-dd")}', '{index}', '{item.Qty}', '{item.CycleTime}', '{item.ProductionTime}', '{route}');";
                sql += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING_HISTORY` (`WORK_ID`, `TIMES`, `TIME_ACTION`, `ACTION`, `OP`) VALUES ('{item.WorkID}', '{item.Times}', now(), '{action}', '{op}');";

                foreach (var subItem in item.TimeFrames)
                {
                    times++;

                    int subQty = subItem.Qty;
                    DateTime subStartTime = subItem.StartTime;
                    DateTime subEndTime = subItem.EndTime;
                    int shiftEx = 1;
                    if (subStartTime.Hour > 18 || subStartTime.Hour < 6)
                    {
                        shiftEx = 2;
                    }
                    DateTime MFG = subStartTime;
                    if(subStartTime.Hour >= 0 && subStartTime.Hour < 6)
                    {
                        MFG = MFG.AddDays(-1);
                    }
                    sql += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING` (`WORK_ID`, `QTY`, `QTY_REAL`, `SIDE`, `START_TIME`, `END_TIME`," +
                        $" `LINE`, `MFG`, `SHIFT`, `OP`, `CREAT_TIME`, `PROCESS`,  `DESCRIPTION`, `TIMES`, `PLAN_INDEX`, `PLAN_ID`) " +
                               $"VALUES ('{workID}', '{subQty}', '{subQty}', '{side}', '{subStartTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{subEndTime.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                               $" '{line}', '{MFG.ToString("yyyy-MM-dd")}', '{shiftEx}', '{op}', NOW(), '{route}' , '{item.Description}' , '{times}', '{index}', '{subItem.ID}') ;";
                }
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;

        }
        public bool UpdatePlan(PlanDetail planDetail, string op, string route)
        {
            int shift = 1;
            if (planDetail.StartTime.Hour > 18 || planDetail.StartTime.Hour < 6)
                shift = 2;

            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING` (`WORK_ID`, `QTY`, `SIDE`, `START_TIME`, `END_TIME`, `LINE`, `MFG`, `SHIFT`, `OP`, `CREAT_TIME`, `ROUTE`,  `DESCRIPTION`, `TIMES`) " +
                $"VALUES ('{planDetail.WorkID}', '{planDetail.Qty}', '{planDetail.Side}', '{planDetail.StartTime.ToString("yyyy-MM-dd hh:mm")}', '{planDetail.EndTime.ToString("yyyy-MM-dd hh:mm")}'," +
                $" '{planDetail.Line}', DATE('{planDetail.StartTime.ToString("yyyy-MM-dd hh:mm")}'), '{shift}', '{op}', NOW(), '{route}' , '{planDetail.Description}' , '{planDetail.Times}');";
            sql += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING_HISTORY` (`WORK_ID`, `TIMES`, `TIME_ACTION`, `ACTION`, `OP`) VALUES ('{planDetail.WorkID}', '{planDetail.Times}', now(), 'UPDATE', '{op}');";

            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

        public bool CancelPlan(PlanDetail planDetail, string op)
        {
            string sql = string.Empty;
            sql += $"UPDATE TRACKING_SYSTEM.WORK_PLANNING SET `STATUS` = 0 WHERE WORK_ID='{planDetail.WorkID}' AND TIMES = '{planDetail.Times}';";
            sql += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_PLANNING_HISTORY` (`WORK_ID`, `TIMES`, `TIME_ACTION`, `ACTION`, `OP`) VALUES ('{planDetail.WorkID}', '{planDetail.Times}', now(), 'CANCLE', '{op}');";
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
