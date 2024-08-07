using LinqToExcel;
using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PLAN_MODULE
{
    class BussinessCusBillImport
    {
        BusinessGloble globle = new BusinessGloble();
        CreateWorkDAO dTO = new CreateWorkDAO();
        public string getCusID(string cusName)
        {
            return globle.mySql.GetDataMySQL($"SELECT CUSTOMER_ID FROM TRACKING_SYSTEM.DEFINE_CUSTOMER where CUSTOMER_NAME='{cusName}';").Rows[0][0].ToString();
        }
        public void createBillImportMaterial(bool isCustomer, out string bill)
        {
            bill = string.Empty;
            DataTable dtbill = DBConnect.getData("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT order BY ID DESC;");
            DateTime timeNow = globle.getTimeServer();
            if (isCustomer)
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString() + "/NVL-DT";

            }
            else
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString() + "/NVL-DT-RE";
            }

        }

        public void loadBill(string bill, string work, DataGridView dgv)
        {
            string cmd = $"SELECT BILL_NUMBER, WORK_ID,PART_NUMBER, QTY FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER='{bill}' AND WORK_ID='{work}';";
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            if (globle.IsTableEmty(dt)) { dgv.DataSource = null; return; }
            int count = 1;
            dgv.DataSource = dt;

        }
        public void loadBill(string bill, DataGridView dgv)
        {
            string cmd = $"SELECT  WORK_ID,PART_NUMBER, QTY FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER='{bill}' ORDER BY WORK_ID;";
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            if (globle.IsTableEmty(dt)) { dgv.DataSource = null; return; }
            int count = 1;
            dgv.DataSource = dt;

        }
        public void LoadListBillImport(DataGridView dgv)
        {
            DataTable dtBillImport = new DataTable();
            dtBillImport.Columns.Add("Bill Number");
            dtBillImport.Columns.Add("Status");
            DataTable dt = DBConnect.getData("SELECT DISTINCT BILL_REQUEST_IMPORT.BILL_NUMBER, BILL_STATUS.`STATUS`  FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT INNER JOIN STORE_MATERIAL_DB.BILL_STATUS ON BILL_REQUEST_IMPORT.STATUS_BILL = BILL_STATUS.ID AND BILL_STATUS.`TYPE` = 'NHẬP' AND BILL_REQUEST_IMPORT.BILL_NUMBER LIKE '%/NVL-DT'  ORDER BY CREATE_TIME DESC;");
            dgv.DataSource = dt;

        }
        public int getPCSRequest(string bill, string work)
        {
            return int.Parse(DBConnect.getData($"SELECT REQUESTS FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER = '{bill}' and WORK_ID='{work}';").Rows[0]["REQUESTS"].ToString());
        }
        public bool addNewCusBill(string bill, string work, string pathNumber, int qty, string op, DateTime timeNow)
        {
            string sql = "INSERT INTO STORE_MATERIAL_DB.BILL_IMPORT_CUS (BILL_NUMBER, WORK_ID, PART_NUMBER, QTY, OP, CREATE_TIME, DATE_CREATE) VALUE" +
                $" ('{bill}', '{work}', '{pathNumber}', '{qty}', '{op}', '{timeNow.ToString("yyyy/MM/dd HH:mm:ss")}', '{timeNow.ToString("yyyy/MM/dd")}') " +
                $" ON DUPLICATE key update QTY=QTY+{qty},CREATE_TIME = '{timeNow.ToString("yyyy/MM/dd HH:mm:ss")}', DATE_CREATE = '{timeNow.ToString("yyyy/MM/dd")}';";
            if (globle.mySql.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool updateCusBill(string bill, string work, string partnumber, int qty, string op)
        {
            string cmd = $"UPDATE `TRACKING_SYSTEM`.`BILL_IMPORT_CUS` SET QTY= {qty}, CREATE_TIME=now(),DATE_CREATE=CURDATE(),OP='{op}' WHERE " +
                $"BILL_NUMBER='{bill}' and WORK_ID='{work}' and PART_NUMBER='{partnumber}' ; ";
            if (globle.mySql.InsertDataMySQL(cmd)) return true;
            return false;

        }
        public bool clearPartonBill(string bill, string work, string part, string userID)
        {
            string cmd = $"DELETE FROM `STORE_MATERIAL_DB`.`BILL_IMPORT_CUS` WHERE `BILL_NUMBER`='{bill}' AND  `WORK_ID`='{work}' AND  `PART_NUMBER`='{part}';";
            cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CLEAR {part} IN {work}',  now(), '{userID}');";

            if (!globle.mySql.InsertDataMySQL(cmd)) return false;
            return true;

        }
        public bool clearWorkOnBill(string bill, string work, string userID)
        {
            string cmd = $"DELETE FROM `STORE_MATERIAL_DB`.`BILL_IMPORT_CUS` WHERE `BILL_NUMBER`='{bill}' AND  `WORK_ID`='{work}';";
            cmd += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CLEAR {work}',  now(), '{userID}');";
            if (!globle.mySql.InsertDataMySQL(cmd)) return false;
            return true;
        }
        public bool clearPartCCDConBill(string bill, string part)
        {
            string cmd = $"DELETE FROM `STORE_MATERIAL_DB`.`BILL_IMPORT_CUS` WHERE `BILL_NUMBER`='{bill}'  AND  `PART_NUMBER`='{part}';";
            if (!globle.mySql.InsertDataMySQL(cmd)) return false;
            return true;
        }
        public int getCountPartInWorkNVL(string bill, string workID)
        {
            return int.Parse(DBConnect.getData($"SELECT COUNT(*) FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER = '{bill}' AND WORK_ID = '{workID}';").Rows[0][0].ToString());
        }
        public int getCountPartBill(string billID)
        {
            return int.Parse(globle.mySql.GetDataMySQL($"SELECT count(*) FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER='{billID}';").Rows[0][0].ToString());
        }
        public bool isBillHasImport(string bill, string work, out int sumWork)
        {
            sumWork = 0;
            DataTable dt = DBConnect.getData($"SELECT sum(QTY) AS SUM_WORK FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS WHERE BILL_NUMBER='{bill}' AND WORK_ID='{work}';");
            if (globle.IsTableEmty(dt)) return false;
            string sum = dt.Rows[0]["SUM_WORK"].ToString();
            if (string.IsNullOrEmpty(sum)) return false;
            sumWork = int.Parse(sum);
            return true;
        }
        public bool IsTableEmty(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return true;
            return false;
        }
        public bool ComfirmBill(string bill, int status, string checkBy)
        {
            string sql = string.Empty;
            sql += $"update STORE_MATERIAL_DB.BILL_REQUEST_IMPORT set STATUS_BILL = '{status}' WHERE BILL_NUMBER= '{bill}';";
            sql += $"UPDATE STORE_MATERIAL_DB.BILL_IMPORT_CUS SET CHECK_BY = '{checkBy}' WHERE  BILL_NUMBER = '{bill}'; ";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }

        string modelID, cusID, statusID, bomVersion;
        public bool upDataToolsFile(string billNumber, string intendTime, string userID, string vender, out string mes)
        {
            mes = string.Empty;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XLS files (*.xls, *.xlt, *.xlsb)|*.xls;*.xlt;*xlsb|XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm";
            dlg.FilterIndex = 2;
            if (dlg.ShowDialog() != DialogResult.OK) return false;
            ExcelQueryFactory excel = new ExcelQueryFactory(dlg.FileName);
            var rowslist = from a in excel.Worksheet(0) select a;
            bool dataBill = false;
            string sql = string.Empty;
            string MFG_PART = string.Empty;
            foreach (var item in rowslist)
            {
                string checkData = item[0].Value.ToString();
                if (checkData == "Order No")
                {
                    dataBill = true;
                    continue;
                }
                if (dataBill)
                {
                    if (string.IsNullOrEmpty(item[1].Value.ToString()) || string.IsNullOrEmpty(item[2].Value.ToString()) || string.IsNullOrEmpty(item[3].Value.ToString()))
                    {
                        mes = "Dữ liệu không được để trống ";
                        return false;
                    }
                    string part = item[1].Value.ToString();
                    string unit = item[2].Value.ToString();
                    int qty = int.Parse(item[3].Value.ToString());
                    if (!dTO.isPartBomMaster(part, out MFG_PART))
                    {
                        mes = $"Mã nội bộ {part} không tồn tại! vui lòng kiểm tra lại";
                        return false;
                    }
                    var dataWrite = new
                    {
                        PartNumber = part,
                        Unit = unit,
                        Qty = qty,
                        MFGPart = MFG_PART
                    };
                    sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_IMPORT_CUS` (`BILL_NUMBER`, `PART_NUMBER`, `MFG_PART`, `UNIT`, `QTY`, `OP`, `CREATE_TIME`, `DATE_CREATE`, `CHECK_BY`) VALUES " +
                            $" ('{billNumber}', '{dataWrite.PartNumber}', '{dataWrite.MFGPart}', '{dataWrite.Unit}', '{dataWrite.Qty}', '{userID}', now(), CURDATE(), '{userID}');";
                }
            }
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_IMPORT` (`BILL_NUMBER`, `CREATE_TIME`, `INTEND_TIME`, `STATUS_BILL`, `VENDER_ID`, `TYPE_BILL`) VALUES ('{billNumber}', NOW(), '{intendTime}', '{1}', '{vender}', '{1}');";

            if (DBConnect.InsertMySql(sql))
            {
                mes = $"Tạo phiếu {billNumber}  thành công ";
                return true;
            }
            mes = $"Tạo phiếu {billNumber} thất bại";
            return false;
        }
    }
}
