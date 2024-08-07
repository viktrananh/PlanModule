using System;
using System.Data;
using System.IO;
using System.Linq;


namespace PLAN_MODULE
{

    public class BusinessGloble
    {
        public ROSE_Dll.sqlClass mySql = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "STORE_MATERIAL_DB", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");
        public bool isPartInBom(string part, string model, string verBom)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BOM_CONTENT where MODEL = '{model}' AND VERSION = '{verBom}' AND INTER_PART = '{part}';");
            if (IsTableEmty(dt)) return false;
            return true;
        }
        public bool IsTableEmty(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return true;
            return false;
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(mySql.GetDataMySQL("SELECT NOW();").Rows[0][0].ToString());
        }
        public bool isWork(string work, out string cusID, out string modelID)
        {

            cusID = string.Empty;
            modelID = string.Empty;
            DataTable dt = mySql.GetDataMySQL($"select *  FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}' and STATUS<>'CLOSE';");
            if (IsTableEmty(dt)) return false;
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            modelID = dt.Rows[0]["MODEL"].ToString();
            return true;
        }
        public bool isWork(string work, out string cusID, out string modelID, out string status, out string versionBom)
        {
            cusID = string.Empty;
            modelID = string.Empty;
            status = string.Empty;
            versionBom = string.Empty;
            DataTable dt = mySql.GetDataMySQL($"select *  FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}';");
            if (IsTableEmty(dt)) return false;
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            modelID = dt.Rows[0]["MODEL"].ToString();
            status = dt.Rows[0]["STATUS"].ToString();
            versionBom = dt.Rows[0]["BOM_VERSION"].ToString();
            return true;
        }
        public bool isworkMother(string work)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER WHERE WORK_ID_MOTHER='{work}' and STATUS<>'CLOSE';");
            if (IsTableEmty(dt)) return false;
            return true;

        }
        public void createBillImportMaterial(bool isCustomer, out string bill)
        {
            bill = string.Empty;
            DataTable dtbill = mySql.GetDataMySQL("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE  DATE(CREATE_TIME) = CURDATE() ORDER BY ID DESC ");
            DateTime timeNow = getTimeServer();
            if (isCustomer)
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString() + "/NVL-DT";

            }
            else
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString() + "/NVL-DT-RE";
            }

        }
        public string getCusID(string cusName)
        {
            return mySql.GetDataMySQL($"SELECT CUSTOMER_ID FROM TRACKING_SYSTEM.DEFINE_CUSTOMER where CUSTOMER_NAME='{cusName}';").Rows[0][0].ToString();
        }
        public string getCusIDFromWork(string work)
        {
            return DBConnect.getData($"SELECT CUSTOMER FROM TRACKING_SYSTEM.WORK_ORDER WHERE WORK_ID='{work}';").Rows[0]["CUSTOMER"].ToString();
        }
        public bool isFoxorLux(string CUS)
        {
            DataTable dt = DBConnect.getData("SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER;");
            bool contains = dt.AsEnumerable().Any(row => CUS == row.Field<String>("CUSTOMER_ID") && row.Field<int>("USE_CUS_TEM") == 1);
            if (contains) return true;
            return false;

        }
        public bool isValidCusID(string cusid)
        {
            DataTable dt = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER WHERE CUSTOMER_ID='{cusid}';");
            if (!IsTableEmty(dt))
                return true;
            return false;
        }
        public bool isSoderPaste(string cusPart)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.STORE_MASTER_PART where PART_NUMBER = '{cusPart}' AND MASTER_TYPE = 3;");
            if (IsTableEmty(dt)) return false;
            return true;

        }
        public bool isOPComFirm(string pass, out string OperName)
        {
            OperName = string.Empty;
            DataTable dtnhanvien = sqlSever.GetDataSQL($" select * from [PanaCIM].[dbo].[operator] where OPERATOR_BARCODE='{pass.Substring(1)}' ;");
            if (IsTableEmty(dtnhanvien)) return false;
            OperName = dtnhanvien.Rows[0]["OPERATOR_NAME"].ToString().Trim();
            return true;
        }
        public bool isMSDPart(string part, ref string MSDlevel)
        {
            MSDlevel = "";
            DataTable dt = mySql.GetDataMySQL($"SELECT MSD_TYPE FROM STORE_MATERIAL_DB.MSD_TYPE inner join STORE_MATERIAL_DB.MSD_PART " +
                $"ON STORE_MATERIAL_DB.MSD_TYPE.MSD_INDEX = STORE_MATERIAL_DB.MSD_PART.MSD_LEVEL AND STORE_MATERIAL_DB.MSD_PART.PART_NUMBER = '{part}'; ");
            if (IsTableEmty(dt)) return false;
            else
                MSDlevel = dt.Rows[0][0].ToString();
            return true;
        }
        public bool isBillExist(string billImport, out int statusBillImport)
        {
            statusBillImport = -1;
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.BILL_IMPORT where BILL_NUMBER='{billImport}';");
            if (IsTableEmty(dt)) return false;
            statusBillImport = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            return true;
        }
        public bool isBillCus(string bill)
        {
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS WHERE BILL_NUMBER = '{bill}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            return true;
        }
        public bool isBillHasImport(string billImport)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_IMPORT_WH_FINISH WHERE BILL_NUMBER='{billImport}';");
            if (IsTableEmty(dt)) return false;
            return true;
        }

  
        public void writeDatatext(string Path, string Data)
        {
            StreamWriter wr = new StreamWriter(Path, true);
            wr.WriteLine(Data);
            wr.Close();
        }
        public bool  isQrCodeOP(string pwd, out string userID, out string address)
        {
            userID = string.Empty;
            address = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL where USER_PASSWORD = '{pwd}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt)) return false;
            userID = dt.Rows[0]["USER_ID"].ToString();
            address = dt.Rows[0]["Address"].ToString();
            return true;
        }
        public bool checkAccount(string userID, string pwd, out string user, out string name, out int authority, out string Address)
        {
            user = string.Empty;
            name = string.Empty;
            authority = -1;
            Address = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID= '{userID}' AND USER_PASSWORD='{pwd}';";
            DataTable dt = DBConnect.getData(sql);
            if (IsTableEmty(dt))
            {
                string cmd = $"SELECT* FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID = '{userID}' AND PWD = '{pwd}'; ";
                DataTable DT = DBConnect.getData(cmd);
                if (IsTableEmty(DT)) return false;
                name = DT.Rows[0]["USER_NAME"].ToString();
                authority = int.Parse(DT.Rows[0]["AUTHORITY"].ToString());
                Address = DT.Rows[0]["Address"].ToString();
                return true;
            }
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            Address = dt.Rows[0]["Address"].ToString();
            return true;
        }
    }
}
