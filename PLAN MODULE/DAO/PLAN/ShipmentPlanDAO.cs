using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    public class ShipmentPlanDAO : BaseDAO
    {
        public DataTable GetAllShipmentPlan(DateTime date)
        {
            string sql = $"SELECT A.WORK_ID, B.MODEL_ID, B.CUS_MODEL, C.CUSTOMER_ID , B.CUS_CODE, B.DATE_EXPORT, A.NUMBER_REQUEST , B.EXPORTS_REAL  FROM TRACKING_SYSTEM.DELIVERY_BILL A " +
                $" INNER JOIN TRACKING_SYSTEM.PART_MODEL_CONTROL C ON A.MODEL COLLATE UTF8_UNICODE_CI = C.ID_MODEL " +
                $"LEFT JOIN TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS B  ON A.BILL_NUMBER = B.BILL_NUMBER AND A.WORK_ID= B.WORK_ID WHERE DATE_REAL= DATE('{date.ToString("yyyy-MM-dd")}') ;";
            DataTable dt = mySQL.GetDataMySQL(sql);


            return dt;
        }
    }
}
