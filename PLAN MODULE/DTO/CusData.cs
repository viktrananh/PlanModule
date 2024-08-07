using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class CusData
    {
        public string  TR_SN { get; set; }
        public string WO { get; set; }
        public string CUST_KP_NO { get; set; }
        public string MFR_KP_NO { get; set; }
        public string MFR_CODE { get; set; }
        public string DATE_CODE { get; set; }
        public string LOT_CODE { get; set; }
        public int QTY { get; set; }
        public string PKG_ID { get; set; }
        public string BillNumber { get; set; }
        public CusData() { }
        public CusData(DataRow row)
        {
            TR_SN = row["TR_SN"].ToString();
            WO = row["WO"].ToString();
            CUST_KP_NO = row["CUST_KP_NO"].ToString();
            MFR_KP_NO = row["MFR_KP_NO"].ToString();
            MFR_CODE = row["MFR_CODE"].ToString();
            DATE_CODE = row["DATE_CODE"].ToString();
            LOT_CODE = row["LOT_CODE"].ToString();
            QTY = int.Parse( row["OUT_QTY"].ToString());
            PKG_ID = row["PKG_ID"].ToString();
            BillNumber = row["BILL_NUMBER"].ToString();
        }
    }
}
