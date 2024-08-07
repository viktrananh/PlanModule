using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
  
    public class BillImportDetail
    {
        public int id { get; set; }
        public string model { get; set; }
        public int recivedqty { get; set; }
        public string cusPart { get; set; }
        public string interPart { get; set; }
        public string mfgPart { get; set; }
        public string billNo { get; set; }
        public DateTime createTime { get; set; }
        public string creater { get; set; }

    }
}
