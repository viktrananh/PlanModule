using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.PlanExport
{
    public class ShipmentPlan
    {
        public string CusID { get; set; }
        public string WorkID { get; set; }
        public string ModelID { get; set; }
        public string  CusModel { get; set; }
        public string CusCode { get; set; }
        public int Request { get; set; }
        public int Real { get; set; }
        public List<Timeline> Timelines = new List<Timeline>();
    }
}
