using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    internal class RequestImportDAO:BaseDAO
    {
        BusinessGloble globle = new BusinessGloble();

        public DataTable GetVenderForCus(string cusID)
        {
            return DBConnect.getData($"SELECT VENDER_NAME, VENDER_ID FROM TRACKING_SYSTEM.DEFINE_VENDER where CUS_ID='{cusID}';");
        }

        public DataTable GetDetailMFGPart(string modelId, string bomvVer, string MFGpart)
        {
            return globle.mySql.GetDataMySQL($"SELECT INTER_PART, DESCRIPTION FROM STORE_MATERIAL_DB.BOM_CONTENT where MODEL = '{modelId}' AND VERSION = '{bomvVer}' AND CS_PART = '{MFGpart}';");
        }
        public string CreatBillName(string thisMachine)
        {
            string bill = string.Empty;
            DataTable dtbill = DBConnect.getData("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where DATE(CREATE_TIME) = CURDATE() AND TYPE_BILL = '2' order BY CREATE_TIME DESC;");
            DateTime timeNow = globle.getTimeServer();
            if (globle.IsTableEmty(dtbill))
            {
                bill = thisMachine.Substring(thisMachine.Length - 2, 2) + timeNow.ToString("ddMMyyyy") + "-01/NVL-DT";
            }
            else
            {
                bill = thisMachine.Substring(thisMachine.Length - 2, 2) + timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + "/NVL-DT";
            }
            return bill;
        }
        public string CreatBillName_PO(string thisMachine, string typeBill)
        {
            string bill = string.Empty;
            DataTable dtbill = DBConnect.getData("SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT where DATE(CREATE_TIME) = CURDATE() AND TYPE_BILL = '2' order BY CREATE_TIME DESC;");
            DateTime timeNow = globle.getTimeServer();
            if (globle.IsTableEmty(dtbill))
            {
                bill = thisMachine.Substring(thisMachine.Length - 2, 2) + timeNow.ToString("ddMMyyyy") + "-01/" + typeBill;
            }
            else
            {
                bill = thisMachine.Substring(thisMachine.Length - 2, 2) + timeNow.ToString("ddMMyyyy") + "-" + (int.Parse(dtbill.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + "/" + typeBill;
            }
            return bill;
        }
    }
}
