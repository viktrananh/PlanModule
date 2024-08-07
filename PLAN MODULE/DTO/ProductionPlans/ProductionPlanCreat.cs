using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.ProductionPlans
{
    public class ProductionPlanCreat
    {

        public string WorkId { get; set; }
        public string ModelId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime MFGDate { get; set; }
        public int Count { get; set; }
        public int ClusterDetailId { get; set; }
        public string PlanIndex { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }
    }
}
