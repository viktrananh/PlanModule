using PLAN_MODULE.DTO.Planed.BillExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.PLAN
{
    internal class BillExportMateriaBUS :BaseBUS
    {
        public bool CancelBill(string bill)
        {
            string sql = $"UPDATE STORE_MATERIAL_DB.BILL_REQUEST_EX set STATUS_EXPORT = '-1' where BILL_NUMBER='{bill}' ;";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool SaveBillExport(List<BillExportMaterialDetail> bills, string billnumber, string wo, int pcbRequest, string process, string bomver, string userID) 
        {
            string sql = string.Empty;

            sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_EX` (`BILL_NUMBER`, `WORK_ID`, `PCBS`, `DATE_EXPORT`, `OP`, `TIME_CREATE`," +
              " `AREA`, `STATUS_EXPORT`, `TYPE_BILL`, `SUB_TYPE` , `BOM_VERSION`)" +
                  $" VALUES ('{billnumber}', '{wo}', '{pcbRequest}', now(), '{userID}', now(), " +
                  $" '{process}',  0 , 5 , 0, '{bomver}');";
            foreach (var item in bills)
            {
                sql += "INSERT INTO `STORE_MATERIAL_DB`.`REQUEST_EXPORT` (`BILL_NUMBER`, `WORK_ID`, `MAIN_PART`, `PART_NUMBER`, `QTY`) " +
                       $" VALUES ('{billnumber}', '{wo}', '{item.MainPart}', '{item.PartNumber}', '{item.Qty}');";
            }

            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
    }
}
