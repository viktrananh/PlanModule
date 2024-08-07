using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PLAN_MODULE.DTO.Laser;

namespace PLAN_MODULE.DAO
{
    class CreateLaserPlanDao : BaseDAO
    {
        public DataTable getPlaned(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.LASER_PLAN WHERE WORK_id='{work}';");
            return dt;
        }

        public List<WorkPlanning> GetPlanedNew(DateTime start, DateTime end)
        {

            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.LASER_PLAN where MFG_DATE >= '{start.ToString("yyyy-MM-dd")}' AND MFG_DATE <= '{end.ToString("yyyy-MM-dd")}';");
            if (istableNull(dt)) return new List<WorkPlanning>();
            List<WorkPlanning> ls = (from r in dt.AsEnumerable()
                                     select new WorkPlanning()
                                     {
                                         WorkID = r.Field<string>("WORK_ID"),
                                         Date = r.Field<DateTime>("MFG_DATE"),
                                         line = r.Field<string>("LINE"),
                                         Qty = r.Field<int>("qty"),
                                         qtyPanelMarked = r.Field<int>("QTY_PANEL"),
                                         qtyPcsMarked = r.Field<int>("QTY_PCS"),
                                     }).ToList();

            return ls;
        }

        public int getSumPlaned(string work)
        {
            string sql = $"select SUM(qty) from `TRACKING_SYSTEM`.`LASER_PLAN`  where `WORK_ID`='{work}';";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt)) return 0;
            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString())) return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public bool WorkLaserPlaned(string work, string date, out int qty)
        {
            qty = 0;
            string sql = $"select * from `TRACKING_SYSTEM`.`LASER_PLAN`  where `WORK_ID`='{work}' and MFG_DATE = '{date}';";
            DataTable dt = DBConnect.getData(sql);
            if (istableNull(dt)) return false;
            qty = int.Parse(dt.Rows[0]["QTY"].ToString());
            return true;
        }

        public bool createPlan(string work, int qty, string date, string line, string op)
        {
            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`LASER_PLAN` (`WORK_ID`,QTY, `LINE`, `MFG_DATE`,`CREATER`,`TIME_CREATE`) VALUES " +
                $" ('{work}','{qty}','{line}', '{date}','{op}',NOW())  ON DUPLICATE KEY UPDATE QTY = {qty} , `CREATER` = '{op}' , `TIME_CREATE` = now() ;";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool isExitLaserMarkedbyDate(string work, string date)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.MVN_LASER_MARKING_DATA where WORK='{work}' and DATE(Date)='{date}';");
            if (istableNull(dt))
                return false;
            return true;
        }
        public bool clearPlanByDate(string work, string date)
        {
            string sql = $"delete FROM TRACKING_SYSTEM.LASER_PLAN where WORK_ID='{work}' and MFG_DATE='{date}' ;";
            return mySQL.InsertDataMySQL(sql);
        }
        public List<LaserMakingData> GetLaserMarkReal(DateTime start, DateTime end)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.MVN_LASER_MARKING_DATA  where `DATE` >= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and `DATE` < '{end.ToString("yyyy-MM-dd HH:mm:ss")}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<LaserMakingData>();

            List<LaserMakingData> ls = new List<LaserMakingData>();
            foreach (DataRow item in dt.Rows)
            {
                LaserMakingData laserMakingData = new LaserMakingData(item);
                ls.Add(laserMakingData);
            }
            return ls;
        }
    }
}
