using DevComponents.DotNetBar.Controls;
//using PLAN_MODULE.GUI.Production;
using PLAN_MODULE.GUI.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Tools
{
    public partial class ToolSupport : Form
    {
        private readonly string _UserID;
        public ToolSupport(string user)
        {
            InitializeComponent();
            _UserID = user;
        }

        public ToolSupport()
        {
            InitializeComponent();
            //_UserID = user;
        }
        void addUserControl(UserControl control, Panel pnl)
        {
            if (!pnl.Controls.Contains(control))
            {
                pnl.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.BringToFront();
            }
            else
            {
                pnl.Controls.Clear();
                addUserControl(control, pnl);
            }
        }

        private async void ToolSupport_Load(object sender, EventArgs e)
        {
     
        }

        private async void btnCreatTemp_Click(object sender, EventArgs e)
        {
            ucCreatTemp uc = new ucCreatTemp(_UserID);
            await addControlTool(uc, "Create Tem", "Tạo tem 6*6");
        }

        private async void btnPOT_Click(object sender, EventArgs e)
        {
        
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private async void btnCycleTime_Click(object sender, EventArgs e)
        {

        }
        public async Task<bool> addControlTool(UserControl uc, string tabpageName, string tabpageText)
        {
            Func<object, bool> myfunc = (object any) =>
            {
                tabFormControlTool.Invoke(new MethodInvoker(delegate 
                {
                    sideNav1.SelectedItem = btnCreatTemp;
                    bool TabpageExisted = false;
                    foreach (TabFormItem tp in tabFormControlTool.Items)
                    {
                        if (tp.Name == tabpageName)
                        {
                            TabpageExisted = true;
                            tabFormControlTool.SelectedTab = tp;
                            break;
                        }
                    }
                    if (!TabpageExisted)
                    {
                        TabFormItem tabpage = tabFormControlTool.CreateTab(tabpageText, tabpageName);
                        uc.Dock = DockStyle.Fill;
                        tabpage.Panel.Controls.Add(uc);
                        tabFormControlTool.SelectedTab = tabpage;
                    }
                }));
                return true;

            };
            Task<bool> task1 = new Task<bool>(myfunc, "");
            task1.Start();
            await task1;
            return task1.Result;
        }

        private async void btnCheckSharedComponent_Click(object sender, EventArgs e)
        {
            ucCheckSharedComponent uc = new ucCheckSharedComponent();
            await addControlTool(uc, "CheckComponent", "Thống kê mã dùng chung");

        }

        private async void sideNavItem4_Click(object sender, EventArgs e)
        {
            ucShipmentPlanUI uc = new ucShipmentPlanUI();
            await addControlTool(uc, "ShipmentPlan", "Kế hoạch xuất hàng");

        }

        private async void sideNavItem3_Click(object sender, EventArgs e)
        {
            //ucPC_Report_Material uc = new ucPC_Report_Material();
            //await addControlTool(uc, "reportMaterial", "Báo cáo nhập xuất linh kiện");

        }

        private async void btnbillExport_Click(object sender, EventArgs e)
        {
            ucListBilExport uc = new ucListBilExport();
            await addControlTool(uc, "lsRPBillExport", "Báo cáo - Danh sách phiếu xuất kho");

        }

        private async void btnRpLaser_Click(object sender, EventArgs e)
        {
            ucRpLaser uc = new ucRpLaser();
            await addControlTool(uc, "laserMarked", "Báo cáo - Khắc tem");

        }
    }
}
