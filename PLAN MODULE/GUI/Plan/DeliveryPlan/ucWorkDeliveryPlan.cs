using DevExpress.Data.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.Office.Interop.Excel;
using PLAN_MODULE.BUS;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
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
    public partial class ucWorkDeliveryPlan : UserControl
    {

        DeliveryPlanDAO _DeliveryPlanDAO = new DeliveryPlanDAO();
        DeliveryPlanBUS _DeliveryPlanBUS = new DeliveryPlanBUS();
        private readonly Work _Work;
        private readonly string _UserId;
        public ucWorkDeliveryPlan(Work work, string userId)
        {
            InitializeComponent();
            _Work = work;
            _UserId = userId;
            this.Load += UcWorkDeliveryPlan_Load;
        }
        List<WorkDeliveryPlan> _WorkDeliveryPlans = new List<WorkDeliveryPlan>();
        BindingSource _BindingSource = new BindingSource();

        private void UcWorkDeliveryPlan_Load(object sender, EventArgs e)
        {
            lbOP.Text = _UserId;
            lbWork.Text = _Work.WorkID;
            LoadData();
            gridView1.RowCellClick += gridView1_RowCellClick;

        }

        void LoadData()
        {
            txtWorkId.Text = _Work.WorkID;
            txtModelId.Text = _Work.ModelWork;
            txtPO.Text = _Work.PO;
            nbrCount.Value = _Work.TotalPCS;
            _WorkDeliveryPlans = _DeliveryPlanDAO.GetDeliveryByWork(_Work.WorkID);

            var dataGridView1 = new BindingList<WorkDeliveryPlan>(_WorkDeliveryPlans);
            dgvDetail.DataSource = dataGridView1;
            _BindingSource.DataSource = _WorkDeliveryPlans;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_DeliveryPlanBUS.UpdateDeliveryPlan(_WorkDeliveryPlans, _Work.WorkID, _UserId))
            {
                MessageBox.Show("Cập nhật kế hoạch giao hàng thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật kế hoạch giao hàng lỗi");
            }
            LoadData();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
           dtDeliveryplan.Value = DateTime.Parse( gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryDate").ToString());
           txtQty.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Count").ToString();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if(string.IsNullOrEmpty(txtQty.Text)) return;
            DateTime date = dtDeliveryplan.Value;
            int qty = int.Parse(txtQty.Text);
            DateTime timeNow = DateTime.Now;
            if(date.Date < timeNow.Date)
            {
                MessageBox.Show($"Kế hoạch giao hàng phải có ngày lớn hơn ngày tạo kế hoạch ! ");
                return;
            }
            if(_WorkDeliveryPlans.Any(x=>x.DeliveryDate.Date == date.Date))
            {
                if(_WorkDeliveryPlans.Any(x => x.DeliveryDate.Date == date.Date && x.Status == true))
                {
                    if(date.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show($"Không thể chỉnh sửa kế hoạch ngày {date.Date.ToString("yyyy-MM-dd")} ");
                        return;
                    }
                    _WorkDeliveryPlans.FirstOrDefault(x => x.DeliveryDate.Date == date.Date).Action = 2;
                  
                }
                else
                {
                    _WorkDeliveryPlans.FirstOrDefault(x => x.DeliveryDate.Date == date.Date).Action = 1;

                }
                _WorkDeliveryPlans.FirstOrDefault(x => x.DeliveryDate.Date == date.Date).Count = qty;
                var dataGridView1 = new BindingList<WorkDeliveryPlan>(_WorkDeliveryPlans);
                dgvDetail.DataSource = dataGridView1;
                _BindingSource.DataSource = _WorkDeliveryPlans;
            }
            else
            {

                var sumQtyDelivery = _WorkDeliveryPlans.Sum(x => x.Count);
                if(sumQtyDelivery + qty > _Work.PCS)
                {
                    MessageBox.Show($"Kế hoạch giao hàng {sumQtyDelivery} + {qty}  = {sumQtyDelivery + qty} lớn hơn số lượng Work {_Work.PCS}");
                    return;
                }

                gridView1.AddNewRow();
                gridView1.SetRowCellValue(GridControl.NewItemRowHandle, "DeliveryDate", date);
                gridView1.SetRowCellValue(GridControl.NewItemRowHandle, "Count", qty);
                gridView1.SetRowCellValue(GridControl.NewItemRowHandle, "Action", 1);

                var a = _WorkDeliveryPlans;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime date = dtDeliveryplan.Value;
            int qty = int.Parse(txtQty.Text);
            DateTime timeNow = DateTime.Now;

            if (date.Date < timeNow.Date)
            {
                MessageBox.Show($"Không được xóa kế hoạch giao hàng này ");
                return;
            }

            var existDelivery = _DeliveryPlanDAO.ExistDelevery(_Work.WorkID, date.Date);

            if (DialogResult.OK != MessageBox.Show("Bạn có chắc chắn muốn Xóa Wo", "Xác nhận", MessageBoxButtons.OKCancel)) return;
            if (_DeliveryPlanBUS.DeleteDelivery(_Work.WorkID, date, qty, _UserId, existDelivery))
            {
                MessageBox.Show($"Xóa kế hoạch giao hàng thành công ");
            }
            else
            {
                MessageBox.Show($"Lỗi ! ");

            }
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
