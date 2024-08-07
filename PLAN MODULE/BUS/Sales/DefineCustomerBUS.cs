using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.Sales
{
    internal class DefineCustomerBUS : BaseBUS
    {
        public bool CreateCustomer(string cusName, string cusID, string companyName, string address, string phone, string opReciver, string email, string information)
        {

            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`DEFINE_CUSTOMER` (`CUSTOMER_NAME`, `CUSTOMER_ID`, `CUSTOMER_KEY`) " +
                $"VALUES ('{cusName}', '{cusID}', '{cusName}')   ON DUPLICATE KEY UPDATE `CUSTOMER_NAME` ='{cusName}' ,  `CUSTOMER_KEY` = '{cusName}' ;";

            sql += $"INSERT INTO `TRACKING_SYSTEM`.`CUSTOMER_DETAIL` (`CUSTOMER_ID`, `COMPANY_NAME`, `ADDRESS`, `PHONE`, `RECEIVER`, `REPRESENTATIVE` , `EMAIL`, `INFORMATION`) " +
                $"VALUES ('{cusID}', '{companyName}', '{address}', '{phone}', '{companyName}', '{opReciver}', '{email}', '{information}') " +
                $"  ON DUPLICATE KEY UPDATE  `COMPANY_NAME` =  '{companyName}', `ADDRESS` =  '{address}', `PHONE` =  '{phone}', `RECEIVER` = '{companyName}', `REPRESENTATIVE`  = '{opReciver}', `EMAIL` = '{email}', `INFORMATION` = '{information}' ;";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
    }
}
