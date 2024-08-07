using PLAN_MODULE.DTO.Planed.BillExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    internal class BillExportGoodsDAO : BaseDAO
    {
        public DataTable GetLsBillExportCus()
        {
            DataTable dt = mySQL.GetDataMySQL("SELECT * FROM TRACKING_SYSTEM.FP_BILLS  WHERE TYPE_BILL = 6 AND STATE != -1  order by `TIME` desc LIMIT 200;");
            return dt;
        }

        public DataTable GetBillExportCusByBillNumber(string bill)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.FP_BILLS  WHERE TYPE_BILL = 6 AND STATE != -1 AND BILL_NUMBER='{bill}' order by `TIME` desc LIMIT 200;");
            return dt;
        }

        public DataTable GetListBill()
        {
            string sql = "SELECT BILL_REQUEST_EX.BILL_NUMBER, DATE_EXPORT, SPECIAL FROM STORE_MATERIAL_DB.BILL_REQUEST_EX inner join  TRACKING_SYSTEM.DELIVERY_BILL on BILL_REQUEST_EX.BILL_NUMBER = DELIVERY_BILL.BILL_NUMBER AND TYPE_BILL = '6' order by DATE_EXPORT desc limit 300; ";
            DataTable dt = DBConnect.getData(sql);
            return dt;
        }
        public int GetPcbaExported(string workID)
        {
            string cmd = $"SELECT sum(QTY) FROM TRACKING_SYSTEM.FB_BOX_HISTORY A INNER JOIN TRACKING_SYSTEM.FP_BILLS B ON A.BILL_NUMBER = B.BILL_NUMBER  where A.WORK_ID='{workID}' AND B.TYPE_BILL = 6;";
            DataTable dt = DBConnect.getData(cmd);
            if (istableNull(dt)) return 0;
            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString())) return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public string createBillRequest(DateTime dtExport)
        {
            string billRequest = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.FP_BILLS where date(INTEND_TIME) =  date('{dtExport.ToString("yyyy-MM-dd HH:mm:ss")}') AND TYPE_BILL = 6 ORDER BY BILL_NUMBER DESC;";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt))
            {
                billRequest = dtExport.ToString("ddMMyy") + "-01/TP";
            }
            else
            {
                billRequest = dtExport.ToString("ddMMyy") + $"-{(int.Parse(dt.Rows[0]["BILL_NUMBER"].ToString().Split('-')[1].Split('/')[0]) + 1).ToString("00")}" + "/TP";
            }
            return billRequest;
        }

        public DataTable getDetailBill(string billNumber)
        {

            string sql = $"SELECT * FROM TRACKING_SYSTEM.FP_BILL_DETAILS where BILL_NUMBER = '{billNumber}';";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt)) return null;
            return dt;
        }
        public string getCustomerBill(string bill, out string cusID)
        {
            cusID = string.Empty;
            string sql = $"SELECT A.BILL_NUMBER, B.CUSTOMER_NAME, B.CUSTOMER_ID FROM STORE_MATERIAL_DB.BILL_REQUEST_EX  A INNER JOIN TRACKING_SYSTEM.DEFINE_CUSTOMER B ON A.CUS_ID = B.CUSTOMER_ID WHERE A.BILL_NUMBER = '{bill}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return null;
            cusID = dt.Rows[0]["CUSTOMER_ID"].ToString();
            return dt.Rows[0]["CUSTOMER_NAME"].ToString();
         
        }
        public bool IsBillCanel(string bill)
        {
            DataTable DT = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.FB_BOX_HISTORY where BILL_NUMBER='{bill}';");
            if(istableNull(DT)) return true;
            return false;
        }

        public bool isBillNumberExist(string bill)
        {
            DataTable dt = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.FP_BILLS where BILL_NUMBER = '{bill}';");
            if (istableNull(dt)) return false;
            return true;
        }

        public DataTable GetListBoxByWork(string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_LIST WHERE WORK_ORDER ='{work}' AND STATE != 2 AND IS_BLOCK = 0 AND BOX_SERIAL NOT IN (SELECT BOX_SERIAL FROM TRACKING_SYSTEM.FP_BOOK_BILL_DETAIL WHERE WORK_ID = '{work}')  order by TIME_PACKING;";
            DataTable dt = DBConnect.getData(sql);
            return dt;
        }

        public DataTable GetListBoxByModel(string MODEL)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.BOX_LIST WHERE MODEL ='{MODEL}' AND STATE != 2 AND IS_BLOCK = 0 AND BOX_SERIAL NOT IN (SELECT BOX_SERIAL FROM TRACKING_SYSTEM.FP_BOOK_BILL_DETAIL)  order by TIME_PACKING;";
            DataTable dt = DBConnect.getData(sql);
            return dt;
        }
        public string getWorkPre(string work_ID)
        {
            string sql = $"SELECT WORK_ID FROM TRACKING_SYSTEM.WORK_PROCESS_LINK where WORK_CHILD='{work_ID}';";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt)) return null;
            return dt.Rows[0][0].ToString();
        }
        public bool checkEcnRelate(string workID,out string error)
        {
            error = "";
            string sql = $"SELECT * FROM ECN_DATA.EcnInfor where LOTNO='{workID}' and ecnstatusid<>4;";
            string workPre = getWorkPre(workID);
            if(!string.IsNullOrEmpty(workPre))
                sql = $"SELECT * FROM ECN_DATA.EcnInfor where( LOTNO='{workID}'  or LOTNO='{workPre}' ) and ecnstatusid<>4;";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt)) return true;
            error = $"Work được khai báo trong {dt.Rows[0]["ecnno"].ToString()} chưa hoàn thành, kiểm tra và thông báo lại cho bộ phận đang thực hiện!";
            return false;
        }
        public List<BookBillExportToCus> GetLsBoxBookedByBill(string bill)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT  A.WORK_ID, B.MODEL, A.REQUEST, A.BOX_COUNT, A.REQUEST_AFTER , A.BOX_SERIAL, A.LOT_NO, B.STATE , B.FQC FROM TRACKING_SYSTEM.FP_BOOK_BILL_DETAIL A INNER JOIN TRACKING_SYSTEM.BOX_LIST B ON A.BOX_SERIAL = B.BOX_SERIAL WHERE BILL_NUMBER = '{bill}' ;");
            if (istableNull(dt)) return null;

            List<BookBillExportToCus> ls = new List<BookBillExportToCus>();
            foreach (DataRow item in dt.Rows)
            {
                BookBillExportToCus bookBillExportToCus = new BookBillExportToCus(item);
                ls.Add(bookBillExportToCus);
            }

            return ls;
        }
        public bool IsBillExported(string bill)
        {
            DataTable DT = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.FB_BOX_HISTORY where BILL_NUMBER='{bill}';");
            if (istableNull(DT)) return false;
            return true;
        }
    }
}
