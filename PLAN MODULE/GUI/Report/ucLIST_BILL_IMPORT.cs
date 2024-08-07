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

namespace PLAN_MODULE
{
    public partial class ucLIST_BILL_IMPORT : UserControl
    {
        BusinessGloble globle = new BusinessGloble();
        BussinessCusBillImport cusBillImport = new BussinessCusBillImport();
        private static ucLIST_BILL_IMPORT instance;
        string billType, productType, statusbill, billNumber;
        DateTime startTime, endTime;
        ROSE_Dll.DataLayer DataLayer = new ROSE_Dll.DataLayer();
        public static ucLIST_BILL_IMPORT Instance
        {

            get
            {
                if (instance == null) instance = new ucLIST_BILL_IMPORT(); return instance;
            }
            private set { instance = value; }
        }
        public ucLIST_BILL_IMPORT()
        {
            InitializeComponent();

            //cusBillImport.LoadListBillImportWaitInput(dgvListBillImport);
        }
        string userID;
        int authority;
        public void reciverData(string userID, int authority)
        {
            this.userID = userID;
            this.authority = authority;
        }



        private void LIST_BILL_IMPORT_Load(object sender, EventArgs e)
        {
            //DataLayer.checkaccountGroupCanUse()
            //cusBillImport.LoadListBillImportWaitInput(dgvListBillImport);
            getTypeBill();

        }
        void getTypeBill()
        {
            DataTable dt = globle.mySql.GetDataMySQL($"SELECT EXTEND,`TYPE` FROM STORE_MATERIAL_DB.BILL_TYPE where DEFINE='Nhập';");
            cbtypeProduct.DataSource = dt;
            cbtypeProduct.DisplayMember = "TYPE";
            cbtypeProduct.ValueMember = "EXTEND";
        }
        void loadBillDetail(string billID)
        {

            string cmd = "";
            if (billID.Contains("NVL-DT-RE"))// nhập lại từ sản xuất
            {
                cmd = $@"SELECT BILL_EXPORT_PC.PART_NUMBER as `Mã nội bộ` , MFG as `Nhà sản xuất`,
                    MFG_PART `Mã nhà sản xuất`,VENDER `Nhà cung cấp`,VENDER_PART `Mã nhà cung cấp`,DESCRIPTION `Mô tả`, WORK_ID as WorkOrder,QTY `Số lượng` 
                    FROM STORE_MATERIAL_DB.BILL_EXPORT_PC inner join
                    STORE_MATERIAL_DB.MASTER_PART on BILL_EXPORT_PC.PART_NUMBER = MASTER_PART.PART_NUMBER collate utf8_general_ci and   BILL_EXPORT_PC.BILL_NUMBER = '{billID}'; ";

            }
            else if (billID.Contains("/TP-"))
            {
                cmd = $"SELECT work_id `Work_order`, model_id `Model nội bộ`, cus_model `Model khách`, BOX_SERIAL `Mã Box`, PCBS `Số lượng`, STATE `Trạng thái` FROM STORE_MATERIAL_DB.BILL_IMPORT_PC_FP where bill_number='{billID}';";
            }
            else
            {
                if (cbShowprice.Checked)
                {
                    if (billID.Contains("NVL-DT"))
                    {
                        MessageBox.Show("Phiếu không có giá !");
                        return;
                    }
                    else
                    {
                        cmd = $"SELECT  A.PART_NUMBER AS `Mã nội bộ `, A.UNIT AS `Đơn vị tính`, A.QTY as `Số yêu cầu `, B.QTY as `Thực nhận` ,UNIT_PRICE `Giá` , VAT , total_cash `Tổng giá` from  STORE_MATERIAL_DB.BILL_IMPORT_CUS A left JOIN  STORE_MATERIAL_DB.BILL_IMPORT_WH B  ON A.PART_NUMBER = B.PART_NUMBER AND A.BILL_NUMBER = B.BILL_NUMBER WHERE A.BILL_NUMBER = '{billID}';";
                    }

                }
                else
                {
                    if(billID.Contains("NVL-DT"))
                    {
                        cmd = $"SELECT A.WORK_ID `Work Order` ,  A.PART_NUMBER AS `Mã nội bộ `, A.UNIT AS `Đơn vị tính`, A.QTY as `Số yêu cầu `, B.QTY as `Thực nhận`  from  STORE_MATERIAL_DB.BILL_IMPORT_CUS A left JOIN  STORE_MATERIAL_DB.BILL_IMPORT_WH B  ON A.PART_NUMBER = B.PART_NUMBER AND A.BILL_NUMBER = B.BILL_NUMBER and A.WORK_ID = B.WORK_ID WHERE A.BILL_NUMBER = '{billID}';";

                    }
                    else
                    {
                        cmd = $"SELECT  A.PART_NUMBER AS `Mã nội bộ `, A.UNIT AS `Đơn vị tính`, A.QTY as `Số yêu cầu `, B.QTY as `Thực nhận`  from  STORE_MATERIAL_DB.BILL_IMPORT_CUS A left JOIN  STORE_MATERIAL_DB.BILL_IMPORT_WH B  ON A.PART_NUMBER = B.PART_NUMBER AND A.BILL_NUMBER = B.BILL_NUMBER WHERE A.BILL_NUMBER = '{billID}';";

                    }
                    
                }
            }
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            dgvDetail.DataSource = dt;
            if (globle.IsTableEmty(dt))
            {
                MessageBox.Show("Phiếu nhập không tồn tại");
                //txtBillNhap.Clear();
                return;
            }
        }
        void loadbillStatus(string billID)
        {
            //DataTable dt = globle.mySql.GetDataMySQL($@"select BILL_NUMBER `Mã Phiếu`,CUSTOMER `Khách Hàng`,CREATE_TIME `Thời gian tạo`,
            //            INTEND_TIME `Thời gian nhập`, CREATE_BY `Người tạo`,`STATUS` `Trạng thái` , FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT INNER JOIN STORE_MATERIAL_DB.BILL_STATUS ON 
            //            BILL_REQUEST_IMPORT.STATUS_BILL = BILL_STATUS.ID AND BILL_STATUS.TYPE = '{cbtypeProduct.Text}' WHERE BILL_NUMBER = '{billID}'; ");
            DataTable dt = globle.mySql.GetDataMySQL($@"select A.BILL_NUMBER `Mã Phiếu`, A.CUSTOMER `Khách Hàng`, A.CREATE_TIME `Thời gian tạo`, A.INTEND_TIME `Thời gian nhập`, A.CREATE_BY `Người tạo`, B.`STATUS` `Trạng thái`  ,C.VENDER_NAME `Nhà cung cấp`
FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT A INNER JOIN STORE_MATERIAL_DB.BILL_STATUS B ON A.STATUS_BILL = B.ID AND AND A.TYPE_BILL = B.TYPE_ID
LEFT JOIN TRACKING_SYSTEM.DEFINE_VENDER C  ON A.VENDER_ID = C.VENDER_ID
WHERE A.BILL_NUMBER = '{billID}' ; ");
            dgvDetail.DataSource = dt;
            if (globle.IsTableEmty(dt))
            {
                MessageBox.Show("Phiếu nhập không tồn tại, hoặc đã nhập xong");
                return;
            }
        }

        void loadbillStatusbyTime(string typebill, DateTime start, DateTime end)
        {
            string cmd = "";
            if (cbtimeFillter.Checked)
                cmd = $@"select BILL_NUMBER `Mã Phiếu`,CUSTOMER `Khách Hàng`,CREATE_TIME `Thời gian tạo`,INTEND_TIME `Thời gian nhập`, CREATE_BY `Người tạo`,`STATUS` `Trạng thái` FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT INNER JOIN BILL_STATUS ON 
                    BILL_REQUEST_IMPORT.STATUS_BILL = BILL_STATUS.ID AND BILL_STATUS.TYPE = '{cbtypeProduct.Text}' WHERE BILL_REQUEST_IMPORT.BILL_NUMBER LIKE '%{typebill}'  AND INTEND_TIME >='{start.ToString("yyyy-MM-dd HH:mm:ss")}' AND INTEND_TIME<='{end.ToString("yyyy-MM-dd HH:mm:ss")}' order by  INTEND_TIME desc ; ";
            else
                cmd = $@"select BILL_NUMBER `Mã Phiếu`, CUSTOMER `Khách Hàng`,CREATE_TIME `Thời gian tạo`,INTEND_TIME `Thời gian nhập`, CREATE_BY `Người tạo`,`STATUS` `Trạng thái` FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT INNER JOIN BILL_STATUS ON
                    BILL_REQUEST_IMPORT.STATUS_BILL = BILL_STATUS.ID AND BILL_STATUS.TYPE = '{cbtypeProduct.Text}' WHERE BILL_REQUEST_IMPORT.BILL_NUMBER LIKE '%{typebill}' order by  INTEND_TIME desc; ";
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            dgvDetail.DataSource = dt;
            if (globle.IsTableEmty(dt))
            {
                MessageBox.Show("Không tìm thấy dữ liệu theo yêu cầu!");
                return;
            }
        }
        private void txtBillImport_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Enter) return;
            string billID = txtBillID.Text.Trim().ToUpper();
            if (cbdetail.Checked)
            {
                loadBillDetail(billID);
            }
            else
            {
                loadbillStatus(billID);
            }
        }

        void clearFuntion()
        {
            //foreach (TextBox txt in pnlFunction.Controls.OfType<TextBox>())
            //{
            //    if (txt.Name == "txtBillImport" || txt.Name == "txtRequests" || txt.Name == "txtInputReal" || txt.Name == "txtMissing") continue;
            //    txt.Clear();
            //}
        }

        private void cbtypeBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            //billType = cbtypeBill.Text;
        }
        int typeBill = 0;
        private void cbtypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            productType = cbtypeProduct.Text;
            txtBillID.Clear();
            cbdetail.Checked = false;
            typeBill = getIDTypeBill(cbtypeProduct.SelectedValue.ToString());
        }
        int getIDTypeBill(string type)
        {
            try
            {
                return int.Parse(globle.mySql.GetDataMySQL($"SELECT IDAUTO FROM STORE_MATERIAL_DB.BILL_TYPE where EXTEND = '{type}';").Rows[0][0].ToString());

            }
            catch
            {
                return 0;
            }
        }
        private void cbdetail_CheckedChanged(object sender, EventArgs e)
        {

            //if(string.IsNullOrEmpty(txtBillID.Text)&&cbdetail.Checked)
            //{
            //    MessageBox.Show("Yêu cầu điền mã phiếu để xem chi tiết!");
            //    return;
            //}    
        }
        void loadSumWorkImport(string work, DateTime timeStart, DateTime timeEnd, int typeBill, string type)
        {
            string sql = string.Empty;
            switch (typeBill)
            {
                case 1 :
                    MessageBox.Show($"Không thể kiểm tra theo work với loại phiếu {cbtypeProduct.SelectedValue.ToString()}");
                    break;
                case 2:
                case 3:
                case 9:
                    sql += "SELECT WORK_ID, sum(QTY) AS QTY FROM STORE_MATERIAL_DB.BILL_IMPORT_WH " +
                              $" WHERE WORK_ID = '{work}'  and BILL_NUMBER LIKE '%{type}' AND LAST_TIME > '{timeStart.ToString("yyyy-MM-dd HH:mm:ss")}' AND LAST_TIME < '{timeEnd.ToString("yyyy-MM-dd HH:mm:ss")}';";
                    break;
                case 7:
                case 8:
                    sql += "SELECT WORK_ID `Work`, MODEL_ID `Model`, CUS_MODEL `CusModel`, SUM(PCBS) `Tổng nhập` FROM STORE_MATERIAL_DB.BILL_IMPORT_PC_FP" +
                        $" WHERE WORK_ID = '{work}' AND TIME_CREAT > '{timeStart.ToString("yyyy-MM-dd HH:mm:ss")}' AND TIME_CREAT < '{timeEnd.ToString("yyyy-MM-dd HH:mm:ss")}' AND STATE = 1;";
                    break;                    
            }
            DataTable dt = globle.mySql.GetDataMySQL(sql);
            dgvDetail.DataSource = dt;
        }
        void loadBillStatusWork(string work, DateTime timeStart, DateTime timeEnd, int typeBill, string type)
        {
            string sql = string.Empty;
            switch (typeBill)
            {
                case 1:
                    MessageBox.Show($"Không thể kiểm tra theo work với loại phiếu {cbtypeProduct.SelectedValue.ToString()}");
                    break;
                case 2:
                case 3:
                case 9:
                    sql += "SELECT WORK_ID, sum(QTY) AS QTY FROM STORE_MATERIAL_DB.BILL_IMPORT_WH " +
                              $" WHERE WORK_ID = '{work}'  and BILL_NUMBER LIKE '%{type}' AND LAST_TIME > '{timeStart.ToString("yyyy-MM-dd HH:mm:ss")}' AND LAST_TIME < '{timeEnd.ToString("yyyy-MM-dd HH:mm:ss")}';";
                    break;
                case 7:
                case 8:
                    sql += "SELECT WORK_ID `Work`, MODEL_ID `Model`, CUS_MODEL `CusModel`, SUM(PCBS) `Tổng nhập` FROM STORE_MATERIAL_DB.BILL_IMPORT_PC_FP" +
                        $" WHERE WORK_ID = '{work}' AND TIME_CREAT > '{timeStart.ToString("yyyy-MM-dd HH:mm:ss")}' AND TIME_CREAT < '{timeEnd.ToString("yyyy-MM-dd HH:mm:ss")}' AND STATE = 1;";
                    break;
                default:
                    break;
            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (cbdetail.Checked)
            {
                loadBillDetail(txtBillID.Text);
                return;
            }
            if (!string.IsNullOrEmpty(txtwork.Text))
            {
                loadSumWorkImport(txtwork.Text, dtpfrom.Value, dtpend.Value, typeBill, cbtypeProduct.SelectedValue.ToString());
                return;
            }
            if (string.IsNullOrEmpty(txtBillID.Text))
            {
                loadbillStatusbyTime(cbtypeProduct.SelectedValue.ToString(), dtpfrom.Value, dtpend.Value);
                return;
            }
            else
            {
                loadbillStatus(txtBillID.Text);
                return;
            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetail.Rows.Count < 1) return;
                SaveFileDialog openFileDialog = new SaveFileDialog();
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                if (DialogResult.OK != openFileDialog.ShowDialog()) return;
                DataTable dt = (DataTable)dgvDetail.DataSource;
                excel.ExportToExcel(dt, openFileDialog.FileName);
                //MessageBox.Show("Save file thành công!");
            }
            catch
            {
                return;
            }

            //File.w

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)

        {
            if (e.RowIndex < 0) return;
            txtBillID.Text = dgvDetail.Rows[e.RowIndex].Cells[0].Value.ToString();
            
        
            
            SendKeys.Send("{ENTER}");
        
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillID.Text)) return;
            if (!DataLayer.checkaccountGroupCanUse(userID, ""))
            {
                MessageBox.Show("Không đủ quyền thực hiện chức năng này!");
                return;
            }
        }

        private void btnCreateData_Click(object sender, EventArgs e)
        {
        }
        private void cbShowprice_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowprice.Checked)
            {
                if (!DataLayer.checkaccountGroupCanUse(userID, "SHOW_PRICE_IN_BILL"))
                {
                    MessageBox.Show("Không đủ quyền!");
                    cbShowprice.Checked = false;
                    return;
                }
            }
        }
        private void lbBillImport_Click(object sender, EventArgs e)
        {
            txtBillID.Enabled = true;
        }
        private void txtSumMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        string mes = string.Empty;
        private void dgvListBillImport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtBillID.Focus();
                SendKeys.Send("{ENTER}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
