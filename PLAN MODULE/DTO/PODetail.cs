using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class PODetail
    {
        public int Id { get; set; }
        public string ModelId { get; set; }
        public string CusModel { get; set; }
        public string PO { get; set; }
        public int Count { get; set; }
        public DateTime MFGDate { get; set; }
        public string OPCreat { get; set; }
        public int State { get; set; }
        public string Comment { get; set; }
        public bool Selected { get; set; }
        public PODetail() { }
        public PODetail(DataRow row)
        {
            Id = int.Parse(row["Id"].ToString());
            ModelId = row["MODEL_ID"].ToString();
            CusModel = row["CUS_MODEL"].ToString();
            PO = row["PO"].ToString();
            Count = string.IsNullOrEmpty(row["Count"].ToString()) ? 0 : int.Parse(row["Count"].ToString());
            MFGDate = string.IsNullOrEmpty(row["MFGDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(row["MFGDate"].ToString());
            State = string.IsNullOrEmpty(row["State"].ToString()) ? 0 : int.Parse(row["State"].ToString());
            Comment = row["Comment"].ToString();
            Selected = !string.IsNullOrEmpty(PO) && Count > 0 ? true : false;
        }
    }
}
