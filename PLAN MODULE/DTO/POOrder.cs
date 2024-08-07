using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class POOrder
    {
        PODao pODao = new PODao();

        public int Id { get; set; }
        public string CusId { get; set; }
        public string PO { get; set; }
        public DateTime DateCreat { get; set; }

        public string OP { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
        public bool Selected { get; set; }
        public List<PODetail> PODetails { get; set; }
        public POOrder() { }
        public POOrder(DataRow row)
        {
            Id = int.Parse(row["Id"].ToString());
            CusId = row["CusId"].ToString();
            PO = row["PO"].ToString();
            DateCreat = DateTime.Parse(row["DateCreat"].ToString());
            OP = row["OP"].ToString();
            Status = int.Parse( row["Status"].ToString());
            Comment = row["Comment"].ToString();
            DataTable dt = pODao.GetPODetailByPO(PO, CusId);
            PODetails = new List<PODetail>();
            foreach (DataRow item in dt.Rows)
            {
                PODetail pODetail = new PODetail(item);
                PODetails.Add(pODetail);
            }
        } 
    }
}
