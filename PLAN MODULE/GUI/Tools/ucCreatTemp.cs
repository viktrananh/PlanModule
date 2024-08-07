using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Tools
{
    public partial class ucCreatTemp : UserControl
    {
        CreateWorkDAO dTOCreate = new CreateWorkDAO();
        CreateTempDAO createTempDAO = new CreateTempDAO();
        private string _USER;
        public ucCreatTemp(string user)
        {
            InitializeComponent();
            _USER = user;
        }

        public ucCreatTemp()
        {
            InitializeComponent();
        }
        public void receiverCreateTemp(string user)
        {
            _USER = user;
        }
     
        int YEAR = 0;
        int WEEK = 0;
        private void CreatTemp_Load(object sender, EventArgs e)
        {
            DateTime timeNow = dTOCreate.getTimeServer();
            WEEK = createTempDAO.GetIso8601WeekOfYear(timeNow);
            YEAR = timeNow.Year;
            int tempType = 0;
            if (rdoPCB.Checked) tempType = 0;
            if (rdoPanel.Checked) tempType = 1;
            if (rdoNG.Checked) tempType = -1;

            lbWeek.Text = $"Tuần {WEEK}";
            ViewTempInWeek(YEAR, WEEK, tempType);
        }

        void ViewTempInWeek(int year, int week, int typeTem)
        {
            DataTable dt = createTempDAO.GetDataTempForWeek(year, week, typeTem);
            try
            {
                dgvViewTemp.Rows.Clear();
            }
            catch
            {

            }
            foreach (DataRow item in dt.Rows)
            {
                string startTemp = item["TEM_START"].ToString();
                string endTemp = item["TEM_END"].ToString();
                string quatity = item["QUANTITY"].ToString();
                string tempType = int.Parse(item["TEM_TYPE"].ToString()) == 1 ? "Panel" : "PCB";
                dgvViewTemp.Rows.Add(startTemp, endTemp, quatity, tempType);
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            int numsTemp = int.Parse(txtNumber.Text.Trim());
            DateTime timeNow = dTOCreate.getTimeServer();

            int typeTemp = 0;
            if (rdoPCB.Checked) typeTemp = 0;
            if (rdoPanel.Checked) typeTemp = 1;
            if (rdoNG.Checked) typeTemp = -1;
            DataTable dtSerial = createTempDAO.createManuTem(numsTemp, typeTemp, out YEAR, out WEEK);

            if (createTempDAO.istableNull(dtSerial))
            {
                MessageBox.Show("Error !");
                return;
            }
            string temStart = dtSerial.Rows[0][0].ToString();
            string temEnd = dtSerial.Rows[dtSerial.Rows.Count - 1][0].ToString();
            if (!createTempDAO.CreatTemInDB(temStart, temEnd, YEAR, WEEK, numsTemp, _USER, typeTemp))
            {
                MessageBox.Show("Error !");
                return;
            }
            ViewTempInWeek(YEAR, WEEK, typeTemp);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CSV File|*.csv";
            dlg.Title = "Save an CSV File";
            dlg.ShowDialog();
            string fileName = dlg.FileName;
            excel.ToCSV(dtSerial, fileName);
        }

        private void dgvViewTemp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvViewTemp.Rows[e.RowIndex];


            dgvViewTemp.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvViewTemp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvViewTemp.Rows[e.RowIndex];
                string startTem = row.Cells[0].Value.ToString();
                string endtemp = row.Cells[1].Value.ToString();
                int time = int.Parse(row.Cells[2].Value.ToString());
                if (e.ColumnIndex == 4)// delete
                {
                    if (!createTempDAO.DeleteTemp(startTem, endtemp, YEAR, WEEK))
                    {
                        MessageBox.Show("Cancel Error !");
                        return;
                    }
                }

            }
        }

      

        private void rdoPCB_CheckedChanged(object sender, EventArgs e)
        {
            int typeTemp = 0;

            ViewTempInWeek(YEAR, WEEK, typeTemp);
        }

        private void rdoPanel_CheckedChanged(object sender, EventArgs e)
        {
            int typeTemp = 1;

            ViewTempInWeek(YEAR, WEEK, typeTemp);
        }

        private void rdoNG_CheckedChanged(object sender, EventArgs e)
        {
            int typeTemp = -1;

            ViewTempInWeek(YEAR, WEEK, typeTemp);
        }
    }
}
