using DevExpress.XtraCharts;
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
using System.Xml.Linq;

namespace PLAN_MODULE.GUI.Plan.Laser
{
    public partial class ucLaserPlan : UserControl
    {
        CreateWorkDAO dTOCreate = new CreateWorkDAO();
        CreateLaserPlanDao daolaserPlan = new CreateLaserPlanDao();
        private string _op;
        WorkOrder WorkOrder = new WorkOrder();
        DAO_WorkInfor DAO_WorkInfor = new DAO_WorkInfor();
        List<WorkPlanning> _DetailWorkPlaned = new List<WorkPlanning>();

        public ucLaserPlan( string op)
        {
            InitializeComponent();
            _op = op;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtWorkId.Enabled)
            {
                MessageBox.Show($"Yêu cầu điền work order=> enter để xác nhận thông tin!");
                return;
            }
            string date = dtpPlan.Value.ToString("yyyy-MM-dd");
            DateTime timenow = dTOCreate.getTimeServer();
            if (dtpPlan.Value.Date < timenow.Date)
            {
                MessageBox.Show($"Lỗi ! Ngày khắc phải lớn hơn hoặc bằng ngày hiện tại");
                return;
            }
            if (dtpPlan.Value.Date > timenow.Date.AddDays(10))
            {
                MessageBox.Show($"Lỗi ! Ngày khắc không quá 10 ngày từ thời điểm hiện tại");
                return;
            }
            int sumMarkPlan = daolaserPlan.getSumPlaned(WorkOrder.WorkID);
            int qty = int.Parse(txtqty.Text);
          
            int qtyOld = 0;
            if (daolaserPlan.WorkLaserPlaned(WorkOrder.WorkID, date, out qtyOld))
            {
                if (qty < qtyOld)
                {
                    MessageBox.Show($"Lỗi ! Chỉ được thêm số lượng khắc");
                    return;
                }
                int countAdd = qty - qtyOld;
                if (countAdd + sumMarkPlan > WorkOrder.NumberTemp)
                {
                    MessageBox.Show($"Lỗi ! Số lượng lập kế hoạch vượt quá số lượng work");
                    return;
                }

            }
            else
            {
                if (qty + sumMarkPlan > WorkOrder.NumberTemp)
                {
                    MessageBox.Show($"Lỗi ! Số lượng lập kế hoạch vượt quá số lượng work");
                    return;
                }
            }
            if (daolaserPlan.createPlan(WorkOrder.WorkID, qty, date, nbrLine.Value.ToString(), _op))
            {
                MessageBox.Show($"Pass");
            }
            else
            {
                MessageBox.Show($"Fail");

            }
            loadPlaned();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtWorkId.Enabled)
            {
                MessageBox.Show($"Yêu cầu điền work order=> enter để xác nhận thông tin!");
                return;
            }
            if (daolaserPlan.isExitLaserMarkedbyDate(WorkOrder.WorkID, dtpPlan.Value.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show($"Đã có dữ liệu khắc ngày {dtpPlan.Value.ToString("yyyy-MM-dd")} không thể xóa");
                return;
            }
            if (!daolaserPlan.clearPlanByDate(WorkOrder.WorkID, dtpPlan.Value.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show($"Fail");
            }
            else
            {
                MessageBox.Show($"Pass");
            }
            loadPlaned();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadPlaned();
        }

        private void loadPlaned()
        {
            DataTable dtPlaned = daolaserPlan.getPlaned(WorkOrder.WorkID);
            _DetailWorkPlaned = new List<WorkPlanning>();
            if (!dTOCreate.istableNull(dtPlaned))
            {
                _DetailWorkPlaned = (from r in dtPlaned.AsEnumerable()
                                     select new WorkPlanning()
                                     {
                                         Date = r.Field<DateTime>("MFG_DATE"),
                                         line = r.Field<string>("LINE"),
                                         Qty = r.Field<int>("qty"),
                                         qtyPanelMarked = r.Field<int>("QTY_PANEL"),
                                         qtyPcsMarked = r.Field<int>("QTY_PCS"),

                                     }).ToList();
                dgvView.DataSource = _DetailWorkPlaned;

            }
            lbMarked.Text = dTOCreate.getMarkInfor(WorkOrder.WorkID);

        }


        private void txtWorkId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)  return;
            WorkOrder = DAO_WorkInfor.getWorkInfor(txtWorkId.Text);
            if (string.IsNullOrEmpty(WorkOrder.WorkID))
            {
                MessageBox.Show($"Work order không tồn tại!");
                return;
            }
            if (WorkOrder.state == WorkOrder.stateClose)
            {
                MessageBox.Show($"Work order đã đóng!");
                return;
            }
            txtWorkId.Enabled = false;
            txtTempNumber.Text = $"Total : {WorkOrder.NumberTemp}";
            loadPlaned();
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            dtpPlan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Date").ToString();
            nbrLine.Value = int.Parse( gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "line").ToString());
            txtqty.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString();
        }
    }
}
