using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO;

namespace PLAN_MODULE.GUI.Plan.Material_Control
{
    public partial class Work_bokingMaterial : UserControl
    {
        string _Work,_UserID;
        public Work_bokingMaterial(string work_id, string user_id)
        {
            InitializeComponent();
            this._Work = work_id;
            this._UserID = user_id;
            txtwork.Text = work_id;
            txtwork_KeyDown(new object() , new KeyEventArgs(Keys.Enter));
            
        }
        DAO_WorkInfor DAO_WorkInfor = new DAO_WorkInfor();
        List<DTO.work_remain_Part> work_Remain_PartsNeedBook = new List<DTO.work_remain_Part>();
        List<DTO.work_remain_Part> work_Remain_Parts = new List<DTO.work_remain_Part>();
        WorkOrder workOrder;
        private void txtwork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(txtwork.Text)) return;
            WorkOrder workOrder = DAO_WorkInfor.getWorkInfor(txtwork.Text);
            if (string.IsNullOrEmpty(workOrder.WorkID))
            {
                MessageBox.Show("Work không hợp lệ!");
                return;
            }
            gridView1.Columns.Clear();
            gridControl1.DataSource = work_Remain_PartsNeedBook = DAO_WorkInfor.getRemainMainPart(workOrder.WorkID);
            txtModel.Text = workOrder.ModelID;
            txtVerBom.Text = workOrder.bomVersion;
            txtPCBS.Text = workOrder.totalPcs.ToString();
            txtwork.Enabled = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void txtPartBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            gridView2.Columns.Clear();
            gridControl2.DataSource =  DAO_WorkInfor.getPartRemainNotBook(txtPartBook.Text, txtWorkBook.Text);
           
        }

        

        private void label3_Click(object sender, EventArgs e)
        {
            txtwork.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount < 1)
            {
                MessageBox.Show("Không có danh sách mã chọn để book linh kiện!");
                return;
            }
            work_Remain_Parts.Clear();
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                if (gridView2.IsRowSelected(i))
                {
                    work_Remain_Parts.Add(new work_remain_Part { WORK_ID = gridView2.GetRowCellValue(0, "WORK_ID").ToString(), 
                        main_part = gridView2.GetRowCellValue(i, "main_part").ToString(), PART_NUMBER = gridView2.GetRowCellValue(i, "PART_NUMBER").ToString(),
                        qtyPartRemain = int.Parse(gridView2.GetRowCellValue(i, "qtyPartRemain").ToString()) });
                }
            }
            if (DAO_WorkInfor.bookMaterialForWork(work_Remain_PartsNeedBook,
                new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model=workOrder.ModelID,BomVersion=workOrder.bomVersion}), 
                work_Remain_Parts, _UserID))
            {
                MessageBox.Show("Hoàn thành!");
                return;
            }
            MessageBox.Show("Lỗi!");
        }

        private void btnListPartLink_Click(object sender, EventArgs e)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = DAO_WorkInfor.getLinkPart(txtwork.Text);
        }

        private void txtWorkBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(txtWorkBook.Text)) return;
            workOrder = DAO_WorkInfor.getWorkInfor(txtWorkBook.Text);
              
            if (string.IsNullOrEmpty(workOrder.WorkID))
            {
                MessageBox.Show("Work không hợp lệ!");
                return;
            }
            if (!workOrder.ModelID.Contains("-10"))
            {
                MessageBox.Show("Chức năng chỉ dùng cho bool link kiện SMT!");
                return;
            }
            gridView2.Columns.Clear();
            gridControl2.DataSource = DAO_WorkInfor.getPartRemainNotBook(txtPartBook.Text, txtWorkBook.Text);
            
        }
    }
}
