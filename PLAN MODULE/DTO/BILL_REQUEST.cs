using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PLAN_MODULE
{
    class BILL_REQUEST
    {
        public string BillNumber { get; set; }
        public string WorkID { get; set; }  
        public string MainPart { get; set; }
        public string PartNumber { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public float Rate { get; set; }
        public float NumberPCBs { get; set; }

    }
}
