using PLAN_MODULE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KITTING
{
    public partial class VIEW_STATUS_BILL : Form
    {

        public VIEW_STATUS_BILL()
        {
            InitializeComponent();
        }
        string useID = string.Empty;
        private void VIEW_STATUS_BILL_Load(object sender, EventArgs e)
        {
            addUserControl(CONFIRM_BILL_IMPORT.Instance);
            CONFIRM_BILL_IMPORT.Instance.receiverData(useID);
        }


        void addUserControl(UserControl control)
        {
            if (!pnlAdd.Controls.Contains(control))
            {
                pnlAdd.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.BringToFront();
            }
            else
            {
                pnlAdd.Controls.Clear();
                addUserControl(control);
            }
        }

        private void menuBillImport_Click(object sender, EventArgs e)
        {
          
        }

        private void menuBi_Click(object sender, EventArgs e)
        {

        }

        private void billNumberTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addUserControl(CONFIRM_BILL_IMPORT.Instance);
            CONFIRM_BILL_IMPORT.Instance.receiverData(useID);
        }

        private void phêDuyệtPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
