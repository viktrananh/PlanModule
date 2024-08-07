using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.Tree;
using PLAN_MODULE.BUS.Sales;
using PLAN_MODULE.DAO.Sales;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Sales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Sales
{
    public partial class ucDefineModel : UserControl
    {
       

        string[] _LeverPotential = new string[] { "S", "A", "B", "C", "D" };
        string[] _LeverPossibility = new string[] { "S", "A", "B", "C", "D" };
        string[] _LeverPiority = new string[] { "S", "A", "B", "C", "D" };

        DefineModelDAO defineModelDAO = new DefineModelDAO();
        DefineModelBUS defineModelBUS = new DefineModelBUS();

        int _FunctionID = 0;
         string _ModelID = string.Empty;
        Model _Model = new Model();
        readonly Customer _Customer = new Customer();
        public ucDefineModel(int functionID, Customer customer, string modelID)
        {
            InitializeComponent();
            MyInitializeModel();
            _ModelID = modelID;
            _FunctionID = functionID;
            _Customer = customer;
            this.Load += UcDefineModel_Load;
            this.btnAddModelChild.Click += BtnAddModelChild_Click;
            this.btnDeleteModelChild.Click += BtnDeleteModelChild_Click;
        }



        private void UcDefineModel_Load(object sender, EventArgs e)
        {
            txtCustomer.Text = _Customer.CustomerName;
            txtCusID.Text = _Customer.CustomerID;
            txtModel.Mask = $"AAA000";
            if (_FunctionID == LoadStatusDefineModel.CREATE)
            {

            }
            if (_FunctionID == LoadStatusDefineModel.UPDATE)
            {
                LoadUpdateView(_Customer.CustomerID, _ModelID);
            }

          
            //LoadMainClass(typeof(DevComponents.Tree.TreeGX));
        }

        void LoadUpdateView(string cusID, string model)
        {
            _Model = ModelControl.LoadDetailModel(cusID, model);
            txtModel.Text = _Model.ModelID;
            txtCusModel.Text = _Model.CusModel;
            txtGerber.Text = _Model.Gerber;
            txtVerBom.Text = _Model.VerBom;
            txtOpcontact.Text = _Model.OPContact;
            txtPhone.Text = _Model.Phone;
            cbIATF.Checked = _Model.isIATF==1?true:false;
            try
            {
                cboPotential.SelectedValue = _Model.Potential;
                cboPossibility.SelectedValue = _Model.Possibility;
                cboPiority.SelectedValue = _Model.Pioirity;
            }
            catch (Exception)
            {


            }

            //_Model = ModelControl.LoadDetailModel("VLS004");
            LoadMainsModel(_Model);
        }
        
      
        void MyInitializeModel()
        {
            cboPotential.DataSource = _LeverPotential;
            cboPossibility.DataSource = _LeverPossibility;
            cboPiority.DataSource = _LeverPiority;
            nodeModel.Text = "Model";
          
        }

        private void Node_MouseUp(object sender, MouseEventArgs e)
        {
           

            if (e.Button == MouseButtons.Right)
            {
            
            }
        }

        #region TreeView


        private void BtnAddModelChild_Click(object sender, EventArgs e)
        {
            string model = ctmnNode.Tag.ToString();

        }
        private void BtnDeleteModelChild_Click(object sender, EventArgs e)
        {
            string model = ctmnNode.Tag.ToString();
        }
        private Hashtable m_EnumeratedTypes = null;
        private void LoadMainsModel(Model model)
        {
            m_EnumeratedTypes = new Hashtable();
            treeGX1.BeginUpdate();
            treeGX1.Nodes.Clear();
            try
            {
                DevComponents.Tree.Node node = new DevComponents.Tree.Node();
                node.Text = model.ModelID;
                node.Expanded = true;
                //ctmnNode.Tag = model.ModelID;
                //node.ContextMenu = ctmnNode;

                node.Image = new Bitmap(@".\Image\Main.png");
                //m_EnumeratedTypes.Add(GetTypeName(rootType), "");
                treeGX1.Nodes.Add(node);
                List<ModelChild> modelChilds = model.ModelChilds;
                LoadModelChilds(modelChilds, node);
            }
            finally
            {
                treeGX1.EndUpdate();
            }
            m_EnumeratedTypes.Clear();
        }

        private void LoadModelChilds(List<ModelChild> modelChilds, DevComponents.Tree.Node parentNode)
        {
            if (defineModelDAO.IsListEmty(modelChilds)) return;


            // Load Classes first
            foreach (ModelChild child in modelChilds)
            {
                string EOL = child.EOL;
                DevComponents.Tree.Node node = new DevComponents.Tree.Node();
                node.Text = child.ModelID;
                node.Expanded = true;
                if(EOL == "1")
                {
                    node.RenderMode = DevComponents.Tree.eNodeRenderMode.Custom;
                    // Assign renderer, renderers can be reused i.e. assigned to more than one node
                    RedNodeRenderer renderer = new RedNodeRenderer();
                    node.NodeRenderer = renderer;
                }
                //ctmnNode.Tag = child.ModelID;
                //node.ContextMenu = ctmnNode;
                parentNode.Nodes.Add(node);

                List<ModelChild> ModelChilds = child.ModelChilds;
                LoadModelChilds(ModelChilds, node);
            }
        }

        private void treeGX1_NodeMouseUp(object sender, DevComponents.Tree.TreeGXNodeMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeGX tree = sender as TreeGX;
                DevComponents.Tree.Node node = tree.SelectedNode;
                string model = node.Text;
                InNodeContextMenu.Tag = model;
                ShowContextMenu(InNodeContextMenu);
            }
        }
        private void ShowContextMenu(ButtonItem cm)
        {
            cm.Popup(MousePosition);
        }

        void miAdd_Click(object sender, EventArgs e)
        {
            string model = InNodeContextMenu.Tag.ToString();
            fmModelChilds f = new fmModelChilds(_Model, model, LoadStatusDefineModel.CREATE);
            f.ShowDialog();
            _Model = ModelControl.LoadDetailModel(_Customer.CustomerID, _ModelID);
            LoadMainsModel(_Model);

        }
        void miEdit_Click(object sender, EventArgs e)
        {
            string model = InNodeContextMenu.Tag.ToString();
            if (model == _Model.ModelID) return;
            var ModelChilds = _Model.ModelChilds;
           
            fmModelChilds f = new fmModelChilds(_Model, model, LoadStatusDefineModel.UPDATE);
            f.ShowDialog();
            _Model = ModelControl.LoadDetailModel(_Customer.CustomerID, _ModelID);
            LoadMainsModel(_Model);
        }
     
        void miLock_Click(object sender, EventArgs e)
        {

            string model = InNodeContextMenu.Tag.ToString();
            if (model == _Model.ModelID) return;
            if(defineModelBUS.LockModel(_Model.ModelChilds, model))
            {
                MessageBox.Show("Lock Pass");
            }
            else
            {
                MessageBox.Show("Lock Fail");
            }
            _Model = ModelControl.LoadDetailModel(_Customer.CustomerID, _ModelID);

            LoadUpdateView(_Customer.CustomerID, _ModelID);

        }
        void miUnblock_Click(object sender, EventArgs e)
        {
            string model = InNodeContextMenu.Tag.ToString();
            if (model == _Model.ModelID) return;
            if (defineModelBUS.UnLockModel(_Model.ModelChilds, model))
            {
                MessageBox.Show("UnLock Pass");
            }
            else
            {
                MessageBox.Show("UnLock Fail");
            }
            _Model = ModelControl.LoadDetailModel(_Customer.CustomerID, _ModelID);
            LoadUpdateView(_Customer.CustomerID, _ModelID);
        }
        void miDelete_Click(object sender, EventArgs e)
        {

            string model = InNodeContextMenu.Tag.ToString();
            if (model == _Model.ModelID) return;
            if (defineModelBUS.DeleteModel(_Model.ModelChilds, model))
            {
                MessageBox.Show("Delete Pass");

            }
            else
            {
                MessageBox.Show("Delete Fail");

            }
            _Model = ModelControl.LoadDetailModel(_Customer.CustomerID, _ModelID);
            LoadUpdateView(_Customer.CustomerID, _ModelID);
        }
        private void trackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            //treeGX1.Zoom = (float)trackBar1.Value / 100;
            //labelZoom.Text = trackBar1.Value.ToString() + "%";
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            string model = txtModel.Text.Trim();
            string cusModel = txtCusModel.Text.Trim();
            string cusID = _Customer.CustomerID;
            string ger = txtGerber.Text.Trim();
            string bom = txtVerBom.Text.Trim();
            string potential = cboPotential.Text.Trim();
            string ppssi = cboPossibility.Text.Trim();
            string piority = cboPiority.Text.Trim();
            string opcontact = txtOpcontact.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (!model.Contains(cusID))
            {
                MessageBox.Show("Tên sản phẩm sai định dạng");
                txtModel.Focus();
                return;
            }
            if(defineModelBUS.CreateModel(model, cusModel, cusID, ger,bom, opcontact, phone, potential, ppssi, piority, cbIATF.Checked))
            {
                MessageBox.Show("Pass");
                _ModelID = model;
                LoadUpdateView(_Customer.CustomerID, model);

            }
            else
            {
                MessageBox.Show("Fail");

            }

        }

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void txtCusModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.Parse(e.KeyChar.ToString().ToUpper());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnLock_Click(object sender, EventArgs e)
        {

        }
    }
}
