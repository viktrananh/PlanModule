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


namespace PLAN_MODULE.GUI.Account
{
    public partial class fmLogin : Form
    {
        public delegate void ACLogin(string user, string pass);
        public event ACLogin loginEvent;

        public fmLogin()
        {
            InitializeComponent();
        }


        AccountDAO accountDAO = new AccountDAO();
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPwd.Text.Trim();
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPwd.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ tên tài khoản và mật khẩu");
                return;
            }
            string userID, name;
            int autho = -1;

            if (!accountDAO.checkAccount(user, pass, out userID, out name, out autho))
            {
                MessageBox.Show("Sai tên tài khoản mật khẩu ");
                return;
            }
            txtPwd.Clear();
            fmMain fm = new fmMain(user, autho);
            this.Hide();
            fm.ShowDialog();
            this.Show();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();     
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            btnLogIn_Click(sender, e);
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
