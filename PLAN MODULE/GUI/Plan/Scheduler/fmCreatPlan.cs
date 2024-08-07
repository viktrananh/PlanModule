using DevComponents.DotNetBar.Controls;
using DevExpress.Data.Helpers;
using DevExpress.Data.Mask;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DAO.PLAN;
using PLAN_MODULE.DTO.Planed;
//using PLAN_MODULE.DTO.Planed.BillImport;
using PLAN_MODULE.DTO.Planed.CreatPlan;
using PLAN_MODULE.DTO.ProductionPlans;
using ROSE_Dll.DAO;
using ROSE_Dll.DTO;
using ROSE_Dll.DTO.ProducingProcess;
using ROSE_Dll.ViewModel.ProducingProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Plan.Scheduler
{
    public partial class fmCreatPlan : Form
    {

        public delegate void SendPlanDetail(bool IsPassCreat);
        public event SendPlanDetail sendPlanDetail;


        readonly string _UserId = string.Empty;
        readonly int _ActionId;
        ProductionPlanVM _ProductionPlanVM = new ProductionPlanVM();

        ClusterDAO _ClusterDAO = new ClusterDAO();
        ProductionPlanBUS _ProductionPlanBUS = new ProductionPlanBUS();
        ProductionPlanDAO _ProductionPlanDAO = new ProductionPlanDAO();
        DeliveryPlanDAO _DeliveryPlanDAO = new DeliveryPlanDAO();

        readonly DateTime _DateSelect = DateTime.MinValue;
        List<PlanStopConfig> _LsPlanConfigs = new List<PlanStopConfig>();
        string _MachineName = Environment.MachineName;
        bool isNewAppointment = true;
        List<Cluster> _Cluster = new List<Cluster>();
        List<PlanDetail> _Plans = new List<PlanDetail>();
        List<LineDefine> _LineDefine = new List<LineDefine>();

        public fmCreatPlan(string userId, int actionId, ProductionPlanVM productionPlanVM)
        {
            InitializeComponent();
            _UserId = userId;
            _ActionId = actionId;
            _Cluster = _ClusterDAO.GetAllCluster();
            _LineDefine = _ClusterDAO.GetAllLineDefine();
            _ProductionPlanVM = productionPlanVM;
        }
        private void CreatPlan_Load(object sender, EventArgs e)
        {
            DateTime timeNow = _ProductionPlanDAO.getTimeServer();

            btnDelete.Enabled = false;

            _LsPlanConfigs = _ProductionPlanDAO.GetConfigPlanStop();


            if (_ActionId == 0)
            {
                LoadUICreat();
            }
            else if (_ActionId == 1)
            {
                LoadUIUpdate();
            }
        }

        void LoadUICreat()
        {
            btnCreatPlan.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            lbTitle.Text = "Tạo kế hoạch sản xuất";

        }
        void LoadUIUpdate()
        {
            btnCreatPlan.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnRefresh.Enabled = false; 
            txtWork.ReadOnly = true;
            txtQty.ReadOnly = true;
            cboCluster.Enabled = false;
            dtStart.Enabled = false;

            lbTitle.Text = "Xem thông tin kế hoạch sản xuất";
            _ProductionPlanVM = _ProductionPlanDAO.GetAllProductionPlanById(_ProductionPlanVM.Id);

            txtWork.Text = _ProductionPlanVM.WorkId;
            ComfirmWorkId(_ProductionPlanVM.WorkId);

            cboCluster.SelectedValue = _ProductionPlanVM.ClusterId;

            txtQty.Text = _ProductionPlanVM.Count.ToString();

            dtStart.Value = _ProductionPlanVM.StartTime;
            List<ProductionPlanVM> ls = new List<ProductionPlanVM>() { _ProductionPlanVM };
            dgvViewPlan.DataSource = ls;
        }
        List<CycleTime> cycleTimes = new List<CycleTime>();
        string _ModelID, _CusID, _Status, _Bomversion, _ModelName;
        int _Totalpcbs, _IsRma, _IsXout, _PcbXout, _PcbOnPanel;
        bool isWorkSMT = false;
        string Router = "PTH";
        string Line = string.Empty;

        string PROCESS = string.Empty;
        List<ClusterInforVms> _ClusterInforVm = new List<ClusterInforVms>();
        private void txtWork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string workID = txtWork.Text;

            ComfirmWorkId(workID);
        }

        void ComfirmWorkId(string workID)
        {
            cycleTimes = new List<CycleTime>();
            PROCESS = string.Empty;
            _ClusterInforVm = new List<ClusterInforVms>();
            if (!_ProductionPlanDAO.isWorkID(workID, out _ModelID, out _CusID, out _Totalpcbs, out _Status, out _Bomversion, out _IsRma,
                out _IsXout, out _PcbXout, out _ModelName, out _PcbOnPanel, out PROCESS))
            {
                txtWork.Text = "";
                MessageBox.Show("Work không tồn tại");
                return;
            }

            List<string> lineConfig = new List<string>();
            _ClusterInforVm = _ClusterDAO.GetAllClusterInforVmByModel(_ModelID).ToList();
            cboCluster.DataSource = _ClusterInforVm;
            cboCluster.DisplayMember = "ClusterName";
            cboCluster.ValueMember = "ClusterId";
            var lsDeliveryDate = _ProductionPlanDAO.GetDeliveryPlan(workID);
            cboDeliveryDate.DataSource = lsDeliveryDate;
            txtQty.Clear();
            txtQty.Focus();
            txtWork.Enabled = false;
            foreach (var item in _ClusterInforVm)
            {
                StepProgressBarItem step = new StepProgressBarItem();
                step.Name = item.ClusterName;
                step.ContentBlock2.Caption = item.ClusterName;
                stepProgressBar1.Items.Add(step);
            }
            stepProgressBar1.ItemOptions.Indicator.ActiveStateDrawMode = IndicatorDrawMode.Full;
            stepProgressBar1.SelectedItemIndex = _ClusterInforVm.Count();

            cboCluster.SelectedIndexChanged += CboCluster_SelectedIndexChanged;
        }
        List<ClusterDetailVm> _ClusterDetailVms = new List<ClusterDetailVm>();
        private void CboCluster_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ClusterVm = (ClusterInforVms)cboCluster.SelectedItem;
            _ClusterDetailVms = new List<ClusterDetailVm>();
            var lineSelects = _LineDefine.Where(x => x.ClusterId == ClusterVm.ClusterId);

            ////_ClusterDetailVms = 

            object datagridview = new BindingList<ClusterDetailVm>(_ClusterDetailVms);


            dgvViewCycletime.DataSource = datagridview;
        }


        private void btnCreatPlan_Click(object sender, EventArgs e)
        {
            DateTime timeNow = _ProductionPlanDAO.getTimeServer();
            string work = txtWork.Text.Trim();
            if (string.IsNullOrEmpty(work)) { MessageBox.Show("Lỗi ! Work không thể trống"); return; }
            if (txtWork.Enabled) { MessageBox.Show("Lỗi ! Work chưa được xác nhận thông tin"); txtWork.Focus(); return; }
            if (string.IsNullOrEmpty(txtQty.Text)) { MessageBox.Show("Lỗi ! Nhập số lượng kế hoạch"); txtQty.Focus(); return; }
            int qty = int.Parse(txtQty.Text.Trim());

            DateTime timestart = dtStart.Value;

            if (timestart < timeNow) { MessageBox.Show("Lỗi ! Sai định dạng thời gian bắt đầu"); return; }
            var clusterDetail = _ClusterDetailVms.Where(x => x.Selected == true).ToList();
            if (_ProductionPlanDAO.IsListEmty(clusterDetail)) { MessageBox.Show("Không tìm thấy thông tin line , chọn line "); return; }

            /// kế hoạch sản xuất cũ

            var ProductionPlanVm = _ProductionPlanDAO.GetAllProductionPlan(timestart.ToString("yyyy-MM-dd"));
            // kế hoạch sản xuất mới
            var productPlanCreat = _ProductionPlanDAO.CreatProductionPlan(work, _ModelID, qty, timestart, clusterDetail);

            foreach (var item in productPlanCreat)
            {
                int lineId = item.LineId;
                bool existPlan = ProductionPlanVm.Any(x => x.LineId == lineId && ((item.StartTime > x.StartTime && item.StartTime < x.EndTime) || (item.EndTime > x.StartTime && item.EndTime < x.EndTime) || (item.StartTime < x.StartTime && item.EndTime > x.EndTime)));

                if (existPlan)
                {
                    MessageBox.Show($"Lỗi ! Đã tồn tại kế hoạch trên line {item.LineName} Từ {item.StartTime} Đến {item.EndTime} "); return;
                }
            }

            object dataGridview = new BindingList<ProductionPlanCreat>(productPlanCreat);
            dgvViewPlan.DataSource = dataGridview;
          
         
        }




        private void labelControl1_Click(object sender, EventArgs e)
        {
            txtWork.Enabled = true;
            cycleTimes = new List<CycleTime>();
            txtQty.Clear();
        }

        private void panelEx2_Click(object sender, EventArgs e)
        {

        }

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            DateTime timeStart = dtStart.Value;
            string workId = txtWork.Text.Trim();
            int qty = int.Parse(txtQty.Text.Trim());



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime timeNow = _ProductionPlanDAO.getTimeServer();
            string work = txtWork.Text.Trim();
            if (string.IsNullOrEmpty(work)) { MessageBox.Show("Lỗi ! Work không thể trống"); return; }
            if (txtWork.Enabled) { MessageBox.Show("Lỗi ! Work chưa được xác nhận thông tin"); txtWork.Focus(); return; }
            int qty = int.Parse(txtQty.Text.Trim());
            if (string.IsNullOrEmpty(txtQty.Text)) { MessageBox.Show("Lỗi ! Nhập số lượng kế hoạch"); txtQty.Focus(); return; }

            DateTime timestart = dtStart.Value;

            if (timestart < timeNow) { MessageBox.Show("Lỗi ! Sai định dạng thời gian bắt đầu"); return; }

            var clusterDetail = _ClusterDetailVms.Where(x => x.Selected == true).ToList();
            if (_ProductionPlanDAO.IsListEmty(clusterDetail)) { MessageBox.Show("Không tìm thấy thông tin line , chọn line "); return; }

            var productPlanCreat = _ProductionPlanDAO.CreatProductionPlan(work, _ModelID, qty, timestart, clusterDetail);

            /// kế hoạch sản xuất cũ
            var ProductionPlanVm = _ProductionPlanDAO.GetAllProductionPlan(timestart.ToString("yyyy-MM-dd"));

            // kế hoạch sản xuất mới

            foreach (var item in productPlanCreat)
            {
                int lineId = item.LineId;
                bool existPlan = ProductionPlanVm.Any(x => x.LineId == lineId && ((item.StartTime > x.StartTime && item.StartTime < x.EndTime) || (item.EndTime > x.StartTime && item.EndTime < x.EndTime) || (item.StartTime < x.StartTime && item.EndTime > x.EndTime)));

                if (existPlan)
                {
                    MessageBox.Show($"Lỗi ! Đã tồn tại kế hoạch trên Line {item.LineName} Từ {item.StartTime} Đến {item.EndTime}."); return;
                }
            }
            object dataGridview = new BindingList<ProductionPlanCreat>(productPlanCreat);
            dgvViewPlan.DataSource = dataGridview;
            if (_ProductionPlanBUS.CreatPlan(productPlanCreat, _UserId))
            {
                MessageBox.Show("Pass");

            }
            else
            {
                MessageBox.Show("Fail");
            }

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtWork_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string work = txtWork.Text;
            string qty = txtQty.Text;

          
        }

    }
}
