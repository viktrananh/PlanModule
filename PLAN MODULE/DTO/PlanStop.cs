using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.CreatPlan
{
    public class PlanStopConfig : PlanStop
    {
        public int ID { get; set; }
        public string TimeName { get; set; }
        public string Shift { get; set; }

        public PlanStopConfig() { }
        public PlanStopConfig(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            TimeName = row["TIME"].ToString();
            TimeStart = DateTime.Parse(row["TIME_START"].ToString()).ToShortTimeString();
            TimeEnd = DateTime.Parse(row["TIME_END"].ToString()).ToShortTimeString();
            Shift = row["SHIFT"].ToString();
        }
    }
    public class PlanStop
    {
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }
}
