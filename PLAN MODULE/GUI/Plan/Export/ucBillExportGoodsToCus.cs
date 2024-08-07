using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Printing;
using PLAN_MODULE.DAO.PLAN;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using Microsoft.Reporting.WinForms;
using PLAN_MODULE.GUI;
using PLAN_MODULE.DTO.Planed.BillExport;
using System.ComponentModel;
using DocumentFormat.OpenXml.Drawing.Charts;
using WarehouseDll.DTO.FinishedProduct;
using WarehouseDll.DAO.FinishedProduct;

namespace PLAN_MODULE
{
    public partial class ucBillExportGoodsToCus : UserControl
    {

        public delegate void SelectFunctionBook(FPBill bill);
        public event SelectFunctionBook SelectFuncBook;

        public delegate void FunctionEditBill(FPBill bill);
        public event FunctionEditBill _FunctionEditBill;
        FPBillExportDAO _FBBillExportDAO = new FPBillExportDAO();

        BillExportGoodsDAO billExportGoodsDAO = new BillExportGoodsDAO();
        BillExportGoodsBUS billExportGoodsBUS = new BillExportGoodsBUS();


        readonly string _UserID = string.Empty;
        int _FunctionID = -1;

        FPBill _Bill = new FPBill();



        string model = "";
        string cus_model = "";
        string cusCode = "";
        string cusID = string.Empty;


        public ucBillExportGoodsToCus(int functionID, FPBill billl, string userID)
        {
            InitializeComponent();
            this._FunctionID = functionID;
            this._Bill = billl;
            this._UserID = userID;
            this.Load += UcBillExportGoodsToCus_Load;
        }
        private void UcBillExportGoodsToCus_Load(object sender, EventArgs e)
        {
            InitializeBill();
        }

        void InitializeBill()
        {
            cbbShipping.SelectedIndex = 0;


            switch (_FunctionID)
            {
                case LoadFunctionBillExportGoodsToCus.CREATE:
                    LoadUICreate();
                    break;
                case LoadFunctionBillExportGoodsToCus.UPDATE:
                    LoadUIUpdate(_Bill);
                    break;
            }
        }

        void LoadUICreate()
        {
            btnCancel.Enabled = false;
            btnPrint.Enabled = false;
            txtWorkPlan.ReadOnly = false;
            txtRequests.ReadOnly = false;
            txtBillNumber.Clear();
            txtWorkPlan.Clear();
            txtCusCode.Clear();
            txtCusModel.Clear();
            txtRequests.Clear();
            txtPO.Clear();
            txtNote.Clear();
            txtWorkPlan.Focus();
            txtModel.Clear();
            _Bill = new FPBill();
            _Bill.FPBillDetailS = new List<FPBillDetail>();
            var dataGridView1 = new BindingList<FPBillDetail>(_Bill.FPBillDetailS);
            dgvDetailBill.DataSource = dataGridView1;
            model = cus_model = cusCode = cusID = string.Empty;
        }
        void LoadUIUpdate(FPBill Bill)
        {
            btnCancel.Enabled = true;
            btnPrint.Enabled = true;
            txtBillNumber.Text = lbBill.Text = Bill.BillNumber;
            //_BillRequest = BillRequestExportGoodToCusControl.LoadDetailBill(Bill.BillNumber);
            var dataGridView1 = new BindingList<FPBillDetail>(Bill.FPBillDetailS);
            dgvDetailBill.DataSource = dataGridView1;

        }

        string modelname = string.Empty;
        //int pcbOnPanel = 0;
        int totalWork = 0;
        int exported = 0;
        string cusCheck = string.Empty;
        string statusWo = string.Empty;
        string _PO = string.Empty;
        private void txtWorkPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string work = txtWorkPlan.Text;
            if (work == string.Empty) return;
            if (!billExportGoodsDAO.checkWork(work, out model, out cusID, out cus_model, out cusCode, out produtionPlan, out statusWo, out totalWork, out _PO, out exported))
            {
                MessageBox.Show($"Không tồn tại Work {work} hoặc chưa có lưu trình !");
                return;
            }
            if (statusWo == "CLOSE")
            {
                MessageBox.Show("Lỗi !Work đã đóng !");
                return;

            }
            string mes = "";
            if (!billExportGoodsDAO.checkEcnRelate(work, out mes))
            {
                MessageBox.Show(mes);
                return;
            }

            if (billExportGoodsDAO.IsListEmty(_Bill.FPBillDetailS))
            {
                cusCheck = cusID;
            }
            else
            {
                if (cusCheck != cusID)
                {
                    txtWorkPlan.Clear();
                    txtModel.Clear();
                    txtCusModel.Clear();
                    txtCusCode.Clear();
                    MessageBox.Show("Phiếu chỉ chứa thông tin một khách hàng !");
                    return;
                }
            }

            txtModel.Text = model;
            txtCusModel.Text = cus_model;
            txtCusCode.Text = cusCode;
            txtPO.Text = _PO;
            txtRequests.Focus();
        }

        int fuction;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string sql = "";
            string timeExport = dtExport.Value.ToString("yyyy/MM/dd HH:mm:ss");
            string numberBill = _FunctionID == LoadFunctionBillExportGoodsToCus.CREATE ? billExportGoodsDAO.createBillRequest(dtExport.Value) : txtBillNumber.Text;
            string vehicle = cbbShipping.Text;
            if (billExportGoodsDAO.isBillNumberExist(numberBill))
            {
                MessageBox.Show($"Phiếu {numberBill} đã tồn tại !");
                return;
            }
            if (billExportGoodsDAO.IsListEmty(_Bill.FPBillDetailS) || _Bill == null)
            {
                MessageBox.Show("Lỗi không tìm thấy dữ liệu  phiếu");
                return;
            }

            if (!billExportGoodsBUS.CreateBill(_Bill, numberBill, cusID, timeExport, vehicle, trueNumber, _UserID))
            {
                MessageBox.Show("Lỗi ! Kiểm tra kết nối đến Server");
            }
            else
            {
                MessageBox.Show($"Tạo phiếu {numberBill} thành công");
                if (_FunctionID == 0)
                {
                    if (_FunctionEditBill != null)
                    {
                        var bill = _FBBillExportDAO.GetFBBillByBillId(numberBill);
                        _FunctionEditBill(bill);
                    }
                }



                if (_FunctionID == LoadFunctionBillExportGoodsToCus.CREATE)
                {
                    _Bill = new FPBill()
                    {
                        CusId = cusID,
                        BillNumber = numberBill,
                        IntendTime = dtExport.Value,
                    };
                    txtWorkPlan.ReadOnly = true;
                    txtRequests.ReadOnly = true;
                    txtBillNumber.Text = numberBill;
                }


            }
            InitializeBill();
            txtNote.Clear();


        }
        private void txtCreateBillNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = (e.KeyChar.ToString()).ToUpper().ToCharArray()[0];
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        bool isBillExist = false;
        string dateExportBillExist = string.Empty;
        string customerBillExist = string.Empty;

        private void txtRequests_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string work = txtWorkPlan.Text;
            string model = txtModel.Text;
            string cusModel = txtCusModel.Text;
            string cusCode = txtCusCode.Text;
            int request = int.Parse(txtRequests.Text);
            string note = txtNote.Text;
            if (request > totalWork - exported)
            {
                MessageBox.Show($"Số lượng yêu cầu vượt mức, tổng work {totalWork}, đã xuất  {exported}, số lượng tối đa có thể xuất tiếp {totalWork - exported}");
                return;
            }
            string unit = "PCS";
            string po = txtPO.Text;
            if (string.IsNullOrEmpty(work) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(txtRequests.Text) || string.IsNullOrEmpty(unit))
            {
                MessageBox.Show(" Thông tin work chưa chưa được nhập đủ");
                return;
            }
            if (!billExportGoodsDAO.checkWork(work, out model, out cusID, out cus_model, out cusCode, out produtionPlan, out statusWo, out totalWork, out _PO, out exported))
            {
                MessageBox.Show(" Work không tồn tại");
                return;
            }
            int indexID = 1;

            if (_Bill != null && !billExportGoodsDAO.IsListEmty(_Bill.FPBillDetailS))
            {
                if (_Bill.CusId != cusID)
                {
                    MessageBox.Show("Không thể tạo phiếu cho 2 khách hàng");
                    return;
                }
                indexID = _Bill.FPBillDetailS.Max(x => x.Id) + 1;

            }
            else
            {
                _Bill.CusId = cusID;
            }
            if (_Bill.FPBillDetailS.Any(x => x.WorkId == work))
            {
                if (request == 0)
                {
                    _Bill.FPBillDetailS.RemoveAll(x => x.WorkId == work);
                }
                else
                {
                    _Bill.FPBillDetailS.Where(x => x.WorkId == work).FirstOrDefault().Request = request;
                }
            }
            else
            {

                FPBillDetail bill = new FPBillDetail()
                {
                    Id = indexID,

                    PO = po,
                    WorkId = work,
                    ModelId = model,
                    CusCode = cusCode,
                    CusModel = cusModel,
                    Request = request,
                    Note = $"{note}",
                };
                _Bill.FPBillDetailS.Add(bill);

            }
            var dataGridView1 = new BindingList<FPBillDetail>(_Bill.FPBillDetailS);
            dgvDetailBill.DataSource = dataGridView1;
            ClearDataDetail();

        }
        void ClearDataDetail()
        {
            foreach (TextBox item in groupPanel2.Controls.OfType<TextBox>())
            {
                item.Clear();
            }
        }
        int produtionPlan = 0;

        int trueNumber = 0;
        private void chkTrueNums_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTrueNums.Checked)
                trueNumber = 1;
            else
                trueNumber = 0;
        }
        private void txtPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtRequests.Focus();
        }

        private void txtRequests_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;

        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            InitializeBill();

        }
        string partFileSetting = ".\\Setting.ini";
        Microsoft.Office.Interop.Excel.Workbook workbook;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string billnumber = lbBill.Text;
            if (!billExportGoodsDAO.IsBillCanel(billnumber))
            {
                MessageBox.Show("Phiếu đã xuất ! Không thể hủy !");
                return;
            }
            if (!billExportGoodsBUS.CancelBill(billnumber))
            {
                MessageBox.Show("FAIL");
                return;
            }
            MessageBox.Show("PASS");
            return;
        }

        private void txtWorkPlan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());

        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            string bill = txtBillNumber.Text.Trim().Replace("/", "");
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = bill + ".xlsx";
            if (DialogResult.OK != dlg.ShowDialog()) return;
            dgvDetailBill.ExportToXlsx(dlg.FileName);
        }

        private void btnBookBill_Click(object sender, EventArgs e)
        {
            string bill = lbBill.Text;
            //if (string.IsNullOrEmpty(bill)) return;

            if (SelectFuncBook != null)
            {
                SelectFuncBook(_Bill);
            }
        }
        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {

            string bill = txtBillNumber.Text;
            _Bill = _FBBillExportDAO.GetFBBillByBillId(bill);
            DateTime date = _Bill.IntendTime;
            foreach (var item in _Bill.FPBillDetailS)
            {
                item.CusModel = "PCBA " + item.CusModel;
            }
            string dateExport = $"{date.Hour}:00 Ngày {date.Day} Tháng {date.Month} Năm {date.Year}";
            if (billExportGoodsDAO.IsListEmty(_Bill.FPBillDetailS)) return;
            customerBillExist = _Bill.CusId;
            string NoteBox = cusID == "PIO" ? "Thùng" : "Thùng";

            int sumPCb = _Bill.FPBillDetailS.Sum(x => x.Request);

            int countRow = _Bill.FPBillDetailS.Count();
            if (_Bill.FPBillDetailS.Count() < 5)
            {
                for (int i = countRow; i < 5; i++)
                {
                    _Bill.FPBillDetailS.Add(new FPBillDetail()
                    {
                        PackInfor = "  Thùng (  PCS/Thùng )"
                    });
                }
            }

            ReportDataSource rs = new ReportDataSource();
            rs.Name = "DataSet1";
            rs.Value = _Bill.FPBillDetailS;


            //param[0] = new ReportParameter("BillNumber", bill, false);
            ReportForm form = new ReportForm();
            form.reportViewer1.LocalReport.DataSources.Clear();
            form.reportViewer1.LocalReport.DataSources.Add(rs);

            if (_Bill.FPBillDetailS.Count > 5)
            {
                form.reportViewer1.LocalReport.ReportEmbeddedResource = "PLAN_MODULE.Report.RPBill_1.rdlc";
            }
            else
            {

                form.reportViewer1.LocalReport.ReportEmbeddedResource = "PLAN_MODULE.Report.RPBill_2.rdlc";
            }

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("BillNumber", bill, false);
            param[1] = new ReportParameter("DateExport", dateExport, false);
            param[2] = new ReportParameter("Total", sumPCb.ToString(), false);
            param[3] = new ReportParameter("customer", customerBillExist, false);
            param[4] = new ReportParameter("boxNote", NoteBox, false);
            form.reportViewer1.LocalReport.SetParameters(param);
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins = new System.Drawing.Printing.Margins(17, 13, 10, 5);
            pg.PaperSize = new System.Drawing.Printing.PaperSize("A4", 800, 1000);
            pg.Landscape = false;
            form.reportViewer1.SetPageSettings(pg);
            form.ShowDialog();
        }
        #endregion
    }
}
