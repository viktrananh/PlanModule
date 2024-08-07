using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace PLAN_MODULE
{
    public class BusinessPruductBillImPort
    {
        public ROSE_Dll.sqlClass mySql = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "STORE_MATERIAL_DB", "admin", "ManuAdmin$123");
        public static ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");

        BusinessGloble globle = new BusinessGloble();

        string store_unit = string.Empty;
        string sub_unit = string.Empty;
        string note = string.Empty;
        string checkinTime = string.Empty;
        string pc_op = string.Empty;
        string workID = string.Empty;
        string cusID, modelID, statusID, bomVersion;
       
        public bool iSDIDRestock(string did, string[] stockCheck, out string partNumber,  out string cusIDPanacim/*, out string area*/, out string mes, out int qty)
        {
            partNumber = mes = cusIDPanacim/* = area*/ = string.Empty;
            string sqlpanacim = $"select * from PANACIM.dbo.reel_data where REEL_BARCODE='{did}';";
            qty = 0;
            DataTable dtpanacim = sqlSever.GetDataSQL(sqlpanacim);
            if (globle.IsTableEmty(dtpanacim))
            {
                mes = $" Lỗi !Không tìm thấy dữ liệu cuộn linh kiện {did} trên Panacim"; return false; 
            };
            qty = int.Parse(dtpanacim.Rows[0]["current_quantity"].ToString());
            partNumber = dtpanacim.Rows[0]["PART_NO"].ToString();
            cusIDPanacim = dtpanacim.Rows[0]["USER_DATA"].ToString();
             //area = dtpanacim.Rows[0]["AREA"].ToString();
            //if (!stockCheck.Contains(area)) { mes = $"Lỗi ! Linh kiện {did} không thuộc kho Sản xuất !"; return false; };

            string sql = $"SELECT A.BILL_NUMBER,  A.DID, B.STATUS_BILL FROM STORE_MATERIAL_DB.BILL_EXPORT_PC A INNER JOIN STORE_MATERIAL_DB.BILL_REQUEST_IMPORT  B ON A.BILL_NUMBER = B.BILL_NUMBER  AND A.DID = '{did}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (globle.IsTableEmty(dt)) return true;
            int statusID = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            string billnumber = dt.Rows[0]["BILL_NUMBER"].ToString();
            if (statusID == 1 || statusID == 0)
            {
                mes = $"DID {did} đã được thêm ở phiếu {billnumber} , Vui lòng kiểm tra lại !"; return false;
            }
            return true;
        }
        string cusIDCheck = string.Empty;
        
        bool isDIDPayOutWH(string did, int qty)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_EXPORT_PC where did = '{did}';");

            return true;
        }
        public bool isDIDRemainPanacim(string did, out string pc_op, out string storeUnit, out string subUnit, out string checkInTime)
        {
            pc_op = workID = storeUnit = subUnit = checkInTime = string.Empty;
            string sql = $"select REEL_BARCODE, USER_DATA_2, PART_NUMBER, CHECKIN_OPERATOR, STORAGE_UNIT, SUB_UNIT, DATEADD(HOUR,7 , DATEADD(s, AREA_CHECKIN_TIME ,'1970-01-01')) as CHECK_IN_TIME from  PanaCIM.dbo.unloaded_reel_view WHERE REEL_BARCODE = '{did}';";
            DataTable dt = globle.sqlSever.GetDataSQL(sql);
            if (globle.IsTableEmty(dt)) return false;

            pc_op = dt.Rows[0]["CHECKIN_OPERATOR"].ToString();
            storeUnit = dt.Rows[0]["STORAGE_UNIT"].ToString();
            subUnit = dt.Rows[0]["SUB_UNIT"].ToString();
            checkInTime = dt.Rows[0]["CHECK_IN_TIME"].ToString();
            return true;
        }
        public bool clearBillImport(string billImport, out string mes)
        {
            if (!globle.IsTableEmty(mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER='{billImport}' AND STATUS_BILL={3};")))
            {
                mes = "Phiếu đã được nhập vào hệ thống, không được xóa!";
                return false;
            }
            string cmd = $"delete from STORE_MATERIAL_DB.BILL_EXPORT_PC WHERE BILL_NUMBER='{billImport}';";
            if (!mySql.InsertDataMySQL(cmd))
            {
                mes = "Xóa phiếu bị lỗi!";
                return false;

            }
            mes = "Đã xóa phiếu " + billImport;
            return true;
        }
        public bool isMasterPart(string partNumber, string cus)
        {
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.MASTER_PART WHERE PART_NUMBER = '{partNumber}' AND CUS = '{cus}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (globle.IsTableEmty(dt)) return false;
            return true;
        }
        public void searchBillimport(string billImport, string pathNumber, DataGridView dgv)
        {
            if (string.IsNullOrEmpty(billImport)) return;
            string cmd = "";
            if (!string.IsNullOrEmpty(pathNumber))
                cmd = $"SELECT WORK_ID, PART_NUMBER ,DID, QTY, PC_OP, STORE_UNIT, SUB_UNIT, NOTE FROM STORE_MATERIAL_DB.BILL_EXPORT_PC WHERE BILL_NUMBER='{billImport}'" +
                    $"and PART_NUMBER='{pathNumber}';";
            else
                cmd = $"SELECT  WORK_ID, PART_NUMBER, DID, QTY, PC_OP, STORE_UNIT, SUB_UNIT, NOTE FROM STORE_MATERIAL_DB.BILL_EXPORT_PC WHERE BILL_NUMBER='{billImport}';";
            dgv.DataSource = mySql.GetDataMySQL(cmd);

        }

        public bool isDIDRestock(string did, string [] stockCheck, out string partDID, out string work, out string pc_op, out string area, out string storageUnit, out string subUnit,  out string cusID, out int qty, out string mess)
        {
            work = partDID = cusID = area = pc_op= storageUnit = subUnit = mess = string.Empty;
            qty = 0;
            string sql = $"select * from PANACIM.dbo.unloaded_reel_view where REEL_BARCODE='{did}';";
            DataTable dt = sqlSever.GetDataSQL(sql);
            if (globle.IsTableEmty(dt)) { mess = $" Lỗi !Không tìm thấy dữ liệu cuộn linh kiện {did} trên Panacim"; return false; };
            work = dt.Rows[0]["USER_DATA_2"].ToString();
            partDID = dt.Rows[0]["PART_NUMBER"].ToString();
            pc_op = dt.Rows[0]["CHECKIN_OPERATOR"].ToString();
            cusID = dt.Rows[0]["USER_DATA"].ToString();
            area = dt.Rows[0]["AREA"].ToString();
            storageUnit = dt.Rows[0]["STORAGE_UNIT"].ToString();
            subUnit = dt.Rows[0]["SUB_UNIT"].ToString();
            qty = int.Parse( dt.Rows[0]["ESTIMATED_QUANTITY"].ToString());
            if(!stockCheck.Contains(area)) { mess = $"Lỗi ! Linh kiện {did} không thuộc kho Sản xuất !"; return false; };

            string cmd = $"SELECT A.BILL_NUMBER,  A.DID, B.STATUS_BILL FROM STORE_MATERIAL_DB.BILL_EXPORT_PC A INNER JOIN STORE_MATERIAL_DB.BILL_REQUEST_IMPORT  B ON A.BILL_NUMBER = B.BILL_NUMBER  AND A.DID = '{did}';";
            DataTable dtc = mySql.GetDataMySQL(cmd);
            if (globle.IsTableEmty(dtc)) return true;
            int statusID = int.Parse(dtc.Rows[0]["STATUS_BILL"].ToString());
            string billnumber = dtc.Rows[0]["BILL_NUMBER"].ToString();
            if (statusID == 1 || statusID == 0)
            {
                mess = $"DID {did} đã được thêm ở phiếu {billnumber} , Vui lòng kiểm tra lại !"; return false;

            }
            return true;
        }
        #region THÀNH PHẨM
        public bool isBoxNotCreateBillExport(string box, out string billImport)
        {
            billImport = string.Empty;
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_IMPORT_PC_FP WHERE BOX_SERIAL = '{box}' AND STATE <> -1;");
            if (globle.IsTableEmty(dt)) return true;
            billImport = dt.Rows[0]["BILL_NUMBER"].ToString();
            int statusBill = int.Parse(mySql.GetDataMySQL($"SELECT STATUS_BILL FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER = '{billImport}';").Rows[0][0].ToString());
            if (statusBill == 0) return false;
            return true;
        }
        public bool isBoxBlock(string box)
        {
            DataTable dataTable = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.BOX_ID where BOX_SERIAL = '{box}' and IS_BLOCK = 1;");
            if (globle.IsTableEmty(dataTable)) return false;
            return true;
        }
        public bool checkBoxHasExport(string boxSerial)
        {

            string sql = $"SELECT * FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT WHERE BOX_SERIAL = '{boxSerial}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (globle.IsTableEmty(dt)) return false;
            return true;
        }

        #endregion
    }
}
