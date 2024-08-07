using DevComponents.DotNetBar.Controls;
using DevExpress.Utils.DragDrop;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class ucBillImportCCDC : UserControl
    {
        public delegate void SelectFunctionBackListMaterial();
        public event SelectFunctionBackListMaterial _BackFunctionMaterialImport;


        private int _FunctionID = 0;
        private string _Bill = String.Empty;
        string _StatusBill = string.Empty;
        string _OPBill = string.Empty;
        public ucBillImportCCDC(int functionID, string bill, string work, string StatusBill, string opBill, string UserID)
        {
            InitializeComponent();
         
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
