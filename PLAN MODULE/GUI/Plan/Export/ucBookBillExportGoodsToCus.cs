using DevExpress.DataProcessing;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Printing.Core.PdfExport.Metafile;
using DevExpress.UnitConversion;
using DevExpress.Utils.DirectXPaint.Svg;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraSpreadsheet.Model;
using DocumentFormat.OpenXml.EMMA;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO.PLAN;
using PLAN_MODULE.DTO.Planed;
using PLAN_MODULE.DTO.Planed.BillExport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseDll.DTO.FinishedProduct;

namespace PLAN_MODULE.GUI.Plan.Export
{
    public partial class ucBookBillExportGoodsToCus : UserControl
    {

        BillExportGoodsDAO billExportGoodsDAO = new BillExportGoodsDAO();
        BillExportGoodsBUS billExportGoodsBUS = new BillExportGoodsBUS();

        FPBill _FPBill = new FPBill();

        public ucBookBillExportGoodsToCus(FPBill bill)
        {
            InitializeComponent();

            _FPBill = bill;

            this.Load += UcBookBillExportGoodsToCus_Load;
        }
        bool _IsBillBook = false;
        private void UcBookBillExportGoodsToCus_Load(object sender, EventArgs e)
        {
            lbTitle.Text = $"Book bill - {_FPBill.BillNumber}";

            cboCus.DataSource = billExportGoodsDAO.GetCustomer();
            cboCus.DisplayMember = "CUSTOMER_NAME";
            cboCus.ValueMember = "CUSTOMER_ID";


            loadInforBill();
        }
        private void cboCus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cusID = cboCus.SelectedValue.ToString();
            DataTable dt = billExportGoodsDAO.getModelByCus(cusID);
            cboModel.DataSource = dt;
            cboModel.DisplayMember = "ID_MODEL";
            cboModel.ValueMember = "ID_MODEL";

        }
        void loadInforBill()
        {
            _LsBook = billExportGoodsDAO.GetLsBoxBookedByBill(_FPBill.BillNumber);
            _IsBillBook = billExportGoodsDAO.IsListEmty(_LsBook) ? false : true;
            if (!billExportGoodsDAO.IsListEmty(_LsBook))
            {
                var dataGridView1 = new BindingList<BookBillExportToCus>(_LsBook);
                dgvDetailBill.DataSource = dataGridView1;
            }
            else
            {
                dgvDetailBill.DataSource = null;
            }

        }
        List<BillRequestExportGoodToCus> _BillRequest = new List<BillRequestExportGoodToCus>();
        List<BookBillExportToCus> _LsBook = new List<BookBillExportToCus>();

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (billExportGoodsDAO.IsListEmty(_LsBook))
            {
                return;
            }


            if (billExportGoodsBUS.CreatBookBill(_LsBook, _FPBill.BillNumber))
            {
                MessageBox.Show("Pass");
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {


            if (billExportGoodsDAO.IsBillExported(_FPBill.BillNumber))
            {
                MessageBox.Show("Phiếu đã xuất , không thể hủy !");
                return;
            }
            if (billExportGoodsBUS.CancelBookBill(_FPBill.BillNumber))
            {
                MessageBox.Show("Cancel Pass");
                loadInforBill();
            }
            else
            {
                MessageBox.Show("Cancel Fail");

            }

        }

        private void btnBookData_Click(object sender, EventArgs e)
        {
            if (_IsBillBook)
            {
                MessageBox.Show("Phiếu đã book dữ liệu !");
                return;
            }
            if (string.IsNullOrEmpty(_FPBill.BillNumber)) return;

            if (billExportGoodsDAO.IsBillExported(_FPBill.BillNumber))
            {
                MessageBox.Show("Phiếu đã xuất !");
                return;
            }


            _LsBook = new List<BookBillExportToCus>();

            foreach (var item in _FPBill.FPBillDetailS)
            {

                string Work = item.WorkId;
                string model = item.ModelId;
                int request = item.Request;
                DataTable dt = billExportGoodsDAO.GetListBoxByWork(Work);
                var qr = (from r in dt.AsEnumerable()
                          select new BookBillExportToCus
                          {
                              LotNo = r.Field<string>("LOTNO"),
                              BoxID = r.Field<string>("BOX_SERIAL"),
                              BoxCount = r.Field<int>("COUNT"),
                              IsFQC = r.Field<int>("FQC") == 1 ? true : false,
                              Work = Work,
                              StateName = r.Field<int>("STATE") == 0 ? "Chưa nhập kho" : "Đã nhập kho",
                              ModelID = model,
                              TotalRequest = request.ToString(),
                              DatePacking = r.Field<DateTime>("DATE_PACKING"),
                              TimePacking = r.Field<DateTime>("TIME_PACKING")

                          });
                var grQuery = (from r in qr
                               group r by new { r.LotNo, r.DatePacking } into gr
                               select new
                               {
                                   Lotno = gr.Key.LotNo,
                                   DatePack = gr.Key.DatePacking,
                                   LsBox = (from a in gr
                                            select new
                                            {
                                                BoxId = a.BoxID,
                                                Qty = a.BoxCount,
                                                IsFqc = a.IsFQC,
                                                State = a.StateName,
                                                TimePack = a.TimePacking
                                            }).OrderBy(x => x.TimePack).ToList(),
                               }).OrderBy(z => z.DatePack).ToList();

                foreach (var r in grQuery)
                {
                    var ls = r.LsBox;
                    foreach (var row in ls)
                    {
                        if (request <= 0) break;

                        var box = row;
                        int boxCount = row.Qty;

                        if (request == box.Qty)
                        {
                            bool checkWorkExisst = _LsBook.Any(x => x.Work == Work);
                            BookBillExportToCus bookbill = new BookBillExportToCus();
                            bookbill.Work = Work;
                            bookbill.ModelID = checkWorkExisst ? null : model;
                            bookbill.TotalRequest = checkWorkExisst ? null : request.ToString();

                            bookbill.BoxCount = boxCount;
                            bookbill.RequestAfter = request - boxCount;
                            bookbill.BoxID = box.BoxId;
                            bookbill.LotNo = r.Lotno;
                            bookbill.IsFQC = box.IsFqc;
                            bookbill.StateName = box.State;
                            request -= boxCount;
                            _LsBook.Add(bookbill);
                        }
                        else
                        {
                            bool boxQtyIsRequest = ls.Any(x => x.Qty == request);
                            if (boxQtyIsRequest)
                            {
                                box = ls.Where(x => x.Qty == request).FirstOrDefault();
                                bool IsBoxBook = _LsBook.Any(x => x.BoxID == box.BoxId);
                                if (IsBoxBook) continue;

                                bool checkWorkExisst = _LsBook.Any(x => x.Work == Work);
                                boxCount = box.Qty;
                                BookBillExportToCus bookbill = new BookBillExportToCus();
                                bookbill.Work = Work;
                                bookbill.ModelID = checkWorkExisst ? null : model;
                                bookbill.TotalRequest = checkWorkExisst ? null : request.ToString();

                                bookbill.BoxCount = boxCount;
                                bookbill.RequestAfter = request - boxCount;
                                bookbill.BoxID = box.BoxId;
                                bookbill.LotNo = r.Lotno;
                                bookbill.IsFQC = box.IsFqc;
                                bookbill.StateName = box.State;
                                _LsBook.Add(bookbill);
                                request -= boxCount;

                            }
                            else
                            {
                                bool checkWorkExisst = _LsBook.Any(x => x.Work == Work);
                                BookBillExportToCus bookbill = new BookBillExportToCus();
                                bookbill.Work = Work;
                                bookbill.ModelID = checkWorkExisst ? null : model;
                                bookbill.TotalRequest = checkWorkExisst ? null : request.ToString();

                                bookbill.BoxCount = boxCount;
                                bookbill.RequestAfter = request - boxCount;
                                bookbill.BoxID = box.BoxId;
                                bookbill.LotNo = r.Lotno;
                                bookbill.IsFQC = box.IsFqc;
                                bookbill.StateName = box.State;
                                request -= boxCount;
                                _LsBook.Add(bookbill);
                            }
                        }

                    }


                }
               

            }

            var dataGridView1 = new BindingList<BookBillExportToCus>(_LsBook);
            dgvDetailBill.DataSource = dataGridView1;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = $"Book Data.xlsx";
            if (DialogResult.OK != dlg.ShowDialog()) return;

            dgvDetailBill.ExportToXlsx(dlg.FileName);
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            loadInforBill();
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _LsBook = new List<BookBillExportToCus>();
            string model = cboModel.SelectedValue.ToString();
            DataTable dt = billExportGoodsDAO.GetListBoxByModel(model);
            int request = int.Parse(txtqty.Text.Trim());
            foreach (DataRow row in dt.Rows)
            {
                if (request <= 0) break;
                string Work = row["WORK_ORDER"].ToString();
                string boxId = row["BOX_SERIAL"].ToString();
                string lotno = row["LOTNO"].ToString();
                bool isFQC = int.Parse(row["FQC"].ToString()) == 1 ? true : false;
                bool checkExisst = billExportGoodsDAO.IsListEmty(_LsBook) ? false : true;
                int boxCount = int.Parse(row["COUNT"].ToString());
                int state = int.Parse(row["STATE"].ToString());

                BookBillExportToCus bookbill = new BookBillExportToCus();
                bookbill.Work = Work;
                bookbill.ModelID = checkExisst ? null : model;
                bookbill.TotalRequest = checkExisst ? null : request.ToString();
                bookbill.BoxCount = boxCount;
                bookbill.RequestAfter = request - boxCount;
                bookbill.BoxID = boxId;
                bookbill.LotNo = lotno;
                bookbill.IsFQC = isFQC;
                bookbill.StateName = state == 0 ? "Chưa nhập kho" : "Đã nhập kho";
                request -= boxCount;

                _LsBook.Add(bookbill);
            }
            var rs = from r in _LsBook
                     group r by r.Work into gr
                     select new
                     {
                         Wo = gr.Key,
                         Count = gr.Sum(x => x.BoxCount),
                     };

            foreach (var item in rs)
            {
                BookBillExportToCus bookbill = new BookBillExportToCus();
                bookbill.Work = item.Wo;
                bookbill.TotalRequest = item.Count.ToString();
                _LsBook.Add(bookbill);
            }
            var dataGridView1 = new BindingList<BookBillExportToCus>(_LsBook);
            dgvDetailBill.DataSource = dataGridView1;
        }
    }
}
