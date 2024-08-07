
using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class PlanDetail : TimeFrame
    {
        ProductionPlanDAO lineAppointmentDAO = new ProductionPlanDAO();
        public string WorkID { get; set; }
        public string Model { get; set; }
        public string CusModel { get; set; }
        public string BomVer { get; set; }
        public string Side { get; set; }
        public string Line { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public int Times { get; set; }
        public string IndexPlan { get; set; }
        public double CycleTime { get; set; }
        public double ProductionTime { get; set; }
        public int PcbOnPanel { get; set; }

        public List<TimeFrame> TimeFrames = new List<TimeFrame>();


        public PlanDetail() { }
        public PlanDetail(DataRow row)
        {
            WorkID = row["WORK_ID"].ToString();
            Model = row["MODEL"].ToString();
            CusModel = row["MODEL_NAME"].ToString();
            BomVer = row["BOM_VERSION"].ToString();
            Side = row["SIDE"].ToString();
            Qty = int.Parse( row["QTY"].ToString());
            Line = row["LINE"].ToString();
            StartTime = DateTime.Parse( row["START_TIME"].ToString());
            EndTime = DateTime.Parse(row["END_TIME"].ToString());
            Route = row["PROCESS"].ToString();
            Description = row["DESCRIPTION"].ToString();
            IndexPlan = row["PLAN_INDEX"].ToString();
            CycleTime = double.Parse(row["CYCLE_TIME"].ToString());
            ProductionTime = double.Parse(row["PRODUCTION_TIME"].ToString());
            PcbOnPanel = int.Parse(row["PCBS"].ToString());
            TimeFrames = lineAppointmentDAO.GetTimeFrames(IndexPlan);
        }
      
    }
}
