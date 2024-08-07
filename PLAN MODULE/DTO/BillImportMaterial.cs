using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class BillImportDetail
    {
        public string billNo { get; set; }
        public string cusPart { get; set; }
        public string interPart { get; set; }
        public string mfgPart { get; set; }
        public int recivedqty { get; set; }
       

    }
    public class BillImportMaterial
    {
        public string  BillNumber { get; set; }
        public string CusID { get; set; }
        public string WorkID { get; set; }
        public string OP { get; set; }
        public DateTime CreatTime { get; set; }
        public string  Supplier { get; set; }
        public string StatusName { get; set; }
        public string StatusID { get; set; }
        //public BillImportMaterial() { }
        //public BillImportMaterial(DataRow row)
        //{
        //    BillNumber = row["BILL_NUMBER"].ToString();
        //    CusID = row["CUSTOMER"].ToString();
        //    WorkID = row["WORK_ID"].ToString();
        //    OP = row["CREATE_BY"].ToString();
        //    CreatTime = DateTime.Parse( row["CREATE_TIME"].ToString());
        //    Supplier = row["VENDER_NAME"].ToString();
        //    StatusName = row["STATUS"].ToString();
        //    StatusID = row["STATUS_BILL"].ToString();
        //}


    }
}
