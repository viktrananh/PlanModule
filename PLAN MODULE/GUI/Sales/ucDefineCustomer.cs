using PLAN_MODULE.BUS.Sales;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PLAN_MODULE
{
    public partial class ucDefineCustomer : UserControl
    {
        public delegate void BackFunction();
        public event BackFunction Backfunction;


        DefineCustomerBUS defineCustomerBUS = new DefineCustomerBUS();
        readonly int _SelectFunc = -1;
        Customer _Customer = new Customer();

        readonly string _UserID = string.Empty;
        int _Authority ;

        public ucDefineCustomer(int FuctionID, Customer customer, string user, int Authority)
        {
            InitializeComponent();
            this._SelectFunc = FuctionID;
            this._Customer = customer;
            this._UserID = user;
            this._Authority = Authority;
            this.Load += UcDefineCustomer_Load;
        }

        private void UcDefineCustomer_Load(object sender, EventArgs e)
        {
            switch (_SelectFunc)
            {
                case LoadStatusDefineCustomer.CREATE:
                    LoadCreatUI();
                    break;
                case LoadStatusDefineCustomer.UPDATE:
                    txtCustomerName.Text = _Customer.CustomerName;
                    txtCustomerID.Text = _Customer.CustomerID;
                    txtAdrress.Text = string.IsNullOrEmpty(_Customer.Address) ? "NA" : _Customer.Address;
                    txtCompanyName.Text = string.IsNullOrEmpty(_Customer.CompanyName) ? "NA" : _Customer.CompanyName;
                    txtInformation.Text = string.IsNullOrEmpty(_Customer.Information) ? "NA": _Customer.Information;
                    txtNameOP.Text = string.IsNullOrEmpty(_Customer.OpContact) ? "NA" : _Customer.OpContact;
                    txtPosition.Text = "NA";
                    txtEmail.Text = string.IsNullOrEmpty(_Customer.Email) ? "NA" : _Customer.Email;
                    txtPhone.Text = string.IsNullOrEmpty(_Customer.Phone) ? "NA" :  _Customer.Phone;
                    LoadEditUI();
                    break;
            }
        }
        void LoadEditUI()
        {
            btnlock.Enabled = true;
            btnDelete.Enabled = true;
            txtCustomerID.ReadOnly = true;
        }
        void LoadCreatUI()
        {
            btnlock.Enabled = false;
            btnDelete.Enabled = false;
            txtCustomerID.ReadOnly = false;


        }

        void loadDetailCus()
        {
            DataTable dt = DBConnect.getData("SELECT CUSTOMER_NAME, CUSTOMER_ID, CUSTOMER_KEY FROM TRACKING_SYSTEM.DEFINE_CUSTOMER;");

        }
        private void txtCusName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }
        private void txtCusKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }
        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtCustomerID.Focus();
        }

        private void txtCustomerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtCompanyName.Focus();
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtAdrress.Focus();
        }

        private void txtAdrress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtInformation.Focus();
        }

        private void txtInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtNameOP.Focus();
        }

        private void txtNameOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtPosition.Focus();
        }

        private void txtPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtPhone.Focus();
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtEmail.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string cusName = txtCustomerName.Text.Trim();
            string cusID = txtCustomerID.Text.Trim();
            string address = txtAdrress.Text.Trim();
            string companyName = txtCompanyName.Text.Trim();
            string information = txtInformation.Text.Trim();
            string OpName = txtNameOP.Text.Trim();
            string position = txtPosition.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            if (string.IsNullOrEmpty(cusName) || string.IsNullOrEmpty(cusID))
            {
                MessageBox.Show("Lỗi ! Không để trống thông tin khách hàng");
            }
            if(cusID.Length != 3)
            {
                MessageBox.Show("Lỗi ! Mã khách hàng phải là 3 kí tự");
                return;
            }
            if (defineCustomerBUS.CreateCustomer(cusName, cusID, companyName, address, phone, OpName, email, information))
            {
                MessageBox.Show("Cập nhật dữ liệu khách hàng thành công  ");
            }
            else
            {
                MessageBox.Show("Fail ");

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Backfunction != null)
            {
                Backfunction();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
