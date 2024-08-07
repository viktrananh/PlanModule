using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillExport
{
    public class BookBillExportToCus
    {
        public string Work { get; set; }
        public string ModelID { get; set; }
        public string TotalRequest { get; set; }
        public int BoxCount { get; set; }
        public int RequestAfter { get; set; }
        public string BoxID { get; set; }
        public string Location { get; set; }
        public string LotNo { get; set; }
        public bool IsFQC { get; set; }
        public string StateName { get; set; }
        public DateTime DatePacking { get; set; }
        public DateTime TimePacking { get; set; }
        public BookBillExportToCus() { }
        public BookBillExportToCus(DataRow row) 
        {
            Work = row["WORK_ID"].ToString();
            ModelID = row["MODEL"].ToString();
            TotalRequest = row["REQUEST"].ToString();
            BoxCount = int.Parse(row["BOX_COUNT"].ToString());
            RequestAfter = int.Parse(row["REQUEST_AFTER"].ToString());
            BoxID = row["BOX_SERIAL"].ToString();
            LotNo = row["LOT_NO"].ToString();
            StateName = int.Parse(row["STATE"].ToString()) == 0 ? "Chưa nhập kho" : "Đã nhập kho";
        }

    }
}
