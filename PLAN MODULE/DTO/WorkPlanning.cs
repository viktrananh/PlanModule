using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE
{
    public class WorkPlanning
    {
        public string WorkID    { get; set; }
        public string Model { get; set; }
        public string line { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public int Qty  { get; set; }
        public int qtyPcsMarked { get; set; }
        public int qtyPanelMarked { get; set; }
        public int Times { get; set; }
    }
}
