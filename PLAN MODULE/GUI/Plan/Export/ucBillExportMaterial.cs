using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DAO.PLAN;
//using PLAN_MODULE.DTO.BOM;
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
using System.IO;
using DevExpress.XtraSpreadsheet.Commands;
using WarehouseDll.DAO.Material;
using DocumentFormat.OpenXml.Spreadsheet;
using ROSE_Dll.DTO;
using Remotion.Logging;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;

namespace PLAN_MODULE.GUI.Plan.Export
{
    public partial class ucBillExportMaterial : UserControl
    {
        public delegate void FuncGoToViewForm(string bill);
        public event FuncGoToViewForm FuncGoToView;

        CreateWorkDAO createWorkDAO = new CreateWorkDAO();
        DAO.BillExportMaterialDAO billExportMaterialDAO = new DAO.BillExportMaterialDAO();
        BillExportMateriaBUS billExportMateriaBUS = new BillExportMateriaBUS();

        List<BillExportMaterialDetail> _BillExportMaterialDetailSample = new List<BillExportMaterialDetail>();
        string _BomVersion, _ModelID, _CusID, _Status, _CusModel, _Process;
        int _IsRMA, IsXout;
        string line, topbot;
        string _UserID = string.Empty;
        int _PCBS, _PCBXOut, _PcbOnPanel;
        List<string> lsMain = new List<string>();
        List<string> lsUsing = new List<string>();
        bool isGroupModel = false;
        int NumberModelGroup = 0;
        BillExportMaterial _BillExport = new BillExportMaterial();
        int FunctionId = 0;

        int _TypeBill = 0;
        public ucBillExportMaterial(BillExportMaterial billExport, int fuctionId, string userID)
        {
            InitializeComponent();
            _UserID = userID;
            _BillExport = billExport;
            FunctionId = fuctionId;
            this.Load += UcBillExportMaterial_Load;
        }
        DAO_WorkInfor DAO_Work = new DAO_WorkInfor();

        WorkOrder _WorkOrder = new WorkOrder();
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "";
            if (DialogResult.OK != openFileDialog.ShowDialog()) return;

            CreateBill(LoadBillWorkByFile(_BillExport.WorkId, File.ReadAllText(openFileDialog.FileName)), "Xác nhận tạo phiếu yêu cầu linh kiện cho work từ File?", BillExportMaterial.XuatTheoFile);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            _BillExport.WorkId = _BomVersion = _ModelID = _CusID = _Status = _CusModel = _Process = _ModelWork = _ModelParent = string.Empty;
            txtBillNumber.Clear();
            gridView1.Columns.Clear();
            dgvRequest.DataSource = billExportMaterialDAO.getListBillInWork(txtwork.Text);
            //LoadBillWork(_Work);
        }

        private void UcBillExportMaterial_Load(object sender, EventArgs e)
        {
            //LoadBillWork(_Work);
            LoadUI();
        }


        void LoadUI()
        {
            if (FunctionId == LoadFunctionBillExportMaterial.CREATE)
            {
                txtwork.Enabled = true;
                txtwork.Text = _BillExport == null ? "" : _BillExport.WorkId;
                _BillExport = new BillExportMaterial();
                dgvReport.DataSource = null;
                btnCreatBIll.Enabled = true;
                btnBillForming.Enabled = true;
                btnBillAI.Enabled = true;
                btnLoadFile.Enabled = true;
                btnPrint.Enabled = false;
                btnCancel.Enabled = false;
                dgvRequest.DataSource = null;
                foreach (TextBox item in groupPanel2.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
            }
            else
            {
                txtwork.Enabled = false;
                txtwork.Text = _BillExport.WorkId;
                txtBillNumber.Text = _BillExport.BillId;
                dgvRequest.DataSource = _BillExport.BillExportMaterialDetails;
                btnCreatBIll.Enabled = false;
                btnBillForming.Enabled = false;
                btnBillAI.Enabled = false;
                btnLoadFile.Enabled = false;
                btnPrint.Enabled = true;
                btnCancel.Enabled = true;
                if (!billExportMaterialDAO.IsWorkID(_BillExport.WorkId, out _ModelWork, out _ModelParent, out _ModelID, out _CusID, out _PCBS, out _Status, out _BomVersion,
                     out _IsRMA, out _IsRMA, out _PCBXOut, out _CusModel, out _PcbOnPanel, out _Process, out _BySet, out _IsGroup))
                {
                    MessageBox.Show("Sai Work !");
                    return;
                }
                _WorkOrder = DAO_Work.getWorkInfor(_BillExport.WorkId);

                //_BillExportMaterialDetailSample = LoadBillWorkByBOM(_WorkOrder);
                var rp = LoadInforExportMaterialByWork(_WorkOrder.WorkID, true);

                dgvReport.DataSource = rp;
                isGroupModel = billExportMaterialDAO.IsGroupModel(_ModelWork, out NumberModelGroup);

                //bool isFoxcon = billExportMaterialDAO.isFoxorLux(_CusID);
                txtModelChild.Text = _ModelWork;
                txtModelID.Text = _ModelID;
                txtVerBom.Text = _BomVersion;
                txtCusID.Text = _CusID;
                txtPCBS.Text = _PCBS.ToString();
                txtProcess.Text = _Process;

            }

        }
        List<BillExportMaterialDetail> LoadInforExportMaterialByWork(string work , bool GetRemain)
        {

            var _BillExportMaterialDetailReportS = new List<BillExportMaterialDetail>();
            var ls = billExportMaterialDAO.GetListSummaryBillExportRequestPC(work, false, GetRemain);


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
                    _BillExportMaterialDetailReportS.Add(billExportMaterialDetail);
                }
            }
            return _BillExportMaterialDetailReportS;
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            gridView1.Columns.Clear();
            dgvRequest.DataSource = billExportMaterialDAO.GetDetailBill(txtBillNumber.Text);
        }

        List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnCreatBIll_Click(object sender, EventArgs e)
        {
            CreateBill(LoadBillWorkByBOM(_BillExport.WorkId), "Xác nhận tạo phiếu yêu cầu linh kiện cho work từ định mức BOM?", LoadSubTypeBillExportMaterial.BOM);

        }
        string userID;

        private void btnDowload_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = "Filedowload" + ".xlsx";
            if (DialogResult.OK != dlg.ShowDialog()) return;



            dgvRequest.ExportToXlsx(dlg.FileName);
        }

        private void btnBillForming_Click(object sender, EventArgs e)
        {
            CreateBill(LoadBillWorkByBOMIsForming(_BillExport.WorkId), "Xác nhận tạo phiếu yêu cầu linh kiện gia công cho Work ?", LoadSubTypeBillExportMaterial.FORMING);

        }

        public void CreateBill(List<BillExportMaterialDetail> _BillExportMaterialDetail, string mess, int subType)
        {
            string bill;
            string dtBill = billExportMaterialDAO.GetBillInWorkBySubType(_BillExport.WorkId, subType);
            if (!string.IsNullOrEmpty(dtBill) && subType != LoadSubTypeBillExportMaterial.FILE)
            {
                MessageBox.Show($"Lỗi ! Đã tạo {LoadSubTypeBillExportMaterial.SubTypeBillExportMaterial.FirstOrDefault(x => x.StatusID == subType).StatusName} cho Work {_BillExport.WorkId} !");
                return;
            }
            if (createWorkDAO.IsListEmty(_BillExportMaterialDetail))
            {
                MessageBox.Show($"Lỗi ! Không tìm thấy danh sách linh kiện cho phiếu");
                return;
            }
            if (DialogResult.OK != MessageBox.Show(mess, "Xác nhận", MessageBoxButtons.OKCancel))
                return;
            bill = billExportMaterialDAO.CreateBillNameNew(_Process, _BillExport.WorkId, _TypeBill);
            if (billExportMateriaBUS.SaveBillExport(_BillExportMaterialDetail, bill, _BillExport.WorkId, _PCBS, _Process, _BomVersion, _UserID, subType, _TypeBill))
            {
                MessageBox.Show($"Tạo phiếu {bill} thành công");

                if (FuncGoToView != null)
                {
                    FuncGoToView(bill);
                }
            }
            else
            {
                MessageBox.Show($"Tạo phiếu Thất bại !");
            }
        }

        private void btnBillAI_Click(object sender, EventArgs e)
        {
            CreateBill(LoadBillWorkByBOMIsAI(_BillExport.WorkId), "Xác nhận tạo phiếu yêu cầu linh kiện AI ?", LoadSubTypeBillExportMaterial.AI);

        }

        private void txtwork_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadUI();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string bill = txtBillNumber.Text.Trim();
            if (string.IsNullOrEmpty(bill)) return;
            if (!billExportMaterialDAO.IsBillExport(bill))
            {
                MessageBox.Show($"Lỗi ! không thể Hủy phiếu đã xuất !");
                return;
            }

            if (billExportMateriaBUS.CancelBill(bill))
            {
                MessageBox.Show($"Pass");
            }
            else
            {
                MessageBox.Show($"Fail");
            }
        }
        List<ROSE_Dll.DTO.BomContent> _BOMContent = new List<ROSE_Dll.DTO.BomContent>();

        string _ModelWork, _ModelParent;
        int _BySet = 0;
        int _IsGroup = 0;
        private void txtwork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            _TypeBill = 0;
            string workId = txtwork.Text;
            if (string.IsNullOrEmpty(workId))
            {
                MessageBox.Show($"yêu cầu điền tên Work!");
                return;
            }
            if (!billExportMaterialDAO.IsWorkID(workId, out _ModelWork, out _ModelParent, out _ModelID, out _CusID, out _PCBS, out _Status, out _BomVersion,
              out _IsRMA, out _IsRMA, out _PCBXOut, out _CusModel, out _PcbOnPanel, out _Process, out _BySet, out _IsGroup))
            {
                MessageBox.Show("Sai Work !");
                return;
            }
            _WorkOrder = DAO_Work.getWorkInfor(workId);
            _BillExport.WorkId = workId;
            _TypeBill = _Process == "SMT" ? BillExportMaterial.phiêuSMT : BillExportMaterial.phiêuFAT;
            isGroupModel = billExportMaterialDAO.IsGroupModel(_ModelWork, out NumberModelGroup);
            //bool isFoxcon = billExportMaterialDAO.isFoxorLux(_CusID);

            //_BillExportMaterialDetailSample = LoadBillWorkByBOM(_WorkOrder);
            var rp = LoadInforExportMaterialByWork(_WorkOrder.WorkID, false);
            dgvReport.DataSource = rp;

            txtModelChild.Text = _ModelWork;
            txtModelID.Text = _ModelID;
            txtVerBom.Text = _BomVersion;
            txtCusID.Text = _CusID;
            txtPCBS.Text = _PCBS.ToString();
            txtProcess.Text = _Process;
            //UcBillExportMaterial_Load(sender, e);
        }
        List<BillExportMaterialDetail> LoadBillWorkByFile(string work, string fileContent)
        {
            lsMain = new List<string>();
            lsUsing = new List<string>();
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            txtwork.Text = work;
            if (string.IsNullOrEmpty(work)) return _BillExportMaterialDetail;
            if (_Status == "CLOSE")
            {
                MessageBox.Show("Work đã đóng !");
                return new List<BillExportMaterialDetail>();
            }
            if (_BomVersion == "1.0")
            {
                MessageBox.Show("Chưa chọn version BOM cho Work");
                return new List<BillExportMaterialDetail>();
            }
            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = _ModelWork, BomVersion = _BomVersion });

            var lsMainRequest = billExportMaterialDAO.GetListMainRequestedByPlan(work);

            if (billExportMaterialDAO.IsListEmty(_BOMContent))
            {
                MessageBox.Show("Không có định mức trong Bom, Vui lòng tạo phiếu yêu cầu xuất theo Phát sinh !");
                return new List<BillExportMaterialDetail>();

            }
            var rs = (from r in _BOMContent
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Quantity = string.Join("|", gr.Select(ra => ra.Quantity)),
                      }).ToList();

            string[] textLine = fileContent.Split('\n');

            List<string> MainTooNumber = new List<string>();
            foreach (var item in textLine)
            {


                if (!item.Contains(_ModelWork + "-")) continue;
                string[] lineContent = item.Split(',');
                string mainPart = lineContent[1];
                int qty = 0;
                string _qty = lineContent[2].Replace("\r", "").Trim();
                if (!int.TryParse(_qty, out qty))
                {
                    MessageBox.Show($"Định dạng số lượng không đúng tại mainPart {mainPart}, kiểm tra lại file");
                    return new List<BillExportMaterialDetail>();
                }

                if (!rs.Any(a => a.MainPart == mainPart))
                {
                    MessageBox.Show($"Bom model {_ModelID} không chứa  mainPart {mainPart}, kiểm tra lại file");
                    return new List<BillExportMaterialDetail>();
                }
                if (_BillExportMaterialDetail.Any(a => a.MainPart == mainPart))
                {
                    MessageBox.Show($"Trong 1 phiếu yêu cầu không được lặp lại mainPart {mainPart}, kiểm tra lại file");
                    return new List<BillExportMaterialDetail>();
                }
                if (lsMainRequest.Any(x => x.Main == mainPart))
                {
                    MessageBox.Show($"Maim {mainPart} đã được tạo trong phiếu xuất {lsMainRequest.FirstOrDefault(x => x.Main == mainPart).Bill} trước đó");
                    return new List<BillExportMaterialDetail>();
                }
                if (qty <= 0)
                {
                    MessageBox.Show($"Số lượng yêu cầu tại mainPart {mainPart} lớn hơn 0 !");
                    return new List<BillExportMaterialDetail>();
                }
                var sQuantity = rs.FirstOrDefault(x => x.MainPart == mainPart).Quantity;

                int quantity = 1;

                if (string.IsNullOrEmpty(sQuantity))
                {
                    MessageBox.Show($"Không tim thấy định mức trong BOM cho Main {mainPart}!");
                    return new List<BillExportMaterialDetail>();
                }

                if (sQuantity.Contains("|"))
                {
                    var arrQuantity = sQuantity.Split('|');
                    foreach (var q in arrQuantity)
                    {
                        if (!string.IsNullOrEmpty(q))
                        {
                            if (!int.TryParse(q, out quantity))
                            {
                                quantity = 1;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sQuantity))
                    {
                        if (!int.TryParse(sQuantity, out quantity))
                        {
                            quantity = 1;
                        }
                    }
                }
                int countMain = rs.Where(a => a.MainPart == mainPart).ToList().Count();
                if(countMain == 1)
                {
                    if (qty > _PCBS * quantity)
                    {
                        MainTooNumber.Add(mainPart);
                        continue;
                    }
                }
                else
                {
                    int maxQty = 0;
                    foreach (var main in rs.Where(a => a.MainPart == mainPart).ToList())
                    {
                        sQuantity = main.Quantity;
                        if (string.IsNullOrEmpty(sQuantity))
                        {
                            continue;
                        }

                        if (sQuantity.Contains("|"))
                        {
                            var arrQuantity = sQuantity.Split('|');
                            foreach (var q in arrQuantity)
                            {
                                if (!string.IsNullOrEmpty(q))
                                {
                                    if (!int.TryParse(q, out quantity))
                                    {
                                        quantity = 1;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sQuantity))
                            {
                                if (!int.TryParse(sQuantity, out quantity))
                                {
                                    quantity = 1;
                                }
                            }
                        }
                        maxQty += _PCBS * quantity;
                    }

                    if (qty > maxQty)
                    {
                        MainTooNumber.Add(mainPart);
                        continue;
                    }
                }
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = mainPart, PartNumber = rs.Where(a => a.MainPart == mainPart).First().InterPart, Qty = qty });
            }
            if (!billExportMaterialDAO.IsListEmty(MainTooNumber))
            {
                string mess = string.Join(",", MainTooNumber);
                MessageBox.Show($"Các mã {mess} vượt quá định mức");
                return new List<BillExportMaterialDetail>();
            }

            return _BillExportMaterialDetail;
        }
        List<BillExportMaterialDetail> LoadBillWorkByBOM(string work)
        {
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            if (string.IsNullOrEmpty(work)) return _BillExportMaterialDetail;

            lsMain = new List<string>();
            lsUsing = new List<string>();


            if (_Status == "CLOSE")
            {
                MessageBox.Show("Work đã đóng !");
                return new List<BillExportMaterialDetail>();
            }
            if (_BomVersion == "1.0")
            {
                MessageBox.Show("Chưa chọn version BOM cho Work");
                return new List<BillExportMaterialDetail>();
            }

            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = _ModelWork, BomVersion = _BomVersion });

            int level = _TypeBill == BillExportMaterial.phiêuSMT ? 4 : 3;
            _BOMContent = GetBillDetail(_BOMContent, level);

            //_BOMContent = _BOMContent.Where(x => x.level == level).ToList();

            var mainForming = _BOMContent.Where(x => x.Forming == 1).Select(x => x.MainPart).ToList();
            var mainAI = _BOMContent.Where(x => x.Process == "AI").Select(x => x.MainPart).ToList();

            _BOMContent = _BOMContent.Where(x => !mainForming.Any(a => a == x.MainPart) && x.Location != "BTP" && !mainAI.Any(b => b == x.MainPart)).ToList();

            var lsMainRequest = billExportMaterialDAO.GetListMainRequestedByPlan(work);
            if (lsMainRequest != null && lsMainRequest.Count() > 0)
            {
                _BOMContent = _BOMContent.Where(x => !lsMainRequest.Any(a => a.Main == x.MainPart)).ToList();

            }

            if (billExportMaterialDAO.IsListEmty(_BOMContent))
            {
                MessageBox.Show("Không có định mức trong Bom, Vui lòng tạo phiếu yêu cầu xuất theo Phát sinh !");
                return new List<BillExportMaterialDetail>();

            }

            var rs = (from r in _BOMContent
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Quantity = string.Join("|", gr.Select(ra => ra.Quantity)),
                      }).ToList();


            foreach (var g in rs)
            {
                if (g.InterPart.Contains(_ModelID)) continue;
                float request = 0;
                string[] _quantity = g.Quantity.Split('|');
                for (int i = 0; i < _quantity.Count(); i++)
                {
                    string qtt = _quantity[i];
                    if (!string.IsNullOrEmpty(qtt))
                    {
                        request = float.Parse(qtt) * _PCBS;
                        break;
                    }
                }
                if (isGroupModel)
                {
                    request = request / NumberModelGroup;
                }
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = g.MainPart, PartNumber = g.InterPart, Qty = request });
            }


            return _BillExportMaterialDetail;
        }

        List<BomContent> GetBillDetail(List< BomContent> bom, int level)
        {
            List<BomContent> bomNew = new List<BomContent>();
            var checkHighLevel = bom.Any(x => x.level == level + 1);
            bomNew = bom.Where(x => x.level == level).ToList();
            if (checkHighLevel)
            {

                int a = 0;
                foreach (var item in bom)
                {
                    if (item.level == level + 1)
                    {
                        //checkHighLevel = true;
                        if(a==0)
                         a = bom.IndexOf(item);
                        bomNew.Add(item);
                        continue;
                    }
                    //foreach (var item2 in bomNew)
                   if (a != 0 /*&& checkHighLevel*/)
                    {
                        bomNew.RemoveAll(x => x.MainPart == bom.ElementAt(a - 1).MainPart);
                        {
                            //var index = bom.IndexOf(item);
                            //checkHighLevel = false;
                            a = 0;
                        }
                    }
                }
                bomNew.AddRange(GetBillDetail(bom, level + 1));
            }
         
            return bomNew;
        }

        List<BillExportMaterialDetail> LoadBillWorkByBOM(WorkOrder workOrder)
        {
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            if (string.IsNullOrEmpty(workOrder.WorkID)) return _BillExportMaterialDetail;

            lsMain = new List<string>();
            lsUsing = new List<string>();

            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = workOrder.ModelID, BomVersion = workOrder.bomVersion });

            if (billExportMaterialDAO.IsListEmty(_BOMContent))
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
                                   Remain = billExportMaterialDAO.GetRemainPanacim(r.InterPart),
                                   Location = r.Location,
                               }).ToList();

            var rs = (from r in queryRemain
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Remain = string.Join(" | ", gr.Select(i => i.Remain)),
                          Quantity = string.Join(" | ", gr.Select(ra => ra.Quantity)),
                      }).ToList();


            foreach (var g in rs)
            {
                if (g.InterPart.Contains(workOrder.ModelID)) continue;
                float request = 0;
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = g.MainPart, PartNumber = g.InterPart, Remain = g.Remain, Qty = request });
            }
            return _BillExportMaterialDetail;
        }

        List<BillExportMaterialDetail> LoadBillWorkByBOMIsForming(string work)
        {
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            if (string.IsNullOrEmpty(work)) return _BillExportMaterialDetail;

            lsMain = new List<string>();
            lsUsing = new List<string>();


            if (_Status == "CLOSE")
            {
                MessageBox.Show("Work đã đóng !");
                return new List<BillExportMaterialDetail>();
            }
            if (_BomVersion == "1.0")
            {
                MessageBox.Show("Chưa chọn version BOM cho Work");
                return new List<BillExportMaterialDetail>();
            }
            //if (_Process != "SMT")
            //{
            //    MessageBox.Show("Work Không phải là work SMT");
            //    return _BillExportMaterialDetail;
            //}

            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = _ModelWork, BomVersion = _BomVersion });

            int level = _TypeBill == BillExportMaterial.phiêuSMT ? 4 : 3;
            _BOMContent = GetBillDetail(_BOMContent, level);

            var mainForming = _BOMContent.Where(x => x.Forming == 1).Select(x => x.MainPart).ToList();
            _BOMContent = _BOMContent.Where(x => mainForming.Any(a => a == x.MainPart) && x.Location != "BTP" ).ToList();

            var lsMainRequest = billExportMaterialDAO.GetListMainRequestedByPlan(work);
            if (lsMainRequest != null && lsMainRequest.Count() > 0)
            {
                _BOMContent = _BOMContent.Where(x => !lsMainRequest.Any(a => a.Main == x.MainPart)).ToList();

            }
            if (billExportMaterialDAO.IsListEmty(_BOMContent))
            {
                MessageBox.Show("Không có định mức trong Bom, Vui lòng tạo phiếu yêu cầu xuất theo Phát sinh !");
                return new List<BillExportMaterialDetail>();

            }
            var rs = (from r in _BOMContent
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Quantity = string.Join("|", gr.Select(ra => ra.Quantity)),
                      }).ToList();


            foreach (var g in rs)
            {
                if (g.InterPart.Contains(_ModelID)) continue;
                float request = 0;
                string[] _quantity = g.Quantity.Split('|');
                for (int i = 0; i < _quantity.Count(); i++)
                {
                    string qtt = _quantity[i];
                    if (!string.IsNullOrEmpty(qtt))
                    {
                        request = float.Parse(qtt) * _PCBS;
                        break;
                    }
                }
                if (isGroupModel)
                {
                    request = request / NumberModelGroup;
                }
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = g.MainPart, PartNumber = g.InterPart, Qty = request });
            }

            return _BillExportMaterialDetail;
        }


        List<BillExportMaterialDetail> LoadBillWorkByBOMIsAI(string work)
        {
            List<BillExportMaterialDetail> _BillExportMaterialDetail = new List<BillExportMaterialDetail>();
            if (string.IsNullOrEmpty(work)) return _BillExportMaterialDetail;

            lsMain = new List<string>();
            lsUsing = new List<string>();


            if (_Status == "CLOSE")
            {
                MessageBox.Show("Work đã đóng !");
                return new List<BillExportMaterialDetail>();
            }
            if (_BomVersion == "1.0")
            {
                MessageBox.Show("Chưa chọn version BOM cho Work");
                return new List<BillExportMaterialDetail>();
            }
            //if (_Process != "SMT")
            //{
            //    MessageBox.Show("Work Không phải là work SMT");
            //    return _BillExportMaterialDetail;
            //}

            _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = _ModelWork, BomVersion = _BomVersion });
            int level = _TypeBill == BillExportMaterial.phiêuSMT ? 4 : 3;
            _BOMContent = GetBillDetail(_BOMContent, level);

            var mainForming = _BOMContent.Where(x => x.Forming == 1).Select(x => x.MainPart).ToList();

            _BOMContent = _BOMContent.Where(x => !mainForming.Any(a => a == x.MainPart) && x.Location != "BTP" && x.Process == "AI").ToList();

            var lsMainRequest = billExportMaterialDAO.GetListMainRequestedByPlan(work);
            if (lsMainRequest != null && lsMainRequest.Count() > 0)
            {
                _BOMContent = _BOMContent.Where(x => !lsMainRequest.Any(a => a.Main == x.MainPart)).ToList();

            }
            if (billExportMaterialDAO.IsListEmty(_BOMContent))
            {
                MessageBox.Show("Không có định mức trong Bom, Vui lòng tạo phiếu yêu cầu xuất theo Phát sinh !");
                return new List<BillExportMaterialDetail>();

            }
            var rs = (from r in _BOMContent
                      where r.MainPart.Length > 10
                      group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                      select new
                      {
                          MainPart = gr.Key.MainPart,
                          InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                          Quantity = string.Join("|", gr.Select(ra => ra.Quantity)),
                      }).ToList();


            foreach (var g in rs)
            {
                if (g.InterPart.Contains(_ModelID)) continue;
                float request = 0;
                string[] _quantity = g.Quantity.Split('|');
                for (int i = 0; i < _quantity.Count(); i++)
                {
                    string qtt = _quantity[i];
                    if (!string.IsNullOrEmpty(qtt))
                    {
                        request = float.Parse(qtt) * _PCBS;
                        break;
                    }
                }
                if (isGroupModel)
                {
                    request = request / NumberModelGroup;
                }
                _BillExportMaterialDetail.Add(new BillExportMaterialDetail() { MainPart = g.MainPart, PartNumber = g.InterPart, Qty = request });
            }


            return _BillExportMaterialDetail;
        }
    }
}
