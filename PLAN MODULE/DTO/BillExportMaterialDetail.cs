using DevExpress.XtraRichEdit.Import.Xaml;
using DevExpress.XtraScheduler.iCalendar.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class BillExportMaterialDetail
    {
        public int Id { get; set; }
        public string BillNumber { get; set; }
        public string MainPart { get; set; }
        public string PartNumber { get; set; }
        public string Remain { get; set; }
        public float Qty { get; set; }
        public int RealExport { get; set; }
        public string Comment { get; set; }
        public BillExportMaterialDetail() { }

        public BillExportMaterialDetail(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            MainPart = row["MAIN_PART"].ToString();
            PartNumber = row["PART_NUMBER"].ToString();
            Qty = float.Parse(row["QTY"].ToString());
            RealExport = string.IsNullOrEmpty( row["REAL_QTY"].ToString()) ? 0 : int.Parse(row["REAL_QTY"].ToString());
        }
    }

    public class BillExportMaterialDetailReport : BillExportMaterialDetail
    {

        public string Comment { get; set; }
        public float QtyArise { get; set; }
        public int RealExportArise { get; set; }
        public BillExportMaterialDetailReport() { }
    }
    public struct partXuat
    {
        public string partNo;
        public int qty;
    }
    struct listDIDonPart
    {
        public string part;
        public DataTable didlist;
    }
}
