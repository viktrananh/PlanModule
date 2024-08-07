using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DAO;
using System.Threading;

namespace PLAN_MODULE.GUI.Production
{
    public partial class ucPC_Report_Material : UserControl
    {
        public ucPC_Report_Material()
        {
            InitializeComponent();
        }
        static string nhapVao = "Liệu nhập vào sản xuất";
        static string traKho = "Liệu xuất trả kho";
        static string chuyenLieu = "Chuyển liệu";
        
        void showMess(string mess)
        {
            MessageBox.Show(mess, "Thông báo", MessageBoxButtons.YesNo);
        }
        WorkOrder WorkOrder = new WorkOrder();
        CreateWorkDAO workDAO = new CreateWorkDAO();
        DAO_WorkInfor daoWorkOrder = new DAO_WorkInfor();
        
        private void txtWork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            WorkOrder = daoWorkOrder.getWorkInfor(txtWork.Text);
            if(string.IsNullOrEmpty(WorkOrder.WorkID))
            {
                showMess("Thông tin work nhập không tồn tại!");
                return;
            }
            lbBomVer.Text = WorkOrder.bomVersion;
            lbModel.Text = WorkOrder.ModelID;
        }

      

        private void btnGetData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {                
                DataTable dtBill = new DataTable();
                dtBill.Columns.Add("Main Part"); 
                dtBill.Columns.Add("Định mức");
                dtBill.Columns.Add("Tổng work yêu cầu");
                dtBill.Columns.Add("Mã nội bộ (1)");
                dtBill.Columns.Add("Số đã yêu cầu (2)");
                dtBill.Columns.Add("Số đã xuất (3)");
                dtBill.Columns.Add("Còn thiếu ( (4) = 2 - 3 )");
                DataTable dt = workDAO.getDetailBom(WorkOrder.ModelID, WorkOrder.totalPcs, WorkOrder.bomVersion);
                dt.DefaultView.Sort="length asc";
                dt = dt.DefaultView.ToTable();
                List<DTO.Part_export> partExports = daoWorkOrder.getPartExported(WorkOrder.WorkID);
                foreach (DataRow item in dt.Rows)
                {
                    string mainPart = item["MAIN_PART"].ToString().Trim();
                    string partNumber = item["INTER_PART"].ToString().Trim();
                    int totalrequest = int.Parse(item["REQUIRE"].ToString());
                    int capacity = int.Parse(item["QUANTITY"].ToString());
                    int totalExport = 0;
                    if (partNumber.Contains(" "))
                    {
                        string[] partArr = partNumber.Split(' ');
                        for (int i = 0; i < partArr.Length; i++)
                        {
                            if (string.IsNullOrEmpty(partArr[i])) continue;
                            //if(partExports.Any(a=>a.part==partArr[i]))
                            //{
                            //    if(totalExport+ partExports.Where(a => a.part == partArr[i]).First().qty_remain>totalrequest)
                            //    {

                            //    }    
                            //    totalExport+=-totalrequest
                            //}    
                        }
                    }
                            
                    string remain = string.Empty;
                    string rest = string.Empty;
                    string exported = string.Empty;
                    string remainTargets = string.Empty;
                    string missing = string.Empty;
                }                
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
