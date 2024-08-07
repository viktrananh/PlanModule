using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLAN_MODULE.DTO;

namespace PLAN_MODULE
{
    public partial class PlanProduct : Form
    {
        CreateWorkDAO dTOCreate = new CreateWorkDAO();
        CreateLaserPlanDao daolaserPlan = new CreateLaserPlanDao();
        private  string _op;
        WorkOrder WorkOrder = new WorkOrder();
        DAO_WorkInfor DAO_WorkInfor = new DAO_WorkInfor();
        public PlanProduct(string op)
        {
            this._op = op;
            InitializeComponent();
        }
        List<WorkPlanning> DetailWorkPlaned;
        
        void loadPlaned()
        {
            try
            {
                dgvView.Rows.Clear();
            }
            catch (Exception)
            {

            }
            DataTable dtPlaned = daolaserPlan.getPlaned(WorkOrder.WorkID);
            DetailWorkPlaned = new List<WorkPlanning>();
            if (!dTOCreate.istableNull(dtPlaned))
            { 
                DetailWorkPlaned = (from r in dtPlaned.AsEnumerable()
                                    select new WorkPlanning()
                                    {
                                        Date = r.Field<DateTime>("MFG_DATE"),
                                        line=r.Field<string>("LINE"),
                                        Qty= r.Field<int>("qty"),
                                        qtyPanelMarked=r.Field<int>("QTY_PANEL"),
                                        qtyPcsMarked=r.Field<int>("QTY_PCS"),
                                        
                                    }).ToList();
                int sst = 1;
                foreach (var item in DetailWorkPlaned)
                {
                    dgvView.Rows.Add( sst.ToString(), item.Date.ToString("yyyy-MM-dd"),item.line,item.Qty,item.qtyPcsMarked,item.qtyPanelMarked);
                    sst++;
                }

            }
            lbMarked.Text = dTOCreate.getMarkInfor(WorkOrder.WorkID);

        }
       

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (txtwork.Enabled)
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
            if(qty+sumMarkPlan > WorkOrder.NumberTemp)
            {
                MessageBox.Show($"Lỗi ! Số lượng lập kế hoạch vượt quá số lượng work");
                return;
            }
            int qtyOld = 0;
            if(daolaserPlan.WorkLaserPlaned(WorkOrder.WorkID, date, out qtyOld))
            {
                if(qty < qtyOld)
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
            if (daolaserPlan.createPlan(WorkOrder.WorkID,qty,  date, nbrLine.Value.ToString(), _op))
            {
                MessageBox.Show($"Pass");
            }
            else
            {
                MessageBox.Show($"Fail");

            }
            loadPlaned();
        }

     

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtwork.Enabled)
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

        private void txtwork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            WorkOrder = DAO_WorkInfor.getWorkInfor(txtwork.Text);
            if(string.IsNullOrEmpty(WorkOrder.WorkID))
            {
                MessageBox.Show($"Work order không tồn tại!");
                return;
            }    
            if(WorkOrder.state==WorkOrder.stateClose)
            {
                MessageBox.Show($"Work order đã đóng!");
                return;
            }
            txtwork.Enabled = false;
            lbTotal.Text = $"Total : {WorkOrder.NumberTemp}";
            loadPlaned();
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtqty.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtqty.Text = txtqty.Text.Remove(txtqty.Text.Length - 1);
            }
        }

        private void lbWork_Click(object sender, EventArgs e)
        {

        }
    }
}
