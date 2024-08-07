using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DAO.Product;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DAO;
using DevExpress.Office.Utils;

namespace PLAN_MODULE.DAO.Product
{
    internal class ProductRequestImportDAO : BaseDAO
    {
        public DataTable loadDetailBill(string bill, string exportType)
        {
            string sql = "";
            if (exportType == "Kế hoạch")
                sql = $"SELECT WORK_ID, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.REQUEST_EXPORT WHERE BILL_NUMBER = '{bill}';";
            else
                sql = $"SELECT WORK_ID, PART_NUMBER, UNIT, QTY FROM STORE_MATERIAL_DB.REQUEST_EXPORT WHERE BILL_NUMBER = '{bill}';";
            return mySQL.GetDataMySQL(sql);
        }

      
     
        public DataTable getListBillEx(string wo, out int sumPCBRequest)
        {
            DataTable dtView = new DataTable();
            dtView.Columns.Add("Work Oder");
            dtView.Columns.Add("Mã nội bộ");
            dtView.Columns.Add("Số lượng yêu cầu");
            dtView.Columns.Add("Cấp thực tế");
            sumPCBRequest = 0;
            DataTable dt = DBConnect.getData($"SELECT A.BILL_NUMBER, A.WORK_ID, B.TOTAL_PCBS,  A.MODEL_ID, A.PCBS FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A " +
                $"INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID where A.WORK_ID = '{wo}' and SUB_TYPE = 0 AND TYPE_BILL = 4 AND STATUS_EXPORT <> -1;");
            if (istableNull(dt)) return null;
            sumPCBRequest = dt.AsEnumerable().Sum(r => r.Field<int>("PCBS"));
            string totalWo = dt.Rows[0]["TOTAL_PCBS"].ToString();
            string clause = string.Empty;
            foreach (DataRow item in dt.Rows)
            {
                string bill = item["BILL_NUMBER"].ToString();
                clause += $" A.BILL_NUMBER = '{bill}' OR";
            }
            clause = clause.Substring(0, clause.Length - 3);
            string cmd = $@"SELECT A.BILL_NUMBER,  A.WORK_ID , A.Part_number , A.QTY `Request`, B.QTY `Real` FROM STORE_MATERIAL_DB.REQUEST_EXPORT A
left JOIN  STORE_MATERIAL_DB.BILL_EXPORT_WH B on B.BILL_REQUEST = A.BILL_NUMBER AND A.WORK_ID = B.WORK_ID AND A.PART_NUMBER = B.PART_NUMBER where {clause} ; ";
            DataTable dtDetail = DBConnect.getData(cmd);

            var rs = from r in dtDetail.AsEnumerable()
                     group r by new
                     {
                         Bill = r.Field<string>("BILL_NUMBER"),
                         Part = r.Field<string>("Part_number").Trim(),
                     } into g
                     select new
                     {
                         Bill = g.Key.Bill,
                         Part = g.Key.Part,
                         Request = g.Select(a => a.Field<int>("Request")).First(),
                         Real = g.Sum(b => b.Field<int?>("Real") ?? 0),
                     };
            var rsCheck = from r in rs
                          group r by r.Part into g
                          select new
                          {
                              Part = g.Key,
                              Request = g.Sum(a => (int)a.Request),
                              Real = g.Sum(b => b.Real)

                          };
            foreach (var r in rsCheck)
            {
                dtView.Rows.Add(wo, r.Part, r.Request, r.Real);
            }
            return dtView;
        }

     
     
     
        //public DataTable getListExportByWork(string work)
        //{
        //    DataTable dtBook = new DataTable();
        //    dtBook.Columns.Add("Work", typeof(string));
        //    dtBook.Columns.Add("MainPart", typeof(string));
        //    dtBook.Columns.Add("PartNumber", typeof(string));
        //    dtBook.Columns.Add("Tổng yêu cầu", typeof(string));
        //    dtBook.Columns.Add("Tổng đã xuất", typeof(int));
        //    dtBook.Columns.Add("Còn thiếu", typeof(int));
        //}
        public List<BillExportMaterial> GetListSummaryBillRequestPC(string work = "")
        {
            BillExportMaterial Bill_Export = new BillExportMaterial();
            DataTable rs = new DataTable();
            rs.Columns.Add("WORK_ID", typeof(string));
            rs.Columns.Add("BILL_NUMBER", typeof(string));
            rs.Columns.Add("TYPE", typeof(string));
            rs.Columns.Add("STATUS", typeof(string));
            rs.Columns.Add("USER_CREAT", typeof(string));
            rs.Columns.Add("TIME_CREAT", typeof(string));
            string condition = string.IsNullOrEmpty(work) ? "" : $" and WORK_ID like '%{work}%'";
            DataTable dt = mySQL.GetDataMySQL($"SELECT  WORK_ID,BILL_NUMBER,SUB_TYPE 'TYPE',STATUS_EXPORT 'STATUS',OP,TIME_CREATE FROM STORE_MATERIAL_DB.BILL_REQUEST_EX " +
                $"where STATUS_EXPORT <>-1 AND ( TYPE_BILL = {BillExportMaterial.phiêuFAT} OR TYPE_BILL= {BillExportMaterial.phiêuSMT}) {condition} GROUP BY BILL_NUMBER ORDER BY TIME_CREATE DESC  limit 100;");
            if (istableNull(dt)) return new List<BillExportMaterial>();

            List<BillExportMaterial> billExxports = new List<BillExportMaterial>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string loaiphieu = int.Parse(dt.Rows[i]["TYPE"].ToString()) == BillExportMaterial.XuatKeHoach ? "Kế hoạch" : "Phát sinh";
                string trangthai = Bill_Export.billStatuses.Where(a => a.value == int.Parse(dt.Rows[i]["STATUS"].ToString())).FirstOrDefault().status;

                BillExportMaterial bill = new BillExportMaterial()
                {
                    BillId = dt.Rows[i]["BILL_NUMBER"].ToString(),
                    WorkId = dt.Rows[i]["WORK_ID"].ToString(),
                    BillTypeName = loaiphieu,
                    BillStatusName = trangthai,
                    OP = dt.Rows[i]["OP"].ToString(),
                    CreateTime = DateTime.Parse(dt.Rows[i]["TIME_CREATE"].ToString()),
                };
                //rs.Rows.Add(dt.Rows[i]["WORK_ID"].ToString(), dt.Rows[i]["BILL_NUMBER"].ToString(), loaiphieu, trangthai, dt.Rows[i]["OP"].ToString(), dt.Rows[i]["TIME_CREATE"].ToString());
            }
            return billExxports;
        }
    }
}
