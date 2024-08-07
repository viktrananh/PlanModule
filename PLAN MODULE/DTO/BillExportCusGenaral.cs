using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillExport
{
    public class BillExportCusGenaral
    {
        public string BillNumber { get; set; }
        public string CusID { get; set; }
        public string OP { get; set; }
        public DateTime TimeIntend { get; set; }
        public string TypeBill { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }

        public List<BillRequestExportGoodToCus> BillRequestExportGoodToCus { get; set; }
        public BillExportCusGenaral() { }
        public BillExportCusGenaral(DataRow row)
        {
            BillNumber = row["BILL_NUMBER"].ToString();
            CusID = row["CUS_ID"].ToString();
            OP = row["OP"].ToString();
            TimeIntend = DateTime.Parse(row["INTEND_TIME"].ToString());
            TypeBill = row["NOTE"].ToString();
            StateId = int.Parse( row["STATE"].ToString());
            StateName = LoadStateBillExportGoodsToCus.StatusBillExportGoodToCus.FirstOrDefault(x => x.StatusID == StateId).StatusName;
            BillRequestExportGoodToCus = BillRequestExportGoodToCusControl.LoadDetailBill(BillNumber);
        }

    }
    public class BillExportCusGenaralControl
    {
        private BillExportCusGenaralControl instance;
        public BillExportCusGenaralControl Instance
        {
            get { if (instance == null) instance = new BillExportCusGenaralControl(); return instance; }
            set { instance = value; }
        }
        private BillExportCusGenaralControl() { }
        public static List<BillExportCusGenaral> LoadLsBill()
        {
            List<BillExportCusGenaral> ls = new List<BillExportCusGenaral>();
            BillExportGoodsDAO export = new BillExportGoodsDAO();
            DataTable dt = export.GetLsBillExportCus();
            foreach (DataRow item in dt.Rows)
            {
                BillExportCusGenaral bill = new BillExportCusGenaral(item);
                ls.Add(bill);
            }
            return ls;
        }


        public static BillExportCusGenaral LoadBillByBillNumber(string billNumber)
        {
            List<BillExportCusGenaral> ls = new List<BillExportCusGenaral>();
            BillExportGoodsDAO export = new BillExportGoodsDAO();
            DataTable dt = export.GetBillExportCusByBillNumber(billNumber);

            if (export.istableNull(dt)) return new BillExportCusGenaral();

            BillExportCusGenaral bill = new BillExportCusGenaral(dt.Rows[0]);


            return bill;
        }
    }
}
