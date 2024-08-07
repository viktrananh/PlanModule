using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillExport
{
    internal class BillExportMaterialDetail
    {
        public string MainPart { get; set; }
        public string PartNumber { get; set; }
        public float Qty { get; set; }
        public BillExportMaterialDetail() { }
    }
}
