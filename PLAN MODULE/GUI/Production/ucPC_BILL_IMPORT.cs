using LinqToExcel;
using PLAN_MODULE.DAO;
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

namespace PLAN_MODULE
{
    public partial class ucPC_BILL_IMPORT : UserControl
    {
        BusinessPruductBillImPort billImPort = new BusinessPruductBillImPort();
        BusinessGloble globle = new BusinessGloble();

        CreateWorkDAO dTO = new CreateWorkDAO();

        private static ucPC_BILL_IMPORT instance;
        public static ucPC_BILL_IMPORT Instance
        {
            get
            {
                if (instance == null) instance = new ucPC_BILL_IMPORT(); return instance;
            }
            private set { instance = value; }
        }
        string userID = string.Empty;
        string mes = string.Empty;

        string path = ".\\Setting.ini";

        public ucPC_BILL_IMPORT()
        {
            InitializeComponent();
        }
        public ucPC_BILL_IMPORT(string user)
        {
            InitializeComponent();
            this.userID = user;
        }
        void loadBillRequestImportByPC()
        {
            string sql = "SELECT BILL_NUMBER FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE BILL_NUMBER LIKE '%NVL-DT-RE%' ORDER BY ID DESC;";
            DataTable dt = DBConnect.getData(sql);
            dgvListBill.DataSource = dt;
        }
        struct groupBillByCusPart
        {
            public string billID;
            public string cusPart;
            public string partInter;
            public int qty;
        }
        string billnumber = string.Empty;
        private void btnLoad_Click(object sender, EventArgs e)
        {

            if (!bgwkUpfile.IsBusy)
            {
                
                OpenFileDialog openfile = new OpenFileDialog();
                if (DialogResult.OK != openfile.ShowDialog()) return;

                if (!openfile.FileName.EndsWith(".xlsx"))
                {
                    mes = "yêu cầu chọn đúng file ";
                    return;
                }
                bgwkUpfile.RunWorkerAsync(argument: openfile.FileName);
            }
        }
        int reportIndex = 0;
        int maxValue = -1;
        private void bgwkUpfile_DoWork(object sender, DoWorkEventArgs e)
        {
            mes = string.Empty;
            string fileLocal = (string)e.Argument;
            ExcelQueryFactory excel = new ExcelQueryFactory(fileLocal);
            var rowslist = from a in excel.Worksheet(0) select a;
            string cmd = "";
            int count = 0;
            int real = 0;            
            string cusDID = string.Empty;
            string area = string.Empty;
            string partDID = string.Empty;
            int realQtydid = 0;
            string[] vhtAndVtl = new string[] { "VHT", "VTL" };
            List<groupBillByCusPart> groupBillByCusParts = new List<groupBillByCusPart>();
            billnumber = createBillImportMaterial();
            foreach (var item in rowslist)
            {
                if (item[0].Value.ToString() == "TỔNG SỐ CUỘN:")
                {
                    count = int.Parse(item[2].Value.ToString());
                    maxValue = count;
                    continue;
                }
                if (count == 0) continue;
                string did = item[1].Value.ToString();
                if (string.IsNullOrEmpty(did)) break;
                string part = item[2].Value.ToString();
                string stogeUnit = item[3].Value.ToString();
                string subUnit = item[4].Value.ToString();
                string work = item[5].Value.ToString();
                string cusPart = string.Empty;
                int qty = int.Parse(item[6].Value.ToString());
                string Op_pc = item[8].Value.ToString();
                if (!billImPort.iSDIDRestock(did, stock, out partDID, out cusDID, /*out area, */out mes, out realQtydid))
                {
                    return;
                }
                if (part.ToUpper() != partDID.ToUpper())
                {
                    mes = $"Lỗi ! Mã linh kiện của {did} không đúng";
                    return;
                }
                if(realQtydid!=qty)
                {
                    mes = $"Lỗi ! Số lượng của {did} không đúng thực tế hệ thống panacim";
                    return;
                }    
                if (part.ToUpper() != partDID.ToUpper())
                {
                    mes = $"Lỗi ! Mã linh kiện của {did} không đúng thực tế hệ thống panacim";
                    return;
                }
                if (string.IsNullOrEmpty(cusIDCheck))
                {
                    cusIDCheck = cusDID;
                }
                if (cusIDCheck != cusDID && real > 0)
                {
                    if (!(vhtAndVtl.Contains(cusIDCheck) && vhtAndVtl.Contains(cusDID)))
                    {
                        mes = $"Lỗi ! Chỉ được trả dữ liệu cho một khách trong một lần! DID Fail {did}";
                        return;
                    }
                }

               
                cmd += $@"INSERT INTO `STORE_MATERIAL_DB`.`BILL_EXPORT_PC` (`BILL_NUMBER`, `WORK_ID`, `DID`, `PART_NUMBER`, `QTY`, `OP`, `PC_OP`, `STORE_UNIT`, `SUB_UNIT`, `CREATE_TIME`) 
                            VALUES('{billnumber}', '{work}', '{did}', '{partDID}', '{qty}', '{userID}', '{Op_pc}', '{stogeUnit}', '{subUnit}', NOW()); ";
            
                real++;
                reportIndex++;
                bgwkUpfile.ReportProgress(reportIndex);
            }
            if (string.IsNullOrEmpty(cmd) || real == 0 || count == 0)
            {
                mes = $"Lỗi ! File sai định dạng ! Kiểm tra định dạng file";
                return;
            }
            cmd += "INSERT INTO STORE_MATERIAL_DB.BILL_REQUEST_IMPORT (BILL_NUMBER, CUSTOMER, CREATE_BY, CREATE_TIME, STATUS_BILL, TYPE_BILL)" +
                   $" VALUE ('{billnumber}', '{cusDID}', '{userID}', now(), {1}, '{3}');";
            if (real != count && real != 0)
            {
                mes = $"Lỗi ! Kiểm tra lại file, tổng số cuộn ={count}, thực tế có ={real}, chú ý trong file không được ngăt quãng";
                return;
            }
            if (!globle.mySql.InsertDataMySQL(cmd))
            {
                mes = "Lỗi, kiểm tra phiếu đã được up trước đó?";
                return;
            }
            mes = $"Up phiếu {billnumber} Thành công!";
            return;
        }

        string cusIDCheck = string.Empty;


        //29072021-33/NVL-DT-RE
        string createBillImportMaterial()
        {
            string bill = string.Empty;
            DataTable dtbill = globle.mySql.GetDataMySQL("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE  DATE(CREATE_TIME) = CURDATE() and TYPE_BILL = 3 ORDER BY ID DESC; ");
            DateTime timeNow = globle.getTimeServer();
            if (globle.IsTableEmty(dtbill))
            {
                bill = timeNow.ToString("ddMMyyyy") + "-01/NVL-DT-RE";
            }
            else
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + "/NVL-DT-RE";

            }
            return bill;

        }
        DataTable dtBill = new DataTable();
        string[] stock;
        private void PC_BILL_IMPORT_Load(object sender, EventArgs e)
        {
            stock = ROSE_Dll.INI_IO.READ(path, "RESTOCK", "STOCK").Split(',');

            loadBillRequestImportByPC();
            dtBill.Columns.Add("DID");
            dtBill.Columns.Add("WORK_ID");
            dtBill.Columns.Add("PART_NUMBER");
            dtBill.Columns.Add("QTY");
            dtBill.Columns.Add("PC_OP");
            dtBill.Columns.Add("AREA");
            dtBill.Columns.Add("STORE_UNIT");
            dtBill.Columns.Add("SUB_UNIT");

        }

        //private void label93_Click(object sender, EventArgs e)
        //{
        //    txtBillImport.Enabled = true;
        //}

        private void dgvListBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string billNumber = dgvListBill.Rows[e.RowIndex].Cells[0].Value.ToString();
                lbBillNumber.Text = billNumber;
                string cmd = $"SELECT WORK_ID, PART_NUMBER ,DID, QTY, PC_OP, STORE_UNIT, SUB_UNIT, NOTE FROM STORE_MATERIAL_DB.BILL_EXPORT_PC WHERE BILL_NUMBER='{billNumber}';";
                DataTable dt = DBConnect.getData(cmd);
                dgvViewBillNumber.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void timerLoadBIll_Tick(object sender, EventArgs e)
        {
            loadBillRequestImportByPC();
        }


        private void txtTotalReal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        string userData, partDID, workdid, pc_op, areaDID, storageUnit, subUnit = string.Empty;



        

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            dtBill.Clear();
            dgvViewBillNumber.DataSource = dtBill;
            lbBillNumber.Text = ".....";
            //btnCreatBill.Text = "Creat Bill";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string bill = lbBillNumber.Text;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE BILL_NUMBER = '{bill}'; ");
            if (globle.IsTableEmty(dt)) return;
            int statusBill = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            if (statusBill > 1)
            {
                MessageBox.Show("Lỗi ! Phiếu đã thao tác ở kho - không được hủy !");
                return;
            }
            if (statusBill == -1)
            {
                MessageBox.Show("Lỗi ! Phiếu đã HỦY- không được hủy !");
                return;
            }
            string sql = $"UPDATE STORE_MATERIAL_DB.BILL_REQUEST_IMPORT SET STATUS_BILL = -1 WHERE BILL_NUMBER = '{bill}';";
            if (DBConnect.InsertMySql(sql))
            {
                loadBillRequestImportByPC();
                MessageBox.Show("Hủy phiếu thành công!");
                return;
            }
            btnRefesh_Click(sender, e);
            loadBillRequestImportByPC();
            MessageBox.Show("Mất kết lối đến server !");
        }

        private void bgwkUpfile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBar.Maximum = maxValue;
                progressBar.Value = e.ProgressPercentage;
                Application.DoEvents();
            }
            catch { }
        }

        private void bgwkUpfile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadBillRequestImportByPC();
            MessageBox.Show(mes);
            maxValue = -1;
            reportIndex = 0;
        }

      
    }
}
