using PLAN_MODULE.BUS.Sales;
using PLAN_MODULE.DAO.Sales;
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
    public partial class fmModelChilds : Form
    {
        DefineModelBUS defineModelBUS = new DefineModelBUS();
        DefineModelDAO defineModelDAO = new DefineModelDAO();


        Model _Model = new Model();
        readonly int _FunctionID = 0;
        readonly string _ModelParent;
        
        public fmModelChilds(Model model, string modelParent, int functionID)
        {
            InitializeComponent();
            InitializeModelChild();

            _Model = model;
            _ModelParent = modelParent;
            _FunctionID = functionID;
            this.Load += FmModelChilds_Load;
        }
        void InitializeModelChild()
        {
           
            cboProcess.DataSource = LoadMyProcess.myProcesses;
            cboProcess.DisplayMember = "P";
            cboProcess.ValueMember = "P";


        }
        private void FmModelChilds_Load(object sender, EventArgs e)
        {
            

            if (_FunctionID == LoadStatusDefineModel.CREATE)
            {
                txtModelParent.Text = _ModelParent;
                txtCusModel.Text = _Model.CusModel;
            }
            else if (_FunctionID == LoadStatusDefineModel.UPDATE)
            {
                LoadInformationModel(_Model.ModelChilds, _ModelParent);
            }
        }
        void LoadInformationModel(List<ModelChild> modelChildren, string model)
        {
            foreach (var item in modelChildren)
            {
                if (item.ModelID == model)
                {
                    txtModelParent.Text = item.ModelParent;
                    txtModelChild.Text = model;
                    txtModelChild.ReadOnly = true;
                    cboProcess.SelectedValue = item.Process;
                    txtCusModel.Text =item.CusModel;
                    txtGerber.Text = item.Gerber;
                    txtVerBom.Text = item.VerBom;
                    nbPcsOnPanel.Value = item.PcbOnPanel;
                    break;
                }
                else
                {
                    LoadInformationModel(item.ModelChilds, model);
                }
            }
        }
        private void txtProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void txtCusModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void txtModelChild_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string productLine = _Model.ModelID;
            string modelChild = txtModelChild.Text.Trim();
            string cusModel = txtCusModel.Text.Trim();
            string process = cboProcess.Text.Trim().ToString();
            string gerber = txtGerber.Text.Trim();
            string bom = txtVerBom.Text.Trim();
            string cusid = _Model.CusID;
            int pcsOnPanel = (int)nbPcsOnPanel.Value;
            if (!modelChild.Contains(cusid))
            {
                MessageBox.Show("Thông tin Model mới phải bắt đầu bằng mã khách hàng");
                return;
            }
            if (modelChild.Count() != 10)
            {
                MessageBox.Show("Sai định dạng, mã Model phải có số ký tự bằng 10");
                return;
            }
            if (string.IsNullOrEmpty(modelChild) || string.IsNullOrEmpty(cusModel) || string.IsNullOrEmpty(process) ||
                string.IsNullOrEmpty(gerber) || string.IsNullOrEmpty(bom))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            if (_FunctionID == LoadStatusDefineModel.CREATE)
            {
                if (defineModelDAO.IsModelChildExist(modelChild))
                {
                    MessageBox.Show("Lỗi ! Model này đã tồn tại ");
                    return;
                }
            }
            if (defineModelBUS.SaveModelChild(modelChild, cusModel, process, cusid, pcsOnPanel, productLine, _ModelParent, gerber, bom))
            {
                MessageBox.Show("Pass");
                this.Close();

            }
            else
            {
                MessageBox.Show("Fail");
            }
        }
    }
}
