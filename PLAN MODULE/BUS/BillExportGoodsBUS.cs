﻿using PLAN_MODULE.DTO.Planed.BillExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDll.DTO.FinishedProduct;

namespace PLAN_MODULE.BUS.PLAN
{
    public class BillExportGoodsBUS : BaseBUS
    {
        public bool CancelBill(string bill)
        {
            string sql = $" UPDATE  STORE_MATERIAL_DB.BILL_REQUEST_EX SET STATUS_EXPORT = '-1' WHERE BILL_NUMBER = '{bill}';";
            sql += $" UPDATE TRACKING_SYSTEM.FP_BILLS set STATE = -1  WHERE BILL_NUMBER = '{bill}';"; 
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

        public bool CreateBill(FPBill bill, string billNumber, string cusID, string timeExport, string vehicle, int trueNumber , string userID )
        {
            string sql = string.Empty;
            int special = 0;
           
            foreach (var item in bill.FPBillDetailS)
            {
                string work = item.WorkId;
                string cus_model = item.CusModel;
                string cus_code = item.CusCode;
                string po = item.PO;
                int request = item.Request;
                string model = item.ModelId;
                sql += $"INSERT INTO TRACKING_SYSTEM.DELIVERY_BILL (BILL_NUMBER, WORK_ID, CUS_MODEL, CUS_CODE, UNIT, PO, NUMBER_REQUEST, MODEL,  STATUS_BILL,SPECIAL,DATE_EXPORTS , NOTE) VALUES " +
                     $"('{billNumber}', '{work}', '{cus_model}', '{cus_code}', 'PCS', '{po}', '{request}', '{model}', '{0}','{special}','{timeExport}' , '{item.Note}') ON DUPLICATE KEY UPDATE NUMBER_REQUEST = {request} ;";
                sql += $"INSERT INTO `TRACKING_SYSTEM`.`FP_BILL_DETAILS` (`BILL_NUMBER`, `WORK_ID`,  `REQUEST`, `STATE_ID`, `CREAT_TIME`) VALUES " +
                    $" ('{billNumber}', '{work}', '{request}', '0', NOW() ); ";
            }
            sql += $"INSERT INTO `TRACKING_SYSTEM`.`FP_BILLS` (`BILL_NUMBER`, `CUS_ID`, `TIME`, `OP`, `TYPE_BILL`, `STATE`, `NOTE`, `INTEND_TIME`, VEHICLE, TRUE_NUMBER) VALUES " +
             $"('{billNumber}', '{cusID}', now(), '{userID}', '{6}', '1', 'TP-CUS', '{timeExport}', '{vehicle}' , '{trueNumber}');";
           
            if (!mySQL.InsertDataMySQL(sql)) return false;
            return true;
           
        }


        public bool CreatBookBill(List<BookBillExportToCus> bookBillExportToCus, string bill)
        {
            string sql = $"UPDATE TRACKING_SYSTEM.FP_BILLS SET IS_BOOK = 1 WHERE BILL_NUMBER='{bill}' ;";

            foreach (var item in bookBillExportToCus)
            {

                sql += !string.IsNullOrEmpty(item.TotalRequest) ? $"INSERT INTO `TRACKING_SYSTEM`.`FP_BOOK_BILL_DETAIL` (`WORK_ID`, `REQUEST`, `BOX_COUNT`, `REQUEST_AFTER`, `BOX_SERIAL`, `LOT_NO`, `BILL_NUMBER`) VALUES " +
                    $" ('{item.Work}', '{item.TotalRequest}', '{item.BoxCount}', '{item.RequestAfter}', '{item.BoxID}', '{item.LotNo}', '{bill}'); " :
                      $"INSERT INTO `TRACKING_SYSTEM`.`FP_BOOK_BILL_DETAIL` (`WORK_ID`, `REQUEST`, `BOX_COUNT`, `REQUEST_AFTER`, `BOX_SERIAL`, `LOT_NO`, `BILL_NUMBER`) VALUES " +
                    $" ('{item.Work}', null, '{item.BoxCount}', '{item.RequestAfter}', '{item.BoxID}', '{item.LotNo}', '{bill}'); ";
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

        public bool CancelBookBill(string bill)
        {
            string sql = $"UPDATE TRACKING_SYSTEM.FP_BILLS SET IS_BOOK = 0 WHERE BILL_NUMBER='{bill}' ;";

            sql += $"DELETE FROM TRACKING_SYSTEM.FP_BOOK_BILL_DETAIL WHERE BILL_NUMBER = '{bill}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
    }
}