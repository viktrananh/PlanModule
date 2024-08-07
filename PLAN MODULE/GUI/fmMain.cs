using DevComponents.DotNetBar.Controls;
using KITTING;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using PLAN_MODULE.DTO.Sales;
using PLAN_MODULE.GUI.Plan;
using PLAN_MODULE.GUI.Plan.Create_Wo;
using PLAN_MODULE.GUI.Plan.DeliveryPlan;
using PLAN_MODULE.GUI.Plan.Export;
using PLAN_MODULE.GUI.Plan.Import;
using PLAN_MODULE.GUI.Plan.Laser;
using PLAN_MODULE.GUI.Plan.Material_Control;
using PLAN_MODULE.GUI.Plan.Scheduler;
//using PLAN_MODULE.GUI.Production;
using PLAN_MODULE.GUI.Report;
using PLAN_MODULE.GUI.Sales;
using PLAN_MODULE.GUI.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseDll.DTO.FinishedProduct;

namespace PLAN_MODULE
{
    public partial class fmMain : Form
    {
        Process process = new Process();
        public delegate void SendUser(string user);
        public event SendUser senduser;
        ROSE_Dll.DataLayer DataLayer = new ROSE_Dll.DataLayer();
        int _Authority;
        public fmMain(string user, int autho)
        {
            InitializeComponent();
            userID = user;
            _Authority = autho;
        }
        const string versionSoft = "6.7";
        void checkVersion()
        {
            DataTable dt = DBConnect.getData("SELECT VERSION FROM COMPUTER_SYSTEM.SOFTWARE_DETAIL where `NAME` = 'PLAN'  AND `USE` = 1 ;");
            string version = dt.Rows[0][0].ToString();
            if (version != versionSoft)
            {
                sendedMes = true;
                MessageBox.Show($"Chương trình đã có phiên bản mới {version} ! Vui lòng tải bản cập nhật mới từ Autodownload ");
                Application.Exit();
            }
            sendedMes = false;
        }
        bool sendedMes = false;
        private void timerCheckVersion_Tick(object sender, EventArgs e)
        {
            if (!sendedMes) checkVersion();

        }
        private async void fmMain_Load(object sender, EventArgs e)
        {
            IMAGE uc = new IMAGE();
            await addControlTool(uc, "IMG", "HOME");
            checkVersion();
            timerCheckVersion.Start();
            btnUser.Text = userID;

            mnProduction.Enabled = false;
            mnPlan.Enabled = false;
            mnFunction.Enabled = true;
            if (DataLayer.checkaccountGroupCanUse(userID, "Admin"))
            {
                mnPlan.Enabled = true;
                mnProduction.Enabled = true;
                mnSale.Enabled = true;
                return;
            }
            else if (_Authority == 15)//KẾ TOÁN
            {
                mnProduction.Enabled = false;
                mnPlan.Enabled = false;
                //return;
            }
            else if (DataLayer.checkaccountGroupCanUse(userID, "PLAN"))
            {
                mnPlan.Enabled = true;
                mnProduction.Enabled = false;
                //return;
            }
            else if (DataLayer.checkaccountGroupCanUse(userID, "SALE"))
            {
                mnPlan.Enabled = false;
                mnProduction.Enabled = false;
                mnSale.Enabled = true;
                //return;
            }
            else if (DataLayer.checkaccountGroupCanUse(userID, "PRODUCTION"))
            {
                mnProduction.Enabled = true;
                mnPlan.Enabled = false;
                mnSale.Enabled = false;
                //return;
            }
            else if (DataLayer.checkaccountGroupCanUse(userID, "IQC"))
            {
                mnPlan.Enabled = false;
                mnProduction.Enabled = false;
                //return;
            }

        }

        string value = string.Empty;
        string userID, name, address;
        private void xácNhậnPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VIEW_STATUS_BILL f = new VIEW_STATUS_BILL();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }


        private async void thànhPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {


            ucListBillExportGoodsToCus uc = new ucListBillExportGoodsToCus(userID);
            await addControlTool(uc, $"BillExportGoods", $"Phiếu xuất thành phẩm");
            uc.FunctionBillRequestExportGoodToCus += Uc_FunctionBillRequestExportGoodToCus;
        }

        private async void Uc_FunctionBillRequestExportGoodToCus(int FuctionID, FPBill bill)
        {
            ucBillExportGoodsToCus uc = new ucBillExportGoodsToCus(FuctionID, bill, userID);
            if (FuctionID == LoadFunctionBillExportGoodsToCus.CREATE)
            {
                await addControlTool(uc, $"CreatBillReportGood", $"Tạo phiếu xuất");

            }
            else if (FuctionID == LoadFunctionBillExportGoodsToCus.UPDATE)
            {
                await addControlTool(uc, $"UpdateBill-{bill.BillNumber}", $"Cập nhật phiếu - {bill.BillNumber}");
            }
            uc.SelectFuncBook += Uc_SelectFuncBook;
            uc._FunctionEditBill += Uc__FunctionEditBill;
        }

        private async void Uc__FunctionEditBill(FPBill bill)
        {
            ucBillExportGoodsToCus uc = new ucBillExportGoodsToCus(1, bill, userID);
            await addControlTool(uc, $"UpdateBill-{bill.BillNumber}", $"Cập nhật phiếu - {bill.BillNumber}");
        }

        private async void Uc_SelectFuncBook(FPBill bill)
        {
            ucBookBillExportGoodsToCus uc = new ucBookBillExportGoodsToCus(bill);
            await addControlTool(uc, $"boolBill-{bill}", $"Book Data Bill - {bill}");
        }

        private async void thànhPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucTOOLS uc = new ucTOOLS(userID);
            await addControlTool(uc, $"Creatwork", $"Tạo công lệnh mới");

        }

        private async void tạoWorkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ucCreatwork uc = new ucCreatwork(userID, false);
            await addControlTool(uc, $"Creatwork", $"Tạo công lệnh mới");
            uc._SendFuction += Uc__SendFuction;
        }

        private async void Uc__SendFuction(int TypeID, Work work)
        {
            if (TypeID == LoadActionWorkOrder.CREAT_BLL_EXPORT_MATERIAL)
            {
                BillExportMaterial bill = new BillExportMaterial();
                bill.WorkId = work.WorkID;
                bill.ModelId = work.ModelWork;
                ucBillExportMaterial uc = new ucBillExportMaterial(bill, LoadFunctionBillExportMaterial.CREATE, userID);
                await addControlTool(uc, $"BillExportMaterial", $"Tạo Phiếu xuất nguyên vật liệu");
            }
            else if (TypeID == LoadActionWorkOrder.BOOK_MATERIAL)
            {
                GUI.Plan.Material_Control.Work_bokingMaterial uc = new GUI.Plan.Material_Control.Work_bokingMaterial(work.WorkID, userID);
                await addControlTool(uc, $"BookMaterial{work.WorkID}", $"Book linh kiện {work.WorkID}");
            }
            else if (TypeID == LoadActionWorkOrder.KE_HOACH_GIAO_HANG)
            {
                ucWorkDeliveryPlan uc = new ucWorkDeliveryPlan(work, userID);
                await addControlTool(uc, $"workDelivery{work.WorkID}", $"Kế hoạch giao hàng Work {work.WorkID}");

            }
            else if(TypeID == LoadActionWorkOrder.DU_LIEU_KHACH_HANG)
            {
                ucCustomerData uc = new ucCustomerData(work, userID);
                await addControlTool(uc, $"custermerData{work.WorkID}", $"Dữ liệu khách hàng-{work.WorkID}");
            }
        }

        private async void requestExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucPC_BILL_IMPORT uc = new ucPC_BILL_IMPORT(userID);
            await addControlTool(uc, $"PC_BILL_IMPORT", $"Yêu cầu nhập linh kiện vào sản xuất");
        }
      

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang hoàn thiện vui lòng quay lại sau !");
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang hoàn thiện vui lòng quay lại sau !");
        }

        private async void linhKiệnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GUI.Plan.Import.ucListBillImportMaterial uc = new GUI.Plan.Import.ucListBillImportMaterial(userID);
            await addControlTool(uc, $"ListBillImportMaterial", $"Yêu cầu nhập NVL");
            uc.FunctionBillRequestMaterial += Uc_FunctionBillRequestMaterial1;
        }
        private async void Uc_FunctionBillRequestMaterial1(int FuctionID, string bill, string wo, string statusName, string OP)
        {
            ucBillImportMaterial uc = new ucBillImportMaterial(FuctionID, bill, wo, statusName, OP, userID);
            await addControlTool(uc, $"BillImportMaterial-New", $"Tạo phiếu nhập NVL");
            uc._BackFunctionMaterialImport += Uc__BackFunctionMaterialImport;
        }

        private async void Uc__BackFunctionMaterialImport()
        {
            GUI.Plan.Import.ucListBillImportMaterial uc = new GUI.Plan.Import.ucListBillImportMaterial(userID);
            await addControlTool(uc, $"ListBillImportMaterial", $"Yêu cầu nhập NVL");
            uc.FunctionBillRequestMaterial += Uc_FunctionBillRequestMaterial1;
        }
        private async void mnSubSaleDec_Click(object sender, EventArgs e)
        {
            ucListCustomer uc = new ucListCustomer();
            await addControlTool(uc, $"ListCustomer", $"Danh sách khách hàng");
            uc.SelectedFunctionListCus += Uc_SelectedFunctionListCus;
        }

        private async void Uc_SelectedFunctionListCus(int statusID, Customer customer)
        {
            if (statusID == LoadStatusDefineCustomer.CREATE)
            {
                ucDefineCustomer uc = new ucDefineCustomer(LoadStatusDefineCustomer.CREATE, customer, userID, _Authority);
                await addControlTool(uc, $"DefineCustomer", $"Tạo mới khách hàng");
                uc.Backfunction += Uc_Backfunction;
            }
            else if (statusID == LoadStatusDefineCustomer.UPDATE)
            {
                ucDefineCustomer uc = new ucDefineCustomer(LoadStatusDefineCustomer.UPDATE, customer, userID, _Authority);
                await addControlTool(uc, $"UpdateCus-{customer.CustomerID}", $"Cập nhật khách - {customer.CustomerID}");
                uc.Backfunction += Uc_Backfunction;
            }
            else if (statusID == LoadStatusDefineCustomer.LOAD_LIST_MODEL)
            {
                ucListModel uc = new ucListModel(customer);
                await addControlTool(uc, $"ListModel-{customer.CustomerID}", $"Danh sách sản phẩm - {customer.CustomerID}");
                uc.SelectedFunctionListModel += Uc_SelectedFunctionListModel;
            }
            else if (statusID == LoadStatusDefineCustomer.CREATE_NEW_MODEL)
            {
                ucDefineModel uc = new ucDefineModel(LoadStatusDefineModel.CREATE, customer, "");
                await addControlTool(uc, "CreatModel", "Tạo mới sản phẩm");
            }
        }

        private async void Uc_SelectedFunctionListModel(int statusID, Customer customer, string ModelID)
        {

            if (statusID == LoadStatusDefineModel.CREATE)
            {
                ucDefineModel uc = new ucDefineModel(statusID, customer, "");
                await addControlTool(uc, "CreatModel", "Tạo mới sản phẩm");
            }
            else if (statusID == LoadStatusDefineModel.UPDATE)
            {
                ucDefineModel uc = new ucDefineModel(statusID, customer, ModelID);
                await addControlTool(uc, $"UpdateModel-{ModelID}", $"Cập nhật thông tin sản phẩm - {ModelID}");

            }
            else if (statusID == LoadStatusDefineModel.DELETE)
            {

            }
            else if (statusID == LoadStatusDefineModel.LOCK)
            {

            }

        }

        private async void Uc_Backfunction()
        {
            ucListCustomer uc = new ucListCustomer();
            await addControlTool(uc, "ListCustomer", "Danh sách khách hàng");
            uc.SelectedFunctionListCus += Uc_SelectedFunctionListCus;
        }


        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



        private async void btnCustomer_Click(object sender, EventArgs e)
        {
            ucListCustomer uc = new ucListCustomer();
            await addControlTool(uc, $"ListCustomer", $"Danh sách khách hàng");
            uc.SelectedFunctionListCus += Uc_SelectedFunctionListCus;
        }
        private async void btnMngPO_Click(object sender, EventArgs e)
        {
            ucListPO uc = new ucListPO();
            await addControlTool(uc, "ListPOs", "Danh sách P.O");
            uc._SelectFunctionOnPO += Uc__SelectFunctionOnPO;
        }

        private async void Uc__SelectFunctionOnPO(int action, POOrder POOrder)
        {
            ucPO uc = new ucPO(userID, action, POOrder);
            if (action == 0)
            {
                await addControlTool(uc, "CreatPO", "Tạo mơi P.O");
                uc._SelectFunctionEditPO += Uc__SelectFunctionEditPO;
            }
            else
            {
                await addControlTool(uc, $"UpdatePO{POOrder.PO}", $"Cập nhật P.O {POOrder.PO}");

            }
        }

        private async void Uc__SelectFunctionEditPO(POOrder POOrder)
        {
            ucPO uc = new ucPO(userID, 1, POOrder);
            await addControlTool(uc, $"UpdatePO{POOrder.PO}", $"Cập nhật P.O {POOrder.PO}");

        }

        private async void btnMngWork_Click(object sender, EventArgs e)
        {
            ucCreatwork uc = new ucCreatwork(userID, false);
            await addControlTool(uc, $"Creatwork", $"Tạo công lệnh mới");
            uc._SendFuction += Uc__SendFuction;
        }
        private async void btnBillMaterial_Click(object sender, EventArgs e)
        {
            GUI.Plan.Import.ucListBillImportMaterial uc = new GUI.Plan.Import.ucListBillImportMaterial(userID);
            await addControlTool(uc, $"ListBillImportMaterial", $"Yêu cầu nhập NVL");
            uc.FunctionBillRequestMaterial += Uc_FunctionBillRequestMaterial1;
        }
        private async void btnBillCCDC_Click(object sender, EventArgs e)
        {
            ucTOOLS uc = new ucTOOLS(userID);
            await addControlTool(uc, $"Creatwork", $"Tạo công lệnh mới");
        }
        private async void btnCheckMaterial_Click(object sender, EventArgs e)
        {
            PLAN_MODULE.GUI.Plan.Material_Control.MaterialFllowing materialFllowing = new GUI.Plan.Material_Control.MaterialFllowing();
            await addControlTool(materialFllowing, "Theo dõi liệu về", "Theo dõi liệu về");
        }
        private async void btnDelivery_Click(object sender, EventArgs e)
        {
            ucDeliveryPlan uc = new ucDeliveryPlan();
            await addControlTool(uc, "Deliveryplan", "Kế hoạch giao hàng");
        }
        private async void btnLaserPlan_Click(object sender, EventArgs e)
        {
            ucLaserPlan uc = new ucLaserPlan(userID);
            await addControlTool(uc, "laserMark", "Kế hoạch khắc laser");
        }

        private async void btnBillGood_Click(object sender, EventArgs e)
        {
            ucListBillExportGoodsToCus uc = new ucListBillExportGoodsToCus(userID);
            await addControlTool(uc, $"BillExportGoods", $"Phiếu xuất thành phẩm");
            uc.FunctionBillRequestExportGoodToCus += Uc_FunctionBillRequestExportGoodToCus;
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mnFunction.Enabled = false;
            mnProduction.Enabled = true;
            mnPlan.Enabled = true;
            this.Close();
        }

        private async void btnProdBillImportMaterial_Click(object sender, EventArgs e)
        {
            ucPC_BILL_IMPORT uc = new ucPC_BILL_IMPORT();
            await addControlTool(uc, $"BillimportMaterial", $"Phiếu nhập linh kiện");

        }

        private async void btnProdBillExportMaterial_Click(object sender, EventArgs e)
        {
            ucListBillExportMaterial uc = new ucListBillExportMaterial(userID);
            addControlTool(uc, $"lsBillExportMaterial", $"Dánh sách phiếu xuất linh kiện");
            uc.FunctionBillExportMaterial += Uc_FunctionBillExportMaterial;

        }



        private void btnProductionPlan_Click(object sender, EventArgs e)
        {
            fmProductionPlan fm = new fmProductionPlan(userID);
            fm.ShowDialog();
        }

        private void toolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolSupport toolSupport = new ToolSupport(userID);
            toolSupport.ShowDialog();
        }

        private async void cậpNhậtUPHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucUPH ucUPH = new ucUPH(userID);
            await addControlTool(ucUPH, $"UPH", $"UPH");
        }

        private async void linhKiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucListBillExportMaterial uc = new ucListBillExportMaterial(userID);
            addControlTool(uc, $"lsBillExportMaterial", $"Dánh sách phiếu xuất linh kiện");
            uc.FunctionBillExportMaterial += Uc_FunctionBillExportMaterial;
        }

        private void Uc_FunctionBillExportMaterial(int FuctionID, int typeBill, BillExportMaterial bill_Export)
        {
          
            if (typeBill != LoadSubTypeBillExportMaterial.ARISE)
            {
                ucBillExportMaterial uc = new ucBillExportMaterial(bill_Export, FuctionID, userID);

                if (FuctionID == 0)
                {
                    addControlTool(uc, $"billExportPlan", $"Phiếu xuất kế hoạch");

                }
                else
                {
                    addControlTool(uc, $"billExportPlan-{bill_Export.BillId}", $"Phiếu xuất kế hoạch - {bill_Export.BillId}");

                }
                uc.FuncGoToView += Uc_FuncGoToView;
            }
            else
            {
                ucBillExportMaterialArises uc = new ucBillExportMaterialArises(userID, FuctionID, bill_Export);
                if(FuctionID == 0)
                {
                    addControlTool(uc, $"billExportArises", $"Phiếu xuất phát sinh");
                    uc.FuncGoToViewFuncGoToView += Uc_FuncGoToViewFuncGoToView;
                }
                else
                {
                    addControlTool(uc, $"billExportArises-{bill_Export.BillId}", $"Phiếu xuất phát sinh - {bill_Export.BillId}");

                }

            }
        }

        private void Uc_FuncGoToViewFuncGoToView(string bill)
        {
            var billExportMaterial = new BillExportMaterialDAO().GetBillExportMaterialByBillNumber(bill);
            ucBillExportMaterialArises uc = new ucBillExportMaterialArises(userID, 1, billExportMaterial);
            addControlTool(uc, $"billExportArises-{billExportMaterial.BillId}", $"Phiếu xuất phát sinh - {billExportMaterial.BillId}");

        }

        private async void Uc_FuncGoToView(string bill)
        {
            var billExportMaterial = new BillExportMaterialDAO().GetBillExportMaterialByBillNumber(bill);
            ucBillExportMaterial uc = new ucBillExportMaterial(billExportMaterial, 1, userID);

           await addControlTool(uc, $"billExportPlan-{billExportMaterial.BillId}", $"Phiếu xuất kế hoạch - {billExportMaterial.BillId}");

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            mnFunction.Enabled = false;
            mnProduction.Enabled = true;
            mnPlan.Enabled = true;
            this.Close();
        }



        public async Task<bool> addControlTool(UserControl uc, string tabpageName, string tabpageText)
        {
            Func<object, bool> myfunc = (object any) =>
            {
                tabFormControlMain.Invoke(new MethodInvoker(delegate
                {
                    bool TabpageExisted = false;
                    foreach (TabFormItem tp in tabFormControlMain.Items)
                    {
                        if (tp.Name == tabpageName)
                        {
                            TabpageExisted = true;
                            tabFormControlMain.SelectedTab = tp;
                            break;
                        }
                    }
                    if (!TabpageExisted)
                    {
                        TabFormItem tabpage = tabFormControlMain.CreateTab(tabpageText, tabpageName);
                        uc.Dock = DockStyle.Fill;
                        tabpage.Panel.Controls.Add(uc);
                        tabFormControlMain.SelectedTab = tabpage;
                    }
                }));
                return true;

            };
            Task<bool> task1 = new Task<bool>(myfunc, "");
            task1.Start();
            await task1;
            return task1.Result;
        }

    }
}
