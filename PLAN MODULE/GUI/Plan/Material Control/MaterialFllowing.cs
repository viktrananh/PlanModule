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
using System.Threading;

namespace PLAN_MODULE.GUI.Plan.Material_Control
{
    public partial class MaterialFllowing : UserControl
    {
        public MaterialFllowing()
        {
            InitializeComponent();
        }
        DAO_WorkInfor DAO_WorkInfor = new DAO_WorkInfor();
        List<WorkOrder> listWorkProduction = new List<WorkOrder>();
        BillImportMaterial BillImportMaterial = new BillImportMaterial();
        BillImportMaterialDAO BillImportMaterialDAO = new BillImportMaterialDAO();
        List<BillImportMaterial> listBillImportMaterial = new List<BillImportMaterial>();
        List<BillImportDetail> listbillImportMaterialDetails = new List<BillImportDetail>();
        List<BillImportMaterial> listbillOver = new List<BillImportMaterial>();
        List<DTO.MainPart_infor> listMainPart_Infors = new List<MainPart_infor>();

        List<DTO.Part_infor> listCsPart_Infors = new List<DTO.Part_infor>();
        //List<ROSE_Dll.DTO.BomContent> bomContents = new List<ROSE_Dll.DTO.BomContent>();
        //ROSE_Dll.DAO.BomDao BomDao = new ROSE_Dll.DAO.BomDao();
        string vender = "";
        
        void ResetUI()
        {
            txtBillImport.Clear();txtBillOver.Clear();txtBillReturn.Clear();txtwork.Clear();
            lbBillImport.Text=lbBillOver.Text=lbBillReturn.Text=lbWorkList.Text = "";
            dgvDetail.DataSource = null;
            listWorkProduction.Clear();
            listBillImportMaterial.Clear();
            listbillOver.Clear();
            listMainPart_Infors.Clear();
            listbillImportMaterialDetails.Clear(); ;
            vender = "";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void txtwork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(txtwork.Text)) return;
            WorkOrder workOrder = DAO_WorkInfor.getWorkInfor(txtwork.Text);
            if(string.IsNullOrEmpty(workOrder.WorkID))
            {
                MessageBox.Show("Work không hợp lệ!");
                return;
            }    
            if(!string.IsNullOrEmpty(vender)&&!workOrder.ModelID.StartsWith(vender))
            {
                MessageBox.Show("Chỉ được nhập các work sản xuất cho cùng 1 khách hàng!");
                return;
            }
            if (string.IsNullOrEmpty(vender))
                vender = workOrder.ModelID.Substring(0, 3);
            listWorkProduction.Add(workOrder);
            lbWorkList.Text += $"{workOrder.WorkID}_{workOrder.ModelID}_{workOrder.totalPcs};";
            txtwork.Clear();

        }

        private void txtBillImport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            BillImportMaterial = BillImportMaterialDAO.GetBillInfor(txtBillImport.Text);
            if(BillImportMaterial==null)
            {
                MessageBox.Show("Mã phiếu nhập không tồn tại!");
                return;
            }
            if (listBillImportMaterial.Any(a => a.BillNumber == BillImportMaterial.BillNumber) || listbillOver.Any(a => a.BillNumber == BillImportMaterial.BillNumber))
            {
                MessageBox.Show("Mã phiếu đã được nhập!");
                return;
            }
            listBillImportMaterial.Add(BillImportMaterial);
            listbillImportMaterialDetails.AddRange(BillImportMaterialDAO.GetBillImportMaterials(BillImportMaterial.BillNumber));
            lbBillImport.Text += $"{BillImportMaterial.BillNumber};";
            txtBillImport.Clear();
        }

        private void txtBillOver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            BillImportMaterial = BillImportMaterialDAO.GetBillInfor(txtBillImport.Text);
            if (BillImportMaterial == null)
            {
                MessageBox.Show("Mã phiếu nhập không tồn tại!");
                return;
            }
            if(listBillImportMaterial.Any(a=>a.BillNumber==BillImportMaterial.BillNumber)||listbillOver.Any(a=>a.BillNumber==BillImportMaterial.BillNumber))
            {
                MessageBox.Show("Mã phiếu đã được nhập!");
                return;
            }    
            listbillOver.Add(BillImportMaterial);
            lbBillImport.Text += $"{BillImportMaterial.BillNumber};";
            txtBillOver.Clear();
        }

        private void btnCreatBIll_Click(object sender, EventArgs e)
        {
           
            Thread cacula = new Thread(() =>
              {
                  DataTable dt = new DataTable();
                  dt.Columns.Add("MainPart", typeof(string));
                  dt.Columns.Add("CusPart", typeof(string));
                  dt.Columns.Add("InterPart", typeof(string));
                  dt.Columns.Add("MFGPart", typeof(string));
                  dt.Columns.Add("Qty_BOM", typeof(string));
                  //dt.Columns.Add("QtyRequest", typeof(string));
                  foreach (var item in listWorkProduction)
                  {
                      dt.Columns.Add(item.WorkID, typeof(string));
                  }
                  foreach (var item in listWorkProduction)
                  {
                      listMainPart_Infors.AddRange(DAO_WorkInfor.getMainPartExportOnWork(item));
                  }
                  foreach (var item in listMainPart_Infors)
                  {

                      DataRow dataRow = dt.NewRow();
                      dataRow[0] = item.mainPart;
                      dataRow[1] = item.csPart;
                      dataRow[2] = item.partNumber;
                      dataRow[3] = item.mfgPart;
                      dataRow[4] = item.qtyBom;
                      for (int i = 5; i < dt.Columns.Count; i++)
                      {
                          if(item.csPart== "339-0020-Z")
                          {
                              string debug = "";
                          }    
                          if (dt.Columns[i].ColumnName == item.workID)
                              dataRow[i] = item.qtyRequest.ToString();
                          else
                              dataRow[i] = "0";
                      }
                      dt.Rows.Add(dataRow);
                  }
                  this.Invoke((MethodInvoker)delegate
                 {
                     dgvDetail.DataSource = dt;
                 });
              });
            cacula.Start();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                dgvDetail.ExportToXlsx(saveFileDialog.FileName);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Thread cacula = new Thread(() =>
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CusPart", typeof(string));
                dt.Columns.Add("Định mức BOM", typeof(int));
                //dt.Columns.Add("QtyRequest", typeof(string));
                foreach (var item in listWorkProduction)
                {
                    dt.Columns.Add(item.WorkID, typeof(string));
                }
                dt.Columns.Add("Tổng cần", typeof(int));
                dt.Columns.Add("Tổng Nhập", typeof(int));
                dt.Columns.Add("Thiếu/Thừa", typeof(int));
                foreach (var item in listWorkProduction)
                {
                    listCsPart_Infors.AddRange(DAO_WorkInfor.getCsPartExportOnWork(item));
                }
                foreach (var item in listCsPart_Infors)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = item.part;
                    dataRow[1] = item.qtyBom;
                    int tongcan = 0;
                    for (int i = 2; i < dt.Columns.Count-3; i++)
                    {
                        if (dt.Columns[i].ColumnName == item.workID)
                        {
                            dataRow[i] = item.qtyRequest;
                            tongcan += item.qtyRequest;
                        }
                        else
                            dataRow[i] = "0";
                    }
                    dataRow[dt.Columns.Count - 3] = tongcan;
                    if (listbillImportMaterialDetails.Any(a => a.cusPart == item.part))
                        dataRow[dt.Columns.Count - 2] = listbillImportMaterialDetails.Where(a => a.cusPart == item.part).Sum(a => a.recivedqty);
                    else
                        dataRow[dt.Columns.Count - 2] = 0;
                    dataRow[dt.Columns.Count - 1] = int.Parse( dataRow[dt.Columns.Count - 3].ToString()) - int.Parse( dataRow[dt.Columns.Count - 2].ToString());
                    dt.Rows.Add(dataRow);
                }
                List<string> remainCusPart = listbillImportMaterialDetails.Where(a =>listCsPart_Infors.Any(b=>b.part== a.cusPart)==false).Select(a=>a.cusPart).ToList();
                foreach (var item in remainCusPart)
                {                    
                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = item;
                    for (int i = 1; i < dt.Columns.Count - 2; i++)
                    {
                        dataRow[i] = 0;
                    }
                    dataRow[dt.Columns.Count - 2] = listbillImportMaterialDetails.Where(a => a.cusPart == item).Sum(a => a.recivedqty);
                    dataRow[dt.Columns.Count - 1] = listbillImportMaterialDetails.Where(a => a.cusPart == item).Sum(a => a.recivedqty);
                    dt.Rows.Add(dataRow);
                }
                this.Invoke((MethodInvoker)delegate
                {
                    dgvDetail.DataSource = dt;
                });
            });
            cacula.Start();
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }
    }
}
