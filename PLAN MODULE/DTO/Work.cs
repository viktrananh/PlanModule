using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class Work
    {
        public string WorkID { get; set; }
        public string ModelWork { get; set; }
        public string CusId { get; set; }
        public int PCS { get; set; }
        public int TotalPCS { get; set; }
        public int TempNumber { get; set; }
        public DateTime DateCreat { get; set; }
        public string Operater { get; set; }
        public string Status { get; set; }
        public string PO { get; set; }
        public string BomVersion { get; set; }
        public int IsSample { get; set; }
        public int  IsRMA { get; set; }
        public int IsXout { get; set; }
        public int PcbXO { get; set; }
        public string WorkParent { get; set; }
        public string Comment { get; set; }
        public DateTime MFGDate { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public int Month_Finish_PP { get; set; }
        public Work() { }
        public Work(DataRow row)
        {
            WorkID = row["WORK_ID"].ToString();
            ModelWork = row["MODEL"].ToString();
            PCS = int.Parse(row["PCBS"].ToString());
            TotalPCS = int.Parse(row["TOTAL_PCBS"].ToString());
            CusId = row["CUSTOMER"].ToString();
            TempNumber = int.Parse(row["STAMP_NUMBER"].ToString());
            DateCreat = DateTime.Parse(row["DATE_CREATE"].ToString());
            Operater = row["CREATER"].ToString();
            BomVersion = row["BOM_VERSION"].ToString();
            Status = string.IsNullOrEmpty(row["STATUS"].ToString()) ? "OPEN" : row["STATUS"].ToString();
            PO = row["PO"].ToString();
            IsRMA = int.Parse(row["IS_RMA"].ToString());
            IsXout = int.Parse(row["IS_XOUT"].ToString());
            PcbXO = int.Parse(row["PCB_XOUT"].ToString());
            IsSample = int.Parse(row["IS_SAMPLE"].ToString());
            WorkParent = string.IsNullOrEmpty(row["WORK_MOTHER"].ToString()) ? "N/A" : row["WORK_MOTHER"].ToString();
            Comment = string.IsNullOrEmpty(row["COMMENT"].ToString()) ? "N/A" : row["COMMENT"].ToString();
            MFGDate = string.IsNullOrEmpty(row["MFGDate"].ToString()) ? DateTime.Now : DateTime.Parse(row["MFGDate"].ToString());
            FirstDate = string.IsNullOrEmpty(row["FirstDate"].ToString()) ? DateTime.Now : DateTime.Parse(row["FirstDate"].ToString());
            LastDate = string.IsNullOrEmpty(row["LastDate"].ToString()) ? DateTime.Now : DateTime.Parse(row["LastDate"].ToString());
            Month_Finish_PP= int.Parse(row["Month_Finish_PP"].ToString());
        }

    }
}
