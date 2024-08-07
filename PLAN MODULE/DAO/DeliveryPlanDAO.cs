using DevExpress.XtraExport.Xls;
using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO
{
    public class DeliveryPlanDAO : BaseDAO
    {
        public List<WorkDeliveryPlan> GetDeliveryByWork(string work)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WorkDeliveryPlan A INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WorkId = B.WORK_ID where WorkId='{work}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<WorkDeliveryPlan>();
            List<WorkDeliveryPlan> workDeliveryPlans = new List<WorkDeliveryPlan>();
            foreach (DataRow item in dt.Rows)
            {
                WorkDeliveryPlan workDeliveryPlan = new WorkDeliveryPlan(item);
                workDeliveryPlans.Add(workDeliveryPlan);
            }
            return workDeliveryPlans;
        }
        public List<WorkDeliveryPlan> GetDeliveryByDate(DateTime date)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WorkDeliveryPlan A INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WorkId = B.WORK_ID WHERE  A.DeliveryDate = '{date.ToString("yyyy-MM-dd")}';";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<WorkDeliveryPlan>();
            List<WorkDeliveryPlan> workDeliveryPlans = new List<WorkDeliveryPlan>();
            foreach (DataRow item in dt.Rows)
            {
                WorkDeliveryPlan workDeliveryPlan = new WorkDeliveryPlan(item);
                workDeliveryPlans.Add(workDeliveryPlan);
            }
            return workDeliveryPlans;
        }
        public bool ExistDelevery(string work, DateTime date)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WorkDeliveryPlan WHERE WorkId = '{work}' AND DeliveryDate = '{date.ToString("yyyy-MM-dd")}';";
            DataTable dt = mySql.GetDataMySQL (sql);
            if(istableNull(dt)) return false;
            return true;
        }

    }
}
