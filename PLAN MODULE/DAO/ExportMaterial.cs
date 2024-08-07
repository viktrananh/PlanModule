using System;
using System.Collections.Generic;
using System.Data;

namespace PLAN_MODULE
{
    public class ExportMaterial
    {
        private string billNumber;
        private DateTime timeExport;
        private List<ReturnCode> listRe;

        public ExportMaterial(string billNumber, DateTime timeExport, List<ReturnCode> listRe)
        {
            BillNumber = billNumber;
            TimeExport = timeExport;
            ListRe = listRe;
        }
        public bool IsTableEmty(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return true;
            return false;
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(DBConnect.getData("SELECT NOW();").Rows[0][0].ToString());
        }
        public ExportMaterial()
        {
        }

        public string BillNumber { get => billNumber; set => billNumber = value; }
        public DateTime TimeExport { get => timeExport; set => timeExport = value; }
        internal List<ReturnCode> ListRe { get => listRe; set => listRe = value; }



        public class ReturnCode
        {
            private string interPart;
            private string work;
            private string unit;
            private int request;

            public string InterPart { get => interPart; set => interPart = value; }
            public string Work { get => work; set => work = value; }
            public string Unit { get => unit; set => unit = value; }
            public int Request { get => request; set => request = value; }

            public ReturnCode(string interPart,  string work, string unit, int request)
            {
                InterPart = interPart;
                Work = work;
                Unit = unit;
                Request = request;
            }

            public ReturnCode()
            {
            }
        }
        public bool cancelBillExPC(string bill, string op)
        {
            string sql = $"UPDATE STORE_MATERIAL_DB.BILL_REQUEST_EX SET STATUS_EXPORT  = -1 WHERE BILL_NUMBER = '{bill}';";
            sql += $"INSERT INTO `STORE_MATERIAL_DB`.`BILL_HISTORY` (`BILL_NUMBER`, `EVENT`, `TIME_EVENT`, `OP`) VALUES ('{bill}', 'CANCEL', NOW(), '{op}');";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        public bool isBillCanDelete(string bill)
        {
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX WHERE BILL_NUMBER = '{bill}' AND TYPE_BILL = 1; ";
            if (DBConnect.InsertMySql(sql)) return true;
            return false;
        }
        
        public int getCountPartInBill(string bill)
        {
            return int.Parse(DBConnect.getData($"SELECT count(*) FROM STORE_MATERIAL_DB.REQUEST_EXPORT where BILL_NUMBER='{bill}';").Rows[0][0].ToString());
        }
        #region IQC
        public bool isBillImport(string bill, out int billType, out int statusBill)
        {
            billType = 0;
            statusBill = -2;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where BILL_NUMBER  = '{bill}';");
            if (IsTableEmty(dt)) return false;
            billType = int.Parse(dt.Rows[0]["TYPE_BILL"].ToString());
            statusBill = int.Parse(dt.Rows[0]["STATUS_BILL"].ToString());
            return true;
        }
        public bool isBIllImportChecked(string billImport, out string billIQC)
        {
            billIQC = string.Empty;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX WHERE CHECK_BILL = '{billImport}';");
            if (IsTableEmty(dt)) return false;
            billIQC = dt.Rows[0]["BILL_NUMBER"].ToString();
            return true;
        }
        public bool isPartInBill(string bill, string partNumber, out int pcbs)
        {
            pcbs = 0;
            DataTable dt = DBConnect.getData($"SELECT * FROM STORE_MATERIAL_DB.BILL_IMPORT_WH where BILL_NUMBER = '{bill}' and PART_NUMBER = '{partNumber}';");
            if (IsTableEmty(dt)) return false;
            pcbs = int.Parse(dt.Rows[0]["QTY"].ToString());
            return true;
        }
        public DataTable getListPartInputBill(string bill)
        {
            return DBConnect.getData($"SELECT WORK_ID, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.BILL_IMPORT_WH where bill_number = '{bill}';");
            
        }
        #endregion
    }
}
