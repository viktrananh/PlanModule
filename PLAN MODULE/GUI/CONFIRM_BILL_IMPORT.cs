using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class CONFIRM_BILL_IMPORT : UserControl
    {
        BusinessGloble globle = new BusinessGloble();
        BusinessLogIn logIn = new BusinessLogIn();
        private static CONFIRM_BILL_IMPORT instance;
        public static CONFIRM_BILL_IMPORT Instance
        {
            get { if (instance == null) instance = new CONFIRM_BILL_IMPORT(); return instance; }
            private set { instance = value; }
        }
        public CONFIRM_BILL_IMPORT()
        {
            InitializeComponent();
        }
        string userID = string.Empty;
        public void receiverData(string user)
        {
            this.userID = user;
        }
        string useComfirm, nameComfirm, address;
        int authority = -1;
        void loadBillImport()
        {
            DataTable dt = DBConnect.getData("SELECT A.BILL_NUMBER, B.`STATUS` FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT A INNER JOIN STORE_MATERIAL_DB.BILL_STATUS B ON A.STATUS_BILL = B.ID AND ( A.STATUS_BILL = 4 or A.STATUS_BILL = 5 ) ORDER BY A.CREATE_TIME DESC;");
            dgvBill.DataSource = dt;
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string pwd = txtBarcode.Text;
            if (string.IsNullOrEmpty(pwd)) return;
            string sql = string.Empty;
            if (!logIn.checkAccountBC(pwd, out useComfirm, out nameComfirm, out authority))
            {
                MessageBox.Show("Tài khoản không tồn tại");
                txtBarcode.Clear();
                return;
            }
            if (isDep != address)
            {
                MessageBox.Show($"Người xác nhận phải thuộc bộ phận {isDep} ");
                txtBarcode.Clear();
                return;
            }

            if (address.Contains("KHO"))
            {

                sql += $"UPDATE STORE_MATERIAL_DB.BILL_IMPORT_WH_FINISH SET IS_WH='{1}', CONFIRM_WH='{useComfirm}', TIME_WH = now(), COMMENT_WH = '{txtComment.Text}' WHERE BILL_NUMBER='{billNumber}';";
                sql += $"UPDATE STORE_MATERIAL_DB.BILL_REQUEST_IMPORT SET STATUS_BILL = '{3}' WHERE BILL_NUMBER='{billNumber}';";

            }
            if (address.Contains("PLAN"))
            {
                if (rdoOK.Checked)
                {
                    sql += $"UPDATE STORE_MATERIAL_DB.BILL_IMPORT_WH_FINISH SET IS_PLAN='{1}', CONFIRM_PLAN='{useComfirm}', TIME_PLAN = now(), COMMENT_PLAN = '{txtComment.Text}' , STATUS_BILL= 1 WHERE BILL_NUMBER='{billNumber}';";
                    sql += $"UPDATE STORE_MATERIAL_DB.BILL_REQUEST_IMPORT SET STATUS_BILL = '{4}' WHERE BILL_NUMBER='{billNumber}';";
                }
                else
                {
                    sql += $"UPDATE STORE_MATERIAL_DB.BILL_IMPORT_WH_FINISH SET IS_PLAN='{0}', CONFIRM_PLAN='{useComfirm}', TIME_PLAN = now(), COMMENT_PLAN = '{txtComment.Text}' WHERE BILL_NUMBER='{billNumber}';";

                }
            }
            if (DBConnect.InsertMySql(sql))
            {
                MessageBox.Show("Xác nhận thành công!");
                txtBarcode.Clear();
                return;
            }
            MessageBox.Show("Xác nhận thất bại!");
            txtBarcode.Clear();
            loadBillImport();
        }

        public void loadBillImportWH(string bill, DataGridView dgv)
        {
            string sql = $"SELECT A.PART_NUMBER, A.QTY AS QTY_CUS, B.QTY AS QTY_REAL FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS A INNER JOIN STORE_MATERIAL_DB.BILL_IMPORT_WH B ON A.BILL_NUMBER = B.BILL_NUMBER AND A.WORK_ID = B.WORK_ID AND A.PART_NUMBER = B.PART_NUMBER WHERE A.BILL_NUMBER = '{bill}';";
            DataTable dt = DBConnect.getData(sql);
            if (globle.IsTableEmty(dt)) return;
            int index = 0;
            dgv.DataSource = dt;

            foreach (DataRow item in dt.Rows)
            {
                int cusNumber = int.Parse(item["QTY_CUS"].ToString());
                int real = int.Parse(item["QTY_REAL"].ToString());
                if (cusNumber == real)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Lime;

                }
                if (real != cusNumber)
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.Red;

                }
                index++;
            }
        }
        string isDep = string.Empty;
        string billNumber = string.Empty;
        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvBill.Rows[e.RowIndex];
                billNumber = row.Cells["bill"].Value.ToString();
                string status = row.Cells["status"].Value.ToString();
                lbBillNumber.Text = "Bill Number : " + billNumber;
                if (status == "Đã nhập kho - Lỗi")
                {
                    lbComfirm.Text = "Bộ Phận Xác Nhận : KHO";
                    isDep = "KHO";
                }
                if (status == "Thủ kho đã xác nhận chênh lệch")
                {
                    lbComfirm.Text = "Bộ Phận Xác Nhận : KẾ HOẠCH";
                    isDep = "PLAN";
                    pnlCheck.Visible = true;
                }
                loadBillImportWH(billNumber, dgvDetailBill);
            }
        }

        private void CONFIRM_BILL_IMPORT_Load(object sender, EventArgs e)
        {
            loadBillImport();
        }
    }
}
