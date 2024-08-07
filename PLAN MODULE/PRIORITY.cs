using PLAN_MODULE.DAO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class PRIORITY : Form
    {
        CreateWorkDAO dTO = new CreateWorkDAO();
        public PRIORITY()
        {
            InitializeComponent();
        }
        string userID = string.Empty;

        public void reveiverData(string user)
        {
            this.userID = user;
        }



        private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string partNumber = txtPartNumber.Text;
            if (string.IsNullOrEmpty(partNumber)) return;

            if (!dTO.isPartNumber(partNumber))
            {
                MessageBox.Show("Mã nội bộ không tồn tại ! ");
                return;
            }
            string SQL = $"SELECT PART_DID, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.DID_CONTROL_REMAIN where  PART_NUMBER = '{partNumber}' AND  AREA ='Warehouse'and  PART_DID NOT IN (SELECT DID FROM STORE_MATERIAL_DB.PRIORITY_COMPONENTS);";
            DataTable dt = DBConnect.getData(SQL);
            dgvDetail.DataSource = dt;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            string note = txtNote.Text;
            if (string.IsNullOrEmpty(note)) { MessageBox.Show("Vui lòng nhập ly do!"); return; }
            foreach (DataGridViewRow row in dgvDetail.Rows)
            {

                bool checkedCell = (bool)row.Cells["select"].Value;
                string did = row.Cells["did"].Value.ToString();
                string partNumber = row.Cells["partNumber"].Value.ToString();
                string qty = row.Cells["qty"].Value.ToString();
                if (string.IsNullOrEmpty(did)) break;
                if (checkedCell == true)
                {
                    sql += "INSERT INTO `STORE_MATERIAL_DB`.`PRIORITY_COMPONENTS` (`DID`, `PART_NUMBER`, `QTY`, `OP`, `TIME_CREATE`, `NOTE`, `STATUS`)" +
                        $" VALUES ('{did}', '{partNumber}', '{qty}', '{userID}', now(),  '{note}' , '{0}') ON DUPLICATE key update  QTY = {qty};";
                }
            }
            if (DBConnect.InsertMySql(sql))
            {
                MessageBox.Show("PASS");
                return;
            }
            MessageBox.Show("FAIL");
        }
        CheckBox headerCheckBox = new CheckBox();

        void addCheckbox()
        {
            Point headerCellLocation = this.dgvDetail.GetCellDisplayRectangle(2, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            headerCheckBox.Location = new Point(headerCellLocation.X + 120, headerCellLocation.Y + 2);
            headerCheckBox.BackColor = Color.White;
            headerCheckBox.Size = new Size(18, 18);

            //Assign Click event to the Header CheckBox.
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
            dgvDetail.Controls.Add(headerCheckBox);

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "Virtual Error";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "select";
            dgvDetail.Columns.Insert(3, checkBoxColumn);

            //Assign Click event to the DataGridView Cell.
            dgvDetail.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
        }
        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgvDetail.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvDetail.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["select"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }
        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                bool isChecked = true;
                foreach (DataGridViewRow row in dgvDetail.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["select"].EditedFormattedValue) == false)
                    {
                        isChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = isChecked;

            }
        }

        private void PRIORITY_Load(object sender, EventArgs e)
        {
            addCheckbox();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            addCheckbox();
            DataTable DT = DBConnect.getData($"SELECT DID, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.PRIORITY_COMPONENTS where PART_NUMBER = '{txtPartNumber.Text}'; ");
            dgvDetail.DataSource = DT;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            foreach (DataGridViewRow item in dgvDetail.Rows)
            {
                bool checkedCell = (bool)item.Cells["select"].Value;
                string did = item.Cells["did"].Value.ToString();
                string partNumber = item.Cells["partNumber"].Value.ToString();
                string qty = item.Cells["qty"].Value.ToString();
                if (checkedCell)
                {
                    sql += $"DELETE FROM STORE_MATERIAL_DB.PRIORITY_COMPONENTS WHERE DID = '{did}';";
                }
            }
            if (DBConnect.InsertMySql(sql))
            {
                MessageBox.Show("Xóa thành công!");
                return;
            }
            MessageBox.Show("Xóa nỗi");
        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
