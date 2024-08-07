using MySqlX.XDevAPI;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS
{
    public class WorkOrderBUS : BaseBUS
    {
        public bool UploadCustomerData(List<CusData> cus, Work wo, string userId, string model, string cusId, string billNumber)
        {
            string sql = string.Empty;
            foreach (var item in cus)
            {
                sql += $"INSERT INTO `STORE_MATERIAL_DB`.`CUSTOMER_DATA` (`TR_SN`, `WO`, `CUST_KP_NO`, `MFR_KP_NO`, `MFR_CODE`, `DATE_CODE`, `LOT_CODE`, `OUT_QTY`, `TIME_INPUT`, `MODEL`, `CUSTOMER` , `CREATER` , `BILL_NUMBER` , `PKG_ID`) VALUES " +
                    $" ('{item.TR_SN}', '{item.WO}', '{item.CUST_KP_NO}', '{item.MFR_KP_NO}', '{item.MFR_CODE}', '{item.DATE_CODE}', '{item.LOT_CODE}', '{item.QTY}', Now(), '{model}', '{cusId}' , '{userId}' , '{billNumber}' , '{item.PKG_ID}' ) " +
                    $" ON DUPLICATE KEY UPDATE `CUST_KP_NO` = '{item.CUST_KP_NO}',`MFR_KP_NO` =  '{item.MFR_KP_NO}', `MFR_CODE` = '{item.MFR_CODE}', `DATE_CODE`='{item.DATE_CODE}', `LOT_CODE` =  '{item.LOT_CODE}', `OUT_QTY` =  '{item.QTY}', `TIME_INPUT` = now() , `PKG_ID` = '{item.PKG_ID}';";
            }

            var bill = (from r in cus
                        group r by r.CUST_KP_NO into gr
                        select new
                        {
                            CusPart = gr.Key,
                            Qty = gr.Sum(x => x.QTY)
                        }).ToList();

            foreach (var item in bill)
            { 
                sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_CUS_INPUT` (`BILL_NUMBER`, `CUS_PART`, `QTY`, `UNIT`) VALUES ('{billNumber}', '{item.CusPart}', '{item.Qty}', 'PCS') " +
                    $"  ON DUPLICATE KEY UPDATE `QTY` =  '{item.Qty}';";
            }
            sql += "INSERT INTO STORE_MATERIAL_DB.BILL_REQUEST_IMPORT (BILL_NUMBER, CUSTOMER, CREATE_BY, CREATE_TIME, INTEND_TIME, " +
                   " STATUS_BILL, TYPE_BILL, `VENDER_ID`,`PO`,`model_id`,`CUS_ID`,`WORK_ID`,`DEFINE_EXPORT` )" +
                   $" VALUE ('{billNumber}', '{cusId}',  '{userId}',now(), now(), " +
                   $" {1}, '{2}', '{cusId}','{wo.PO}','{wo.ModelWork}','{cusId}','{wo.WorkID}','{1}' ) ON DUPLICATE KEY UPDATE INTEND_TIME = NOW();";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

    }
}
