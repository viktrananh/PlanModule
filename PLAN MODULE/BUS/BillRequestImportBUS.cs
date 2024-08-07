
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.PLAN
{
    internal class BillRequestImportBUS : BaseBUS
    {
       
        public bool CancelBill(string bill, string userID)
        {
            string sql = $"UPDATE  STORE_MATERIAL_DB.BILL_REQUEST_IMPORT  SET STATUS_BILL = {-1} WHERE BILL_NUMBER = '{bill}';";
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CANCEL',  now(), '{userID}');";
            sql += $"DELETE FROM STORE_MATERIAL_DB.CUSTOMER_DATA where ID > 1  AND BILL_NUMBER = '{bill}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

      
    }
}
