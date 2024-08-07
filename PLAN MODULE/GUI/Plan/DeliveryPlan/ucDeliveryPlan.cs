using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Plan.DeliveryPlan
{
    public partial class ucDeliveryPlan : UserControl
    {
        DeliveryPlanDAO _DeliveryPlanDAO = new DeliveryPlanDAO();
        public ucDeliveryPlan()
        {
            InitializeComponent();
            this.Load += UcDeliveryPlan_Load;
        }

        private void UcDeliveryPlan_Load(object sender, EventArgs e)
        {
            DateTime date = dtDelivery.DateTime;
            GetDeliveryByDate(date);
        }

        void GetDeliveryByDate(DateTime date)
        {
            List<WorkDeliveryPlan> deliveryPlans= _DeliveryPlanDAO.GetDeliveryByDate(date);
            dgvDetails.DataSource = deliveryPlans;
        }

        private void dtDelivery_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = dtDelivery.DateTime;
            GetDeliveryByDate(date);
        }
    }
}
