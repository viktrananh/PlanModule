using DevComponents.Editors.DateTimeAdv;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO.Planed.PlanExport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Report
{
    public partial class ucShipmentPlanUI : UserControl
    {
        BaseDAO baseDAO = new BaseDAO();
        private static ucShipmentPlanUI instance;
        public static ucShipmentPlanUI Instance
        {
            get { if (instance == null) instance = new ucShipmentPlanUI(); return instance; }
            private set { instance = value; }

        }
        public ucShipmentPlanUI()
        {
            InitializeComponent();
            InitializeShipment();
        }
        List<ShipmentView> shipmentPlans = new List<ShipmentView>();
        void InitializeShipment()
        {
            DateTime time = dtMonth.Value;

            LoadShipment(time);
        }
        void LoadShipment(DateTime date)
        {
            object dataSource = null;
            shipmentPlans = ShipmentPlanControl.Instance.LoadListWorkExport(date);
            dataSource = shipmentPlans;
            gridControl1.DataSource = dataSource;
        }
      
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitializeShipment();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DateTime time = dtMonth.Value;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = $"Monthly shipment report {time.Year} - {time.Month}" + ".xlsx";
            if (DialogResult.OK != dlg.ShowDialog()) return;
            gridControl1.ExportToXlsx(dlg.FileName);
        }
    }
}
