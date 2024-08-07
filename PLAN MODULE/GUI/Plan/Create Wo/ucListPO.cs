using DevExpress.UnitConversion;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
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

namespace PLAN_MODULE.GUI.Plan
{
    public partial class ucListPO : UserControl
    {
        public delegate void SelectFunctionOnPO(int action, POOrder POOrder);
        public event SelectFunctionOnPO _SelectFunctionOnPO;
        public ucListPO()
        {
            InitializeComponent();
        }
        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void ucListPO_Load(object sender, EventArgs e)
        {
            LoadPOs();
        }
        List<POOrder> _POOrders = new List<POOrder>();
        void LoadPOs()
        {
            _POOrders = new List<POOrder>();
            object dataSourcePO = null;
            object context = null;
            _POOrders = POOrderControl.LoadPOOrders();
            dataSourcePO = _POOrders;
            context = _POOrders;
            dgvPOs.DataSource = dataSourcePO;
            UpdateBindings(context);
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            if(_SelectFunctionOnPO != null)
            {
                var po = lbPO.Text.Trim();
                var POOrder = _POOrders.Where(x=>x.PO == po).FirstOrDefault();
                _SelectFunctionOnPO(0, null);
            }
        }
        private void UpdateBindings(object context)
        {

            if (context != null)
            {
                lbPO.DataBindings.Clear();

                lbPO.DataBindings.Add(new Binding("Text", context, "PO"));
              
            }
            else
            {
                lbPO.Text = "";
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (_SelectFunctionOnPO != null)
            {
                var po = lbPO.Text.Trim();
                if (string.IsNullOrEmpty(po)) return;
                var POOrder = _POOrders.Where(x => x.PO == po).FirstOrDefault();
                _SelectFunctionOnPO(1, POOrder);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPOs();
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
          
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var POOrder = gridView1.GetRow(gridView1.FocusedRowHandle) as POOrder;
            if (_SelectFunctionOnPO != null)
            {
                var po = lbPO.Text.Trim();
                if (string.IsNullOrEmpty(po)) return;
                _SelectFunctionOnPO(1, POOrder);
            }
        }
    }
}
