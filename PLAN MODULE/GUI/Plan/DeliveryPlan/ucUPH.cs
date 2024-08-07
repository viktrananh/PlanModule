using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Plan.DeliveryPlan
{
    public partial class ucUPH : UserControl
    {
        DAO.DaoUPH DaoUPH = new DAO.DaoUPH();
        public string user;
        public ucUPH(string user)
        {
            InitializeComponent();
            this.user = user;
            DaoUPH.LoadModel(ref cbbModel);
            DaoUPH.LoadCluster(ref cbbCluster);
            gridControl1.DataSource = DaoUPH.GetUPHs();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DaoUPH.updateUPH(user, cbbModel.SelectedValue.ToString(), cbbCluster.SelectedValue.ToString(), int.Parse(nbUph.Value.ToString())))
            {
                MessageBox.Show("Cập nhật UPH thành công.");
                gridControl1.DataSource = DaoUPH.GetUPHs();
            }
            else
                MessageBox.Show("Lỗi.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DaoUPH.deleteUPH( cbbModel.SelectedValue.ToString(), cbbCluster.SelectedValue.ToString()))
            {
                MessageBox.Show("Xóa UPH thành công.");
                gridControl1.DataSource = DaoUPH.GetUPHs();
            }
            else
                MessageBox.Show("Lỗi.");
        }
    }
}
