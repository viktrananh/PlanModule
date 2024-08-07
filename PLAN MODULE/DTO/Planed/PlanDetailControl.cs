using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    internal class PlanDetailControl : LineAppointmentDAO
    {
        private static PlanDetailControl instance;
        public static PlanDetailControl Instance
        {
            get { if (instance == null) instance = new PlanDetailControl(); return PlanDetailControl.instance; }
            private set { PlanDetailControl.instance = value; }

        }

        public List<PlanDetail> LoadLsPlanDetail(DateTime dateTime)
        {
            List<PlanDetail> planDetails = new List<PlanDetail>();

            DataTable dt = GetTablePlan(dateTime);
            foreach (DataRow item in dt.Rows)
            {
                PlanDetail planDetail = new PlanDetail(item);
                planDetails.Add(planDetail);
            }

            return planDetails;
        }
    }
}
