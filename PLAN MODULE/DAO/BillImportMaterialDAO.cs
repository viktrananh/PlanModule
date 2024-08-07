//using PLAN_MODULE.DTO.Planed.BillImport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DTO;

namespace PLAN_MODULE.DAO
{
    public class BillImportMaterialDAO : BaseDAO
    {
        public List<BillImportMaterial> GetBills()
        {
            string sql = $@"SELECT  A.BILL_NUMBER, A.CUSTOMER, A.WORK_ID, A.CREATE_BY, A.CREATE_TIME, C.VENDER_NAME,  B.`STATUS`,  A.STATUS_BILL FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT A
                         INNER JOIN STORE_MATERIAL_DB.BILL_STATUS B  ON A.TYPE_BILL = B.TYPE_ID AND A.STATUS_BILL = B.ID
                         LEFT JOIN TRACKING_SYSTEM.DEFINE_VENDER C ON A.VENDER_ID = C.VENDER_ID where A.TYPE_BILL = 2  and (MONTH(CREATE_TIME) = MONTH(curdate()) or  MONTH(CREATE_TIME) = MONTH(date_add(curdate(), INTERVAL - 1 MONTH)))  ORDER BY CREATE_TIME DESC; ";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<BillImportMaterial>();
            return (from a in dt.AsEnumerable()
                    select new BillImportMaterial
                    {
                        BillNumber = a.Field<string>("BILL_NUMBER"),
                        WorkID = a.Field<string>("WORK_ID"),
                        CusID = a.Field<string>("CUSTOMER"),
                        CreatTime = a.Field<DateTime>("CREATE_TIME"),
                        StatusName = a.Field<string>("STATUS"),
                        StatusID = a.Field<string>("STATUS_BILL").ToString(),
                        OP = a.Field<string>("CREATE_BY"),
                        Supplier = a.Field<string>("VENDER_NAME"),
                    }).ToList();

        }
        public BillImportMaterial GetBillInfor(string billId)
        {
            string sql = $@"SELECT  A.BILL_NUMBER, A.CUSTOMER, A.WORK_ID, A.CREATE_BY, A.CREATE_TIME, C.VENDER_NAME,  B.`STATUS`,  A.STATUS_BILL FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT A
                         INNER JOIN STORE_MATERIAL_DB.BILL_STATUS B  ON A.TYPE_BILL = B.TYPE_ID AND A.STATUS_BILL = B.ID
                         LEFT JOIN TRACKING_SYSTEM.DEFINE_VENDER C ON A.VENDER_ID = C.VENDER_ID where A.TYPE_BILL = 2  and BILL_NUMBER='{billId}'; ";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return null;
            return (from a in dt.AsEnumerable()
                    select new BillImportMaterial
                    {
                        BillNumber = a.Field<string>("BILL_NUMBER"),
                        CusID = a.Field<string>("CUSTOMER"),
                        CreatTime = a.Field<DateTime>("CREATE_TIME"),
                        StatusName = a.Field<string>("STATUS"),
                        StatusID = a.Field<string>("STATUS_BILL").ToString(),
                        OP = a.Field<string>("CREATE_BY"),
                        Supplier = a.Field<string>("VENDER_NAME"),
                    }).ToList().First();

        }
        public List<BillImportDetail> GetBillImportMaterials(string billID)
        {
            List<BillImportDetail> billImportMaterials = new List<BillImportDetail>();
            string cmd = $"SELECT BILL_NUMBER,PART_NUMBER,CUS_PART,MFG_PART,QTY FROM STORE_MATERIAL_DB.BILL_WH_INPUT where BILL_NUMBER='{billID}';";
            DataTable dt = mySQL.GetDataMySQL(cmd);
            if (istableNull(dt)) return billImportMaterials;
            return (from a in dt.AsEnumerable()
                    select new BillImportDetail
                    {
                        billNo = billID,
                        mfgPart = a.Field<string>("mfg_part"),
                        cusPart = a.Field<string>("cus_part"),
                        interPart = a.Field<string>("mfg_part"),
                        recivedqty=a.Field<int>("qty")
                    }).ToList();
        }
    }
}
