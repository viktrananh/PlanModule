using ROSE_Dll.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI
{
    public partial class fmConfirm : Form
    {
        public fmConfirm()
        {
            InitializeComponent();
        }
        AccountDAO accountDAO = new AccountDAO();
        public string _UserId;
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPwd.Text))
            {
                _UserId = string.Empty;
                MessageBox.Show("Vui lòng nhập đủ tên tài khoản và mật khẩu");
                return;
            }
            string userID, name;
            int autho = -1;

            if (!accountDAO.checkAccount(user, pass, out userID, out name, out autho))
            {
                _UserId = string.Empty;
                MessageBox.Show("Sai tên tài khoản mật khẩu ");
                return;
            }
            
            _UserId = user;
            txtUser.Clear();
            txtPwd.Clear();
            this.Close();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtPwd.Focus();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            btnLogIn_Click(sender, e);
        }
    }
}
