
using System.Data;

namespace PLAN_MODULE
{
    public class BusinessLogIn
    {
        BusinessGloble globle = new BusinessGloble();
        public ROSE_Dll.sqlClass mySql = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "STORE_MATERIAL_DB", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");


        public bool checkAccount(string userID, string pwd, out string user, out string name, out int authority, out string Address)
        {
            user = string.Empty;
            name = string.Empty;
            authority = -1;
            Address = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID= '{userID}' AND USER_PASSWORD='{pwd}';";
            DataTable dt = DBConnect.getData(sql);
            if (globle.IsTableEmty(dt))
            {
                string cmd = $"SELECT* FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_ID = '{userID}' AND PWD = '{pwd}'; ";
                DataTable DT = DBConnect.getData(cmd);
                if (globle.IsTableEmty(DT)) return false;
            }
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            Address = dt.Rows[0]["Address"].ToString();
            return true;
        }

        public bool checkAccountBC(string pwd, out string user, out string name, out int authority)
        {
            user = string.Empty;
            name = string.Empty;
            authority = -1;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.USER_CONTROL WHERE USER_PASSWORD='{pwd}';";
            DataTable dt = DBConnect.getData(sql);
            if (globle.IsTableEmty(dt)) return false;
            name = dt.Rows[0]["USER_NAME"].ToString();
            authority = int.Parse(dt.Rows[0]["AUTHORITY"].ToString());
            return true;
        }
        public bool checkAuthority(int authority, string function)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.DEFINE_ACCOUNT_ATHORITY where AUTHORITY = '{authority}' AND ACTION_NAME = '{function}';";
            DataTable DT = DBConnect.getData(sql);
            if (globle.IsTableEmty(DT)) return false;
            return true;
        }
        public bool isAccountQA(string user, ref string name)
        {
            DataTable dt = sqlSever.GetDataSQL($" select * from [PanaCIM].[dbo].[operator] where OPERATOR_BARCODE='{user.Substring(1)}' ;");
            if (globle.IsTableEmty(dt)) return false;
            name = dt.Rows[0]["OPERATOR_NAME"].ToString();
            if (!name.StartsWith("QA") && !name.StartsWith("IQC"))
                return false;
            return true;
        }
        public DataTable getaccountPanacim(string pass, ref string name)
        {
            DataTable dt = sqlSever.GetDataSQL($" select * from [PanaCIM].[dbo].[operator] where OPERATOR_BARCODE='{pass.Substring(1)}' ;");
            if (!globle.IsTableEmty(dt))
                name = dt.Rows[0]["OPERATOR_NAME"].ToString();
            return dt;
        }
    }
}
