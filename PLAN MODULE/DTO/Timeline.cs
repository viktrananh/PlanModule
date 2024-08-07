using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.PlanExport
{
    public class Timeline
    {
        public string TimeExport { get; set; }
        public  int  Request { get; set; }
        public int Real { get; set; }
        public int Diffrent { get; set; }
        public string Note { get; set; }
        public int BoxC { get; set; }
        public int BoxP { get; set; }
        public List<Timeline> TimelineHour { get; set; }
    }
}
