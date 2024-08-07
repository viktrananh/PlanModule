using PLAN_MODULE.DTO.Planed.BillImport;
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
        public bool CreateDataBillImport(List<BillImportMaterialDetail> billImportMaterials, string work, string billNumber, string cusID, string timeInter, string OP, string venderID)
        {
            string sql = string.Empty;
            sql += $"DELETE FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS WHERE BILL_NUMBER = '{billNumber}';";
            sql += $"DELETE FROM  STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE BILL_NUMBER = '{billNumber}';";
            foreach (var item in billImportMaterials)
            {
                double qty = item.Qty;
                string part = item.PartNumber;
                string mfg = item.MFGPart;
                if (item.State != 1) continue;
                sql += "INSERT INTO STORE_MATERIAL_DB.BILL_IMPORT_CUS (BILL_NUMBER, WORK_ID, PART_NUMBER, MFG_PART,  QTY, OP, CREATE_TIME, DATE_CREATE, `UNIT_PRICE`, `CURRENCY`, `VAT`, `TOTAL_CASH`) VALUE" +
                $" ('{billNumber}', '{work}', '{part}', '{mfg}', '{qty}', '{OP}', NOW(), CURDATE() , '{item.UnitPrice}', '{item.Currency}' , '{item.VAT}', '{item.TotalPrice}'  ) " +
                $" ON DUPLICATE key update  `UNIT_PRICE` =  '{item.UnitPrice}', `CURRENCY` = '{item.Currency}', `VAT` =  '{item.VAT}' , `TOTAL_CASH` = '{item.TotalPrice}',  QTY=QTY+{qty}, CREATE_TIME = NOW(),  DATE_CREATE = CURDATE();";
            }
            sql += "INSERT INTO STORE_MATERIAL_DB.BILL_REQUEST_IMPORT (BILL_NUMBER, CUSTOMER, WORK_ID, CREATE_BY, CREATE_TIME, INTEND_TIME, STATUS_BILL, TYPE_BILL, `VENDER_ID`)" +
                      $" VALUE ('{billNumber}', '{cusID}', '{work}', '{OP}',now(), '{timeInter}', {1} ,'{2}', '{venderID}');";
            if (mySQL.InsertDataMySQL(sql)) return true;
        
            return false;

        }
        public bool CancelBill(string bill, string userID)
        {
            string sql = $"UPDATE  STORE_MATERIAL_DB.BILL_REQUEST_IMPORT  SET STATUS_BILL = {-1} WHERE BILL_NUMBER = '{bill}';";
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CANCEL',  now(), '{userID}');";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
    }
}
