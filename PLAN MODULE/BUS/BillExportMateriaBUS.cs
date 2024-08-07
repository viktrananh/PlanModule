using PLAN_MODULE.DTO.Planed.BillExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DTO;
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
        public bool CreateBill(BillExportMaterial bill, string userID, string note, string process)
        {
            string sql = "";
            sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_EX` (`BILL_NUMBER`, `WORK_ID`, `MODEL_ID`, `PCBS`, `TIME_CREATE`, `STATUS_EXPORT`, `TYPE_BILL`, `SUB_TYPE`,OP , `BOM_VERSION` , `NOTE` , `AREA`) " +
                    $" VALUES ('{bill.BillId}', '{bill.WorkId}', '{bill.ModelId}', '{bill.Pcbs}', now() , 0, '{bill.BillType}' , '{bill.ExportType}','{userID}', '{bill.BomVersion}', '{note}' , '{process}') " +
                    $" ON DUPLICATE KEY UPDATE `TIME_CREATE` = now() ;";
            foreach (var item in bill.BillExportMaterialDetails)
            {
                sql += $"INSERT INTO `STORE_MATERIAL_DB`.`REQUEST_EXPORT` (`BILL_NUMBER`, `WORK_ID`, `PART_NUMBER`, `QTY`,`MAIN_PART`) " +
                    $"VALUES ('{bill.BillId}', '{bill.WorkId}', '{item.PartNumber}', '{item.Qty}','{item.MainPart}') " +
                    $" ON DUPLICATE KEY UPDATE `QTY` = '{item.Qty}';";

            }
            return mySQL.InsertDataMySQL(sql);

        }

        public bool UpdateBill(BillExportMaterial bill, string userID)
        {
            string sql = "";
            sql += $"update STORE_MATERIAL_DB.BILL_REQUEST_EX set TIME_CREATE = NOW() WHERE BILL_NUMBER='{bill.BillId}' ;";
            foreach (var item in bill.BillExportMaterialDetails)
            {
                if(item.Id != 0)
                {
                    if(item.Qty == 0)
                    {
                        sql += $"DELETE FROM  STORE_MATERIAL_DB.REQUEST_EXPORT  WHERE ID='{item.Id}';";

                    }
                    else
                    {
                        sql += $"UPDATE STORE_MATERIAL_DB.REQUEST_EXPORT SET QTY ='{item.Qty}' WHERE ID='{item.Id}';";

                    }
                }
                else
                {
                    if (item.Qty == 0)
                    {
                        continue;
                    }
                    else
                    {
                        sql += $"INSERT INTO `STORE_MATERIAL_DB`.`REQUEST_EXPORT` (`BILL_NUMBER`, `WORK_ID`, `PART_NUMBER`, `QTY`,`MAIN_PART`) " +
                 $"VALUES ('{bill.BillId}', '{bill.WorkId}', '{item.PartNumber}', '{item.Qty}','{item.MainPart}') " +
                 $" ON DUPLICATE KEY UPDATE `QTY` = '{item.Qty}';";
                    }
                       
                }

            }
            return mySQL.InsertDataMySQL(sql);

        }
        public bool ApproveBill(string bill, string userID)
        {
            string sql = "";
            sql += $"UPDATE `STORE_MATERIAL_DB`.`BILL_REQUEST_EX`  SET STATUS_EXPORT=1, APPROVE='{userID}',TIME_APPROVE=NOW() WHERE `BILL_NUMBER`='{bill}';";
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'APPROVE', NOW(), '{userID}');";
            return mySQL.InsertDataMySQL(sql);

        }
        public bool SaveBillExport(List<BillExportMaterialDetail> bills, string billnumber, string wo, int pcbRequest, string process, string bomver, string userID, int subType, int typeBill) 
        {
            string sql = string.Empty;

            sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_EX` (`BILL_NUMBER`, `WORK_ID`, `PCBS`, `DATE_EXPORT`, `OP`, `TIME_CREATE`," +
              " `AREA`, `STATUS_EXPORT`, `TYPE_BILL`, `SUB_TYPE` , `BOM_VERSION`)" +
                  $" VALUES ('{billnumber}', '{wo}', '{pcbRequest}', now(), '{userID}', now(), " +
                  $" '{process}',  1 , {typeBill} , {subType}, '{bomver}');";
            foreach (var item in bills)
            {
                sql += "INSERT INTO `STORE_MATERIAL_DB`.`REQUEST_EXPORT` (`BILL_NUMBER`, `WORK_ID`, `MAIN_PART`, `PART_NUMBER`, `QTY`) " +
                       $" VALUES ('{billnumber}', '{wo}', '{item.MainPart}', '{item.PartNumber}', '{item.Qty}');";
                //sql += $"INSERT IGNORE INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_MAINPART` (`WORK_ID`,  `MAIN_PART`, `QTY_REQUEST`) VALUES ('{wo}', '{item.MainPart}', '{item.Qty}');";
            }

            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        //public bool SaveWorkRequestMainPartExport(List<BillExportMaterialDetail> bills,  string wo)
        //{
        //    string sql = string.Empty;
        //    foreach (var item in bills)
        //    {                
        //        sql += $"INSERT IGNORE INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_MAINPART` (`WORK_ID`,  `MAIN_PART`, `QTY_REQUEST`) VALUES ('{wo}', '{item.MainPart}', '{item.Qty}');";
        //    }
        //    if (mySQL.InsertDataMySQL(sql)) return true;
        //    return false;
        //}
    }
}
