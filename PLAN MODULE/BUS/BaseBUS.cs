using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS
{
    public class BaseBUS
    {
        public ROSE_Dll.sqlClass mySQL = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "TRACKING_SYSTEM", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");
        public bool istableNull(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;
            return false;
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(mySQL.GetDataMySQL("SELECT NOW();").Rows[0][0].ToString());
        }
        public bool IsListEmty<T>(List<T> myList)
        {
            if (myList != null && myList.Count != 0) return false;
            return true;
        }

      
    }
}
