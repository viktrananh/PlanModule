using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillExport
{
    public class BillRequestExportGoodToCus
    {
        public int ID { get; set; }
        public string Work { get; set; }
        public string CusModel { get; set; }
        public string CusCode { get; set; }
        public string ModelID { get; set; }
        public string CusID { get; set; }
        public string PO { get; set; }
        public string Unit { get; set; }
        public int Request { get; set; }
        public string Real { get; set; }
        public string Note { get; set; }

        public BillRequestExportGoodToCus() { }
        public BillRequestExportGoodToCus(string cusModel, string work, string unit, int request)
        {
            CusModel = cusModel;
            Work = work;
            Unit = unit;
            Request = request;
        }
        public BillRequestExportGoodToCus(DataRow row, int id, bool isPrint = false)
        {
            ID = id;
			Work = row["WORK_ID"].ToString();
            Unit = row["UNIT"].ToString();
            Request = int.Parse(row["REQUEST"].ToString());
            Real = string.Empty;
            string note = row["NOTE"].ToString();
            if (isPrint)
                Note = string.IsNullOrEmpty(note) ? $"      Thùng (     pcs/thùng)\n      Thùng (     pcs/thùng)" : $"      Thùng (     pcs/thùng)\n   {note}";
            else
                Note = note;
        }
    }
    public class BillRequestExportGoodToCusControl
    {
        private BillRequestExportGoodToCusControl instance;
        public BillRequestExportGoodToCusControl Instance
        {
            get { if (instance == null) instance = new BillRequestExportGoodToCusControl(); return instance; }
            set { instance = value; }
        }
        private BillRequestExportGoodToCusControl() { }

        public static List<BillRequestExportGoodToCus> LoadDetailBill(string Bill, bool isPrint = false)
        {
            List<BillRequestExportGoodToCus> ls = new List<BillRequestExportGoodToCus>();

            BillExportGoodsDAO billDAO = new BillExportGoodsDAO();
            DataTable dt = billDAO.getDetailBill(Bill);
            if (billDAO.istableNull(dt)) return new List<BillRequestExportGoodToCus>();
            int id = 1;
            foreach (DataRow item in dt.Rows)
            {
                BillRequestExportGoodToCus bill = new BillRequestExportGoodToCus(item, id, isPrint);
                ls.Add(bill);
                id++;
            }
            return ls;
        }

        public static List<BillRequestExportGoodToCus> LoadLsByModel(string Model)
        {
            List<BillRequestExportGoodToCus> ls = new List<BillRequestExportGoodToCus>();

            BillExportGoodsDAO billDAO = new BillExportGoodsDAO();
            DataTable dt = billDAO.getDetailBill(Model);
            int id = 1;
            foreach (DataRow item in dt.Rows)
            {
                BillRequestExportGoodToCus bill = new BillRequestExportGoodToCus(item, id);
                ls.Add(bill);
                id++;
            }
            return ls;
        }
    }

}
