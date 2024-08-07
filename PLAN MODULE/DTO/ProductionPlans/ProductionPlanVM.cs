using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.ProductionPlans
{
    public class ProductionPlanVM
    {
        public int Id { get; set; }
        public string WorkId { get; set; }
        public string ModelId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime MFGDate { get; set; }
        public int Count { get; set; }
        public int ClusterDetailId { get; set; }
        public double CycleTime { get; set; }
        public string PlanIndex { get; set; }
        public double Performance { get; set; }
        public int LineId { get; set; }
        public string LineName { get; set; }
        public int ClusterId { get; set; }
        public string ClusterName { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }
        public int StageId { get; set; }
        public string StageName { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public int State { get; set; }
    }
}
