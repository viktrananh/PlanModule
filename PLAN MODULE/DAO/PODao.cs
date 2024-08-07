using DevComponents.DotNetBar;
using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO
{
    public class PODao : BaseDAO
    {
        public DataTable GetPOs()
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.POOrder;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
      
        public POOrder GetPOOrderByPOName(string po)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.POOrder where PO = '{po}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new POOrder();
            POOrder order= new POOrder(dt.Rows[0]);
            return order;
        }

        
        public DataTable GetPODetailByPO(string po, string cusId)
        {
         
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL A " +
              $" inner JOIN  TRACKING_SYSTEM.PODetail B ON  A.MODEL_ID = B.ModelId and  B.PO = '{po}' where A.CUS_ID ='{cusId}'  ;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public DataTable GetPOByPOName(string PO)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.POOrder WHERE PO = '{PO}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }

        public bool IsPOApplyWork(string po)
        {
            DataTable DT = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER WHERE PO='{po}';");
            if (istableNull(DT)) return false;
            return true;
        }

        public bool IsPOOfModel(string model, string po)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.PODetail where PO = '{po}' AND ModelId= '{model}';";
            DataTable dt = mySql.GetDataMySQL(sql);

            if (istableNull(dt)) return false;
            return true;
        }
        public bool CreatWorkWithPO(string po)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER WHERE PO = '{po}' ;";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            return true;

        }

        public POOrder GetDefaultPOOrder(string cusId)
        {
            POOrder order = new POOrder();
            List<PODetail> ls = new List<PODetail>();
            DataTable dt = GetLsModel(cusId);
            if (istableNull(dt)) ls = new List<PODetail>(); ;
            ls = (from r in dt.AsEnumerable()
                                       select new PODetail()
                                       {
                                           ModelId = r.Field<string>("MODEL_ID"),
                                           CusModel = r.Field<string>("CUS_MODEL"),
                                           Count = 0,
                                           MFGDate = DateTime.Now.AddDays(1),
                                           Selected = false,
                                       }).ToList();
            order.PODetails = ls;
            return order;
        }

       
    }
}
