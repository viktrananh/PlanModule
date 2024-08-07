using System.Data;

namespace PLAN_MODULE.DTO
{
    public class BillPlan
    {

        private string cusmodel;
        private string work;
        private string unit;
        private int request;
        private int real;
        private string parking;
        private int boxC;
        private int boxP;

        public string Cusmodel { get => cusmodel; set => cusmodel = value; }
        public string Work { get => work; set => work = value; }
        public string Unit { get => unit; set => unit = value; }
        public int Request { get => request; set => request = value; }
        public int Real { get => real; set => real = value; }
        public string Parking { get => parking; set => parking = value; }
        public int BoxC { get => boxC; set => boxC = value; }
        public int BoxP { get => boxP; set => boxP = value; }

        public BillPlan() { }
        public BillPlan(string cusmodel, string work, string unit, int request, int real, string parking, int boxC, int boxP)
        {
            Cusmodel = cusmodel;
            Work = work;
            Unit = unit;
            Request = request;
            Real = real;
            Parking = parking;
            BoxC = boxC;
            this.boxP = boxP;
        }


        public BillPlan(DataRow row)
        {

            Cusmodel = "PCBA " + row["CUS_MODEL"].ToString();
            Work = row["WORK_ID"].ToString();
            Unit = row["UNIT"].ToString();
            Request = int.Parse(row["REQUESTS"].ToString());

            Real = int.Parse(row["EXPORTS_REAL"].ToString());
            string billnumber = row["BILL_NUMBER"].ToString();
            int PcbCount = 0;
            if (row["WORK_ID"].ToString().Length != 10 && !Work.Contains("0000"))
            {
                DataTable dt = DBConnect.getData($"SELECT PCB_COUNT FROM TRACKING_SYSTEM.BOX_PACKING where CUS_CODE = '{row["WORK_ID"].ToString()}';");
                PcbCount = int.Parse(dt.Rows[0]["PCB_COUNT"].ToString());
            }
            else
            {
                string sql = $"SELECT MODEL FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID_MOTHER = '{row["WORK_ID"].ToString()}'";
                DataTable DT = DBConnect.getData(sql);
                string model = DT.Rows[0]["MODEL"].ToString();
                string SQL = $"SELECT PCB_COUNT FROM TRACKING_SYSTEM.BOX_PACKING WHERE MODEL_ID = '{model}';";
                DataTable dt = DBConnect.getData(SQL);
                PcbCount = int.Parse(dt.Rows[0]["PCB_COUNT"].ToString());
            }
            string packed = Process.getDetailNote(billnumber, row["WORK_ID"].ToString(), PcbCount);
            Parking = packed;
            string boxc = row["BOX_C"].ToString();
            string boxp = row["BOX_P"].ToString();
            int c = 0;
            int p = 0;
            if (!string.IsNullOrEmpty(boxc))
            {
                c = int.Parse(boxc);
            }
            if (!string.IsNullOrEmpty(boxp))
            {
                p = int.Parse(boxp);
            }
            boxC = c;
            BoxP = p;
            //int boxs = int.Parse(row["EXPORTS_REAL"].ToString()) / PcbCount;
            //int odd = int.Parse(row["EXPORTS_REAL"].ToString()) % PcbCount;
            //this.Parking = boxs + $" Thùng ( {PcbCount} PCS/ Thùng) \r\n 1 Thùng ( {odd} PCS / Thùng) ";

        }

    }
   
}