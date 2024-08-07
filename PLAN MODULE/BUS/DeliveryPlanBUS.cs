using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS
{
    public class DeliveryPlanBUS : BaseBUS
    {
        public bool UpdateDeliveryPlan(List<WorkDeliveryPlan> workDeliveryPlans, string work, string Op)
        {
            string sql = string.Empty;
            foreach (var item in workDeliveryPlans)
            {
                sql += $"INSERT INTO `MASTER`.`WorkDeliveryPlan` (`WorkId`, `DeliveryDate`, `Count`,  `DateCreat`, `Op`) VALUES " +
             $" ('{work}', '{item.DeliveryDate.ToString("yyyy-MM-dd")}', '{item.Count}',  Now(), '{Op}') " +
             $" ON DUPLICATE KEY UPDATE `Count` = '{item.Count}' , `DateCreat` = Now(), `Op` = '{Op}'  ;";
                string actionName = string.Empty;

                if (item.Action == 0) continue;
                switch (item.Action)
                {
                    case 1:
                        actionName = "Creat";
                        break;
                    case 2:
                        actionName = "Update";
                        break;
                    case -1:
                        actionName = "Delete";
                        break;
                }
                sql += $"INSERT INTO `MASTER`.`WorkDeliveryPlanHistory` (`WorkId`, `DeliveryDate`, `Count`, `Action`, `TimeAction`, `Op`) VALUES " +
                         $" ('{work}', '{item.DeliveryDate.ToString("yyyy-MM-dd")}',  '{item.Count}',  '{actionName}', NOW(), '{Op}');";
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool DeleteDelivery(string work, DateTime date, int count, string userId, bool existDelivery)
        {
            string sql = $"DELETE FROM TRACKING_SYSTEM.WorkDeliveryPlan WHERE WorkId = '{work}' AND DeliveryDate = '{date.ToString("yyyy-MM-dd")}';";
            if(existDelivery )
            {
                sql += $"INSERT INTO `TRACKING_SYSTEM`.`WorkDeliveryPlanHistory` (`WorkId`, `DeliveryDate`, `Action`, `TimeAction`, `Op`, `Count`) " +
                    $" VALUES ('{work}', '{date.ToString("yyyy-MM-dd")}', 'Delete', Now(), '{userId}', '{count}');";
            }
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
    }
}
