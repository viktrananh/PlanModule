using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class StationModel
    {
        public string  StationID { get; set; }
        public string StationName { get; set; }
        public int IsQuantity { get; set; }
        public string  Side { get; set; }
        public int TemType { get; set; }
        public int ClusterID { get; set; }
        public string ClusterKey { get; set; }
    }
}
