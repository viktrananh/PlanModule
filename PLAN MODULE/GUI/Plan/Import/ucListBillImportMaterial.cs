using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DTO;
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

namespace PLAN_MODULE.GUI.Plan.Import
{
    public partial class ucListBillImportMaterial : UserControl
    {
        public delegate void SelectFunction(int FuctionID, string bill, string wo, string statusName, string OP);
        public event SelectFunction FunctionBillRequestMaterial;

        private readonly string _UserID = string.Empty;
        public ucListBillImportMaterial(string userid)
        {
            InitializeComponent();

            _UserID = userid;
            this.Load += UcListBillImportMaterial_Load;

        }

        private void UcListBillImportMaterial_Load(object sender, EventArgs e)
        {
            InitializeListBill();
        }

        List<BillImportMaterial> _BillImportMaterial = new List<BillImportMaterial>();
        void InitializeListBill()
        {
            object dataSource = null;
            string dataMember = null;

            _BillImportMaterial = new DAO.BillImportMaterialDAO().GetBills();
            dataSource = _BillImportMaterial;
            lbCountBill.Text = _BillImportMaterial.Count().ToString();

            dgvLsBill.PrimaryGrid.DataSource = dataSource;
            dgvLsBill.PrimaryGrid.DataMember = dataMember;
            Binding(dataSource);
        }
        //bool _IsBingding = false;
        void Binding(object context)
        {
            if (context != null)
            {
                lbBill.DataBindings.Clear();
                lbStatusID.DataBindings.Clear();
                lbStatusName.DataBindings.Clear();
                lbWorkID.DataBindings.Clear();
                lbOP.DataBindings.Clear();

                lbBill.DataBindings.Add(new Binding("Text", context, "BillNumber"));
                lbStatusID.DataBindings.Add(new Binding("Text", context, "StatusID"));
                lbStatusName.DataBindings.Add(new Binding("Text", context, "StatusName"));
                lbOP.DataBindings.Add(new Binding("Text", context, "OP"));
                lbWorkID.DataBindings.Add(new Binding("Text", context, "WorkID"));

                //_IsBingding = true;
            }
            else
            {
                lbBill.Text = "";
                lbStatusID.Text = "";
                lbStatusName.Text = "";
                lbOP.Text = "";
                lbWorkID.Text = "";
                //_IsBingding = false;
            }
        }
        private void btncreate_Click(object sender, EventArgs e)
        {
            if (FunctionBillRequestMaterial != null)
            {
                FunctionBillRequestMaterial(LoadFunctionBillImportMaterial.CREATE, lbBill.Text.Trim(), lbWorkID.Text.Trim(), lbStatusName.Text.Trim(), lbOP.Text.Trim());

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

       
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (FunctionBillRequestMaterial != null)
            {
                FunctionBillRequestMaterial(LoadFunctionBillImportMaterial.UPDATE, lbBill.Text.Trim(), lbWorkID.Text.Trim(), lbStatusName.Text.Trim(), lbOP.Text.Trim());

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            string statusBillID = lbStatusID.Text;
            if (statusBillID != "0" && statusBillID != "1")
            {
                MessageBox.Show("Lỗi ! không thể hủy phiếu nàu !");
                return;
            }
            BillRequestImportBUS billImportBUS = new BillRequestImportBUS();
            if (billImportBUS.CancelBill(lbBill.Text.Trim(), _UserID))
            {
                MessageBox.Show($"Hủy phiếu {lbBill.Text.Trim()} thành công.");
            }
            else
            {
                MessageBox.Show("Lỗi ! Hủy thất bại.");

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitializeListBill();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
          
        }
    }
}
