using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    internal class BillExportMaterialDAO : BaseDAO
    {
        public DataTable GetDetailBil(string bill)
        {
            string SQL = $"SELECT WORK_ID,  MAIN_PART, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.REQUEST_EXPORT WHERE BILL_NUMBER = '{bill}';";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            return dt;
        }
        public bool IsBillCanCancel(string bill)
        {
            string SQL = $"SELECT * FROM STORE_MATERIAL_DB.BILL_EXPORT_WH WHERE BILL_NUMBER = '{bill}' ;";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            if (istableNull(dt)) return true;
            return false;
        }
        public string getBillInWork(string work)
        {

            DataTable dt = DBConnect.getData($"SELECT BILL_NUMBER FROM STORE_MATERIAL_DB.BILL_REQUEST_EX WHERE WORK_ID= '{work}' AND STATUS_EXPORT <> '-1' and TYPE_BILL= '5' AND SUB_TYPE = '0' GROUP BY BILL_NUMBER ORDER BY TIME_CREATE;");
            if (istableNull(dt)) return null;

            return dt.Rows[0][0].ToString();
        }
        public string CreateBillName(string process)
        {
            string bill = string.Empty;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where DATE(TIME_CREATE)  = CURDATE() AND BILL_NUMBER LIKE '%{process}%'  ORDER BY ID DESC;");
            if (istableNull(dt))
            {
                bill = getTimeServer().ToString("ddMMyyyy") + $"-01/{process}";
            }
            else
            {
                bill = getTimeServer().ToString("ddMMyyyy") + "-" + (int.Parse(dt.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + $"/{process}";
            }
            return bill;
        }
    }
}
