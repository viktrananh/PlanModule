using DocumentFormat.OpenXml.Presentation;
using PLAN_MODULE.BUS;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DAO.PLAN;
using PLAN_MODULE.DTO;
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

namespace PLAN_MODULE.GUI.Plan.Create_Wo
{
    public partial class ucCustomerData : UserControl
    {
        CreateWorkDAO _CreateWorkDAO = new CreateWorkDAO();
        string _ThisMachine = Environment.MachineName;
        CreateWorkDAO _CreatWorkDAO = new CreateWorkDAO();
        WorkOrderBUS _WorkOrderBUS = new WorkOrderBUS();
        RequestImportDAO _RequestImportDAO = new RequestImportDAO();

        Work _Work = new Work();
        string _UserId;
        public ucCustomerData(Work work,string userId )
        {
            InitializeComponent();
            _Work = work;
            _UserId = userId;

            this.Load += UcCustomerData_Load;
        }
        List<CusData> _CusDatas = new List<CusData>();
        private void UcCustomerData_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            txtWorkId.Text = _Work.WorkID;
            txtModelId.Text = _Work.ModelWork;
            txtBomVer.Text = _Work.BomVersion;
            dtMfgDate.Value = _Work.MFGDate;
            _CusDatas = _CreateWorkDAO.GetCustomerDatas(_Work.WorkID);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = _CusDatas;
            dgvView.DataSource = bindingSource;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnBookMaterial_Click(object sender, EventArgs e)
        {
            if (!bgrwLoadData.IsBusy)
            {
                bgrwLoadData.RunWorkerAsync();
            }
        }

        private void bgrwLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            string workId = _Work.WorkID;
            Work workOrder = new CreateWorkDAO().GetWorkOrderById(workId);


            if (workOrder == null || string.IsNullOrEmpty(workOrder.WorkID))
            {
                MessageBox.Show($"Work {workId} không tồn tại");
                return;
            }


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

            if (dt == null && dt.Rows.Count == 0) return;
            string sql = string.Empty;
            string model = workOrder.ModelWork;
            string cusId = workOrder.CusId;
            foreach (DataRow item in dt.Rows)
            {
                string tr_sn = item[0].ToString();
                string work = item[1].ToString();
                if (work.Length == 12)
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
                if (!_CreatWorkDAO.IsListEmty(_CusDatas))
                {
                    var checkTR_SN = _CusDatas.Any(x => x.TR_SN == tr_sn);
                    if (checkTR_SN)
                    {
                        MessageBox.Show($"Mã TR_SN - {checkTR_SN} - phải là duy nhất");
                        return;
                    }

                    if (work != workOrder.WorkID)
                    {
                        MessageBox.Show($"Mã TR_SN {tr_sn} có work khác với wo đã chọn {workOrder.WorkID}");
                        return;
                    }
                }
                _CusDatas.Add(new CusData()
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


         
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string billRequest = _CreateWorkDAO.IsListEmty(_CusDatas) ? _RequestImportDAO.CreatBillName(_ThisMachine) : _CusDatas.FirstOrDefault().BillNumber;

            if (_CreatWorkDAO.IsListEmty(_CusDatas))
            {
                MessageBox.Show("Không tìm thấy dữ liệu trong file");
                return;
            }


            if (_WorkOrderBUS.UploadCustomerData(_CusDatas, _Work, _UserId, _Work.ModelWork, _Work.CusId, billRequest))
            {
                MessageBox.Show($"Cập nhật dữ liệu thành công - Phiếu nhập {billRequest} ");
                return;
            }
            else
            {
                MessageBox.Show("Lỗi");
                return;
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string tr_sn = txtTR_SN.Text.Trim(); ;
            string work = _Work.WorkID;
            if (work.Length == 12)
                work = work.Substring(2, 10);

            string cust_kp_no = txtCUST_KP_NO.Text.Trim(); ;
            string mfr_kp_no = txtMFR_KP_NO.Text.Trim() ;
            string mfr_code = txtMFR_CODE.Text.Trim();
            string date_code = txtDATE_CODE.Text.Trim();
            string lot_code = txtLOT_CODE.Text.Trim() ;
            string pkg_id = txtPKD_ID.Text.Trim();


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
                qty = int.Parse(txtQty.Text);
            }
            catch (Exception)
            {

                MessageBox.Show($"Hàng dữ liệu {tr_sn} có số lượng sai định dạng");
                return;
            }
            if (!_CreatWorkDAO.IsListEmty(_CusDatas))
            {
                var checkTR_SN = _CusDatas.Any(x => x.TR_SN == tr_sn);
                if (checkTR_SN)
                {
                    MessageBox.Show($"Mã TR_SN - {checkTR_SN} - phải là duy nhất");
                    return;
                }

                if (work != _Work.WorkID)
                {
                    MessageBox.Show($"Mã TR_SN {tr_sn} có work khác với wo đã chọn {_Work.WorkID}");
                    return;
                }
            }
            _CusDatas.Add(new CusData()
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
            });
            gridView1.MoveLast();
        }

        private void txtTR_SN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtPKD_ID.Focus();
        }

        private void txtPKD_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtCUST_KP_NO.Focus();
        }

        private void txtCUST_KP_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtMFR_KP_NO.Focus();
        }

        private void txtMFR_KP_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtMFR_CODE.Focus();
        }

        private void txtMFR_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtDATE_CODE.Focus();
        }

        private void txtDATE_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtLOT_CODE.Focus();
        }

        private void txtLOT_CODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtQty.Focus();
        }
    }
}
