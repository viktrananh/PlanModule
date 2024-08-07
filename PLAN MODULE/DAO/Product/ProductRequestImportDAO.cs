using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLAN_MODULE.DAO.Product;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DAO;

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

        public string createBillName(string process)
        {
            string bill = string.Empty;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where DATE(TIME_CREATE)  = CURDATE() AND BILL_NUMBER LIKE '%{process}%'  ORDER BY ID DESC;");
            if (istableNull(dt))
            {
                bill = getTimeServer().ToString("ddMMyyyy") + $"-01/{process}";
            }
            else
            {
                bill = getTimeServer().ToString("ddMMyyyy") + "-" + (int.Parse(dt.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + $"/{process}";
            }
            return bill;
        }
        public bool CreateBill(Bill_Export_Material bill)
        {
            string sql = "";
            sql += "INSERT INTO `STORE_MATERIAL_DB`.`BILL_REQUEST_EX` (`BILL_NUMBER`, `WORK_ID`, `MODEL_ID`, `PCBS`, `TIME_CREATE`, `STATUS_EXPORT`, `TYPE_BILL`, `SUB_TYPE`) " +
                    $" VALUES ('{bill.billID}', '{bill.workID}', '{bill.modelID}', '{bill.pcbs}', now() , 0, '{bill.billType}' , '{bill.exportType}');";
            foreach (var item in bill.SubPart_Requests)
            {
                sql += $"INSERT INTO `STORE_MATERIAL_DB`.`REQUEST_EXPORT` (`BILL_NUMBER`, `WORK_ID`, `PART_NUMBER`, `UNIT`, `QTY`,`MAIN_PART`) " +
                    $"VALUES ('{bill.billID}', '{bill.workID}', '{item.subpartNo}', '{item.unit}',  '{item.qtyRequest}','{item.mainPart}');";
            }
            return mySQL.InsertDataMySQL(sql);

        }
        public DataTable GetListSummaryBillRequestPC()
        {
            Bill_Export_Material Bill_Export = new Bill_Export_Material();
            DataTable rs = new DataTable();
            rs.Columns.Add("WORK_ID", typeof(string));
            rs.Columns.Add("BILL_NUMBER", typeof(string));
            rs.Columns.Add("TYPE", typeof(string));
            rs.Columns.Add("STATUS", typeof(string));
            DataTable dt = mySQL.GetDataMySQL($"SELECT WORK_ID,BILL_NUMBER,SUB_TYPE 'TYPE',STATUS_EXPORT 'STATUS' FROM STORE_MATERIAL_DB.BILL_REQUEST_EX " +
                $"where STATUS_EXPORT <>-1 AND ( TYPE_BILL = {Bill_Export_Material.phiêuFAT} OR (TYPE_BILL= {Bill_Export_Material.phiêuSMT} and sub_type={Bill_Export_Material.XuatPhatSinh})) GROUP BY BILL_NUMBER ORDER BY TIME_CREATE DESC ;");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string loaiphieu = int.Parse(dt.Rows[i]["TYPE"].ToString()) == Bill_Export_Material.XuatKeHoach ? "Kế hoạch" : "Phát sinh";
                string trangthai = Bill_Export.billStatuses.Where(a => a.value == int.Parse(dt.Rows[i]["STATUS"].ToString())).FirstOrDefault().status;
                rs.Rows.Add(dt.Rows[i]["WORK_ID"].ToString(), dt.Rows[i]["BILL_NUMBER"].ToString(), loaiphieu, trangthai);
            }
            return rs;
        }
    }
}
