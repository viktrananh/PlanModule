using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public class Process
    {
        string stationPacking = "";
        int productionPlanPanel = 0;
        string model = "";

        public bool IsTableEmty(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return true;
            return false;
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(DBConnect.getData("SELECT NOW();").Rows[0][0].ToString());
        }

        public bool checkWork(string work, out string model, out string cusID, out string cusModel, out string cusCode,  out int totalPCB, out string  statusWo)
        {
            model = cusID = cusModel = cusCode = statusWo =  string.Empty;
            totalPCB = 0;
            string sql = $"SELECT A.WORK_ID, A.MODEL, A.CUSTOMER, A.TOTAL_PCBS , A.`STATUS`, B.CUS_MODEL, B.CUS_CODE  FROM TRACKING_SYSTEM.WORK_ORDER A INNER JOIN TRACKING_SYSTEM.BOX_PACKING B ON A.MODEL = B.MODEL_ID where work_id = '{work}';";
            DataTable DT = DBConnect.getData(sql);
            if (IsTableEmty(DT)) return false;
            model = DT.Rows[0]["MODEL"].ToString();
            cusID = DT.Rows[0]["CUSTOMER"].ToString();
            cusModel = DT.Rows[0]["CUS_MODEL"].ToString();
            cusCode = DT.Rows[0]["CUS_CODE"].ToString();
            statusWo = DT.Rows[0]["STATUS"].ToString();
            try
            {
                totalPCB = int.Parse(DT.Rows[0]["TOTAL_PCBS"].ToString());
            }
            catch 
            {
            }
            return true;
        }
        public bool isWorkMFG(string cusCode, out string model, out string cusModel)
        {
            model = "";
            cusModel = "";
            DataTable dt = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.BOX_PACKING where CUS_CODE = '{cusCode}';");
            if (IsTableEmty(dt)) return false;
            model = dt.Rows[0]["MODEL_ID"].ToString();
            cusModel = dt.Rows[0]["CUS_MODEL"].ToString();
            return true;
        }
        public bool isWorkClose(string work)
        {
            string sql = $"SELECT `STATUS` FROM TRACKING_SYSTEM.WORK_ORDER  where WORK_ID_MOTHER = '{work}';";
            string status = DBConnect.getData(sql).Rows[0]["STATUS"].ToString();
            if (status == "CLOSE") return true;
            return false;
        }
        public int getPcbOnPanel(string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 1;
            return int.Parse(dt.Rows[0]["PCB/PANEL"].ToString());
        }
        public bool getDetailModel(string modelID, out string modelName, out int pcbs)
        {
            modelName = string.Empty;
            pcbs = 0;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.PART_MODEL_CONTROL WHERE ID_MODEL = '{modelID}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            modelName = dt.Rows[0]["MODEL_NAME"].ToString();
            pcbs = int.Parse(dt.Rows[0]["PCBS"].ToString());
            return true;
        }
        public int getPcbOfWorkHasRequest(string work)
        {
            string sql = $"select SUM(NUMBER_REQUEST) AS SUM_RE from TRACKING_SYSTEM.DELIVERY_BILL where WORK_ID = '{work}' and STATUS_BILL = 0;";
            DataTable DT = DBConnect.getData(sql);
            if (IsTableEmty(DT)) return 0;
            string sum_re = DT.Rows[0]["SUM_RE"].ToString();
            if (string.IsNullOrEmpty(sum_re)) return 0;
            return int.Parse(sum_re);
        }
        public int getPCBExportEd(string work)
        {
            string sql = $"SELECT SUM(EXPORTS_REAL) as SUM_ED FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS WHERE WORK_ID = '{work}' AND STATUS_WORK = 1;";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            string sum = dt.Rows[0]["SUM_ED"].ToString();
            if (string.IsNullOrEmpty(sum)) return 0;
            return int.Parse(sum);
        }
        public bool checkMfg(string mfg)
        {
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.BOM_DETAIL where MFG_PART = '{mfg}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            return true;
        }
        public bool getStationPacking(string model, out string stationPacking)
        {
            stationPacking = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DEFINE_STATION_ID WHERE MODEL_ID = '{model}' AND STATION_NAME = 'PACKING'";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            stationPacking = dt.Rows[0]["STATION_ID"].ToString();
            return true;
        }

        public bool getPCBProduceOfWork(string work, string station, out int pcbProduce) //SỐ PCB SX
        {
            pcbProduce = 0;
            string sql = $"SELECT COUNT(*) AS PCB_PRODUCE FROM TRACKING_SYSTEM.PCB_ID_TRACER WHERE ORDER_NO = '{work}' AND CURRENT_STATION = '{station}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            pcbProduce = int.Parse(dt.Rows[0]["PCB_PRODUCE"].ToString());
            return true;
        }
        public int getPcbPacked(string work, string model)
        {
            string sql = $"SELECT COUNT(*) AS PCB_PACKED FROM TRACKING_SYSTEM.BOX_ID WHERE WORK_ORDER = '{work}' AND MODEL= '{model}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            return int.Parse(dt.Rows[0]["PCB_PACKED"].ToString());
        }
        public bool getCusModel(string model, out string cus_model)
        {
            cus_model = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_PACKING WHERE MODEL_ID = '{model}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            cus_model = dt.Rows[0]["CUS_MODEL"].ToString();
            return true;
        }
        public string cusCodeIsWorkFAT(string model)
        {
            return DBConnect.getData($"SELECT CUS_CODE FROM TRACKING_SYSTEM.BOX_PACKING where MODEL_ID = '{model}';").Rows[0]["CUS_CODE"].ToString();

        }
        public int getPCbWaitPacking(string work, string model)
        {
            if (!getStationPacking(model, out stationPacking)) return 0;
            string beforePacked = "MV000" + (int.Parse(stationPacking.Replace("MV000", "")) - 1).ToString();
            string sql = $"SELECT COUNT(*) AS PCB_BEFORE_PACKED FROM TRACKING_SYSTEM.PCB_ID_TRACER WHERE ORDER_NO = '{work}' AND CURRENT_STATION = '{beforePacked}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            return int.Parse(dt.Rows[0]["PCB_BEFORE_PACKED"].ToString());

        }
        public bool isBoxExten(string box, out string model, out string work, out string time, out int count)
        {
            model = "";
            work = "";
            time = "";
            count = 0;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_ID_EXTEND WHERE BOX_SERIAL='{box}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            work = dt.Rows[0]["WORK_ORDER"].ToString();
            time = dt.Rows[0]["TIME_PACKING"].ToString();
            count = Int16.Parse(dt.Rows[0]["PCBS"].ToString());
            return true;
        }
        #region phiếu xuất thành phẩm
        public int getLastBillNumber()
        {
            string time = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"SELECT BILL_NUMBER FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE DATE_CREATED = (select max(DATE_CREATED) FROM TRACKING_SYSTEM.DELIVERY_BILL) AND DAY(DATE_CREATED) = DAY(CURDATE()) AND MONTH(DATE_CREATED) = MONTH(CURDATE()) AND YEAR(DATE_CREATED) = YEAR(CURDATE()); ";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;

            string lastBill = dt.Rows[0]["BILL_NUMBER"].ToString().Substring(12);
            return int.Parse(lastBill);

        }
        public int getTypeBill(string bill)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{bill}';";
            DataTable dt = DBConnect.getData(sql);
            if (dt.Rows.Count <= 6)
            {
                return 2;
            }
            return 1;
        }
        public bool isBillNumberExist(string bill)
        {
            DataTable dt = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{bill}';");
            if (IsTableEmty(dt)) return false;
            return true;
        }
        public bool getBillNumber(string billNUmber, out string timeExport)
        {
            timeExport = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNUmber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            timeExport = dt.Rows[0]["DATE_EXPORTS"].ToString();

            return true;
        }
        public int getSumRequestOnBill(string billNumber)
        {
            string sql = $"select sum(NUMBER_REQUEST) as SUM_REQUEST FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            return int.Parse(dt.Rows[0]["SUM_REQUEST"].ToString());
        }
        public DataTable getDetailBillNUmber(string billNumber)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt;
        }
        public int checkStatusWork(string billNumber, string work)
        {
            string sql = $"select STATUS_BILL from TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}' AND WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 2;
            return int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
        }
        public bool updateStatusWork(string billNumber, string work, int status)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL SET STATUS_BILL = '{status}' WHERE BILL_NUMBER = '{billNumber}' AND WORK_ID = '{work}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool comfirmBill(string billNumber, int status)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL SET STATUS_BILL = '{status}' WHERE BILL_NUMBER = '{billNumber}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool checkBillStatus(string billNumber)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            int status = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            if (status == 1) return false;
            return true;
        }
        public bool checkTimeExport(string timeExport)
        {
            string now = DateTime.Now.ToString("yyyy/MM/dd");
            //DateTime.Parse(now) - DateTime.Parse(timeExport);
            if (DateTime.Parse(now) > DateTime.Parse(timeExport)) return true;
            return false;

        }
        public bool getCusCode(string model, string cusmodel, out string cusCode)
        {
            cusCode = "";
            string sql = $"SELECT CUS_CODE FROM TRACKING_SYSTEM.BOX_PACKING where MODEL_ID = '{model}' AND CUS_MODEL = '{cusmodel}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            cusCode = dt.Rows[0]["CUS_CODE"].ToString();
            return true;
        }
        #endregion
        #region Phiếu nhập
        public bool isBillRqInput(string bill, out int status)
        {
            status = -1;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER = '{bill}';");
            if (IsTableEmty(dt)) return false;
            status = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            return true;
        }
        public bool cancelBillImport(string bill, string userID)
        {
            string sql = $"UPDATE  STORE_MATERIAL_DB.BILL_REQUEST_IMPORT  SET STATUS_BILL = {-1} WHERE BILL_NUMBER = '{bill}';";
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CANCEL',  now(), '{userID}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool addBillInport(string bill, string cus, string unit, string req, string createBy, string time, string intend, int status)
        {
            string sql = "INSERT INTO TRACKING_SYSTEM.BILL_IMPORT (BILL_NUMBER, CUSTOMER, UNIT, REQUESTS, CREATE_BY, CREATE_TIME, INTEND_TIME, STATUS_BILL)" +
                $" VALUE ('{bill}', '{cus}', '{unit}', '{req}', '{createBy}', '{time}', '{intend}', '{status}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool isBillNew(string bill)
        {
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER = '{bill}';");
            if (IsTableEmty(dt)) return true;
            return false;
        }
        #endregion
        //position
        public bool checkBoxExist(string boxSeria, out string model, out string work, out string timePacking, out int count, out int state)
        {
            model = "";
            work = "";
            timePacking = "";
            count = 0;
            state = 0;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_LIST where BOX_SERIAL = '{boxSeria}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            work = dt.Rows[0]["WORK_ORDER"].ToString();
            timePacking = dt.Rows[0]["TIME_PACKING"].ToString();
            count = int.Parse(dt.Rows[0]["COUNT"].ToString());
            state = int.Parse(dt.Rows[0]["STATE"].ToString());
            return true;
        }
        public bool IsModelAutoPacking(string model, out int pcb_panel, out int panel_packing)
        {
            pcb_panel = 0;
            panel_packing = 0;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_PACKING where MODEL_ID = '{model}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            pcb_panel = int.Parse(dt.Rows[0]["PCB_PANEL"].ToString());
            panel_packing = int.Parse(dt.Rows[0]["PANEL_PACKING"].ToString());
            return true;

        }
        public bool IsPanelPacked(string model)
        {
            string sql = $"SELECT PANEL_PACKING FROM TRACKING_SYSTEM.BOX_PACKING where MODEL_ID = '{model}';";
            DataTable dt = DBConnect.getData(sql);
            int isPanel = int.Parse(dt.Rows[0]["PANEL_PACKING"].ToString());
            if (isPanel == 1) return true;
            return false;
        }
        public bool checkBoxHasInput(string boxSerial, out string location)
        {
            location = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION where BOX_SERIAL = '{boxSerial}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            location = dt.Rows[0]["POSOTION"].ToString();
            return true;
        }
        public int getDetailWorkBill(string billNumber, string work, out string model, out string cus_model, out string mfg, out string unit)
        {
            model = "";
            cus_model = "";
            mfg = "";
            unit = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}' AND WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            model = dt.Rows[0]["MODEL"].ToString();
            cus_model = dt.Rows[0]["CUS_MODEL"].ToString();
            mfg = dt.Rows[0]["MFG_PART"].ToString();
            unit = dt.Rows[0]["UNIT"].ToString();
            return int.Parse(dt.Rows[0]["NUMBER_REQUEST"].ToString());
        }
        public int checkNumberOfBoxesInPlace(string location)
        {
            string sql = $"select COUNT(*) AS COUNT FROM TRACKING_SYSTEM.BOX_POSITION where POSITION = '{location}' ;";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            return int.Parse(dt.Rows[0]["COUNT"].ToString());
        }
        public DataTable getBoxSettingWork(string work)
        {
            string sql = $"select DISTINCT POSITION FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt;
        }
        public string getWorkInLocation(string location)
        {
            string sql = $"SELECT WORK_ID FROM TRACKING_SYSTEM.BOX_POSITION WHERE POSITION = '{location}'";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt.Rows[0]["WORK_ID"].ToString();
        }
        public int getPCBSRemain(string work)
        {
            string sql = $"SELECT SUM(PCBS) AS SUM_PCBS FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}'  ORDER BY TIME_PRINT;";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            string remain = dt.Rows[0]["SUM_PCBS"].ToString().Trim();
            if (string.IsNullOrEmpty(remain)) return 0;
            return int.Parse(remain);
        }
        public bool checkWorkHasRequestInBill(string work, DataTable dtBill, out int requestWork)
        {
            requestWork = 0;
            foreach (DataRow item in dtBill.Rows)
            {
                string wo = item["WORK_ID"].ToString();

                if (wo == work)
                {
                    requestWork = int.Parse(item["NUMBER_REQUEST"].ToString());
                    return false;
                }
            }
            return true;
        }
        public bool getBoxRemain(string boxSerial, string work, out int count, out string datePacking, out string position)
        {
            count = -1;
            position = "";
            datePacking = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}' AND BOX_SERIAL = '{boxSerial}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            count = int.Parse(dt.Rows[0]["PCBS"].ToString());
            position = dt.Rows[0]["POSITION"].ToString();
            datePacking = dt.Rows[0]["DATE_PACKING"].ToString();
            return true;
        }
        public bool getEmloyee(string barCode, out string mnv, out string name, out int authority)
        {
            mnv = "";
            name = "";
            authority = -1;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL where USER_PASSWORD = '{barCode}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            mnv = dt.Rows[0]["USER_ID"].ToString();
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            return true;
        }
        public bool getTheBoxOut(string boxSerial, string workID)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"delete from TRACKING_SYSTEM.BOX_POSITION where BOX_SERIAL = '{boxSerial}' AND WORK_ID = '{workID}';";
            sql += "SET SQL_SAFE_UPDATES=1";

            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool addTableExport(string billNumber, string workID, string cus_model, string unit, string mfg, int request, int exReal, string dateExport, string exBy, int statusWork, int boxC, int boxP)
        {
            string sql = $"insert into TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS (BILL_NUMBER, WORK_ID, CUS_MODEL, UNIT, MFG_PART, REQUESTS, EXPORTS_REAL, DATE_EXPORT, EXPORT_BY, STATUS_WORK, BOX_C, BOX_P) VALUES ('{billNumber}', '{workID}', '{cus_model}', '{unit}', '{mfg}', '{request}', '{exReal}', '{dateExport}', '{exBy}', '{statusWork}', '{boxC}', '{boxP}')";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool isWorkHasComfirm(string bill, string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS where BILL_NUMBER = '{bill}' AND WORK_ID= '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            return true;
        }
        public bool checkWorkHasExportInBill(string billNumber, string work, out int exRealHas)
        {
            exRealHas = 0;
            string sql = $"select sum(PCBS) AS SUM_PCBS FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{billNumber}' AND  WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            string exReal = dt.Rows[0]["SUM_PCBS"].ToString();
            if (string.IsNullOrEmpty(exReal)) return false;
            exRealHas = int.Parse(exReal);
            return true;
        }
        public bool writeHistoryOutBox(string billNumber, string work, string boxSerial, string location, int pcbs, string timeOut, string outBy, string comfimby)
        {
            string sql = $"INSERT INTO TRACKING_SYSTEM.HISTORY_BOX_EXPORT (BILL_NUMBER, WORK_ID, BOX_SERIAL, LOCATION, PCBS, TIME_OUT, PUT_OUT_BY, COMFIRM_NOTFIFO) VALUES ('{billNumber}', '{work}', '{boxSerial}', '{location}', '{pcbs}', '{timeOut}', '{outBy}', '{comfimby}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public DataTable getHisBoxInWorkHasExport(string billNumber, string workId)
        {
            string sql = $"select BOX_SERIAL, LOCATION, PCBS FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{billNumber}' AND WORK_ID = '{workId}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt;
        }
        public string getTimeExportBill(string bill)
        {
            string sql = $"select DISTINCT DATE_EXPORTS FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{bill}';";
            DataTable dt = DBConnect.getData(sql);
            return dt.Rows[0]["DATE_EXPORTS"].ToString();
        }
        public DataTable getDetailBill(string billNumber)
        {
            string sql = $"SELECT WORK_ID, CUS_MODEL, CUS_CODE, MODEL, UNIT, MFG_PART, NUMBER_REQUEST FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt;
        }
        public bool updateStatusBillnumber(string billNumber, int status)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL set STATUS_BILL = '{status}' WHERE BILL_NUMBER = '{billNumber}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool continueBill(string billNumber, int status, string dateExport)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL set STATUS_BILL = '{status}' DATE_EXPORTS = '{dateExport}' WHERE BILL_NUMBER = '{billNumber}';";
            sql += "SET SQL_SAFE_UPDATES=1;";

            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool checkTimeExportCotinueBill(string billNumber, string dateExbotNew)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}';";
            DataTable DT = DBConnect.getData(sql);
            if (IsTableEmty(DT)) return false;
            DateTime timeBefor = DateTime.Parse(DT.Rows[0]["DATE_EXPORTS"].ToString());
            DateTime timeNew = DateTime.Parse(dateExbotNew);
            int result = DateTime.Compare(timeNew, timeBefor);
            int result1 = DateTime.Compare(timeNew, DateTime.Now);
            if (result == 1 && result1 == 1) return true;
            return false;
        }

        public bool checkBillExchagedCancel(string billNumber)
        {
            string sql = $"select STATUS_BILL FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            int status = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            if (status != 0) return false;
            return true;
        }

        public bool saveHisExchangeCancel(string billNumber, string dateCancel, string cancelBy, string reasonsCancel)
        {
            string sql = $"insert into TRACKING_SYSTEM.HISTORY_EXCHANGE_BILL (BILL_NUMBER,  DATE_CANCEL, CANCEL_BY, REASONS_CANCEL) value ('{billNumber}',  '{dateCancel}', '{cancelBy}', '{reasonsCancel}')";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool checkBillExchagedContinue(string billNumber)
        {
            string sql = $"select STATUS_BILL FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            int status = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            if (status != -1) return false;
            return true;
        }
        public bool saveHisExchangeContinue(string billNumber, string dateContinue, string continueBy, string reasonsContinue)
        {
            string sql = $"insert into TRACKING_SYSTEM.HISTORY_EXCHANGE_BILL (BILL_NUMBER, DATE_CONTINUE, CONTINUE_BY, REASONS_CONTINUE) value ('{billNumber}', '{dateContinue}', '{continueBy}', '{reasonsContinue}')";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool checkBillExchangeEdit(string billNumber, out int statusEdit)
        {
            statusEdit = 1;
            string sql = $"select STATUS_BILL FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            statusEdit = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            if (statusEdit == 1) return false;
            return true;
        }
        public bool saveHisExchageEdit(string billNumber, string dateEdit, string editBy, string editContents)
        {
            string sql = $"insert into TRACKING_SYSTEM.HISTORY_EXCHANGE_BILL (BILL_NUMBER, DATE_EDIT, EDIT_BY, EDIT_CONTENTS) value ('{billNumber}', '{dateEdit}', '{editBy}', '{editContents}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        //detailwwork
        public int getIDBillWork(string billNumber, string work)
        {
            string sql = $"SELECT ID FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}' AND WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;
            return int.Parse(dt.Rows[0]["ID"].ToString());
        }
        public bool editWorkInBill(int idEdit, string workNew, string cus_model, string unit, string mfgPart, int numberRequest, string model, string dateExport)
        {
            string sql = $"update TRACKING_SYSTEM.DELIVERY_BILL SET WORK_ID = '{workNew}', CUS_MODEL = '{cus_model}', UNIT = '{unit}', MFG_PART = '{mfgPart}', NUMBER_REQUEST = '{numberRequest}', MODEL = '{model}', DATE_EXPORTS = '{dateExport}' WHERE ID = '{idEdit}';";
            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool delWorkRequest(string billNumber, string work)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"delete from TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER = '{billNumber}' AND WORK_ID = '{work}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool comfirmNewBill(string billNumber, string workID, string cusModel, string unit, string mfg, int numberRequest, string model, string dateEx, string createBy, string dateCreate, int status)
        {
            string sql = $"INSERT INTO TRACKING_SYSTEM.DELIVERY_BILL (BILL_NUMBER, WORK_ID, CUS_MODEL, UNIT, MFG_PART, NUMBER_REQUEST, MODEL, DATE_EXPORTS, CREATED_BY, DATE_CREATED, STATUS_BILL) VALUES ('{billNumber}', '{workID}', '{cusModel}', '{unit}', '{mfg}', '{numberRequest}', '{model}', '{dateEx}' , '{createBy}', '{dateCreate}', '{status}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;

        }
        #region FIFO
        public string getDatePackingLatestOfWork(string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION where WORK_ID = '{work}' ORDER BY TIME_PRINT;";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return null;
            return dt.Rows[0]["TIME_PRINT"].ToString().Substring(0, 10);
        }
        public DataTable getAllBoxOfWorkInRemain(string work)
        {
            DataTable dtView = new DataTable();
            dtView.Columns.Add("BOX_SERIAL");
            dtView.Columns.Add("LOCATION");
            dtView.Columns.Add("PCBS");
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION where WORK_ID = '{work}';";
            DataTable dt = DBConnect.getData(sql);
            foreach (DataRow item in dt.Rows)
            {
                string box = item["BOX_SERIAL"].ToString();
                string location = item["POSITION"].ToString();
                string count = item["PCBS"].ToString();
                DataRow row = dtView.NewRow();
                row["BOX_SERIAL"] = box;
                row["LOCATION"] = location;
                row["PCBS"] = count;
                dtView.Rows.Add(row);
            }
            return dtView;
        }
        public DataTable listBoxOnLocations(DataTable dtLocation)
        {
            DataTable dtView = new DataTable();
            dtView.Columns.Add("BOX_SERIAL");
            dtView.Columns.Add("LOCATION");
            dtView.Columns.Add("PCBS");
            foreach (DataRow item in dtLocation.Rows)
            {
                string location = item["POSITION"].ToString();
                string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION where POSITION = '{location}';";
                DataTable dt = DBConnect.getData(sql);
                foreach (DataRow rowL in dt.Rows)
                {
                    string boxS = rowL["BOX_SERIAL"].ToString();
                    string pcbs = rowL["PCBS"].ToString();
                    DataRow row = dtView.NewRow();
                    row["BOX_SERIAL"] = boxS;
                    row["LOCATION"] = location;
                    row["PCBS"] = pcbs;
                    dtView.Rows.Add(row);
                }
            }
            return dtView;
        }
        public DataTable getListBoxRemainOfWork(string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}' ORDER BY TIME_PRINT ;";
            return DBConnect.getData(sql);
        }
        public DataTable returnListBoxOfWorkRequest(string work, int requestOfWork)
        {
            int sumPcbs = 0;
            DataTable dtView = new DataTable();
            dtView.Columns.Add("BOX_SERIAL");
            dtView.Columns.Add("LOCATION");
            dtView.Columns.Add("PCBS");
            string datePacking = getDatePackingLatestOfWork(work);
            int remain = getPCBSRemain(work);
            if (remain < requestOfWork)
            {
                dtView = DBConnect.getData($"select BOX_SERIAL, POSITION, PCBS from TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work }';");
                return dtView;
            }
            DateTime time = DateTime.Parse(datePacking);//caan suwar
            while (sumPcbs < requestOfWork)
            {
                string timePacked = time.ToString("yyyy-MM-dd");
                string sqlLOCATION = $"SELECT DISTINCT POSITION FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}' AND TIME_PRINT  LIKE '%{timePacked}%';";
                DataTable dtLocaion = DBConnect.getData(sqlLOCATION);
                if (!IsTableEmty(dtLocaion))
                {
                    foreach (DataRow item in dtLocaion.Rows)
                    {
                        string location = item["POSITION"].ToString();
                        string sqlSum = $"select sum(PCBS) as SUM_PCB FROM TRACKING_SYSTEM.BOX_POSITION WHERE POSITION = '{location}';";
                        DataTable dtSum = DBConnect.getData(sqlSum);
                        sumPcbs += int.Parse(dtSum.Rows[0]["SUM_PCB"].ToString());
                    }
                    foreach (DataRow item in dtLocaion.Rows)
                    {
                        string location = item["POSITION"].ToString();
                        string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION where POSITION = '{location}';";
                        DataTable dt = DBConnect.getData(sql);
                        foreach (DataRow rowL in dt.Rows)
                        {
                            string boxS = rowL["BOX_SERIAL"].ToString();
                            string pcbs = rowL["PCBS"].ToString();
                            DataRow row = dtView.NewRow();
                            row["BOX_SERIAL"] = boxS;
                            row["LOCATION"] = location;
                            row["PCBS"] = pcbs;
                            dtView.Rows.Add(row);
                        }
                    }
                }
                time = time.AddDays(+1);
            }
            return dtView;

        }
        public bool checkBoxInFifo(string boxSerial, DataTable dtFifo)
        {
            foreach (DataRow item in dtFifo.Rows)
            {
                string boxInFifo = item["BOX_SERIAL"].ToString();
                if (boxSerial == boxInFifo) return true;
            }
            return false;
        }
        public bool iskBoxInFifo(string work, string datePacking)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_POSITION WHERE WORK_ID = '{work}' AND DATE_PACKING < '{datePacking}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return true;
            return false;
        }
        #endregion
        //login


        public bool checkAccount(string userID, string pwd, out string user, out string name, out int authority)
        {
            user = string.Empty;
            name = string.Empty;
            authority = -1;
            string sql = $"select USER_ID,PWD,USER_NAME , AUTHORITY from TRACKING_SYSTEM.USER_CONTROL where USER_ID = '{userID}' and PWD = '{pwd}' " +
                $"union select USER_ID,PWD,USER_NAME ,AUTHORITY from TRACKING_SYSTEM.USER_CONTROL where USER_ID = '{userID}' and USER_PASSWORD = '{pwd}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            user = userID;
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            return true;
        }
        public bool checkAccountBarCode(string userID, string barCode, out string user, out string name, out int authority, out string address)
        {
            user = "";
            name = "";
            authority = -1;
            address = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL where USER_ID = '{userID}' and USER_PASSWORD = '{barCode}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            user = dt.Rows[0]["USER_ID"].ToString();
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            address = dt.Rows[0]["Address"].ToString();
            return true;
        }
        public bool isUserComfirm(string barcode, out string user, out string name, out int authority, out string add)
        {
            user = "";
            name = "";
            authority = -1;
            add = "";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL where USER_PASSWORD = '{barcode}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            user = dt.Rows[0]["USER_ID"].ToString();
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            add = dt.Rows[0]["Address"].ToString();
            return true;
        }
        public bool checkAuthority(int authority, string function)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DEFINE_ACCOUNT_ATHORITY where AUTHORITY = '{authority}' AND ACTION_NAME = '{function}' AND REFUSE = 0;";
            DataTable DT = DBConnect.getData(sql);
            if (IsTableEmty(DT)) return false;
            return true;
        }
        #region Xuất thành phẩm

        public bool checkStatusBillReport(string bill)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{bill}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            foreach (DataRow item in dt.Rows)
            {
                int status = int.Parse(item["STATUS_BILL"].ToString());
                if (status != 1) return false;
            }
            return true;
        }
        //public bool isBillNotReviews()
        //{
        //    string sql 
        //}
        public bool checkBoxHasExport(string boxSerial)
        {

            string sql = $"SELECT * FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BOX_SERIAL = '{boxSerial}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            return true;
        }

        public bool checkCofirmStoge(string bill)
        {
            string SQL = $"SELECT * FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS WHERE BILL_NUMBER  = '{bill}' AND STOCKER_BY IS NOT NULL;";
            DataTable DT = DBConnect.getData(SQL);
            if (IsTableEmty(DT)) return false;
            return true;
        }

        public bool cancleHasExBill(string bill)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL set STATUS_BILL = 0 WHERE  BILL_NUMBER  = '{bill}' ;";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool delDataHas(string bill)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES = 0;";
            sql += $"delete from TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS where BILL_NUMBER = '{bill}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool isBoxBlock(string box)
        {
            DataTable dataTable = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.BOX_ID where BOX_SERIAL = '{box}' and IS_BLOCK = 1;");
            if (IsTableEmty(dataTable)) return false;
            return true;
        }

        public bool delBoxhasExport(string bill)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES = 0;";
            sql += $"DELETE FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{bill}';";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        //public bool addRemoveInLocation(string bill, string work)
        //{

        //}
        public DataTable getWorkHasEx(string bill)
        {
            DataTable dt = DBConnect.getData($"SELECT distinct WORK_ID FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{bill}';");
            return dt;
        }

        #endregion

        //EXCEL


        public int getPCbCount(string work)
        {
            string sql = $"SELECT MODEL FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID_MOTHER = '{work}'";
            DataTable DT = DBConnect.getData(sql);
            string model = DT.Rows[0]["MODEL"].ToString();
            string SQL = $"SELECT PCB_COUNT FROM TRACKING_SYSTEM.BOX_PACKING WHERE MODEL_ID = '{model}';";
            DataTable dt = DBConnect.getData(SQL);
            return int.Parse(dt.Rows[0]["PCB_COUNT"].ToString());
        }
        //report
        public DataTable getListBoxHasExport(string bill, string work)
        {
            string sql = $"SELECT BOX_SERIAL FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{bill}' and WORK_ID = '{work}';";
            return DBConnect.getData(sql);
        }
        public bool removeWork(string bill, string work)
        {
            string sql = "";
            sql += "SET SQL_SAFE_UPDATES=0;";
            sql += $"update TRACKING_SYSTEM.DELIVERY_BILL set STATUS_BILL = 0 WHERE  BILL_NUMBER  = '{bill}' AND WORK_ID ='{work}' ;";
            sql += $"DELETE FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS WHERE  BILL_NUMBER  = '{bill}' AND WORK_ID ='{work}' ;";
            sql += $"DELETE FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT  WHERE  BILL_NUMBER  = '{bill}' AND WORK_ID ='{work}' ;";
            sql += "SET SQL_SAFE_UPDATES=1;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        //public bool delBoxHasExport(string bill, string work)
        //{
        //    string sql = "";
        //    sql += "SET SQL_SAFE_UPDATES=0;";
        //    sql += $"DELETE FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT  WHERE  BILL_NUMBER  = '{bill}' AND WORK_ID ='{work}' ;";
        //    sql += "SET SQL_SAFE_UPDATES=1;";
        //    if (DBConnect.InsertMySql(sql)) return true;
        //    return false;
        //}
        //public bool removeWork(string bill, string work)
        //{
        //    string sql = "";
        //    sql += "SET SQL_SAFE_UPDATES=0;";
        //    sql += $"DELETE FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS WHERE  BILL_NUMBER  = '{bill}' AND WORK_ID ='{work}' ;";
        //    sql += "SET SQL_SAFE_UPDATES=1;";
        //    if (DBConnect.InsertMySql(sql)) return true;
        //    return false;
        //}
        //import
        public int getLastBillNumberImport()
        {
            string time = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = $"SELECT BILL_NUMBER FROM TRACKING_SYSTEM.BILL_IMPORT WHERE CREATE_TIME = (select max(CREATE_TIME) FROM TRACKING_SYSTEM.DELIVERY_BILL) AND DAY(CREATE_TIME) = DAY(CURDATE()) AND MONTH(CREATE_TIME) = MONTH(CURDATE()) AND YEAR(CREATE_TIME) = YEAR(CURDATE()); ";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return 0;

            string lastBill = dt.Rows[0]["BILL_NUMBER"].ToString().Substring(12);
            return int.Parse(lastBill);

        }
        public bool addBillNumber(string bill, string wo, string cusName, string cusId, string mfgName, string mfgID, string unit, int request, string by, string time, int status)
        {
            string sql = $"INSERT INTO  TRACKING_SYSTEM.BILL_IMPORT (BILL_NUMBER, WORK_ID, CUS_NAME, CUS_ID, PRODUCER_NAME, PRODUCER_ID, UNIT, REQUESTS, CREATE_BY, CREATE_TIME, STATUS_BILL) VALUES ('{bill}', '{wo}', '{cusName}', '{cusId}', '{mfgName}', '{mfgID}' ,'{unit}' ,'{request}', '{by}', '{time}', '{status}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        #region export Material
        public bool isCSPart(string partNumer, out string mfgPart)
        {
            mfgPart = "";
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.MASTER_PART where PART_NUMBER = '{partNumer}';";
            DataTable DT = DBConnect.getData(sql);
            if (IsTableEmty(DT)) return false;
            mfgPart = DT.Rows[0]["MFG_PART"].ToString();
            return true;
        }

        public DataTable getListWorkOfModel(string model)
        {
            DataTable dt = DBConnect.getData($"SELECT  WORK_ID_MOTHER FROM TRACKING_SYSTEM.WORK_ORDER where MODEL = '{model}';");
            return dt;
        }
        public string getLastBill(string dateEx)
        {
            DataTable dt = DBConnect.getData($"SELECT DISTINCT BILL_NUMBER FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE DATE_EXPORTS LIKE '%{dateEx}%' AND TYPE_OF = 2 ORDER BY DATE_EXPORTS DESC;");
            if (IsTableEmty(dt)) return null;
            return dt.Rows[0]["BILL_NUMBER"].ToString();

        }
        #endregion
        #region printBill
        public string getCusBillRequest(string billNumber)
        {
            DataTable dt = DBConnect.getData($"SELECT CUS_MODEL FROM TRACKING_SYSTEM.DELIVERY_BILL WHERE BILL_NUMBER = '{billNumber}';");
            return DBConnect.getData($"SELECT CUSTOMER_ID FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where MODEL_NAME = '{dt.Rows[0]["CUS_MODEL"].ToString()}';").Rows[0]["CUSTOMER_ID"].ToString();

        }

        public string getCusOfBill(string billNumber)
        {
            DataTable dt = DBConnect.getData($"SELECT CUS_MODEL FROM TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS WHERE BILL_NUMBER = '{billNumber}';");
            return DBConnect.getData($"SELECT CUSTOMER_ID FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where MODEL_NAME = '{dt.Rows[0]["CUS_MODEL"].ToString()}';").Rows[0]["CUSTOMER_ID"].ToString();
        }
        public static string getDetailNote(string bill, string work, int boxCount)
        {
            string note = "";
            int countBoxFull = 0;
            List<int> listCout = new List<int>();
            DataTable dataTable = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BILL_NUMBER = '{bill}' AND WORK_ID = '{work}';");
            foreach (DataRow item in dataTable.Rows)
            {
                string box = item["BOX_SERIAL"].ToString();
                int pcbs = int.Parse(item["PCBS"].ToString());
                if (pcbs == boxCount)
                {
                    countBoxFull++;
                }
                else if (pcbs > 0)
                {
                    listCout.Add(pcbs);
                }
            }
            note += countBoxFull + $"Thùng  ( {boxCount} PCS/Thùng ) \r\n";
            foreach (var item in listCout)
            {
                note += "1 Thùng " + "(" + item + "PCS/Thùng ) \r\n";
            }
            return note;
        }
        #endregion
        #region TOOLS
        public string getInterPart(string mfgPart,string cusID, out string description, out string masterType, out string cusPart)
        {

            description = masterType = cusPart=  string.Empty;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.MASTER_PART WHERE MFG_PART = '{mfgPart}' and CUS='{cusID}' ;");
            if (IsTableEmty(dt)) return null;
            description = dt.Rows[0]["DESCRIPTION"].ToString();
            masterType = dt.Rows[0]["MASTER_TYPE"].ToString();
            cusPart = dt.Rows[0]["CUS_PART"].ToString();
            return dt.Rows[0]["PART_NUMBER"].ToString();

        }


        #endregion
        #region phiếu xuất linh kiện
        public bool isBillRQExport(string bill, out int status)
        {
            status = -2;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where BILL_NUMBER = '{bill}' and  SUB_TYPE = 1 ;");
            if (IsTableEmty(dt)) return false;
            status = int.Parse(dt.Rows[0]["STATUS_EXPORT"].ToString());
            return true;
        }
        #endregion
    }
}
