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

namespace PLAN_MODULE.GUI.Sales
{
    public partial class ucListCustomer : UserControl
    {
        public ucListCustomer()
        {
            InitializeComponent();
            this.Load += UcListCustomer_Load;

        }
        public delegate void SelectFunction(int statusID, Customer customer);
        public event SelectFunction SelectedFunctionListCus;


        private void UcListCustomer_Load(object sender, EventArgs e)
        {
            InitializeCus();
        }
        List<Customer> _Customers = new List<Customer>();

        void InitializeCus()
        {
            object dataSourceBomDetail = null;
            object context = null;
            _Customers = CustomerControl.LoadCustomer();
            dataSourceBomDetail = _Customers;
            dgvCustomer.DataSource = dataSourceBomDetail;

            context = _Customers;
            UpdateBindings(context);
            lbCountCus.Text = _Customers.Count().ToString();

        }

        bool _IsBingding = false;
        Customer _Customer = new Customer();
        private void UpdateBindings(object context)
        {

            if (context != null)
            {
                lbCusName.DataBindings.Clear();
                lbCusID.DataBindings.Clear();
                lbInformation.DataBindings.Clear();
                lbPhone.DataBindings.Clear();
                lbEmail.DataBindings.Clear();
                lbCusName.DataBindings.Add(new Binding("Text", context, "CustomerName"));
                lbCusID.DataBindings.Add(new Binding("Text", context, "CustomerID"));
                lbInformation.DataBindings.Add(new Binding("Text", context, "Information"));
                lbPhone.DataBindings.Add(new Binding("Text", context, "Phone"));
                lbEmail.DataBindings.Add(new Binding("Text", context, "Email"));

                //_MODEL = lbModel.DataBindings.ToString();
                //_VERSION = lbVersion.Text;
                //_STATUS = lbStatus.Text;
                _IsBingding = true;
            }
            else
            {
                lbCusName.Text = "";
                lbCusID.Text = "";
                lbInformation.Text = "";
                lbPhone.Text = "";
                lbEmail.Text = "";
                _IsBingding = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitializeCus();
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            if(SelectedFunctionListCus != null)
            {
                SelectedFunctionListCus(LoadStatusDefineCustomer.CREATE, null);
            }
        }

        private void btnNewModel_Click(object sender, EventArgs e)
        {
            if (SelectedFunctionListCus != null)
            {
                string cus = lbCusID.Text.Trim();
                Customer SelectCus = _Customers.Where(x=>x.CustomerID== cus).FirstOrDefault();
                SelectedFunctionListCus(LoadStatusDefineCustomer.LOAD_LIST_MODEL, SelectCus);
            }
        }

        private void btnNewModel_Click_1(object sender, EventArgs e)
        {
            if (SelectedFunctionListCus != null)
            {
                string cus = lbCusID.Text.Trim();
                Customer SelectCus = _Customers.Where(x => x.CustomerID == cus).FirstOrDefault();
                SelectedFunctionListCus(LoadStatusDefineCustomer.CREATE_NEW_MODEL, SelectCus);
            }
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (SelectedFunctionListCus != null)
            {
                string cus = lbCusID.Text.Trim();
                Customer SelectCus = _Customers.Where(x => x.CustomerID == cus).FirstOrDefault();
                SelectedFunctionListCus(LoadStatusDefineCustomer.UPDATE, SelectCus);
            }
        }
    }
}
