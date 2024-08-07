using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.Sales
{
    internal class DefineCustomerDAO : BaseDAO
    {
        public bool ExistCustomer(string cusName, string cusID)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER WHERE CUSTOMER_NAME='{cusName}' AND CUSTOMER_ID='{cusID}';");
            if (istableNull(dt)) return false;
            return true;
        }

        public DataTable GetAllCustomer()
        {
            DataTable DT = mySQL.GetDataMySQL("SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER A INNER JOIN TRACKING_SYSTEM.CUSTOMER_DETAIL B ON A.CUSTOMER_ID = B.CUSTOMER_ID;");
            return DT;
        }
    }
}
