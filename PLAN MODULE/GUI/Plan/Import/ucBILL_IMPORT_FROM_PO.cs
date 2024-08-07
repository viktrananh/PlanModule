using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLAN_MODULE.BUS.PLAN;
using PLAN_MODULE.DAO.PLAN;
using ROSE_Dll;

namespace PLAN_MODULE
{
    public partial class ucBILL_IMPORT_FROM_PO : UserControl
    {
        private string _TYPE_BILL;
        private string _THIS_MACHINE = Environment.MachineName;
        private string _BILL_NUMBER;
        private string _modelID, _statusID, _bomversion;
        private string _cusIDCheck = string.Empty;
        private bool _isMaster = false;
        public ucBILL_IMPORT_FROM_PO(string user)
        {
            InitializeComponent();
            this.userID = user;
        }


        public ucBILL_IMPORT_FROM_PO()
        {
            InitializeComponent();
            this.Load += BILL_IMPORT_FROM_PO_Load;
            this.txtFilterStringPO.TextChanged += TxtFilterStringPO_TextChanged;
            this.txtFilterStringImportRequest.TextChanged += TxtFilterStringImportRequest_TextChanged;
            this.btnViewImportRequest.Click += BtnViewImportRequest_Click;
            this.btnCreateRequest.Click += BtnCreateRequest_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnEdit.Click += BtnEdit_Click;
            this.dgvBillDetail.CellEndEdit += DgvBillDetail_CellEndEdit;
            this.btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận hủy phiếu","Question",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {

            }
        }

        private void DgvBillDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvBillDetail.Columns["WORK_ID"].Index)
            {
                if (!CheckWork(dgvBillDetail.CurrentRow.Cells["WORK_ID"].Value.ToString()))
                {
                    dgvBillDetail.CurrentRow.Cells["WORK_ID"].Value = "";
                }
            }
            else if(e.ColumnIndex == dgvBillDetail.Columns["VAT"].Index)
            {
                int Qty = (dgvBillDetail.CurrentRow.Cells["QTY"].Value == null) ? 0 : Convert.ToInt32(dgvBillDetail.CurrentRow.Cells["QTY"].Value);
                float price = (dgvBillDetail.CurrentRow.Cells["UNIT_PRICE"].Value == null) ? 0 : Convert.ToSingle(dgvBillDetail.CurrentRow.Cells["UNIT_PRICE"].Value);
                int VAT = (dgvBillDetail.CurrentRow.Cells["VAT"].Value == null) ? 0 : Convert.ToInt32(dgvBillDetail.CurrentRow.Cells["VAT"].Value);
                double total = Measuarement_Total_cash(price,Qty,VAT);
                dgvBillDetail.CurrentRow.Cells["TOTAL_CASH"].Value = total;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            this.dgvBillDetail.ReadOnly = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận lưu","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                if (this.InsertDataTo_BILL_REQUEST_IMPORT() && InsertDataTo_BILL_IMPORT_CUS())
                {
                    MessageBox.Show("Lưu thành công");
                }
                else
                {
                    MessageBox.Show("Không lưu được");
                }
            }
            this.LoadDataTablePO();
            this.LoadDataImportRequest();
            this.loadVender();
        }

        private void BILL_IMPORT_FROM_PO_Load(object sender, EventArgs e)
        {
            this.LoadDataTablePO();
            this.LoadDataImportRequest();
            this.loadVender();
            DataTable dtWork = dba.GetWorkID();
            WORK_ID.DataSource = dtWork;
            WORK_ID.DisplayMember = "WORK_ID";
            WORK_ID.ValueMember = "WORK_ID";
        }

        private void BtnCreateRequest_Click(object sender, EventArgs e)
        {
            this.LoadDataBillDetailByOrder();
            this.LoadBillNoInContentByPO();
        }

        private void BtnViewImportRequest_Click(object sender, EventArgs e)
        {
            this.LoadDataBillDetailByRequest();
            this.LoadBillNoInContent();
        }

        private void TxtFilterStringImportRequest_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterStringImportRequest.Text.Trim().Length > 0)
            {
                this.LoadDataImportRequestByFilter(txtFilterStringImportRequest.Text);
            }
            else
            {
                this.LoadDataImportRequest();
            }
        }

        private void TxtFilterStringPO_TextChanged(object sender, EventArgs e)
        {
            if (txtFilterStringPO.Text.Trim().Length > 0)
            {
                this.LoadDataPOByFilter(txtFilterStringPO.Text);
            }
            else
            {
                this.LoadDataTablePO();
            }
        }

        private Dba dba = new Dba();
        private static ucBILL_IMPORT_FROM_PO instance;
        public static ucBILL_IMPORT_FROM_PO Instance
        {
            get
            {
                if (instance == null) instance = new ucBILL_IMPORT_FROM_PO(); return instance;
            }
            private set { instance = value; }
        }
        string cusID = string.Empty;
        string userID = string.Empty;
        bool isFoxccon = false;
        public void reciverDataCusBillImport(string user)
        {
            userID = user;
        }
        private void LoadDataTablePO()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã PO", typeof(string)), new DataColumn("Ngày tạo", typeof(string)) });
            DataTable dtPO = dba.GetDataPO();
            if (dtPO.Rows.Count > 0)
            {
                foreach (DataRow r in dtPO.Rows)
                {
                    dt.Rows.Add( r["OrderID"], r["CreateDate"].ToString());
                }
            }
            dgvPO.DataSource = dt;
            try
            {
              
            }
          catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDataPOByFilter(string filterString)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã PO", typeof(string)), new DataColumn("Ngày tạo", typeof(string)) });
            DataTable dtPO = dba.GetDataPO();
            if (dtPO.Rows.Count > 0)
            {
                foreach (DataRow r in dtPO.Rows)
                {
                    if (r["OrderID"].ToString().Contains(filterString))
                    {
                        dt.Rows.Add(r["OrderID"], r["CreateDate"].ToString());
                    }
                }
            }
            dgvPO.DataSource = dt;
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDataImportRequest()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã Phiếu", typeof(string)), new DataColumn("Ngày Nhập", typeof(string)) });
                DataTable dtPO = dba.GetImportRequest();
                if (dtPO.Rows.Count > 0)
                {
                    foreach (DataRow r in dtPO.Rows)
                    {
                        dt.Rows.Add(r["BILL_NUMBER"], r["INTEND_TIME"].ToString());
                    }
                }
                dgvDataImportRequestHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDataImportRequestByFilter(string FilterString)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Mã Phiếu", typeof(string)), new DataColumn("Ngày Nhập", typeof(string)) });
                DataTable dtPO = dba.GetImportRequest();
                if (dtPO.Rows.Count > 0)
                {
                    foreach (DataRow r in dtPO.Rows)
                    {
                        if (r["BILL_NUMBER"].ToString().Contains(FilterString))
                        {
                            dt.Rows.Add(r["BILL_NUMBER"], r["INTEND_TIME"].ToString());
                        }
                    }
                }
                dgvDataImportRequestHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDataBillDetailByRequest()
        {
            dgvBillDetail.Rows.Clear();
            dgvBillDetail.ReadOnly = true;
            string billNumber = dgvDataImportRequestHistory.CurrentRow.Cells[0].Value.ToString();
            DataTable dt = dba.GetDataImportDetial(billNumber);
            if (dt.Rows.Count > 0)
            {
                int index = 0;
                foreach(DataRow r in dt.Rows)
                {
                    index++;
                    object[] obj = new object[9]
                    {
                        index,
                        r["WORK_ID"].ToString(),
                        r["PART_NUMBER"].ToString(),
                        r["UNIT"].ToString(),
                        r["QTY"],
                        r["UNIT_PRICE"],
                        r["CURRENCY"],
                        r["VAT"],
                        r["TOTAL_CASH"]
                    };
                    dgvBillDetail.Rows.Add(obj);
                }
            }
        }
        private void LoadDataBillDetailByOrder()
        {
            dgvBillDetail.Rows.Clear();
            dgvBillDetail.ReadOnly = false;
            string OrderID = dgvPO.CurrentRow.Cells[0].Value.ToString();
            DataTable dt = dba.GetDataPurchaseOrderDetail(OrderID);
            if (dt.Rows.Count > 0)
            {
                int index = 0;
                foreach(DataRow r in dt.Rows)
                {
                    index++;
                    object[] obj = new object[9]
                    {
                        index,
                        "",
                        r["MFGCode"].ToString(),
                        r["Unit"].ToString(),
                        r["Quantity"],
                        r["Price"],
                        r["UnitPrice"],
                        "",
                        ""
                    };
                    dgvBillDetail.Rows.Add(obj);
                    _TYPE_BILL = r["TYPE_PART"].ToString();
                }
            }
        }
        private void LoadBillNoInContentByPO()
        {
            string PO_Number = dgvPO.CurrentRow.Cells[0].Value.ToString();
            lbPONo.Text = PO_Number;
        }
        private void LoadBillNoInContent()
        {
            try
            {
                string billNumber = dgvDataImportRequestHistory.CurrentRow.Cells[0].Value.ToString();
                string PO_Number = dba.GetORDERIDImportDetail(billNumber, out string intend_time, out string vendor);

                lbRequestNo.Text = billNumber;
                lbPONo.Text = PO_Number;
                date_intent.Value = Convert.ToDateTime(intend_time);
                cbxVendor.Text = vendor;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool InsertDataTo_BILL_REQUEST_IMPORT()
        {
            RequestImportDAO requestImportDAO = new RequestImportDAO();
            string billType = "NVL-PO";
            string venderID = string.Empty;
            DataTable dt = dba.GetDataPurchaseOrderDetail(lbPONo.Text);
            int type = 2;
            if(dt.Rows[0]["TYPE_PART"].ToString() == "CCDC")
            {
                type = 1;
                billType = "CCDC-PO";
            }
            string billNumber = requestImportDAO.CreatBillName_PO(_THIS_MACHINE, billType);
            _BILL_NUMBER = billNumber;
            if (isFoxccon)
                venderID = cusID;
            else
                venderID = cbxVendor.SelectedValue.ToString();
            if (dgvBillDetail.Rows.Count<1) return false;
            if (dba.CreateDataBillImport(billNumber, cusID, date_intent.Value.ToString("yyyy/MM/dd HH:mm:ss"), userID,type, venderID,lbPONo.Text))
            {
                MessageBox.Show($"Tạo phiếu {billNumber} thành công !");
                return true;
            }
            return false;
        }
        private bool InsertDataTo_BILL_IMPORT_CUS()
        {
            try
            {
                string billNumber = _BILL_NUMBER;
                if(dgvBillDetail.Rows.Count > 0)
                {
                    foreach (DataGridViewRow r in dgvBillDetail.Rows)
                    {
                        string Work = (r.Cells["WORK_ID"].Value == null) ? "" : r.Cells["WORK_ID"].Value.ToString();
                        if (Work.Length < 1)
                        {
                            MessageBox.Show("Lỗi WORK dòng" + r.Index);
                            return false;
                        }
                    }
                    bool checkInsertData = true;
                    foreach (DataGridViewRow r in dgvBillDetail.Rows)
                    {
                        string Work = (r.Cells["WORK_ID"].Value == null) ? "" : r.Cells["WORK_ID"].Value.ToString();
                        string Part = (r.Cells["PART_NUMBER"].Value == null) ? "" : r.Cells["PART_NUMBER"].Value.ToString(); ;
                        int Qty = (r.Cells["QTY"].Value == null) ? 0 : Convert.ToInt32(r.Cells["QTY"].Value);
                        string UserID = userID;
                        string Unit = (r.Cells["UNIT"].Value == null) ? "" : r.Cells["UNIT"].Value.ToString();
                        float price = (r.Cells["UNIT_PRICE"].Value == null) ? 0 : Convert.ToSingle(r.Cells["UNIT_PRICE"].Value);
                        string currency = (r.Cells["CURRENCY"].Value == null) ? "" : r.Cells["CURRENCY"].Value.ToString();
                        int VAT = (r.Cells["VAT"].Value == null) ? 0 : Convert.ToInt32(r.Cells["VAT"].Value);
                        double Total = (r.Cells["TOTAL_CASH"].Value == null) ? 0 : Convert.ToSingle(r.Cells["TOTAL_CASH"].Value);
                        if(!dba.InsertDataToBILL_IMPORT_CUS(_BILL_NUMBER, Work, Part, Qty, UserID, Unit, price, currency, VAT, Total))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
           
        }
        private void loadVender()
        {
            DataTable dt = DBConnect.getData("SELECT VENDER_NAME, VENDER_ID FROM TRACKING_SYSTEM.DEFINE_VENDER;");
            cbxVendor.DataSource = dt;
            cbxVendor.DisplayMember = "VENDER_NAME";
            cbxVendor.ValueMember = "VENDER_ID";
        }
        private bool CheckWork(string work)
        {
            BusinessGloble globle = new BusinessGloble();
            string workOrder = work;
            if (!globle.isWork(workOrder, out cusID, out _modelID, out _statusID, out _bomversion))
            {
                MessageBox.Show("Work không tồn tại !");
                return false;
            }
            if (workOrder.Contains($"{cusID}0000000"))
            {
                _isMaster = true;
            }
            if (string.IsNullOrEmpty(_cusIDCheck))
            {
                _cusIDCheck = cusID;
            }
            if (cusID != _cusIDCheck)
            {
                MessageBox.Show("Lỗi ! Phiếu không thể tồn tại dữ liệu Work của 2 khách hàng!");
                return false;
            }
            if (_isMaster)
            {
                _bomversion = "Master Bom";
            }
            else
            {
                if (_bomversion == "1.0" || string.IsNullOrEmpty(_bomversion))
                {
                    MessageBox.Show("Lỗi ! Chưa chọn BOM cho work này!");
                    return false;
                }
            }
            return true;
        }
        public double Measuarement_Total_cash(float price, double qty, int VAT)
        {
            double total = (price * qty) * VAT / 100;
            return total;
        }
    }
    #region Dba
    public class Dba
    {
        public ROSE_Dll.sqlClass mySql = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "STORE_MATERIAL_DB", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");
        
        public DataTable GetDataPO()
        {
            string sql = $"Select * from PURCHASE_MANAGEMENT.PurchaseOrders";
            DataTable dt = mySql.GetDataMySQL(sql);
            return dt;
        }
        public DataTable GetImportRequest()
        {
            string sql = "SELECT DISTINCT BILL_REQUEST_IMPORT.BILL_NUMBER,BILL_REQUEST_IMPORT.INTEND_TIME, BILL_STATUS.`STATUS`  FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT INNER JOIN STORE_MATERIAL_DB.BILL_STATUS ON BILL_REQUEST_IMPORT.STATUS_BILL = BILL_STATUS.ID AND BILL_STATUS.`TYPE` = 'NHẬP'  ORDER BY CREATE_TIME DESC;";
            DataTable dt = mySql.GetDataMySQL(sql);
            return dt;
        }

        public DataTable GetDataImportDetial(string bill)
        {
            string cmd = $"SELECT * FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER='{bill}';";
            DataTable dt = mySql.GetDataMySQL(cmd);
            return dt;
        }
        public DataTable GetDataPurchaseOrderDetail(string PO_No)
        {
            string cmd = $"SELECT * FROM PURCHASE_MANAGEMENT.PurchaseOrderDetail2 where OrderID='{PO_No}';";
            DataTable dt = mySql.GetDataMySQL(cmd);
            return dt;
        }
        public string GetORDERIDImportDetail(string bill, out string intend_time, out string vendor)
        {
            string cmd = $"SELECT STORE_MATERIAL_DB.BILL_REQUEST_IMPORT.ORDERID , STORE_MATERIAL_DB.BILL_REQUEST_IMPORT.INTEND_TIME, STORE_MATERIAL_DB.BILL_REQUEST_IMPORT.CUSTOMER FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER='{bill}' group by BILL_NUMBER;";
            DataTable dt = mySql.GetDataMySQL(cmd);
            if (dt.Rows.Count > 0)
            {
                intend_time = dt.Rows[0][1].ToString();
                vendor = dt.Rows[0][2].ToString();
                return dt.Rows[0][0].ToString();
            }
            else
            {
                intend_time = null;
                vendor = "";
                return "";
            }
        }
        public bool InsertDataToBILL_IMPORT_CUS(string BillNumber, string Work, string Part, int Qty, string UserID,string Unit, float price, string currency, int VAT, double Total_Cash)
        {
            string sql = "INSERT INTO STORE_MATERIAL_DB.BILL_IMPORT_CUS (BILL_NUMBER, WORK_ID, PART_NUMBER, QTY, OP, CREATE_TIME, DATE_CREATE,UNIT,UNIT_PRICE, CURRENCY, VAT,TOTAL_CASH) VALUE" +
              $" ('{BillNumber}', '{Work}', '{Part}', '{Qty}', '{UserID}', NOW(), CURDATE(),'{Unit}','{price}','{currency}','{VAT}','{Total_Cash}') " +
              $" ON DUPLICATE key update QTY=QTY+{Qty},CREATE_TIME = NOW(), DATE_CREATE = CURDATE(), VAT = '{VAT}',TOTAL_CASH = '{Total_Cash}', UNIT_PRICE = {price},CURRENCY= '{currency}',UNIT = '{Unit}',WORK_ID = '{Work}';";
            if (DBConnect.InsertMySql(sql))
            {
                return true;
            }
            return false;
        }
        public bool CreateDataBillImport(string billNumber, string cusID, string timeInter, string OP,int typeBill, string venderID, string OrderID)
        {
            string sql = string.Empty;
            sql += "INSERT INTO STORE_MATERIAL_DB.BILL_REQUEST_IMPORT (BILL_NUMBER, CUSTOMER, CREATE_BY, CREATE_TIME, INTEND_TIME, STATUS_BILL, TYPE_BILL, `VENDER_ID`,`ORDERID`)" +
                  $" VALUE ('{billNumber}', '{cusID}',  '{OP}',now(), '{timeInter}', {0} ,'{typeBill}', '{venderID}','{OrderID}');";
            if (DBConnect.InsertMySql(sql))
            {
                return true;
            }
            return false;
        }
        public DataTable GetWorkID()
        {
            string cmd = $"SELECT WORK_ID FROM TRACKING_SYSTEM.WORK_ORDER;";
            DataTable dt = mySql.GetDataMySQL(cmd);
            return dt;
        }
    }
    #endregion
}
