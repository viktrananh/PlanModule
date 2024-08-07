using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DAO.PLAN;
using PLAN_MODULE.DTO;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using WarehouseDll.DTO.Material.Import;
using DevExpress.Data.Svg;

namespace PLAN_MODULE
{
    public partial class ucBillImportMaterial : UserControl
    {
        public delegate void SelectFunctionBackListMaterial();

        public event SelectFunctionBackListMaterial _BackFunctionMaterialImport;
        BusinessGloble globle = new BusinessGloble();

        BillRequestImportBUS requestImportBUS = new BillRequestImportBUS();

        BillImportMaterialDAO billImportMaterialDAO = new BillImportMaterialDAO();

        CreateWorkDAO createWorkDAO = new CreateWorkDAO();
        WorkOrder workOrder = new WorkOrder();
        string thisMachine = Environment.MachineName;
        RequestImportDAO requestImportDAO = new RequestImportDAO();

        private int _FunctionID = 0;
        private string _Bill = String.Empty;
        string _StatusBill = string.Empty;
        string _OPBill = string.Empty;
        //string cusID = "";
        string workDefine, modelDefine, cusDefine;
        public ucBillImportMaterial(int functionID, string bill, string work, string StatusBill, string opBill, string UserID)
        {
            InitializeComponent();
            requestImportDAO.LoadCustomer(cbbCusID);
            requestImportDAO.LoadCustomer(cbVender);
            _FunctionID = functionID;
            _Bill = bill;
            _StatusBill = StatusBill;
            _OPBill = opBill;
            _UserID = UserID;
            InitializeDetailBill();
        }

        void InitializeDetailBill()
        {
            if (_FunctionID == LoadFunctionBillImportMaterial.UPDATE)
            {
                groupPanel2.Enabled = false;
                dgvDetail.DataSource = billImportMaterialDAO.GetBillImportMaterials(_Bill);

            }
        }
        string _UserID = string.Empty;
        string mes = string.Empty;
        string note = "";


        private void btnExport_Click(object sender, EventArgs e)
        {
            //dgvDetail.DataSource =  billImportMaterialDAO.GetBillImportMaterials(_Bill);

        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {

            if (!bgrwUpfile.IsBusy)
            {
                bgrwUpfile.RunWorkerAsync();
            }

        }

        private void cbCus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cusID = cbbCusID.SelectedValue.ToString();
            DataTable dt = createWorkDAO.getModelByCus(cusID);
            cbbModel.DataSource = dt;
            cbbModel.DisplayMember = "ID_MODEL";
            cbbModel.ValueMember = "ID_MODEL";
        }

        private void cbbModel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvDetail_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.csv)|*.csv";
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                dgvDetail.ExportToCsv(saveFileDialog.FileName);
            }
        }

        private void bgrwUpfile_DoWork(object sender, DoWorkEventArgs e)
        {
            string billRequest = requestImportDAO.CreatBillName(thisMachine);
            ROSE_Dll.DAO.BomDao bomDao = new ROSE_Dll.DAO.BomDao();
            List<ROSE_Dll.DTO.ManuMasterPart> partMasters = new List<ROSE_Dll.DTO.ManuMasterPart>();
            List<ROSE_Dll.DTO.BomContent> bomContents = new List<ROSE_Dll.DTO.BomContent>();
            string bomversionDefine = "";
            int definedExport = 0;
            cusDefine = modelDefine = workDefine = "";
            string vendor = "";

            BillImport bill = new BillImport();
            bill.BillImportInfors = new List<BillImportInfor>();
            this.Invoke((MethodInvoker)delegate
          {
              vendor = cbVender.SelectedValue.ToString();
              if (cbInputCusOnly.Checked)
                  cusDefine = cbbCusID.SelectedValue.ToString();
              if (cbModel.Checked)
              {
                  modelDefine = cbbModel.SelectedValue.ToString();
                  bomversionDefine = bomDao.getCurrentVer(modelDefine);
              }

          });
            int manuBuy = 0;
            partMasters = bomDao.GetManuMasterParts(vendor);
            if (cbInputCusOnly.Checked)
            {
                note = $"Chỉ nhập và xuất cho khách {cusDefine}";
                definedExport = 1;
            }
            if (cbModel.Checked)
            {
                ROSE_Dll.DTO.BomGeneral bomGeneral = new ROSE_Dll.DTO.BomGeneral() { BomVersion = bomversionDefine, Model = modelDefine };
                if (modelDefine.StartsWith("SEF"))
                    bomContents = bomDao.GetBomContentsAML(bomGeneral);
                else
                    bomContents = bomDao.GetBomContents(bomGeneral);
                note = $"Chỉ nhập và xuất cho model {modelDefine}";
                definedExport = 1;
            }

            if (cbWork.Checked)
            {
                workDefine = txtwork.Text;
                note = $"Chỉ nhập và xuất cho work {workDefine}";
                definedExport = 1;
                workOrder = new DAO_WorkInfor().getWorkInfor(txtwork.Text);
                if (string.IsNullOrEmpty(workOrder.WorkID))
                {
                    MessageBox.Show("Work không tồn tại, kiểm tra lại!");
                    return;
                }
                if (workOrder.state == WorkOrder.stateClose)
                {
                    MessageBox.Show("Work đã đóng, kiểm tra lại!");
                    return;
                }
                if (!workOrder.ModelID.StartsWith(cusDefine))
                {
                    MessageBox.Show($"Work không thuộc khách hàng {cusDefine}, kiểm tra lại!");
                    return;
                }
                if (cbModel.Checked)
                {
                    if (workOrder.ModelID != modelDefine)
                    {
                        MessageBox.Show($"Work không thuộc model {modelDefine}, kiểm tra lại!");
                        return;
                    }
                }

                ROSE_Dll.DTO.BomGeneral bomGeneral = new ROSE_Dll.DTO.BomGeneral() { BomVersion = workOrder.bomVersion, Model = workOrder.ModelID };
                if (modelDefine.StartsWith("SEF"))
                    bomContents = bomDao.GetBomContentsAML(bomGeneral);
                else
                    bomContents = bomDao.GetBomContents(bomGeneral);
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
            int colCount = sheet.Range.Columns.Count();
            int rowCount = sheet.Range.Rows.Count();
            mes = string.Empty;
            string sql = string.Empty;
            //string cus = string.Empty;
            string workCheck = string.Empty;
            int reportWork = 0;

            bool startGetdata = false;
            this.Invoke((MethodInvoker)delegate
            {
                progressBarControl1.Properties.Maximum = rowCount;
                progressBarControl1.EditValue = 0;

            });

            List<string> cusOK = new List<string>();
            for (int i = 0; i < rowCount; i++)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    progressBarControl1.EditValue = i + 1;

                });
                if (sheet.Range.Rows[i].Cells[0].Value.ToString() == "CUS_PART")
                {
                    startGetdata = true;
                    continue;
                }
                if (!startGetdata) continue;
                try
                {
                    string csPart = sheet.Range.Rows[i].Cells[0].Value.ToString();
                    if (string.IsNullOrEmpty(csPart)) break;
                    int Qty = int.Parse(sheet.Range.Rows[i].Cells[1].Value.ToString());
                    string unit = sheet.Range.Rows[i].Cells[2].Value.ToString();
                    if (string.IsNullOrEmpty(unit) || string.IsNullOrEmpty(csPart))
                    {
                        MessageBox.Show("dữ liệu CUS_PART, QTY, UNIT không được bỏ trống!");
                        return;
                    }
                    if (cbModel.Checked || cbWork.Checked)// nhập cho model cụ thể
                    {
                        if (!bomContents.Any(a => a.CSPart == csPart))
                        {
                            mes += csPart + ",";
                        }
                    }
                    else if (!partMasters.Any(a => a.cusPart == csPart))// nhập theo bên mua
                    {
                        mes += csPart + ",";
                    }
                    if (cusOK.Contains(csPart))
                    {
                        MessageBox.Show("Trong 1 phiếu yêu cầu mã khách hàng phải là duy nhất!");
                        return;
                    }

                    BillImportInfor billImportInfor = new BillImportInfor()
                    {
                        BillNumber = billRequest,
                        Part = csPart,
                        Qty = Qty
                    };
                    bill.BillImportInfors.Add(billImportInfor);
                    sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_CUS_INPUT` (`BILL_NUMBER`, `CUS_PART`, `QTY`, `UNIT`) VALUES ('{billRequest}', '{csPart}', '{Qty}', '{unit}');";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                reportWork++;

                bgrwUpfile.ReportProgress(reportWork);
            }
            if (!string.IsNullOrEmpty(mes))
            {
                MessageBox.Show(mes, "Lỗi , các mã khách hàng sau chưa có trong BOM");
                return;
            }
            if (string.IsNullOrEmpty(sql))
            {
                MessageBox.Show($"Không tìm thấy dữ liệu trong file hoặc file không đúng định dạng !");

                return;
            }
            bill.BillNumber = billRequest;
            bill.CusId = vendor;
            bill.OP = _UserID;
            bill.PO = txtPO.Text;
            bill.VenderId = vendor;
            bill.DefineBill = definedExport;

            sql += "INSERT INTO STORE_MATERIAL_DB.BILL_REQUEST_IMPORT (BILL_NUMBER, CUSTOMER, CREATE_BY, CREATE_TIME, INTEND_TIME, " +
                      " STATUS_BILL, TYPE_BILL, `VENDER_ID`,`PO`,`model_id`,`CUS_ID`,`WORK_ID`,`DEFINE_EXPORT`)" +
                      $" VALUE ('{billRequest}', '{vendor}',  '{_UserID}',now(), now(), " +
                      $" {1}, '{2}', '{vendor}','{txtPO.Text}','{modelDefine}','{cusDefine}','{workDefine}','{definedExport}');";
            this.Invoke((MethodInvoker)delegate
            {
                progressBarControl1.EditValue = rowCount;

            });
            if (DBConnect.InsertMySql(sql))
            {
                MessageBox.Show($"Tạo phiếu {billRequest} thành công");
                return;
            }

            MessageBox.Show($"Tạo phiếu thất bại");
            return;
        }

        private void txtwork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

        }
    }
}
