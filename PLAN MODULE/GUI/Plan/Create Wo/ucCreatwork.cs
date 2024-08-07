using PLAN_MODULE.BUS;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO.Planed;
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

using PLAN_MODULE.DAO.PLAN;
using DevExpress.XtraRichEdit.Fields;

namespace PLAN_MODULE.GUI.Plan
{
    public partial class ucCreatwork : UserControl
    {
        public delegate void SendFuction(int TypeID, Work work);
        public event SendFuction _SendFuction;
        CreateWorkDAO _CreatWorkDAO = new CreateWorkDAO();
        WorkOrderBUS _WorkOrderBUS = new WorkOrderBUS();
        RequestImportDAO _RequestImportDAO = new RequestImportDAO();

        PODao _PODao = new PODao();
        string _ThisMachine = Environment.MachineName;
        //CreatWorkBUS _CreatWorkBUS = new CreatWorkBUS();
        bool isPC = false;
        public ucCreatwork(string userID, bool isPc = false)
        {
            InitializeComponent();
            _UserID = userID;
            this.isPC = isPc;
            this.Load += UcCreatwork_Load;
        }
        int _IsSample = 0;
        int _IsRMA = 0;
        int _IsXOut = 0;
        string _UserID = string.Empty;
        int _PcbOnPanel = 0;
        private void chkSanple_CheckedChanged(object sender, EventArgs e)
        {
            _IsSample = chkSanple.Checked ? 1 : 0;
        }
        private void chkRMA_CheckedChanged(object sender, EventArgs e)
        {
            _IsRMA = chkRMA.Checked ? 1 : 0;
        }
        private void chkXOut_CheckedChanged(object sender, EventArgs e)
        {
            _IsXOut = chkXOut.Checked ? 1 : 0;
        }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void txtPCBXout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void txtWorkID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }
        private void txtWorkParent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }
        private void UcCreatwork_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            if (isPC)
                for (int i = 0; i < toolStrip1.Items.Count; i++)
                {
                    if (toolStrip1.Items[i].Name == "btnPlanDetail")
                        toolStrip1.Items[i].Enabled = true;
                    else
                        toolStrip1.Items[i].Enabled = false;
                }

            dtLast.Value = dtFirst.Value.AddDays(30);
            dtFirst.Value = dtMfg.Value.AddDays(1);
        }

        void LoadCustomer()
        {
            DataTable dt = _CreatWorkDAO.GetCustomer();
            cboCustomer.DataSource = dt;
            cboCustomer.DisplayMember = "CUSTOMER_NAME";
            cboCustomer.ValueMember = "CUSTOMER_ID";
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cusID = cboCustomer.SelectedValue.ToString();
            DataTable dt = _CreatWorkDAO.getModelByCus(cusID);
            cboModel.DataSource = dt;
            cboModel.DisplayMember = "ID_MODEL";
            cboModel.ValueMember = "PROCESS";

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UcCreatwork_Load(sender, e);
        }
        List<Work> _Works = new List<Work>();
        string _ModelMother = string.Empty;
        ModelConfig _ModelConfig = new ModelConfig();
        private void cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ModelConfig = new ModelConfig();
            string model = cboModel.Text.Trim();

            LoadDataWork(model);
            DataTable dtVersion = _CreatWorkDAO.getBomversion(model);

            if (_CreatWorkDAO.istableNull(dtVersion))
            {
                cboBOM.DataSource = null;
                return;
            }
            _PcbOnPanel = _CreatWorkDAO.GetPcbOnPanelByModel(model);
            cboBOM.DataSource = dtVersion;
            cboBOM.DisplayMember = "VERSION";
            cboBOM.ValueMember = "VERSION";
        }
        void LoadDataWork(string model, string work = "")
        {
            _Works = new List<Work>();
            _Works = WorkControl.LoadLsWork(model, work);
            var dataGridView1 = new BindingList<Work>(_Works);
            dgvDetail.DataSource = dataGridView1;
            _ModelConfig = ModelConfigControl.LoadModelConfig(model);
            txtProcess.Text = _ModelConfig.Process;
            txtProductLine.Text = _ModelConfig.ProductLine;
            lbCount.Text = _Works.Count().ToString();

            txtWorkID.Clear();
            txtQty.Text = "0";
            txtPCBXout.Text = "0";
            txtWorkParent.Clear();
            txtPO.Clear();
            BingdingData(dataGridView1);

        }
        void BingdingData(object context)
        {
            if (context != null)
            {
                lbWork.DataBindings.Clear();
                lbStatusName.DataBindings.Clear();

                lbWork.DataBindings.Add(new Binding("Text", context, "WorkID"));
                lbStatusName.DataBindings.Add(new Binding("Text", context, "Status"));

            }
            else
            {
                lbWork.Text = "";
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string work = txtWorkID.Text;
            string workMother = txtWorkParent.Text.Trim();
            string cus = cboCustomer.SelectedValue.ToString();
            string model = cboModel.Text.Trim();
            string PO = txtPO.Text.Trim();
            string bomVersion = cboBOM.Text.Trim();
            int numberPCBS = 0;
            int numberPCBXout = 0;
            int numberTemp = 0;
            if (string.IsNullOrEmpty(work)) return;
            if (!int.TryParse(txtQty.Text, out numberPCBS)) return;
            if (!int.TryParse(txtPCBXout.Text, out numberPCBXout)) return;
            if (!int.TryParse(txtTempNumber.Text, out numberTemp)) return;
            if (numberPCBS < 1) return;
            string modelParent, cuslParent, statuslParent;
            string processMother = string.Empty;
            string processChild = cboModel.SelectedValue.ToString();
            bool IsLinkMother = false;
            string Comment = txtComment.Text.Trim();
            DateTime mfgDate = dtMfg.Value;
            DateTime firstDate = dtFirst.Value;
            DateTime lastDate = dtLast.Value;
            if (!_CreatWorkDAO.IsModelPTHOnly(model) && _IsRMA != 1) // Model chạy ở nhiều xưởng
            {
                if (string.IsNullOrEmpty(PO))
                {
                    MessageBox.Show($"Vui lòng nhập P.O ");
                    return;
                }
                if (!_PODao.IsPOOfModel(_ModelConfig.ProductLine, PO))
                {
                    MessageBox.Show($"Không tìm thấy thông tin PO - {PO} khai báo cho Model - {_ModelConfig.ProductLine}, khai báo PO trước ");
                    return;
                }
                if (_ModelConfig.Process != "SMT")
                {
                    if (!_CreatWorkDAO.IsWorkID(workMother, out modelParent, out cuslParent, out statuslParent, out processMother))
                    {
                        MessageBox.Show("WO mẹ không tồn tại !");
                        return;
                    }
                    if (statuslParent == "CLOSE")
                    {
                        MessageBox.Show(" Work mẹ đã đóng !");
                        return;
                    }
                    if (modelParent.ToUpper() != _ModelConfig.ModelParent)
                    {
                        MessageBox.Show($"WO {workMother} không thể là wo mẹ !");
                        return;
                    }
                    IsLinkMother = true;
                }
              
            }
            if (_IsXOut == 1)
            {
                if (numberPCBXout < 1)
                {
                    MessageBox.Show($"Vui lòng nhập số lượng PCS XOut !");
                    return;
                }
            }

            POOrder pOOrder = _PODao.GetPOOrderByPOName(PO);

            string ModelOld, cusOld, statusOld, verBomOld;
            int pcbOld, isRMAOld, isXoutOld, pcbXoutOld, isSampOld, tempNumberOld;



            bool ExistWorkID = _CreatWorkDAO.isWorkID(work, out ModelOld, out cusOld, out pcbOld, out statusOld, out verBomOld, out isRMAOld, out isXoutOld, out pcbXoutOld, out isSampOld, out tempNumberOld);
            if (ExistWorkID)
            {
                if (pcbOld != numberPCBS)
                {
                    MessageBox.Show($"Không được thay đổi lượng wo!");
                    return;
                }
                if (statusOld == "CLOSE")
                {
                    MessageBox.Show($"Work đã được đóng không thể cập thật thông tin !");
                    return;
                }
                if (_CreatWorkDAO.IsWorkRunning(work))
                {
                    
                    if (model != ModelOld)
                    {
                        MessageBox.Show($"Work đã được thêm cho model {ModelOld} !");
                        return;
                    }                    
                    if (tempNumberOld > numberTemp)
                    {
                        MessageBox.Show($"Chỉ được tăng số lượng temp cho wo!");
                        return;
                    }
                    if (_IsRMA != isRMAOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo rework khi wo đã sản xuất !");
                        return;
                    }
                    if (_IsXOut != isXoutOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo X - OUT khi wo đã sản xuất !");
                        return;
                    }
                    if (numberPCBXout != pcbXoutOld)
                    {
                        MessageBox.Show($"Không thể cập nhật số lượng x - out khi wo đã sản xuất !");
                        return;
                    }
                    if (_IsSample != isSampOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo mẫu  khi wo đã sản xuất !");
                        return;
                    }
                }
                if (_IsRMA != 1)
                {
                    var lsWorkByPO = WorkControl.LoadLsWorkByPO(PO, model, false, work);

                    var sumWorks = lsWorkByPO.Where(x => x.IsRMA == 0).Sum(x => x.PCS);
                    var sumworksNew = sumWorks + (numberPCBS - numberPCBXout);
                    var totalPO = pOOrder.PODetails.Where(x => x.ModelId == _ModelConfig.ProductLine).Sum(a => a.Count);
                    if (totalPO < sumworksNew )
                    {

                        MessageBox.Show($"Số lượng tổng các wo {sumWorks} + {numberPCBS} lớn hơn số lượng PO {totalPO}");
                        return;
                    }
                }
                  
            }
            else
            {
                if(_IsRMA != 1)
                {
                    var lsWorkByPO = WorkControl.LoadLsWorkByPO(PO, model);

                    var sumWorks = lsWorkByPO.Where(x => x.IsRMA != 1).Sum(x => x.PCS);
                    var sumworksNew = sumWorks + (numberPCBS - numberPCBXout);
                    var totalPO = pOOrder.PODetails.Where(x => x.ModelId == _ModelConfig.ProductLine).Sum(a => a.Count);

                    if (totalPO < sumworksNew )
                    {

                        MessageBox.Show($"Số lượng tổng các wo {sumWorks} + {numberPCBS} lớn hơn số lượng PO {totalPO}");
                        return;
                    }
                }
            
            }

            if (numberPCBS % _PcbOnPanel != 0 && _IsRMA != 1)
            {
                MessageBox.Show($" Vui lòng tạo số lượng sản xuất là Panel chẵn. Số dư pcb {numberPCBS} / {_PcbOnPanel} = {numberPCBS % _PcbOnPanel}.");
                return;
            }



            if (!_CreatWorkDAO.CreateWork(cus, model, work, numberPCBS, _UserID, _IsRMA, _IsXOut, _IsSample, numberPCBXout, workMother, processMother,
                processChild, PO, IsLinkMother, bomVersion, Comment, ExistWorkID, mfgDate, firstDate, lastDate, numberTemp,int.Parse( nbMonthFinishPP.Value.ToString())))
            {
                MessageBox.Show("Cập nhật work thất bại");
            }
            else
            {
                MessageBox.Show($"Cập nhật Work {work} thành công !");
            }
            LoadDataWork(model, work);

        }
        string _ModelID, _CusID, _Status;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string work = txtWorkID.Text;
            string workLink = string.Empty;
            if (DialogResult.OK != MessageBox.Show("Bạn có chắc chắn muốn Xóa Wo", "Xác nhận", MessageBoxButtons.OKCancel)) return;
            if (!_CreatWorkDAO.isWorkID(work, out _ModelID, out _CusID, out _Status))
            {
                MessageBox.Show("Work không tồn tại !");
                return;
            }
            if (_Status == "CLOSE")
            {
                MessageBox.Show(" Work đã đóng không thể xóa !");
                return;
            }
            if (_CreatWorkDAO.IsWorkCreatedManuData(work))
            {
                MessageBox.Show("work number này đã có dữ liệu nhập vào hệ thống, không được xóa");
                return;
            }
            if (_CreatWorkDAO.IsWorkHasLink(work, out workLink))
            {
                MessageBox.Show($"Work đã được link với Work {workLink}, Xóa wo link trước");
                return;
            }
            if (_CreatWorkDAO.IsWorkRunning(work))
            {
                MessageBox.Show("Work đã chạy không thể xóa");
                return;
            }
            if (!_CreatWorkDAO.DeleteWork(work, _UserID))
            {
                MessageBox.Show("error!");
            }
            else
            {
                MessageBox.Show("Done!");

            }
            LoadDataWork(_ModelID, work);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string cusID = cboCustomer.Text.Trim();
            string modelID = cboModel.Text.Trim();
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = cusID + "(" + modelID + ")" + ".xlsx";
            if (DialogResult.OK != dlg.ShowDialog()) return;
         


            dgvDetail.ExportToXlsx(dlg.FileName);
        }

        private void btnBillExportMaterial_Click(object sender, EventArgs e)
        {
            if (_SendFuction != null)
            {
                string work = txtWorkID.Text;
                var  Work = _Works.FirstOrDefault(X=>X.WorkID == work);
                _SendFuction(5, Work);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Work = txtWorkID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WorkID").ToString();
            string TotalPCS = txtQty.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalPCS").ToString();
            string WorkParent = txtWorkParent.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WorkParent").ToString();
            string Comment = txtComment.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Comment").ToString();
            string PO = txtPO.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PO").ToString();
            dtMfg.Value = (DateTime)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MFGDate");
            dtFirst.Value = (DateTime)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FirstDate");
            dtLast.Value = (DateTime)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LastDate");
            txtTempNumber.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TempNumber").ToString();
            int IsRMA = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsRMA").ToString());
            int IsXout = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsXout").ToString());
            string PcbXO = txtPCBXout.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PcbXO").ToString();
            int IsSample = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsSample").ToString());
            nbMonthFinishPP.Value = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Month_Finish_PP").ToString());

            chkRMA.Checked = IsRMA == 1 ? true : false;
            chkSanple.Checked = IsSample == 1 ? true : false;
            chkXOut.Checked = IsXout == 1 ? true : false;
        }
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            string tempNumber = txtQty.Text;
            if (!string.IsNullOrEmpty(tempNumber))
            {
                txtTempNumber.Text = tempNumber;
            }
        }
        private void txtTempNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        private void cboBOM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnBookMaterial_Click(object sender, EventArgs e)
        {
            if (_SendFuction != null)
            {
                string work = txtWorkID.Text;
                var Work = _Works.FirstOrDefault(X => X.WorkID == work);

                _SendFuction(6, Work);
            }
        }

        private void panelEx2_Click(object sender, EventArgs e)
        {

        }

        private void dtFisrt_ValueChanged(object sender, EventArgs e)
        {
            dtLast.Value = dtFirst.Value.AddDays(30);
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            if(_SendFuction != null)
            {
                string workId = lbWork.Text;
                var Work = _Works.FirstOrDefault(X => X.WorkID == workId);

                _SendFuction(7, Work);
            }
        }

        private void btnCusData_Click(object sender, EventArgs e)
        {
            //var work = gridView1.GetRow(gridView1.FocusedRowHandle) as Work;

            //if (_SendFuction != null)
            //{
            //    _SendFuction(LoadActionWorkOrder.DU_LIEU_KHACH_HANG, work);
            //}

            if (!bgwkCusData.IsBusy)
            {
                bgwkCusData.RunWorkerAsync();
            }
        }

        List<CusData> _CustomerDatas = new List<CusData>();
        private void bgwkCusData_DoWork(object sender, DoWorkEventArgs e)
        {

            string workId = txtWorkID.Text.Trim();
            Work workOrder = new CreateWorkDAO().GetWorkOrderById(workId);

            string billRequest = _RequestImportDAO.CreatBillName(_ThisMachine);

            if (workOrder == null || string.IsNullOrEmpty(workOrder.WorkID))
            {
                MessageBox.Show($"Work {workId} không tồn tại");
                return;
            }
            List < ROSE_Dll.DTO.BomContent > bomContents= new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = workOrder.ModelWork, BomVersion = workOrder.BomVersion });

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XLS files (*.xls, *.xlt, *.xlsb)|*.xls;*.xlt;*xlsb|XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm";
            dlg.FilterIndex = 2;
            this.Invoke((MethodInvoker)delegate
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
            });
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            workbook.LoadFromFile(dlg.FileName);
            Spire.Xls.Worksheet sheet = workbook.Worksheets[0];
            DataTable dt = sheet.ExportDataTable();

            if (dt == null && dt.Rows.Count == 0) return ;
            string sql = string.Empty;
            _CustomerDatas = new List<CusData>();
            string model = workOrder.ModelWork;
            string cusId = workOrder.CusId;
            foreach (DataRow item in dt.Rows)
            {
                string tr_sn = item[0].ToString();
                string work = item[1].ToString();
                if(work.Length == 12)
                    work = work.Substring(2, 10);
                
                string cust_kp_no = item[2].ToString();
                string mfr_kp_no = item[3].ToString();
                string mfr_code = item[4].ToString();
                string date_code = item[5].ToString();
                string lot_code = item[6].ToString();
                string pkg_id = item[8].ToString();

            
                int qty = 0;

                if (string.IsNullOrEmpty(tr_sn))
                {
                    MessageBox.Show("Mã TR_SN không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(work))
                {
                    MessageBox.Show("Work không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(cust_kp_no))
                {
                    MessageBox.Show("Cust_kp_no không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(mfr_kp_no))
                {
                    MessageBox.Show("Mfr_kp_no không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(mfr_code))
                {
                    MessageBox.Show("Mfr_kp_no không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(date_code))
                {
                    MessageBox.Show("date_code không thể trống");
                    return;
                }
                if (string.IsNullOrEmpty(lot_code))
                {
                    MessageBox.Show("lot_code không thể trống");
                    return;
                }
                try
                {
                    qty = int.Parse(item[7].ToString());
                }
                catch (Exception)
                {

                    MessageBox.Show($"Hàng dữ liệu {tr_sn} có số lượng sai định dạng");
                    return;
                }
                if (!_CreatWorkDAO.IsListEmty(_CustomerDatas))
                {
                    var checkTR_SN = _CustomerDatas.Any(x => x.TR_SN == tr_sn);
                    if (checkTR_SN)
                    {
                        MessageBox.Show($"Mã TR_SN - {checkTR_SN} - phải là duy nhất");
                        return;
                    }

                    if(work != workOrder.WorkID)
                    {
                        MessageBox.Show($"Mã TR_SN {tr_sn} có work khác với wo đã chọn {workOrder.WorkID}");
                        return;
                    }
                }
                _CustomerDatas.Add(new CusData()
                {
                    TR_SN = tr_sn,
                    WO = work,
                    CUST_KP_NO = cust_kp_no,
                    MFR_KP_NO = mfr_kp_no,
                    MFR_CODE = mfr_code,
                    DATE_CODE = date_code,
                    LOT_CODE = lot_code,
                    QTY = qty,
                    PKG_ID = pkg_id
                }); ;
            }


            if (_CreatWorkDAO.IsListEmty(_CustomerDatas))
            {
                MessageBox.Show("Không tìm thấy dữ liệu trong file");
                return;
            }
            string error = "";
            List<CusData> research = _CustomerDatas.GroupBy(a => new { a.MFR_KP_NO, a.CUST_KP_NO }).Select(b=>new CusData() { CUST_KP_NO=b.Key.CUST_KP_NO,MFR_KP_NO=b.Key.MFR_KP_NO}).ToList();
            foreach (var item in research)
            {
                if (!bomContents.Any(a => a.CSPart == item.CUST_KP_NO && a.MfgPart == item.MFR_KP_NO))
                    error += $" CSPart={item.CUST_KP_NO}, MFGPart={item.MFR_KP_NO}\r\n";
            }
            if(!string.IsNullOrEmpty(error))
            {
                MessageBox.Show($"Các mã sau chưa có trong bom ver {workOrder.BomVersion} của work {workOrder.WorkID}, model {workOrder.ModelWork} \r\n {error}");
                return;
            }
            //group a by new
            //{
            //    a.CUST_KP_NO,
            //    a.MFR_KP_NO
            //} 
            //foreach (var item in _CustomerDatas.GroupBy(a => { a.MFR_KP_NO,a.CUST_KP_NO}).)
            //{

                //}

            if (_WorkOrderBUS.UploadCustomerData(_CustomerDatas , workOrder, _UserID,model, cusId, billRequest))
            {
                MessageBox.Show($"Thêm dữ liệu thành công - Phiếu nhập {billRequest} ");
                return;
            }
            else
            {
                MessageBox.Show("Lỗi");
                return;
            }
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != MessageBox.Show("Bạn có chắc chắn muốn đóng Wo", "Xác nhận", MessageBoxButtons.OKCancel)) return;
            string work = txtWorkID.Text;
            if (!_CreatWorkDAO.isWorkID(work, out _ModelID, out _CusID, out _Status))
            {
                MessageBox.Show("Work không tồn tại !");
                return;
            }
            if (_Status == "CLOSE")
            {
                MessageBox.Show(" Work đã đóng !");
                return;
            }
            if (!_CreatWorkDAO.CloseWork(work, _ModelID, _UserID))
            {
                MessageBox.Show("Chúc bạn may mắn lần sau ! ");
            }
            else
            {
                MessageBox.Show($"Close work {work} done");

            }
            LoadDataWork(_ModelID, work);

        }

        private void btnUprate_Click(object sender, EventArgs e)
        {
            #region Bỏ
            //OpenFileDialog openfile = new OpenFileDialog();

            //if (DialogResult.OK == openfile.ShowDialog())
            //{

            //    model = "";
            //    if (!openfile.FileName.EndsWith(".xlsx"))
            //    {
            //        MessageBox.Show("yêu cầu chọn đúng file chương trình chạy");
            //        return;
            //    }

            //    ExcelQueryFactory excel = new ExcelQueryFactory(openfile.FileName);
            //    List<pickup_rate> list = new List<pickup_rate>();
            //    var rowslist = from a in excel.Worksheet(0) select a;
            //    float rate = 0F;
            //    foreach (var item in rowslist)
            //    {
            //        if (item[0].ToString() != "")
            //        {
            //            if (!float.TryParse(item[1], out rate))
            //            {
            //                MessageBox.Show("định dạng tỉ lệ hao hụt của mã " + item[0] + "không đúng định dạng, phải là kiểu số thực");
            //                return;
            //            }
            //            pickup_rate temp = new pickup_rate { part_number = item[0], rate = rate };
            //            if (list.Count == 0 || !list.Contains((temp)))
            //            {
            //                list.Add(temp);

            //            }
            //        }
            //    }
            //    string sqlcmd = "";
            //    sqlcmd += $"delete from `TRACKING_SYSTEM`.`PICKUP_RATE_CUS` where CUS='{cbbcustomer_tabWork.SelectedValue.ToString()}' AND MODEL='{cbbmodel_tabWork.Text}';";
            //    foreach (var item in list)
            //    {

            //        sqlcmd += $"INSERT IGNORE INTO `TRACKING_SYSTEM`.`PICKUP_RATE_CUS` (`PART_NUMBER`, `RATE`, `CUS`, `MODEL`) VALUES ('{item.part_number.Trim()}', '{item.rate}', '{cbbcustomer_tabWork.SelectedValue.ToString()}', '{cbbmodel_tabWork.Text}');";
            //    }
            //    if (!mySQL.InsertDataMySQL(sqlcmd))
            //    {
            //        MessageBox.Show("Lỗi");
            //        return;
            //    }
            //    MessageBox.Show("Done!");
            //}

            #endregion
            MessageBox.Show("Chức năng tạm khóa");
        }

    }
}
