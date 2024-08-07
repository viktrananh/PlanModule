using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillExport
{
    internal class BillExportMaterial
    {
        public string BillNumber { get; set; }
        public string WorkID { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
