using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    internal class Work
    {
        public string WorkID { get; set; }
        public string ModelWork { get; set; }
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

        public Work() { }
        public Work(DataRow row)
        {
            WorkID = row["WORK_ID"].ToString();
            ModelWork = row["MODEL"].ToString();
            PCS = int.Parse(row["PCBS"].ToString());
            TotalPCS = int.Parse(row["TOTAL_PCBS"].ToString());
            TempNumber = int.Parse(row["STAMP_NUMBER"].ToString());
            DateCreat = DateTime.Parse(row["DATE_CREATE"].ToString());
            Operater = row["CREATER"].ToString();
            BomVersion = row["BOM_VERSION"].ToString();
            Status = string.IsNullOrEmpty(row["STATUS"].ToString()) ? "OPEN" : row["STATUS"].ToString();
            PO = row["PO"].ToString();
            IsRMA = int.Parse(row["IS_RMA"].ToString());
            IsXout = int.Parse(row["IS_XOUT"].ToString());
            IsSample = int.Parse(row["IS_SAMPLE"].ToString());
            WorkParent = string.IsNullOrEmpty(row["WORK_MOTHER"].ToString()) ? "N/A" : row["WORK_MOTHER"].ToString();
            Comment = string.IsNullOrEmpty(row["COMMENT"].ToString()) ? "N/A" : row["COMMENT"].ToString();
        }

    }
}
