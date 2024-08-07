using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE
{
    public partial class ucListBilExport : UserControl
    {
        public ucListBilExport()
        {
            InitializeComponent();
        }
        BusinessGloble globle = new BusinessGloble();       
        void getTypeBill()
        {
            DataTable dt = globle.mySql.GetDataMySQL($"SELECT TYPE, IDAUTO FROM STORE_MATERIAL_DB.BILL_TYPE where DEFINE='Xuất';");
            cbtypeProduct.DataSource = dt;
            cbtypeProduct.DisplayMember = "TYPE";
            cbtypeProduct.ValueMember = "IDAUTO";
        }
        private void LIST_BILL_EXPORT_Load(object sender, EventArgs e)
        {
            getTypeBill();
            this.cbtypeProduct.SelectedIndexChanged += new System.EventHandler(this.cbtypeProduct_SelectedIndexChanged);

        }
        int getbillExportTPstatus(string billID)
        {
            try
            {
                return int.Parse(globle.mySql.GetDataMySQL($"SELECT STATUS_BILL FROM TRACKING_SYSTEM.DELIVERY_BILL where BILL_NUMBER='{billID}';").Rows[0][0].ToString());
            }
            catch
            {
                return -2;
            }
        }
        int getbillExportNVLType(string billID)
        {
            try
            {
                return int.Parse(globle.mySql.GetDataMySQL($"SELECT TYPE_BILL FROM STORE_MATERIAL_DB.BILL_REQUEST_EX WHERE BILL_NUMBER='{billID}';").Rows[0][0].ToString());
            }
            catch
            {
                return -2;
            }
        }
        void loadBillDetail(string billID)
        {
            if (string.IsNullOrEmpty(billID)) return;
            DataTable dt;
            string cmd = "";
            int billstatus;
            if (billID.EndsWith("/TP"))//xuất thành phẩm
            {
                billstatus = getbillExportTPstatus(billID);

                switch (billstatus)
                {
                    case -2:
                        MessageBox.Show("Không tìm thấy dữ liệu phiếu xuất này!");
                        break;
                    case 1:
                        cmd = $@"SELECT DELIVERY_BILL.WORK_ID WorkOder, DELIVERY_BILL.CUS_MODEL `Model khách hàng`, DELIVERY_BILL.CUS_CODE `Mã code`, DELIVERY_BILL.UNIT `Đơn vị`, 
                            DELIVERY_BILL.Model `Model nội bộ`, DELIVERY_BILL.NUMBER_REQUEST `Số lượng yêu cầu`, EXPORT_FINISHED_PRODUCTS.EXPORTS_REAL `Thực xuẩt`,
                            EXPORT_FINISHED_PRODUCTS.EXPORTS_REAL - DELIVERY_BILL.NUMBER_REQUEST `Chênh lệch`,
                            DELIVERY_BILL.created_by `Người tạo`,
                             DELIVERY_BILL.date_created `Thời gian tạo`, DELIVERY_BILL.date_exports `Thời gian yêu cầu xuất` FROM TRACKING_SYSTEM.DELIVERY_BILL inner join
                            TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS on
                            TRACKING_SYSTEM.DELIVERY_BILL.BILL_NUMBER = TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS.BILL_NUMBER and DELIVERY_BILL.BILL_NUMBER = '{billID}'; ";
                        break;
                    default:
                        cmd = $@"SELECT DELIVERY_BILL.WORK_ID WorkOder, DELIVERY_BILL.CUS_MODEL `Model khách hàng`, DELIVERY_BILL.CUS_CODE `Mã code`, DELIVERY_BILL.UNIT `Đơn vị`, 
                                DELIVERY_BILL.Model `Model nội bộ`, DELIVERY_BILL.NUMBER_REQUEST `Số lượng yêu cầu`,BILL_STATUS.`STATUS` `Trạng thái`, DELIVERY_BILL.created_by `Người tạo`,
                                 DELIVERY_BILL.date_created `Thời gian tạo`, DELIVERY_BILL.date_exports `Thời gian yêu cầu xuất` FROM TRACKING_SYSTEM.DELIVERY_BILL
                                inner join BILL_STATUS on DELIVERY_BILL.STATUS_BILL=BILL_STATUS.ID and BILL_STATUS.`TYPE`='XUẤT THÀNH PHẨM'
                                and DELIVERY_BILL.BILL_NUMBER='{billID}';";
                        break;
                }
                dt = globle.mySql.GetDataMySQL(cmd);
            }
            else// xuất nguyên vật liệu
            {
                billstatus = getbillExportNVLType(billID);
                switch (billstatus)
                {
                    case -2:
                        MessageBox.Show("Không tìm thấy dữ liệu phiếu xuất này!");
                        break;
                    case 1://CCDC
                        cmd = $"SELECT  A.Part_number `Mã nội bộ`, A.QTY`Số lượng yêu cầu`, B.QTY `Số Lượng thực xuất` FROM STORE_MATERIAL_DB.REQUEST_EXPORT A INNER JOIN  STORE_MATERIAL_DB.BILL_EXPORT_WH B on  A.PART_NUMBER =B.PART_NUMBER where A.BILL_NUMBER='{billID}';";
                        break;
                    case 5:
                    case 4://NVL
                        cmd = $"SELECT A.WORK_ID `WorkOder`, A.Part_number `Mã nội bộ`, A.QTY`Số lượng yêu cầu`, B.QTY `Số Lượng thực xuất`, B.FIFO FROM STORE_MATERIAL_DB.REQUEST_EXPORT A INNER JOIN  STORE_MATERIAL_DB.BILL_EXPORT_WH B on A.WORK_ID = B.WORK_ID AND A.PART_NUMBER =B.PART_NUMBER where A.BILL_NUMBER='{billID}';";
                        break;
                }

                dt = globle.mySql.GetDataMySQL(cmd);
            }
            dgvDetail.DataSource = dt;

        }
        void loadbillStatus(string billID, int type)
        {
            string cmd = "";

            cmd = $@"SELECT A.bill_number `Mã phiếu`, A.WORK_ID WorkOder, A.MODEL_ID `Model nội bộ`, PCBS `Số lượng`, time_create `Thời gian tạo` ,B.`STATUS` `Trạng thái`
                            FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A
                             inner join STORE_MATERIAL_DB.BILL_STATUS B on A.STATUS_EXPORT = B.ID AND A.TYPE_BILL = B.TYPE_ID WHERE A.`TYPE_BILL`= '{type}' AND A.bill_number = '{billID}' ";

            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            dgvDetail.DataSource = dt;
            if (globle.IsTableEmty(dt))
            {
                MessageBox.Show("Phiếu xuất không tồn tại");
                return;
            }
        }
        void loadbillNVL(DateTime start, DateTime end, int type)
        {
            string cmd = "";
            switch (type)
            {
                //case 6:
                //    cmd = $@"SELECT DELIVERY_BILL.BILL_NUMBER `Mã phiếu`,DELIVERY_BILL.WORK_ID WorkOder, DELIVERY_BILL.CUS_MODEL `Model khách hàng`, DELIVERY_BILL.CUS_CODE `Mã code`, DELIVERY_BILL.UNIT `Đơn vị`, 
                //        DELIVERY_BILL.Model `Model nội bộ`, DELIVERY_BILL.NUMBER_REQUEST `Số lượng yêu cầu`, BILL_STATUS.`STATUS` `Trạng thái`, DELIVERY_BILL.created_by `Người tạo`,
                //          DELIVERY_BILL.date_created `Thời gian tạo`, DELIVERY_BILL.date_exports `Thời gian yêu cầu xuất` FROM TRACKING_SYSTEM.DELIVERY_BILL
                //         inner join BILL_STATUS on DELIVERY_BILL.STATUS_BILL = BILL_STATUS.ID and BILL_STATUS.`TYPE_ID`= '{type}' ";
                //    if (cbtimeFillter.Checked)
                //    {
                //        cmd += $"and date_exports>= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and date_exports<= '{end.ToString("yyyy-MM-dd HH:mm:ss")}'; ";
                //    }
                //    break;
                default:
                    cmd = $@"SELECT A.bill_number `Mã phiếu`, A.WORK_ID WorkOder, A.MODEL_ID `Model nội bộ`, PCBS `Số lượng`, time_create `Thời gian tạo` ,B.`STATUS` `Trạng thái`
                            FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A
                             inner join STORE_MATERIAL_DB.BILL_STATUS B on A.STATUS_EXPORT = B.ID AND A.TYPE_BILL = B.TYPE_ID WHERE A.`TYPE_BILL`= '{type}' ";

                    if (cbtimeFillter.Checked)
                    {
                        cmd += $"and time_create>= '{start.ToString("yyyy-MM-dd HH:mm:ss")}' and time_create<= '{end.ToString("yyyy-MM-dd HH:mm:ss")}';";
                    }
                    break;
            }
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            dgvDetail.DataSource = dt;
            if (globle.IsTableEmty(dt))
            {
                MessageBox.Show("Không tìm thấy dữ liệu theo yêu cầu!");
                //txtBillNhap.Clear();
                return;
            }
        }
         private void btnsearch_Click(object sender, EventArgs e)
        {
            gridView1.Columns.Clear();
            int type = int.Parse( cbtypeProduct.SelectedValue.ToString());
            string bill = txtBillID.Text.Trim();
            string cusID = string.Empty;
            if (cbdetail.Checked && !string.IsNullOrEmpty(bill))
            {
                loadBillDetail(txtBillID.Text);
                return;
            }           
            //danh sách
            if (type == 6) // detail bill 
            {
                loadBill_XuatTP(dtpfrom.Value.ToString("yyyy-MM-dd HH:mm:ss"), dtpend.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                loadbillNVL(dtpfrom.Value, dtpend.Value, type);
            } 
            return;
           
        }
        void loadBill_XuatTP(string start, string end)
        {
            string clause = string.Empty;
            string cmd = $@"SELECT A.BILL_NUMBER `Bill Number`, A.WORK_ID `WorkOder`, A.CUS_MODEL `Model khách hàng`, 
A.CUS_CODE `Mã code`, A.UNIT `Đơn vị`, A.Model `Model nội bộ`, A.NUMBER_REQUEST `Số lượng yêu cầu`,
B.EXPORTS_REAL `Thực xuẩt`,B.EXPORTS_REAL - A.NUMBER_REQUEST `Chênh lệch`,C.OP `Người tạo`,C.TIME_CREATE `Thời gian tạo`, 
C.DATE_EXPORT `Thời gian yêu cầu xuất` FROM TRACKING_SYSTEM.DELIVERY_BILL A left join
                            TRACKING_SYSTEM.EXPORT_FINISHED_PRODUCTS B on
                            A.BILL_NUMBER = B.BILL_NUMBER AND A.WORK_ID = B.WORK_ID
inner join STORE_MATERIAL_DB.BILL_REQUEST_EX C ON A.BILL_NUMBER = C.BILL_NUMBER
 where C.DATE_EXPORT > '{start}' AND  C.DATE_EXPORT < '{end}' {clause} ; ";
            DataTable dt = globle.mySql.GetDataMySQL(cmd);
            dgvDetail.DataSource = dt;
        }
        

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog openFileDialog = new SaveFileDialog();
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                if (DialogResult.OK != openFileDialog.ShowDialog()) return;
                dgvDetail.ExportToCsv(openFileDialog.FileName);
                MessageBox.Show("Save file thành công!");
            }
            catch
            {
                return;
            }
        }

        private void cbtypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBillID.Clear();
            cbdetail.Checked = false;

        }
      
        
    }
}
