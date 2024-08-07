using DevExpress.XtraCharts.Native;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using PLAN_MODULE.DAO.Tool;
//using PLAN_MODULE.DTO.Planed.BillImport;
using PLAN_MODULE.DTO.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.GUI.Tools
{
    public partial class ucCheckSharedComponent : UserControl
    {
        CheckSharedComponentDAO _CheckSharedComponentDAO = new CheckSharedComponentDAO();
        List<string> _LsProcess = new List<string>() { "SMT", "PTH", "ASSY" };
        public ucCheckSharedComponent()
        {
            InitializeComponent();
            InitializeCheck();
            this.cboCustomer.SelectedIndexChanged += CboCustomer_SelectedIndexChanged;
        }
        List<CheckSharedCombonentBOM> _LsBOM= new List<CheckSharedCombonentBOM>();
        private void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cusID = cboCustomer.SelectedValue.ToString();
            _LsBOM = new List<CheckSharedCombonentBOM>();
            string Process = cboProcess.Text.Trim();
            _LsBOM = _CheckSharedComponentDAO.GetLsModelBom(cusID, Process);
            //var dataGridView1 = new BindingList<CheckSharedCombonentBOM>(_LsBOM.OrderByDescending(x => x.State).ToList());
            dgvListModel.DataSource = _LsBOM.OrderByDescending(x => x.State);
        }

        void InitializeCheck()
        {
            cboCustomer.DataSource = _CheckSharedComponentDAO.GetCustomer();
            cboCustomer.DisplayMember = "CUSTOMER_NAME";
            cboCustomer.ValueMember = "CUSTOMER_ID";

            cboProcess.DataSource = _LsProcess;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            var a = _LsBOM;
            string cusID = cboCustomer.SelectedValue.ToString();

            List<ROSE_Dll.DTO.BomContent> bomDetails = _CheckSharedComponentDAO.GetLsBomDetail(_LsBOM, cusID);
            var ls = bomDetails.Where(x => x.MainPart.Length>6).ToList();
            var customData = (from r in ls
                              select new
                              {
                                  Vender = r.CSPart,
                                  PartNumber = r.InterPart,
                                  mfgPart = r.MfgPart,
                                  ModelID = r.MainPart.Substring(0, 10),
                                  LCT = r.Location,
                                  QTT = r.Quantity,
                                  
                              }).ToList();
            dgvViewDetail.DataSource = customData.OrderBy(x => x.PartNumber).ToList(); 

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "DownloadFile" + ".xlsx";

            if (DialogResult.OK != dlg.ShowDialog()) return;
            dgvViewDetail.ExportToXlsx(dlg.FileName);

         
        }

        private void cboCustomer_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
