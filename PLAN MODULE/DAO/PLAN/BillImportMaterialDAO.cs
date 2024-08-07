using PLAN_MODULE.DTO.Planed.BillImport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DTO;

namespace PLAN_MODULE.DAO.PLAN
{
    public class BillImportMaterialDAO : BaseDAO
    {
        public DataTable GetLsBill()
        {
            string sql = $@"SELECT  A.BILL_NUMBER, A.CUSTOMER, A.WORK_ID, A.CREATE_BY, A.CREATE_TIME, C.VENDER_NAME,  B.`STATUS`,  A.STATUS_BILL FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT A
                         INNER JOIN STORE_MATERIAL_DB.BILL_STATUS B  ON A.TYPE_BILL = B.TYPE_ID AND A.STATUS_BILL = B.ID
                         LEFT JOIN TRACKING_SYSTEM.DEFINE_VENDER C ON A.VENDER_ID = C.VENDER_ID where A.TYPE_BILL = 2  and (MONTH(CREATE_TIME) = MONTH(curdate()) or  MONTH(CREATE_TIME) = MONTH(date_add(curdate(), INTERVAL - 1 MONTH)))  ORDER BY CREATE_TIME DESC; ";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;

        }
        //public bool UpdateBillImport (BillImportDetail billImportDetail)
        //{
        //    string sql = $"update STORE_MATERIAL_DB.BILL_IMPORT_CUS set qty='{billImportDetail.qty}', cus_part='{billImportDetail.cusPart}', model='{billImportDetail.model}'" +
        //        $",create_time= now(), op='{billImportDetail.creater}' where id='{billImportDetail.id}';";
        //    return mySQL.InsertDataMySQL(sql);
        //}

       
        public bool DeleteBillImport(BillImportDetail billImportDetail)
        {
            string sql = $"delete from STORE_MATERIAL_DB.BILL_IMPORT_CUS where id='{billImportDetail.id}';";
            return mySQL.InsertDataMySQL(sql);
        }
        public string getCusIDfromBill(string billID)
        {
            return mySQL.GetDataMySQL($"SELECT customer FROM STORE_MATERIAL_DB.BILL_REQUEST_IMPORT   where BILL_NUMBER='{billID}';").Rows[0][0].ToString();
        }

        public List<DTO.BillImportDetail>  GetDetailBill(string bill)
        {
            string sql = $"SELECT ID,BILL_NUMBER,CUS_PART,PART_NUMBER,MFG_PART,RECIVE_QTY,CREATE_TIME,Model,OP FROM STORE_MATERIAL_DB.BILL_IMPORT_CUS where BILL_NUMBER='{bill}';";
            DataTable dt = mySQL.GetDataMySQL(sql);

            return (from a in dt.AsEnumerable()
                    select new DTO.BillImportDetail
                    {
                        id = a.Field<int>("id"),
                        billNo = a.Field<string>("bill_number"),
                        cusPart = a.Field<string>("CUS_PART"),
                        interPart = a.Field<string>("PART_NUMBER"),
                        mfgPart = a.Field<string>("MFG_PART"),
                        creater = a.Field<string>("op"),
                        createTime = a.Field<DateTime>("CREATE_TIME"),
                        model = a.Field<string>("model"),
                        recivedqty = a.Field<int>("RECIVE_QTY"),
                    }).ToList();
        }
    }
}
