using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class Part_Request
    {
        public string workID { get; set; }
        public string part_number { get; set; }
        public string unit { get; set; }
        public int qty { get; set; }
    }
}
