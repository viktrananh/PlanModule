using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class ucTOOLS : UserControl
    {
        DAO.BaseDAO BaseDAO = new DAO.BaseDAO();

        private static ucTOOLS instance;
        public static ucTOOLS Instance
        {
            get { if (instance == null) instance = new ucTOOLS(); return instance; }
            private set { instance = value; }

        }

        Process process = new Process();
        public ucTOOLS()
        {
            InitializeComponent();

           
        }
        public ucTOOLS(string user)
        {
            InitializeComponent();
            System.Data.DataTable dt = DBConnect.getData("SELECT CUSTOMER_NAME, CUSTOMER_ID FROM TRACKING_SYSTEM.DEFINE_CUSTOMER ORDER BY CUSTOMER_NAME;");
            cbCus.DataSource = dt;
            cbCus.DisplayMember = "CUSTOMER_NAME";
            cbCus.ValueMember = "CUSTOMER_ID";
            userID = user;
        }
        DataTable dtTools = new DataTable();
        string userID = "";


        public void receiverTools(string user)
        {
            this.userID = user;
        }
        string description = string.Empty;
        private void txtMfgPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string mfgPart = txtMfgPart.Text;
            if (string.IsNullOrEmpty(mfgPart)) return;
            string masterType = string.Empty;
            string cusPart;
            string partNumber = process.getInterPart(mfgPart, cbCus.SelectedValue.ToString(), out description, out masterType, out cusPart);
            if(masterType !="0") { MessageBox.Show("Mã nội bộ không phải là mã CCDC"); return; }
            if (string.IsNullOrEmpty(partNumber)) { MessageBox.Show("Không tìm thấy mã nội bộ tương ứng"); return; }
            txtInterPart.Text = partNumber;
            lbDescription.Text = description;
            txtCusPart.Text = cusPart;
            txtInterPart.ReadOnly = true;
            txtNums.Focus();
        }
        private void TOOLS_Load(object sender, EventArgs e)
        {
            loadListBill();
            loadVender();
            //dtTools.Clear();
            dtTools = new DataTable();
            dtTools.Columns.Add("CUS Part");
            dtTools.Columns.Add("Inter Part");
            dtTools.Columns.Add("Unit");
            dtTools.Columns.Add("Nums");
            dtTools.Columns.Add("Unit Price");
            dtTools.Columns.Add("Currency");
            dtTools.Columns.Add("VAT");
            dtTools.Columns.Add("Total Cash");
        }
        void loadListBill()
        {
            DataTable DT = DBConnect.getData("SELECT BILL_NUMBER , INTEND_TIME FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER LIKE '%CCDC%' AND STATUS_BILL = 1 ORDER BY CREATE_TIME DESC;");
            dgvListBill.DataSource = DT;
            clearUI();
        }
        List<string> lsPart = new List<string>();
        void loadVender()
        {
            DataTable dt = DBConnect.getData("SELECT VENDER_NAME, VENDER_ID FROM TRACKING_SYSTEM.DEFINE_VENDER;");
            cboVender.DataSource = dt;
            cboVender.DisplayMember = "VENDER_NAME";
            cboVender.ValueMember = "VENDER_NAME";
            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
            lsPart = (from a in dt.AsEnumerable()
                                   select a.Field<string>("VENDER_NAME")).ToList();
            auto.AddRange(lsPart.ToArray<string>());
            cboVender.AutoCompleteCustomSource = auto;
            //cboVender.TextChanged += UpdateAutoCompleteComboBox;
            //cboVender.KeyDown += AutoCompleteComboBoxKeyPress;
        }
        private bool _canUpdate = true;

        private bool _needUpdate = false;

        //private void cboVender_TextChanged(object sender, EventArgs e)
        //{
        //    //HandleTextChanged();
        //}

        private void HandleTextChanged()
        {
            var txt = cboVender.Text;
            var list = from d in lsPart
                       where d.ToUpper().Contains(cboVender.Text.ToUpper())
                       select d;
            if (list.Count() > 0)
            {
                cboVender.DataSource = list.ToList();
                //comboBox1.SelectedIndex = 0;
                var sText = cboVender.Items[0].ToString();
                cboVender.SelectionStart = txt.Length;
                cboVender.SelectionLength = sText.Length - txt.Length;
                cboVender.DroppedDown = true;
                return;
            }
            else
            {
                cboVender.DroppedDown = false;
                cboVender.SelectionStart = txt.Length;
            }
        }

        //private void cboVender_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Back)
        //    {
        //        int sStart = cboVender.SelectionStart;
        //        if (sStart > 0)
        //        {
        //            sStart--;
        //            if (sStart == 0)
        //            {
        //                cboVender.Text = "";
        //            }
        //            else
        //            {
        //                cboVender.Text = cboVender.Text.Substring(0, sStart);
        //            }
        //        }
        //        e.Handled = true;
        //    }
        //}

        private void UpdateAutoCompleteComboBox(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null)
                return;
            string txt = comboBox.Text;
            string foundItem = String.Empty;
            foreach (var item in comboBox.Items)
            {
                string a = item.ToString();
                if (!String.IsNullOrEmpty(txt) && item.ToString().ToLower().StartsWith(txt.ToLower()))
                {
                    foundItem = item.ToString();
                    break;
                }
            }
            
            if (!String.IsNullOrEmpty(foundItem))
            {
                if (String.IsNullOrEmpty(txt) || !txt.Equals(foundItem))
                {
                    comboBox.TextChanged -= UpdateAutoCompleteComboBox;
                    comboBox.Text = foundItem;
                    comboBox.DroppedDown = true;
                    Cursor.Current = Cursors.Default;
                    comboBox.TextChanged += UpdateAutoCompleteComboBox;
                }

                comboBox.SelectionStart = txt.Length;
                comboBox.SelectionLength = foundItem.Length - txt.Length;
            }
            else
                comboBox.DroppedDown = false;
        }
        private void AutoCompleteComboBoxKeyPress(object sender, KeyEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.DroppedDown)
            {
                switch (e.KeyCode)
                {
                    case Keys.Back:
                        int sStart = comboBox.SelectionStart;
                        if (sStart > 0)
                        {
                            sStart--;
                            comboBox.Text = sStart == 0 ? "" : comboBox.Text.Substring(0, sStart);
                        }
                        e.SuppressKeyPress = true;
                        break;
                }

            }
        }


        private void btnAddData_Click(object sender, EventArgs e)
        {
            string currency = string.Empty;
            if (rdoVND.Checked)
                currency = "VND";
            else
                currency = "$";
           
                if (string.IsNullOrEmpty(txtInterPart.Text) || string.IsNullOrEmpty(txtNums.Text) || string.IsNullOrEmpty(cboUnit.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtTotalCasch.Text))
                {

                    MessageBox.Show("Vui lòng nhập đủ các trường dữ liệu"); ;
                    return;

                }               
                DataRow row = dtTools.NewRow();
                row["CUS Part"] = txtCusPart.Text;
                row["Inter Part"] = txtInterPart.Text;
                row["Unit"] = cboUnit.Text;
                row["Nums"] = txtNums.Text;
                row["Unit Price"] = txtPrice.Text;
                row["VAT"] = cboVat.Text;
                row["Currency"] = currency;
                row["Total Cash"] = txtTotalCasch.Text.Replace("₫", "").Replace("$", "").Trim();
                dtTools.Rows.Add(row);
                clearUI();
                dgvView.DataSource = dtTools;
            
        }
        void clearUI()
        {
            foreach (TextBox txt in pnlFunction.Controls.OfType<TextBox>())
            {
                txt.Clear();
            }
        }
        string createBillNumber()
        {
            string bill = string.Empty;
            DataTable dtbill = DBConnect.getData("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT WHERE  DATE(CREATE_TIME) = CURDATE() and TYPE_BILL = 1 ORDER BY ID DESC ;");
            DateTime timeNow = process.getTimeServer();
            if (BaseDAO.istableNull(dtbill))
            {
                bill = timeNow.ToString("ddMMyyyy") + "-01/CCDC-DT";
            }
            else
            {
                bill = timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + "/CCDC-DT";

            }

            lbBillNumber.Text = bill;
            return bill;
        }
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            float shippingfee = 0;
            string bill = createBillNumber();
            if (BaseDAO.istableNull(dtTools))
            {
                return;
            }
            string venderId = cbCus.SelectedValue.ToString();
            foreach (DataRow item in dtTools.Rows)
            {
                int vat = int.Parse(cboVat.Text.Replace("%", ""));
                float totalCash = float.Parse(item["Total Cash"].ToString().Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture);
                //sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_IMPORT_CUS` (`BILL_NUMBER`,`cus_part`, `PART_NUMBER`, `UNIT`, `QTY`, `OP`, `CREATE_TIME`, `DATE_CREATE`, `CHECK_BY`,`WORK_ID`," +
                //    " `UNIT_PRICE`, `CURRENCY`, `VAT`, `TOTAL_CASH`) VALUES " +
                //    $" ('{bill}','{item["CUS Part"]}', '{item["Inter Part"]}', '{item["Unit"]}', '{int.Parse(item["Nums"].ToString())}', '{userID}', now(), CURDATE(), '{userID}', ''," +
                //    $" '{item["Unit Price"]}', '{Currency}', '{vat}', '{totalCash}') ON DUPLICATE KEY UPDATE QTY=QTY+{item["Nums"].ToString()};";
                sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_CUS_INPUT` (`BILL_NUMBER`, `CUS_PART`, `QTY`, `UNIT`, `UNIT_PRICE`, `CURRENCY`, `VAT`, `TOTAL_CASH`) " +
                    $"VALUES ('{bill}', '{item["CUS Part"]}', '{int.Parse(item["Nums"].ToString())}', '{item["Unit"]}','{item["Unit Price"]}', '{item["Currency"].ToString()}', '{vat}', '{totalCash}');";
            }
            if (chkShipping.Checked)
            {
                shippingfee = float.Parse(txtShippingFee.Text, CultureInfo.InvariantCulture);
            }

            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_IMPORT` (`BILL_NUMBER`, `CREATE_TIME`, `INTEND_TIME`, `STATUS_BILL`, `SHIPPING_FEE`, CREATE_BY, TYPE_BILL, `VENDER_ID`,`CUSTOMER`) " +
                $"VALUES ('{bill}', NOW(), '{dtpkTimeEx.Value.ToString("yyyy/MM/dd HH:mm:ss")}', '{1}', '{shippingfee}', '{userID}', '{1}', '{venderId}','{venderId}');";

            if (!DBConnect.InsertMySql(sql))
            {
                loadListBill();
                MessageBox.Show("Lỗi kết nối đến server");
                return;
            }
            btnComfirm.Text = "Tạo phiếu";
            dtTools.Clear();
            dgvView.DataSource = dtTools;
            loadListBill();
            MessageBox.Show($"Tạo thành công mã phiếu {bill}");
            this.TOOLS_Load(sender, e);


        }
        void loaddgvListBillDetail(string bill)
        {

            dgvView.DataSource = DBConnect.getData($@"SELECT CUS_PART,QTY,UNIT,UNIT_PRICE,CURRENCY,VAT,TOTAL_CASH FROM STORE_MATERIAL_DB.BILL_CUS_INPUT where BILL_NUMBER='{bill}'; ");

        }
        private void dgvListBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvListBill.Rows[e.RowIndex];
                string bill = row.Cells["billNumber"].Value.ToString();
                loaddgvListBillDetail(bill);
                lbBillNumber.Text = bill;
            }
        }
        string mes = string.Empty;
        private void btnUpFile_Click(object sender, EventArgs e)
        {
            //string bill = createBillNumber();
            //string vender = cboVender.SelectedValue.ToString();
            //if (cusBillImport.upDataToolsFile(bill, dtpkTimeEx.Value.ToString("yyyy/MM/dd HH:mm:ss"), userID, vender, out mes))
            //{
            //    MessageBox.Show(mes);
            //    return;
            //}
            //MessageBox.Show(mes);
        }
        bool check = false;
        int statusBill = -2;

        private void bntCancel_Click(object sender, EventArgs e)
        {
            string bill = lbBillNumber.Text;
            if (!process.isBillRqInput(bill, out statusBill))
            {
                MessageBox.Show("Bill không tồn tại");
                return;
            }
            if (statusBill != 0 && statusBill != 1)
            {
                MessageBox.Show("Phiếu đã thao tác, vui lòng không hủy");
                return;
            }
            DialogResult dlg = MessageBox.Show($"Bạn có chắc chắn muốn huy phiếu  {bill}", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dlg != DialogResult.Yes) return;
            if (process.cancelBillImport(bill, userID))
            {

                lbBillNumber.Text = "";
                loadListBill();
                MessageBox.Show("Đã hủy phiếu thành công");

                loaddgvListBillDetail(lbBillNumber.Text);
                return;
            }
            MessageBox.Show("Đã hủy phiếu Thất bại");
            //clearUI();
            return;
        }

        private void dgvView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMfgPart.Text = dgvView.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtInterPart.Text= dgvView.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtNums.Text= dgvView.Rows[e.RowIndex].Cells[2].Value.ToString();
                cboUnit.Text= dgvView.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        CultureInfo culture;

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            float unitPrice = float.Parse(txtPrice.Text, CultureInfo.InvariantCulture);
            int request = int.Parse(txtNums.Text);
            int Currency = 0;
            int vat = int.Parse(cboVat.Text.Replace("%", ""));
            if (rdoUSA.Checked)
                Currency = 1;
            getCash(unitPrice, vat, Currency, request, culture);
        }
        void getCash(float unitPrice, int vat, int Currency, int request, CultureInfo culture)
        {
            if(Currency == 0)
            {
                culture = new CultureInfo("vi-VN");
                float sumCasch = unitPrice * request;
                sumCasch += sumCasch * vat / 100;
                txtTotalCasch.Text = sumCasch.ToString("c", culture);

            }
            else
            {
                culture = new CultureInfo("en-us");
                float sumCasch = unitPrice * request;
                sumCasch += sumCasch * vat / 100;
                txtTotalCasch.Text = sumCasch.ToString("c", culture);
            }

        }
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
               
                e.Handled = true;

            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;

            }

        }
        private void txtNums_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void rdoVND_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void rdoUSA_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void txtShippingFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void chkShipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShipping.Checked)
                txtShippingFee.Visible = true;
            else
                txtShippingFee.Visible = false;
            txtShippingFee.Clear();

        }

        private void cboVat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrice.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void txtShippingFee_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtShippingFee.Text)) return;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal value = decimal.Parse(txtShippingFee.Text, System.Globalization.NumberStyles.AllowThousands);
            txtShippingFee.Text = String.Format(culture, "{0:N0}", value);
            txtShippingFee.Select(txtShippingFee.Text.Length, 0);
        }

        private void pnlFunction_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
