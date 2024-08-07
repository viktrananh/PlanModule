using DevExpress.XtraBars;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using ROSE_Dll;
//using PLAN_MODULE.DTO.Planed.BillImport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Plan.Export
{
    public partial class ucListBillExportMaterial : UserControl
    {
        public delegate void SelectFunction(int FuctionID, int typeBill, BillExportMaterial bill_Export);
        public event SelectFunction FunctionBillExportMaterial;
        ROSE_Dll.DataLayer dataLayer = new ROSE_Dll.DataLayer();

        private readonly string _UserID = string.Empty;
        BillExportMaterialDAO _BillExxportMaterialDAO = new BillExportMaterialDAO();
        public ucListBillExportMaterial(string userid)
        {
            InitializeComponent();

            _UserID = userid;
            this.Load += UcListBillExportMaterial_Load;

        }

        private void UcListBillExportMaterial_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                InitializeListBill();

            });
            th.Start();
            th.IsBackground = true;
        }

        List<BillExportMaterial> _bill_Export_Materials = new List<BillExportMaterial>();
        void InitializeListBill()
        {
            object dataSource = null;
            string dataMember = null;

            _bill_Export_Materials = _BillExxportMaterialDAO.GetListSummaryBillExportRequestPC(NotDetail: true);
            dataSource = _bill_Export_Materials;
            lbCountBill.Text = _bill_Export_Materials.Count().ToString();

            this.Invoke((MethodInvoker)delegate
            {
                dgvLsBill.DataSource = dataSource;
                Binding(dataSource);
            });

        }
        bool _IsBingding = false;
        void Binding(object context)
        {
            if (context != null)
            {

                lbStatusID.DataBindings.Clear();
                lbStatusName.DataBindings.Clear();
                lbWorkID.DataBindings.Clear();
                lbOP.DataBindings.Clear();


                lbStatusID.DataBindings.Add(new Binding("Text", context, "BillStatus"));
                lbStatusName.DataBindings.Add(new Binding("Text", context, "BillStatusName"));
                lbOP.DataBindings.Add(new Binding("Text", context, "OP"));
                lbWorkID.DataBindings.Add(new Binding("Text", context, "WorkId"));



            }
            else
            {
                lbStatusID.Text = "";
                lbStatusName.Text = "";
                lbOP.Text = "";
                lbWorkID.Text = "";

            }
        }
        private void btncreate_Click(object sender, EventArgs e)
        {
            if (FunctionBillExportMaterial != null)
            {
                if (!dataLayer.checkaccountGroupCanUse(_UserID, "PLAN"))
                {
                    MessageBox.Show("Bạn không có quyền tạo phiếu xuât linh kiện theo kế hoạch");
                    return;
                }
                FunctionBillExportMaterial(LoadFunctionBillExportMaterial.CREATE, BillExportMaterial.XuatKeHoach, null);
            }
        }
        private void btnCreatBillArises_Click(object sender, EventArgs e)
        {
            if (FunctionBillExportMaterial != null)
            {
                if (!dataLayer.checkaccountGroupCanUse(_UserID, "PRODUCTION"))
                {
                    MessageBox.Show("Bạn không có quyền tạo phiếu xuất" +
                        " linh kiện phát sinh");
                    return;
                }
                FunctionBillExportMaterial(LoadFunctionBillExportMaterial.CREATE, BillExportMaterial.XuatPhatSinh, null);
            }
        }
        private void dgvLsBill_DoubleClick(object sender, EventArgs e)
        {
            if (FunctionBillExportMaterial != null)
            {
                //FunctionBillRequestMaterial(LoadFunctionBillImportMaterial.UPDATE, lbBill.Text.Trim(), lbWorkID.Text.Trim(), lbStatusName.Text.Trim(), lbOP.Text.Trim());

            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                popMenu.ShowPopup(dgvLsBill.PointToScreen(new Point(e.Point.X, e.Point.Y)));
            }
        }
        private void btnViewBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bill = gridView1.GetRow(gridView1.FocusedRowHandle) as BillExportMaterial;
            var billEx = _BillExxportMaterialDAO.GetBillExportMaterialByBillNumber(bill.BillId, true);
            if (bill.BillType != LoadSubTypeBillExportMaterial.ARISE)
            {
                FunctionBillExportMaterial(LoadFunctionBillExportMaterial.VIEW, bill.BillType, billEx);
            }
            else
            {
                FunctionBillExportMaterial(LoadFunctionBillExportMaterial.VIEW, bill.BillType, billEx);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitializeListBill();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            var bill = gridView1.GetRow(gridView1.FocusedRowHandle) as BillExportMaterial;
            var billEx = _BillExxportMaterialDAO.GetBillExportMaterialByBillNumber(bill.BillId, true);

            if (bill.BillType != LoadSubTypeBillExportMaterial.ARISE )
            {


                MessageBox.Show("Không thể sửa phiếu kế hoạch");
                return;
            }
            else
            {
                if (!dataLayer.checkaccountGroupCanUse(_UserID, "PRODUCTION"))
                {
                    MessageBox.Show("Bạn không có quyền sửa phiếu xuất linh kiện phát sinh");
                    return;
                }
                FunctionBillExportMaterial(LoadFunctionBillExportMaterial.UPDATE, BillExportMaterial.XuatPhatSinh, billEx);

            }

        }
    }
}
