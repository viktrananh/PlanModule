using DevExpress.Export.Xl;
using DevExpress.Utils.DPI;
using DevExpress.XtraRichEdit.API.Native;
using DocumentFormat.OpenXml.Spreadsheet;
using PLAN_MODULE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO
{
    public class BillExportMaterialDAO : BaseDAO
    {
        public DataTable GetDetailBill(string bill)
        {
            string SQL = $"SELECT WORK_ID,  MAIN_PART, PART_NUMBER, QTY FROM STORE_MATERIAL_DB.REQUEST_EXPORT WHERE BILL_NUMBER = '{bill}';";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            return dt;
        }
        public bool IsWorkRequestByPlanFull(string work)
        {
            DataTable dt = DBConnect.getData($"SELECT A.BILL_NUMBER, A.WORK_ID, B.TOTAL_PCBS,  A.MODEL_ID, A.PCBS FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A " +
                $"INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID where A.WORK_ID = '{work}' and ( SUB_TYPE = 0 or SUB_TYPE = 2 or SUB_TYPE = 3 ) AND STATUS_EXPORT <> -1;");
            if (istableNull(dt)) return false;

            return true;
        }
        public int GetRemainPanacim(string partNumber)
        {
            string sql = $"SELECT PART_NUMBER,SUM(ESTIMATED_QUANTITY) as REMAIN  FROM PanaCIM.dbo.unloaded_reel_view where  AREA='KHOSMT'  and PART_NUMBER='{partNumber}'  group by PART_NUMBER;";

            DataTable dt = sqlSever.GetDataSQL(sql);
            if (istableNull(dt)) return 0;
            if (string.IsNullOrEmpty(dt.Rows[0]["REMAIN"].ToString())) return 0;

            return int.Parse(dt.Rows[0]["REMAIN"].ToString());
        }

        public bool IsBillExport(string bill)
        {
            string SQL = $"SELECT * FROM STORE_MATERIAL_DB.BILL_EXPORT_WH WHERE BILL_NUMBER = '{bill}' ;";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            if (istableNull(dt)) return true;
            return false;
        }
        public string getBillInWork(string work)
        {

            DataTable dt = DBConnect.getData($"SELECT BILL_NUMBER FROM STORE_MATERIAL_DB.BILL_REQUEST_EX " +
                $"WHERE WORK_ID= '{work}' AND STATUS_EXPORT <> '-1' and TYPE_BILL= '5' AND SUB_TYPE = '0'" +
                $" GROUP BY BILL_NUMBER ORDER BY TIME_CREATE;");
            if (istableNull(dt)) return null;

            return dt.Rows[0][0].ToString();
        }


        public string GetBillInWorkBySubType(string work, int subType)
        {

            DataTable dt = DBConnect.getData($"SELECT BILL_NUMBER FROM STORE_MATERIAL_DB.BILL_REQUEST_EX " +
                $"WHERE WORK_ID= '{work}' AND STATUS_EXPORT <> '-1' AND SUB_TYPE = '{subType}'" +
                $" GROUP BY BILL_NUMBER ORDER BY TIME_CREATE;");
            if (istableNull(dt)) return null;

            return dt.Rows[0][0].ToString();
        }
        public DataTable getListBillInWork(string work)
        {

            DataTable dt = DBConnect.getData($"SELECT BILL_NUMBER,WORK_ID,SUB_TYPE,STATUS_EXPORT,PCBS,OP,TIME_CREATE FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where  work_id='{work}' and status_export<>-1 and( TYPE_BILL=4 or type_bill=5 ) order by id desc;");
            if (istableNull(dt)) return null;

            return dt;
        }
        public string CreateBillName(string process)
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
        public string CreateBillNameNew(string process, string workId, int typeBill)
        {
            string bill = string.Empty;
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX WHERE WORK_ID='{workId}' and TYPE_BILL = {typeBill} and BILL_NUMBER like '{workId}%'  ORDER BY ID  DESC;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt))
            {
                bill = $"{workId}-01/{process}";
            }
            else
            {
                string billOld = dt.Rows[0]["BILL_NUMBER"].ToString();
                int indexWo = billOld.IndexOf($"{workId}") + workId.Length + 1;
                var orderNO = billOld.Substring(indexWo, 2);
                var order = int.Parse(orderNO);
                bill = $"{workId}-{(order + 1).ToString().PadLeft(2, '0')}/{process}";
            }
            return bill;
        }

        //public string createBillName(string process)
        //{
        //    string bill = string.Empty;
        //    DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where DATE(TIME_CREATE)  = CURDATE() AND BILL_NUMBER LIKE '%{process}%'  ORDER BY ID DESC;");
        //    if (istableNull(dt))
        //    {
        //        bill = getTimeServer().ToString("ddMMyyyy") + $"-01/{process}";
        //    }
        //    else
        //    {
        //        bill = getTimeServer().ToString("ddMMyyyy") + "-" + (int.Parse(dt.Rows[0]["BILL_NUMBER"].ToString().Split('/')[0].Split('-')[1]) + 1).ToString("00") + $"/{process}";
        //    }
        //    return bill;
        //}
        public List<BillExportMaterial> GetListSummaryBillExportRequestPC(string work = "", bool NotDetail =true, bool GetRemain = false)
        {
            BillExportMaterial Bill_Export = new BillExportMaterial();

            string condition = string.IsNullOrEmpty(work) ? "" : $" and A.WORK_ID = '{work}' ";
            string sql = $"SELECT  * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID  where A.STATUS_EXPORT <>-1 AND month( A.TIME_CREATE) > month(now()) -2  AND " +
                $" ( A.TYPE_BILL = {BillExportMaterial.phiêuFAT} OR A.TYPE_BILL= {BillExportMaterial.phiêuSMT}) {condition} ORDER BY A.TIME_CREATE DESC ;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<BillExportMaterial>();

            List<BillExportMaterial> billExxports = new List<BillExportMaterial>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var subType = int.Parse(dt.Rows[i]["SUB_TYPE"].ToString());
                string loaiphieu = LoadSubTypeBillExportMaterial.SubTypeBillExportMaterial.FirstOrDefault(x => x.StatusID == subType).StatusName;
                int statusid = int.Parse(dt.Rows[i]["STATUS_EXPORT"].ToString());
                string trangthai = Bill_Export.billStatuses.Where(a => a.value == int.Parse(dt.Rows[i]["STATUS_EXPORT"].ToString())).FirstOrDefault().status;

                BillExportMaterial bill = new BillExportMaterial()
                {
                    BillId = dt.Rows[i]["BILL_NUMBER"].ToString(),
                    ModelId = dt.Rows[i]["MODEL"].ToString(),
                    WorkId = dt.Rows[i]["WORK_ID"].ToString(),
                    BillType = int.Parse(dt.Rows[i]["SUB_TYPE"].ToString()),
                    BillTypeName = loaiphieu,
                    BillStatus = int.Parse(dt.Rows[i]["STATUS_EXPORT"].ToString()),
                    BillStatusName = trangthai,
                    OP = dt.Rows[i]["OP"].ToString(),
                    BomVersion = dt.Rows[i]["BOM_VERSION"].ToString(),
                    CreateTime = DateTime.Parse(dt.Rows[i]["TIME_CREATE"].ToString()),
                    Note = dt.Rows[i]["NOTE"].ToString(),
                    BillExportMaterialDetails = NotDetail ? new List<BillExportMaterialDetail>() : LoadBillDetail(dt.Rows[i]["BILL_NUMBER"].ToString() , GetRemain),
                };
                billExxports.Add(bill);
                //rs.Rows.Add(dt.Rows[i]["WORK_ID"].ToString(), dt.Rows[i]["BILL_NUMBER"].ToString(), loaiphieu, trangthai, dt.Rows[i]["OP"].ToString(), dt.Rows[i]["TIME_CREATE"].ToString());
            }
            return billExxports;
        }

        public BillExportMaterial GetBillExportMaterialByBillNumber(string bill, bool GetRemain = false)
        {
            BillExportMaterial Bill_Export = new BillExportMaterial();

            DataTable dt = mySQL.GetDataMySQL($"SELECT  * FROM STORE_MATERIAL_DB.BILL_REQUEST_EX where BILL_NUMBER = '{bill}' ;");
            if (istableNull(dt)) return new BillExportMaterial();

            string loaiphieu = int.Parse(dt.Rows[0]["SUB_TYPE"].ToString()) == BillExportMaterial.XuatKeHoach ? "Kế hoạch" : "Phát sinh";
            string trangthai = Bill_Export.billStatuses.Where(a => a.value == int.Parse(dt.Rows[0]["STATUS_EXPORT"].ToString())).FirstOrDefault().status;

            BillExportMaterial billExport = new BillExportMaterial()
            {
                BillId = dt.Rows[0]["BILL_NUMBER"].ToString(),
                WorkId = dt.Rows[0]["WORK_ID"].ToString(),
                BillType = int.Parse(dt.Rows[0]["SUB_TYPE"].ToString()),
                BillTypeName = loaiphieu,
                BillStatus = int.Parse(dt.Rows[0]["STATUS_EXPORT"].ToString()),
                BillStatusName = trangthai,
                OP = dt.Rows[0]["OP"].ToString(),
                BomVersion = dt.Rows[0]["BOM_VERSION"].ToString(),
                CreateTime = DateTime.Parse(dt.Rows[0]["TIME_CREATE"].ToString()),
                BillExportMaterialDetails = LoadBillDetail(dt.Rows[0]["BILL_NUMBER"].ToString(), GetRemain),
            };
            return billExport;
        }



        public List<BillExportMaterialDetail> LoadBillDetail(string bill , bool GetRemain = false)
        {
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.REQUEST_EXPORT  WHERE BILL_NUMBER = '{bill}';";
            //var danhsachPartDaXuatTrongPhieu = getBill_Exported(bill);

            DataTable dt = mySQL.GetDataMySQL(sql);

            if (istableNull(dt)) return new List<BillExportMaterialDetail>();
            List<BillExportMaterialDetail> ls = new List<BillExportMaterialDetail>();
            foreach (DataRow item in dt.Rows)
            {
                string main = item["MAIN_PART"].ToString(); 
                string sub = item["PART_NUMBER"].ToString();
                float qtyRequest = float.Parse( item["QTY"].ToString());
                int real = int.Parse(item["QTY_REAL"].ToString());
                if (string.IsNullOrEmpty(main))
                {
                    continue;
                }
                BillExportMaterialDetail billExportMaterialDetail = new BillExportMaterialDetail();
                billExportMaterialDetail.BillNumber = bill;
                billExportMaterialDetail.MainPart = main;
                billExportMaterialDetail.PartNumber = sub;
                billExportMaterialDetail.Qty += qtyRequest;

                billExportMaterialDetail.RealExport = real;
                string partNumber = sub;
                if (GetRemain)
                {
                    if (partNumber.Contains("\n") || partNumber.Contains(" "))
                    {
                        string[] partArr = partNumber.Split('\n');
                        if (partArr.Length < 2)
                            partArr = partNumber.Split(' ');
                        partNumber = partNumber.Replace("\n", "  ");
                        List<int> lsPart = new List<int>();
                        for (int i = 0; i < partArr.Length; i++)
                        {
                            int remain = GetRemainPanacim(partArr[i]);
                            lsPart.Add(remain);
                        }
                        string a = string.Join(" | ", lsPart);
                        billExportMaterialDetail.Remain += a.ToString();
                    }
                    else
                    {
                        int remain = GetRemainPanacim(partNumber);
                        billExportMaterialDetail.Remain += remain.ToString();
                    }
                }
               
                ls.Add(billExportMaterialDetail);
            }
          
            return ls;
        }
        public List<partXuat> getBill_Exported(string billExport)
        {
            List<partXuat> parts = new List<partXuat>();
            string cmd = $"SELECT PART_NUMBER, SUM(QTY) FROM STORE_MATERIAL_DB.BILL_EXPORT_WH  WHERE BILL_REQUEST='{billExport}'  GROUP BY PART_NUMBER ;";
            DataTable dt = mySql.GetDataMySQL(cmd);
            if (istableNull(dt)) return parts;
            foreach (DataRow item in dt.Rows)
            {
                parts.Add(new partXuat { partNo = item[0].ToString(), qty = int.Parse(item[1].ToString()) });
            }
            return parts;
        }

        public IEnumerable<dynamic> GetListMainRequestedByPlan(string work)
        {
            string sql = $"SELECT B.BILL_NUMBER, B.MAIN_PART FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A " +
                $" INNER JOIN STORE_MATERIAL_DB.REQUEST_EXPORT  B  ON A.BILL_NUMBER = B.BILL_NUMBER " +
                $" where A.WORK_ID = '{work}' AND STATUS_EXPORT <> -1 AND (A.SUB_TYPE = 0 or A.SUB_TYPE = 2 OR A.SUB_TYPE = 3) ;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<dynamic>();
            var rs = (from r in dt.AsEnumerable()
                      select new
                      {
                          Main = r.Field<string>("MAIN_PART"),
                          Bill = r.Field<string>("BILL_NUMBER")
                      }).Distinct().ToList();
            return rs;

        }
    }
}
