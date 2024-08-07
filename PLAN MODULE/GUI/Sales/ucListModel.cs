using DevComponents.DotNetBar.Controls;
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
    public partial class ucListModel : UserControl
    {
        public delegate void SelectFunction(int statusID, Customer customer, string ModelID);
        public event SelectFunction SelectedFunctionListModel;

        readonly Customer _Customer = new Customer();
        public ucListModel(Customer cus)
        {
            InitializeComponent();
            _Customer = cus;
            this.Load += UcListModel_Load;
        }

        private void UcListModel_Load(object sender, EventArgs e)
        {
            LoadModels();
        }

        List<Model> _Models = new List<Model>();

        void LoadModels()
        {
            _Models = ModelControl.LoadLsModel(_Customer.CustomerID);
            var dataGridView1 = new BindingList<Model>(_Models);
            dgvModels.DataSource = dataGridView1;
            Binding(dataGridView1);
        }
        void Binding(object context)
        {
            if (context != null)
            {
                lbModel.DataBindings.Clear();
                lbModel.DataBindings.Add(new Binding("Text", context, "ModelID"));
             }
            else
            {
                lbModel.Text = "";
            }
        }
        private void btnCreatModel_Click(object sender, EventArgs e)
        {
            SelectedFunctionListModel(LoadStatusDefineModel.CREATE, _Customer, "") ;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string ModelID = lbModel.Text.Trim();
            SelectedFunctionListModel(LoadStatusDefineModel.UPDATE, _Customer, ModelID);
        }

        private void dgvModels_DoubleClick(object sender, EventArgs e)
        {
           btnEdit_Click(sender, e);
        }

        private void dgvModels_Click(object sender, EventArgs e)
        {
            string Model=  gridView1.GetFocusedRowCellValue("ModelID").ToString();
            lbModel.Text = Model;

        }

     
    }
}
