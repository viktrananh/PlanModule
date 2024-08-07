using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed.BillExport;
//using PLAN_MODULE.DTO.Planed.BillImport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseDll.DAO.FinishedProduct;
using WarehouseDll.DTO.FinishedProduct;

namespace PLAN_MODULE.GUI.Plan.Export
{
    public partial class ucListBillExportGoodsToCus : UserControl
    {
        public delegate void SelectFunction(int FuctionID, FPBill bill);
        public event SelectFunction FunctionBillRequestExportGoodToCus;

        private readonly string _UserID = string.Empty;
        public ucListBillExportGoodsToCus(string userid)
        {
            InitializeComponent();
            _UserID = userid;
            this.Load += UcListBillExportGoods_Load;
        }
        FPBillExportDAO _FBBillExportDAO = new FPBillExportDAO();
        private void UcListBillExportGoods_Load(object sender, EventArgs e)
        {
            InitializeListBill();
        }


        List<FPBill> _FPBills = new List<FPBill>();

        void InitializeListBill()
        {


            _FPBills = _FBBillExportDAO.GetAllFBBill(6, DateTime.Now.AddMonths(-2), DateTime.Now.AddDays(1));

            var dataGridView1 = new BindingList<FPBill>(_FPBills);
            dgvlsBill.DataSource = dataGridView1;
            lbCountBill.Text = _FPBills.Count().ToString();
            BingdingData(dataGridView1);
        }
        bool _IsBingding = false;
        void BingdingData(object context)
        {
            if (context != null)
            {
                lbBill.DataBindings.Clear();
                lbBill.DataBindings.Add(new Binding("Text", context, "BillNumber"));
            }
            else
            {
                lbBill.Text = "";
            }
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            if (FunctionBillRequestExportGoodToCus != null)
            {
                FunctionBillRequestExportGoodToCus(LoadFunctionBillExportGoodsToCus.CREATE, null);
            }
        }
        private void dgvLsBill_Click(object sender, EventArgs e)
        {
            int statusID = int.Parse(lbStatusID.Text.Trim());
            foreach (DevComponents.DotNetBar.ButtonX item in pnlStatusBill.Controls.OfType<DevComponents.DotNetBar.ButtonX>())
            {
                if (int.Parse(item.Tag.ToString()) <= statusID) item.Enabled = true;
                else item.Enabled = false;
            }
        }

        private void dgvLsBill_DoubleClick(object sender, EventArgs e)
        {
            if (FunctionBillRequestExportGoodToCus != null)
            {
                FunctionBillRequestExportGoodToCus(LoadFunctionBillImportMaterial.UPDATE, null);

            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (FunctionBillRequestExportGoodToCus != null)
            {

                FPBill billSelect = gridView1.GetRow(gridView1.FocusedRowHandle) as FPBill;
                var fpBill = _FBBillExportDAO.GetFBBillByBillId(billSelect.BillNumber);
                FunctionBillRequestExportGoodToCus(LoadFunctionBillImportMaterial.UPDATE, fpBill);

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
          
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitializeListBill();
        }
    }
}
