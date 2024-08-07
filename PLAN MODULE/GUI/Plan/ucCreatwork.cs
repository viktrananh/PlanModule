using DocumentFormat.OpenXml.Bibliography;
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

namespace PLAN_MODULE.GUI.Plan
{
    public partial class ucCreatwork : UserControl
    {
        public delegate void SendFuction(int TypeID, string work);
        public event SendFuction _SendFuction;
        CreateWorkDAO _CreatWorkDAO = new CreateWorkDAO();
        CreatWorkBUS _CreatWorkBUS = new CreatWorkBUS();
        bool isPC = false;
        public ucCreatwork(string userID, bool isPc=false)
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
            if(isPC)
                for (int i = 0; i < toolStrip1.Items.Count; i++)
                {
                    if (toolStrip1.Items[i].Name == "btnPlanDetail")
                        toolStrip1.Items[i].Enabled = true;
                    else
                        toolStrip1.Items[i].Enabled = false;
                }
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


            txtWorkID.Clear();
            txtQty.Text = "0";
            txtPCBXout.Text = "0";
            txtWorkParent.Clear();
            txtPO.Clear();
            //BingdingData(dataGridView1);

        }
        void BingdingData(object context)
        {
            if (context != null)
            {
                txtWorkID.DataBindings.Clear();
                txtQty.DataBindings.Clear();
                txtWorkParent.DataBindings.Clear();
                txtPO.DataBindings.Clear();
                txtPCBXout.DataBindings.Clear();
                chkRMA.DataBindings.Clear();
                chkSanple.DataBindings.Clear();
                chkXOut.DataBindings.Clear();
                txtComment.DataBindings.Clear();

                txtWorkID.DataBindings.Add(new Binding("Text", context, "WorkID"));
                txtQty.DataBindings.Add(new Binding("Text", context, "PCS"));
                txtPCBXout.DataBindings.Add(new Binding("Text", context, "PcbXO"));
                txtWorkParent.DataBindings.Add(new Binding("Text", context, "WorkParent"));
                txtPO.DataBindings.Add(new Binding("Text", context, "PO"));
                chkRMA.DataBindings.Add(new Binding("CheckValue", context, "IsRMA", false, DataSourceUpdateMode.OnPropertyChanged));
                chkSanple.DataBindings.Add(new Binding("CheckValue", context, "IsSample", false, DataSourceUpdateMode.OnPropertyChanged));
                chkXOut.DataBindings.Add(new Binding("CheckValue", context, "IsXout", false, DataSourceUpdateMode.OnPropertyChanged));
                txtComment.DataBindings.Add(new Binding("Text", context, "Comment"));
            }
            else
            {
                txtWorkID.Text = "";
                txtQty.Text = "0";
                txtPCBXout.Text = "0";
                txtWorkParent.Text = "";
                txtPO.Text = "";
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
            if (!_CreatWorkDAO.IsModelPTHOnly(model) && _IsRMA != 1)
            {
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
            string ModelOld, cusOld, statusOld, verBomOld;
            int pcbOld, isRMAOld, isXoutOld, pcbXoutOld, isSampOld , tempNumberOld;

            bool ExistWorkID = _CreatWorkDAO.isWorkID(work, out ModelOld, out cusOld, out pcbOld, out statusOld, out verBomOld, out isRMAOld, out isXoutOld, out pcbXoutOld, out isSampOld, out tempNumberOld);
            if (ExistWorkID)
            {
                if (_CreatWorkDAO.IsWorkRunning(work))
                {
                    if(statusOld == "CLOSE")
                    {
                        MessageBox.Show($"Work đã được đóng không thể cập thật thông tin !");
                        return;
                    }
                    if (model != ModelOld)
                    {
                        MessageBox.Show($"Work đã được thêm cho model {ModelOld} !");
                        return;
                    }
                    if(pcbOld > numberPCBS)
                    {
                        MessageBox.Show($"Chỉ được tăng số lượng wo!");
                        return;
                    }
                    if(tempNumberOld > numberTemp)
                    {
                        MessageBox.Show($"Chỉ được tăng số lượng temp cho wo!");
                        return;
                    }
                    if(_IsRMA != isRMAOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo rework khi wo đã sản xuất !");
                        return;
                    }
                    if(_IsXOut != isXoutOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo X - OUT khi wo đã sản xuất !");
                        return;
                    }
                    if(numberPCBXout != pcbXoutOld)
                    {
                        MessageBox.Show($"Không thể cập nhật số lượng x - out khi wo đã sản xuất !");
                        return;
                    }
                    if(_IsSample != isSampOld)
                    {
                        MessageBox.Show($"Không thể cập nhật từ wo thường sang wo mẫu  khi wo đã sản xuất !");
                        return;
                    }
                }
            }
            if (!_CreatWorkBUS.CreateWork(cus, model, work, numberPCBS, _UserID, _IsRMA, _IsXOut, _IsSample, numberPCBXout, workMother, processMother,
                processChild, PO, IsLinkMother, bomVersion, Comment, ExistWorkID , numberTemp))
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
            if (!_CreatWorkBUS.DeleteWork(work, _UserID))
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
                _SendFuction(5, work);
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string Work = txtWorkID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WorkID").ToString();
            string TotalPCS = txtQty.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalPCS").ToString();
            string WorkParent = txtWorkParent.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WorkParent").ToString();
            string Comment = txtComment.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Comment").ToString();
            string PO = txtPO.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PO").ToString();
            txtTempNumber.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TempNumber").ToString();

            int IsRMA = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsRMA").ToString());
            int IsXout = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsXout").ToString());
            string PcbXO = txtPCBXout.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PcbXO").ToString();
            int IsSample = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "IsSample").ToString());


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

        private void btnPlanDetail_Click(object sender, EventArgs e)
        {
            if(!isPC)
            {
                MessageBox.Show("Quyền đã chuyển cho bộ phận sản xuất !");
                return;
            }    
            PlanProduct planDetail = new PlanProduct(_UserID);
            planDetail.Show();
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
            if (!_CreatWorkBUS.CloseWork(work, _ModelID, _UserID))
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
