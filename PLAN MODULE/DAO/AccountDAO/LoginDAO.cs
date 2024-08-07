using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.AccountDAO
{
    internal class LoginDAO : BaseDAO
    {
        public bool checkAccount(string userID, string pwd, out string name, out string authority)
        {
            name = string.Empty;
            authority ="";
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID= '{userID}' AND USER_PASSWORD='{pwd}' UNION SELECT * FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID= '{userID}' AND PWD='{pwd}'; ";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = dt.Rows[0]["GROUP_AUTHORITY"].ToString();
            return true;
        }
        public bool IsBarcodeUser(string barcode, out string user, out int authority)
        {
            user = "";
            authority = -1;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL where USER_PASSWORD = '{barcode}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            user = dt.Rows[0]["USER_ID"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            return true;
        }
        public bool checkVersion(string versionSoft, out string mess)
        {
            mess = string.Empty;
            DataTable dt = mySQL.GetDataMySQL("SELECT VERSION FROM COMPUTER_SYSTEM.SOFTWARE_DETAIL where `NAME` = 'REWORK'  AND `USE` = 1 ;");
            string version = dt.Rows[0][0].ToString();
            if (version != versionSoft)
            {
                mess = $"Chương trình đã có phiên bản {version} ! Vui lòng tải bản Update mới ";
                return false;
            }
            return true;
        }
    }
}
