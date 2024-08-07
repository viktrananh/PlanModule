using DevExpress.XtraRichEdit.Fields;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillImport
{
    public class BillImportMaterialDetail
    {
        public string PartNumber { get; set; }
        public string MFGPart { get; set; }
        public string CSPart { get; set; }
        public string Description { get; set; }
        public double Qty { get; set; }
        public int State { get; set; }
        public double UnitPrice { get; set; }
        public int Currency { get; set; }
        public double VAT { get; set; }
        public double TotalPrice { get; set; }
        public BillImportMaterialDetail() { }
        public BillImportMaterialDetail(DataRow row)
        {
            PartNumber = row["PART_NUMBER"].ToString();
            MFGPart = row["MFG_PART"].ToString();
            Qty = int.Parse( row["QTY"].ToString());
            UnitPrice = string.IsNullOrEmpty(row["UNIT_PRICE"].ToString()) ? 0 : double.Parse(row["UNIT_PRICE"].ToString());
            Currency = string.IsNullOrEmpty(row["CURRENCY"].ToString()) ? 0 : int.Parse(row["CURRENCY"].ToString());
            VAT = string.IsNullOrEmpty(row["VAT"].ToString()) ? 0 : double.Parse(row["VAT"].ToString());
            TotalPrice = string.IsNullOrEmpty(row["TOTAL_CASH"].ToString()) ? 0 : double.Parse(row["TOTAL_CASH"].ToString());
        }

    }
}
