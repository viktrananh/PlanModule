using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LinqToExcel;
using System.IO;
using System.Drawing.Printing;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using PLAN_MODULE.DAO.Product;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DAO;
using DevComponents.Instrumentation;
using DocumentFormat.OpenXml.Spreadsheet;
using ROSE_Dll.DTO;
using Spire.Pdf.Exporting.XPS.Schema;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.GUI.Account;
using PLAN_MODULE.GUI;
using PLAN_MODULE.DTO.Planed;
using DevExpress.XtraGrid.Views.BandedGrid;
using WarehouseDll.DTO.Material.Import;
namespace PLAN_MODULE
{
    public partial class ucBillExportMaterialArises : UserControl
    {

        public delegate void FuncGoToViewArisesForm(string bill);
        public event FuncGoToViewArisesForm FuncGoToViewFuncGoToView;

        BillExportMaterialDAO _BillExportMaterialDAO = new BillExportMaterialDAO();
        BillExportMateriaBUS _BillExportMateriaBUS = new BillExportMateriaBUS();
        WorkOrder WorkOrder = new WorkOrder();
        DAO_WorkInfor DAO_Work = new DAO_WorkInfor();
        public string _UserId;
        ROSE_Dll.DataLayer dataLayer = new ROSE_Dll.DataLayer();
        ROSE_Dll.DAO.AccountDAO accountDAO = new ROSE_Dll.DAO.AccountDAO();

        BillExportMaterial _BillExport = new BillExportMaterial();
        readonly int _FunctionId;
        public ucBillExportMaterialArises(string userId, int functionId, BillExportMaterial billexport)
        {

            InitializeComponent();
            _BillExport = billexport;
            _FunctionId = functionId;
            _UserId = userId;
            this.Load += UcPC_BILL_EXPORT_Load;
        }

        private void UcPC_BILL_EXPORT_Load(object sender, EventArgs e)
        {
            RefreshForm();

        }


        void RefreshForm()
        {
            txtWork.Clear();
            txtOP.Text = _UserId;
            if (_FunctionId == LoadFunctionBillExportMaterial.CREATE)
            {
                lbTitle.Text = $"&Tạo phiếu xuất linh kiện phát sinh";
                txtWork.Enabled = true;
                btnSave.Enabled = true;
                btnApp.Enabled = false;
                btnCancel.Enabled = false;
                _BillExportMaterialPlanDetail = new List<BillExportMaterialDetail>();
                _BillExportMaterialAriseDetail = new List<BillExportMaterialDetail>();
                _BillExportMaterialDetailSample = new List<BillExportMaterialDetail>();
                _BillExport = new BillExportMaterial();
                _BillExport.BillExportMaterialDetails = new List<BillExportMaterialDetail>();

                txtModelId.Clear();
                txtBomVersion.Clear();
                txtWork.Clear();
                txtTotal.Clear();

            }
            else if (_FunctionId == LoadFunctionBillExportMaterial.VIEW)
            {
                lbTitle.Text = $"&Xem phiếu xuất linh kiện phát sinh - {_BillExport.BillId}";
                txtWork.Enabled = false;
                btnSave.Enabled = false;
                btnApp.Enabled = true;
                btnCancel.Enabled = false;
                txtMain.Enabled = true;
                txtQty.Enabled = true;
            }
            else
            {
                lbTitle.Text = $"&Cập nhật phiếu xuất linh kiện phát sinh - {_BillExport.BillId}";
                txtWork.Enabled = false;
                btnSave.Enabled = true;
                btnApp.Enabled = false;
                btnCancel.Enabled = true;
                txtMain.Enabled = true;
                txtQty.Enabled = true;
            }
            switch (_FunctionId)
            {
                case LoadFunctionBillExportMaterial.VIEW:
                case LoadFunctionBillExportMaterial.UPDATE:
                    txtWork.Text = _BillExport.WorkId;
                    txtModelId.Text = _BillExport.ModelId;
                    txtBomVersion.Text = _BillExport.BomVersion;
                    txtBillNumber.Text = _BillExport.BillId;
                    dgvRequest.DataSource = _BillExport.BillExportMaterialDetails;
                    LoadInforWork(_BillExport.WorkId, true);
                    break;
                default:
                    break;
            }
        }

        void Binding(GridBand band, object ob)
        {
            BandedGridColumn col = new BandedGridColumn();
            
            band.Columns.Add(new BandedGridColumn());
        }


        private void btnComfirm_Click(object sender, EventArgs e)
        {
            if (_BillExport.BillExportMaterialDetails.Count() < 1) return;

            if (_FunctionId == LoadFunctionBillExportMaterial.CREATE)
            {
                _BillExport.BillType = WorkOrder.ModelID.Contains("-1") ? BillExportMaterial.phiêuSMT : BillExportMaterial.phiêuFAT;
                _BillExport.ExportType = BillExportMaterial.XuatPhatSinh;
                string process = WorkOrder.ModelID.Contains("-1") ? "SMT" : "PTH";

                _BillExport.BillId = _BillExportMaterialDAO.CreateBillNameNew(process, WorkOrder.WorkID, _BillExport.BillType);
                _BillExport.WorkId = WorkOrder.WorkID;
                _BillExport.Pcbs = 0;
                _BillExport.BomVersion = WorkOrder.bomVersion;
                string NOTE = txtNote.Text.Trim();
                if (!_BillExportMateriaBUS.CreateBill(_BillExport, _UserId, NOTE , process))
                {
                    MessageBox.Show($"Tạo Phiếu lỗi!");
                   

                }
                else
                {
                    MessageBox.Show($"Tạo Phiếu {_BillExport.BillId} thành công");

                    if (FuncGoToViewFuncGoToView != null)
                    {
                        FuncGoToViewFuncGoToView(_BillExport.BillId);

                    }
                }
            }
            else if (_FunctionId == LoadFunctionBillExportMaterial.UPDATE)
            {
                if (_BillExport.BillStatus != 0)
                {
                    MessageBox.Show($"Phiếu đã phê duyệt hoặc đã xuất không thể cập nhật thông tin phiếu!");
                    return;
                }
                if (!_BillExportMaterialDAO.IsBillExport(_BillExport.BillId))
                {
                    MessageBox.Show($"Phiếu đã xuất không thể cập nhật thông tin phiếu!");
                    return;
                }
                if (!_BillExportMateriaBUS.UpdateBill(_BillExport, _UserId))
                {
                    MessageBox.Show($"Cập nhật Phiếu lỗi!");

                }
                else
                {
                    MessageBox.Show($"Cập nhật Phiếu {_BillExport.BillId} thành công");

                    _BillExport = _BillExportMaterialDAO.GetBillExportMaterialByBillNumber(_BillExport.BillId);
                }
            }

            btnRefesh_Click(sender, e);

        }



        int statusExport = -2;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!_BillExportMaterialDAO.IsBillExport(_BillExport.BillId))
            {
                MessageBox.Show($"Phiếu đã xuất không thể hủy!");
                return;
            }
            if (_BillExportMateriaBUS.CancelBill(_BillExport.BillId))
            {
                MessageBox.Show($"Hủy phiếu thành công!");
            }
            else
            {
                MessageBox.Show($"Lỗi!");
            }
        }
        bool isGroupModel = false;
        int NumberModelGroup;
        List<BillExportMaterialDetail> _BillExportMaterialDetailSample = new List<BillExportMaterialDetail>();

        private void txtWork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string workId = txtWork.Text;
            LoadInforWork(workId, false);
        }

        void LoadInforWork(string workId, bool GetRemain)
        {
            WorkOrder = new WorkOrder();
            _BOMContent = new List<BomContent>();

            if (string.IsNullOrEmpty(workId))
            {
                MessageBox.Show("Chưa nhập thông tin work");
                return;
            }
            WorkOrder = DAO_Work.getWorkInfor(txtWork.Text);
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();

            if (string.IsNullOrEmpty(WorkOrder.WorkID))
            {
                MessageBox.Show("Sai work");
                return;
            }
            isGroupModel = _BillExportMaterialDAO.IsGroupModel(WorkOrder.ModelID, out NumberModelGroup);

            if (WorkOrder.state == WorkOrder.stateClose)
            {
                MessageBox.Show("Work đã đóng");
                return;
            }
            if (string.IsNullOrEmpty(WorkOrder.bomVersion))
            {
                MessageBox.Show($"Không tìm thấy bom cho Work {WorkOrder.WorkID} ! Liên hệ BP KH-CU");
                return;
            }
            if (WorkOrder.isRMA!=1&& !_BillExportMaterialDAO.IsWorkRequestByPlanFull(WorkOrder.WorkID))
            {
                MessageBox.Show($"Work {WorkOrder.WorkID} chưa tạo phiếu yêu cầu xuất theo kế hoạch , Liên hệ BP KH-CU");
                return;
            }
            txtMain.Enabled = true;
            txtQty.Enabled = true;
            txtModelId.Text = WorkOrder.ModelID;
            txtBomVersion.Text = WorkOrder.bomVersion;
            txtTotal.Text = WorkOrder.totalPcs.ToString();

            _BillExportMaterialDetailSample = LoadBillWorkByBOM(WorkOrder);
            var rp =  LoadInforExportMaterialByWork(WorkOrder.WorkID, GetRemain);

            dgvReport.DataSource = rp;
            string[] mainArr = _BillExportMaterialDetailSample.Select(x => x.MainPart).ToArray();
            AutoCompleteStringCollection autoCompleteList = new AutoCompleteStringCollection();
            autoCompleteList.AddRange(mainArr);
            txtMain.AutoCompleteCustomSource = autoCompleteList;
            txtWork.Enabled = false;
            txtMain.Focus();
        }

        List<ROSE_Dll.DTO.BomContent> _BOMContent = new List<ROSE_Dll.DTO.BomContent>();
        List<string> lsMain = new List<string>();
        List<string> lsUsing = new List<string>();


        List<BillExportMaterialDetail> _BillExportMaterialPlanDetail = new List<BillExportMaterialDetail>();
        List<BillExportMaterialDetail> _BillExportMaterialAriseDetail = new List<BillExportMaterialDetail>();

        List<BillExportMaterialDetail> LoadInforExportMaterialByWork(string work, bool GetRemain)
        {

            var billReport = new List<BillExportMaterialDetail>();
           



            _BillExportMaterialPlanDetail = new List<BillExportMaterialDetail>();
            _BillExportMaterialAriseDetail = new List<BillExportMaterialDetail>();
            var ls = _BillExportMaterialDAO.GetListSummaryBillExportRequestPC(work, false , GetRemain);


            var mainOrder = ls.SelectMany(x => x.BillExportMaterialDetails).ToList();



            foreach (var item in ls)
            {

                string bill = item.BillId;
                int type = item.BillType; // loại phiếu
                foreach (var b in item.BillExportMaterialDetails)
                {
                    string main = b.MainPart;
                    string sub = b.PartNumber;
                    float qtyRequest = b.Qty;
                    BillExportMaterialDetail billExportMaterialDetail = new BillExportMaterialDetail()
                    {
                        BillNumber = b.BillNumber,
                        MainPart = main,
                        PartNumber = sub,
                        Qty = qtyRequest,
                        RealExport = b.RealExport,
                        Comment = item.BillTypeName,
                    };
                    billReport.Add(billExportMaterialDetail);
                    if(type != LoadSubTypeBillExportMaterial.ARISE)
                    {
                        _BillExportMaterialPlanDetail.Add(billExportMaterialDetail);
                    }
                    else
                    {
                        _BillExportMaterialAriseDetail.Add(billExportMaterialDetail);
                    }
                }
            }
            return billReport;
        }
        List<BillExportMaterialDetail> LoadBillWorkByBOM(WorkOrder workOrder)
        {
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            if (string.IsNullOrEmpty(workOrder.WorkID)) return _BillExportMaterialDetail;

            lsMain = new List<string>();
            lsUsing = new List<string>();



            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = workOrder.ModelID, BomVersion = workOrder.bomVersion });

            if (_BillExportMaterialDAO.IsListEmty(_BOMContent))
            {
                MessageBox.Show("Không có định mức trong Bom, Vui lòng tạo phiếu yêu cầu xuất theo Phát sinh !");
                return _BillExportMaterialDetail;

            }

            var queryRemain = (from r in _BOMContent
                               select new
                               {
                                   MainPart = r.MainPart,
                                   InterPart = r.InterPart,
                                   Quantity = r.Quantity,
                                   //Remain = _BillExportMaterialDAO.GetRemainPanacim(r.InterPart),
                                   Location = r.Location,
                               }).ToList();
            var rs = (from r in queryRemain
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Quantity = string.Join(" | ", gr.Select(ra => ra.Quantity)),
                          //Remain = string.Join(" | ", gr.Select(ra => ra.Remain)),
                      }).ToList();


            foreach (var g in rs)
            {
                if (g.InterPart.Contains(workOrder.ModelID)) continue;
                float request = 0;
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = g.MainPart, PartNumber = g.InterPart, Qty = request });
            }
            return _BillExportMaterialDetail;
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void panelEx5_Click(object sender, EventArgs e)
        {

        }
        private void txtMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string main = txtMain.Text;
            if (string.IsNullOrEmpty(main)) return;
            if (txtWork.Enabled)
            {
                MessageBox.Show("Vui lòng xác nhận work. Press Enter in the Work box ");
                txtWork.Focus();
                return;
            }
            txtQty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string main = txtMain.Text;
            string qty = txtQty.Text;
            string work = txtWork.Text;
            if (string.IsNullOrEmpty(qty)) return;

            var checkMain = _BillExportMaterialDetailSample.Any(x => x.MainPart == main);

            float qtyNumber = float.Parse(qty);
            if (!checkMain)
            {
                MessageBox.Show("Mã Main không tồn tại");
                txtMain.Focus();
                txtQty.Clear();
                return;
            }

            var ls = _BillExportMaterialDAO.GetListSummaryBillExportRequestPC(work, false);

            var lsKg = ls.Where(x => x.BillType != LoadSubTypeBillExportMaterial.ARISE);

            var dateCheckMainPart = new DateTime(2024, 06, 21);
            bool checkMainPartExported = !_BillExportMaterialDAO.IsListEmty(ls) ? ls.Any(x => x.CreateTime.Date < dateCheckMainPart) : false;
            if (WorkOrder.isRMA != 1 && !_BillExportMaterialDAO.IsListEmty(_BillExportMaterialPlanDetail))
            {
                var checkQty = _BillExportMaterialPlanDetail.Any(x => x.MainPart == main && (x.Qty > x.RealExport || x.Qty == 0));
                if (checkQty && !checkMainPartExported)
                {
                    MessageBox.Show($"Mã Main {main} chưa được xuất đủ điều kiện ở phiếu xuất kế hoạch , xuất đủ ở phiếu kế hoạch trước khi tạo phiếu phát sinh ");
                    txtMain.Focus();
                    txtQty.Clear();
                    return;
                }

                var checkQtyAriese = _BillExportMaterialAriseDetail.Any(x => x.MainPart == main && (x.Qty > x.RealExport));
                if (checkQtyAriese)
                {
                    MessageBox.Show($"Mã Main {main} chưa được xuất đủ hết ở phiếu phát sinh đã tạo trước đó ");
                    txtMain.Focus();
                    txtQty.Clear();
                    return;
                }
            }
            if (_FunctionId == LoadFunctionBillExportMaterial.UPDATE)
            {
                if (!string.IsNullOrEmpty(_BillExport.BomVersion))
                {
                    if (_BillExport.BomVersion != WorkOrder.bomVersion)
                    {
                        MessageBox.Show($"Version BOM tạo phiếu ({_BillExport.BomVersion}) khác Version BOM hiện tại ({WorkOrder.bomVersion})  của Work");
                        txtMain.Focus();
                        txtQty.Clear();
                        return;
                    }
                }
            }
            var sub = _BillExportMaterialDetailSample.FirstOrDefault(x => x.MainPart == main).PartNumber;
            BillExportMaterialDetail bill = new BillExportMaterialDetail()
            {
                MainPart = main,
                PartNumber = sub,
                Qty = qtyNumber,
            };
            bool chekMainExist = false;
            if (!_BillExportMaterialDAO.IsListEmty(_BillExport.BillExportMaterialDetails))
            {
                chekMainExist = _BillExport.BillExportMaterialDetails.Any(x => x.MainPart == main);
            }

            if (chekMainExist)
            {
                _BillExport.BillExportMaterialDetails.FirstOrDefault(x => x.MainPart == main).Qty = qtyNumber;
            }
            else
            {
                _BillExport.BillExportMaterialDetails.Add(bill);

            }

            dgvRequest.DataSource = null;

            dgvRequest.DataSource = _BillExport.BillExportMaterialDetails;

            txtMain.Clear();
            txtQty.Clear();
            txtMain.Focus();

        }




        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void btnApp_Click(object sender, EventArgs e)
        {

            if (_BillExport.BillStatus != LoadFunctionBillExportMaterial.CREATE)
            {
                MessageBox.Show("Bạn không thể duyệt phiếu này");
                return;
            }
            DialogResult dlg = MessageBox.Show($"Bạn có chắc chắn muốn duyệt phiếu {_BillExport.BillId}?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dlg != DialogResult.Yes) return;
            fmConfirm fm = new fmConfirm();
            fm.ShowDialog();

            string userIDapp = fm._UserId;


            if (!dataLayer.checkaccountGroupCanUse(userIDapp, "KITTING_APPROVE_EXPORT"))
            {
                MessageBox.Show("Tài khoản không được cấp quyền duyệt phiếu yêu cầu phát sinh ");
                return;
            }
            if (_BillExportMateriaBUS.ApproveBill(_BillExport.BillId, userIDapp))
            {

                MessageBox.Show($"Phê duyệt phiếu yêu cầu {_BillExport.BillId} thành công.");


            }
            else
            {
                MessageBox.Show($"Lỗi, kiểm tra lại.");
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }


    }
}
