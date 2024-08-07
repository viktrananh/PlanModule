using DevExpress.XtraLayout.Filtering.Templates;
using DevExpress.XtraRichEdit.Fields;
using PLAN_MODULE.BUS;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Plan
{
    public partial class ucPO : UserControl
    {

        public delegate void SelectFunctionEditPO( POOrder POOrder);
        public event SelectFunctionEditPO _SelectFunctionEditPO;

        CreateWorkDAO _CreatWorkDAO = new CreateWorkDAO();
        PODao _PODao = new PODao();
        POBUS _POBUS = new POBUS();

        private readonly int _ActionId;
        private string _UserId;
        POOrder _POOrder = new POOrder();
        public ucPO(string userId, int actionId, POOrder pOOrder)
        {
            InitializeComponent();

            _UserId = userId;
            _ActionId = actionId;
            _POOrder = pOOrder;

        }

        private void ucPO_Load(object sender, EventArgs e)
        {
            LoadCustomer();

            MyInitialize();
        }
        void MyInitialize()
        {
            if (_ActionId == 0)
            {
                LoadUICreat();
            }
            else
            {
                LoadUIUpdate();
            }
        }
        void LoadUICreat()
        {
            _POOrder = new POOrder();
            txtPO.Clear();
            txtPO.Focus();
            txtPO.Enabled = true;
            cboCustomer.Enabled = true;

            btnClose.Enabled = false;
            btnDelete.Enabled = false;

        }
        void LoadUIUpdate()
        {
            btnClose.Enabled = true;
            btnDelete.Enabled = true;
            txtPO.Text = _POOrder.PO;
            cboCustomer.SelectedValue = _POOrder.CusId;
            txtPO.Enabled = false;
            cboCustomer.Enabled = false;
            _POOrder = _PODao.GetPOOrderByPOName(_POOrder.PO);
            dgvModels.DataSource = _POOrder.PODetails;
        }
        void LoadCustomer()
        {
            DataTable dt = _CreatWorkDAO.GetCustomer();
            cboCustomer.DataSource = dt;
            cboCustomer.DisplayMember = "CUSTOMER_NAME";
            cboCustomer.ValueMember = "CUSTOMER_ID";
            this.cboCustomer.SelectedIndexChanged += new System.EventHandler(this.cboCustomer_SelectedIndexChanged);

        }
        List<Model> _Models = new List<Model>();
        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cusid = cboCustomer.SelectedValue.ToString();
            cboModel.DataSource = _CreatWorkDAO.GetLsModel(cusid);
            cboModel.DisplayMember = "MODEL_ID";
            cboModel.ValueMember = "MODEL_ID";
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            string cusId = cboCustomer.SelectedValue.ToString().Trim();
            string po = txtPO.Text.Trim();
            string modelId = cboModel.Text.Trim();
            DateTime MFG = dtMFG.Value;
            string mfgDate = dtMFG.Value.ToString("yyyy-MM-dd");
            int count = int.Parse(txtCount.Text.Trim());
            int invoiceQty = int.Parse(nbrInvoiceQty.Value.ToString());
            string comment = txtComment.Text.Trim();
            if (txtPO.Enabled)
            {
                MessageBox.Show($"Vui lòng xác nhận P.O trước !");
                return;
            }
            if (_ActionId == 0)
            {
                invoiceQty = 0;
                if (_PODao.IsPOOfModel(modelId, po))
                {
                    MessageBox.Show($"PO {po} đã được tạo cho Model {modelId} trước đó");
                    return;
                }
                if (MFG.Date <= DateTime.Now.Date)
                {
                    MessageBox.Show($"Ngày sản xuất không đúng tiêu chuẩn");
                    return;
                }
            }
            else
            {
                invoiceQty = 0;
                if (_PODao.IsPOApplyWork(po))
                {
                    bool modelExist = _POOrder.PODetails.Any(x => x.ModelId == modelId);
                    if (modelExist)
                    {
                        int oldCount = _POOrder.PODetails.FirstOrDefault(x => x.ModelId == modelId).Count;
                        if (count < oldCount)
                        {
                            MessageBox.Show($"Không thể giảm số lượng PO đã khai báo công lệnh ");
                            return;
                        }
                        int state = _POOrder.PODetails.FirstOrDefault(x => x.ModelId == modelId).State;
                        if (state == 1)
                        {
                            MessageBox.Show($"Không thể cập nhật PO đã đóng ");
                            return;
                        }
                    }

                 
                }
              

            }



            if (_POBUS.POUpdate(cusId, modelId, po, mfgDate, count, comment, _UserId, invoiceQty))
            {
                MessageBox.Show("Cập nhật P.O thành công");
                if (_SelectFunctionEditPO != null)
                {
                    var poOrder = POOrderControl.LoadPOOrderByName(po);
                    _SelectFunctionEditPO(poOrder);
                }
            }
            else
            {
                MessageBox.Show("Cập nhật P.O Lỗi");

            }
            MyInitialize();


        }

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var PODetail = gridView1.GetRow(gridView1.FocusedRowHandle) as PODetail;
            cboModel.SelectedValue = PODetail.ModelId;
            txtComment.Text = PODetail.Comment;
            txtCount.Text = PODetail.Count.ToString();
        }

        private void txtPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string po = txtPO.Text.Trim();
            if (string.IsNullOrEmpty(po)) return;

            var POOrder = _PODao.GetPOOrderByPOName(po);
            if (POOrder.PODetails != null && POOrder.PODetails.Count() > 0)
            {
                MessageBox.Show("PO đã tồn tại!");
                return;

            }
            txtPO.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MyInitialize();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string cusId = cboCustomer.SelectedValue.ToString().Trim();
            string po = txtPO.Text.Trim();
            string modelId = cboModel.Text.Trim();
            DateTime MFG = dtMFG.Value;
            string mfgDate = dtMFG.Value.ToString("yyyy-MM-dd");
            int count = int.Parse(txtCount.Text.Trim());
            string comment = txtComment.Text.Trim();


            if (_POBUS.POClose(po, modelId, _UserId))
            {
                MessageBox.Show("Đóng PO thành công");

            }
            else
            {
                MessageBox.Show("Lỗi ! Đóng PO thất bại");
            }
            MyInitialize();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string cusId = cboCustomer.SelectedValue.ToString().Trim();
            string po = txtPO.Text.Trim();
            string modelId = cboModel.Text.Trim();
            DateTime MFG = dtMFG.Value;
            string mfgDate = dtMFG.Value.ToString("yyyy-MM-dd");
            int count = int.Parse(txtCount.Text.Trim());
            string comment = txtComment.Text.Trim();
            if (_PODao.IsPOApplyWork(po))
            {
                MessageBox.Show($"Không thể xóa PO đã được khai báo công lệnh ");
                return;
            }

            if(_POBUS.PODelete(po , modelId, _UserId))
            {
                MessageBox.Show("Xóa PO thành công");

            }
            else
            {
                MessageBox.Show("Lỗi ! Xóa PO thất bại");
            }
            MyInitialize();

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int state = Convert.ToInt16(gridView1.GetRowCellValue(e.RowHandle, "State"));

            if (state == 1)
            {
                e.Appearance.BackColor = Color.Orange;
            }
            //Override any other formatting  
            e.HighPriority = true;
        }

        private void txtInvoiceQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
