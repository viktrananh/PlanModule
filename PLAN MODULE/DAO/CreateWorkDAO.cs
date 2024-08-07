using LinqToExcel;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.DAO
{
    public class CreateWorkDAO : BaseDAO
    {
        public bool IsModelPTHOnly(string model)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.PART_MODEL_CONTROL WHERE ID_MODEL='{model}' and PTH_ONLY = 1;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            return true;
        }

        public int GetPcbOnPanelByModel(string model)
        {
            DataTable DT = mySql.GetDataMySQL($"SELECT PCBS FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where ID_MODEL='{model}';");
            if(istableNull(DT)) return 1;
            return int.Parse(DT.Rows[0]["PCBS"].ToString());
        }

        public bool CreateWork(string customer, string model, string work, int numberPCBs, string op, int isRMA, int isXOUT, int isSamp, int pcbXout,
                  string woMother, string processMother, string processChild, string PO, bool isLinkMother, string bomversion, string comment, bool existWorkID, DateTime mfgDate, DateTime firstDate, DateTime lasDate, int tempNumber = 0, int monthFN=0)
        {
            string cmd = string.Empty;
            //if (existWorkID) return false;
            string action = existWorkID ? "UPDATE" : "CREATE";
            if (action == "CREATE")
            {
                BillExportMaterialDAO billExportMaterialDAO = new BillExportMaterialDAO();
                List<ROSE_Dll.DTO.BomContent> _BOMContent = new List<ROSE_Dll.DTO.BomContent>();
                int NumberModelGroup = 0;
                bool isGroupModel = billExportMaterialDAO.IsGroupModel(model, out NumberModelGroup);
                _BOMContent = new ROSE_Dll.DAO.BomDao().GetBomContents(new ROSE_Dll.DTO.BomGeneral() { Model = model, BomVersion = bomversion });
                if (billExportMaterialDAO.IsListEmty(_BOMContent))
                {
                    MessageBox.Show($"Không có định mức trong Bom version {bomversion} !");
                    return false;

                }
                var rs = (from r in _BOMContent
                          where r.MainPart.Length > 6
                          group r by new { MainPart = r.MainPart, Location = r.Location } into gr
                          select new
                          {
                              MainPart = gr.Key.MainPart,
                              InterPart = string.Join(" ", gr.Select(i => i.InterPart)),
                              Quantity = string.Join("|", gr.Select(ra => ra.Quantity)),
                          }).ToList();


                foreach (var g in rs)
                {
                    if (g.InterPart.Contains(model)) continue;
                    float request = 0;
                    string[] _quantity = g.Quantity.Split('|');
                    for (int i = 0; i < _quantity.Count(); i++)
                    {
                        string qtt = _quantity[i];
                        if (!string.IsNullOrEmpty(qtt))
                        {
                            request = float.Parse(qtt) * numberPCBs;
                            break;
                        }
                    }
                    if (isGroupModel)
                    {
                        request = request / NumberModelGroup;
                    }
                    cmd += $"INSERT IGNORE INTO `STORE_MATERIAL_DB`.`WORK_REMAIN_MAINPART` (`WORK_ID`,  `MAIN_PART`, `QTY_REQUEST`) VALUES ('{work}', '{g.MainPart}', '{request}');";
                }
            }    

            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER` (`WORK_ID`, `PO`, `CUSTOMER`, `MODEL`, `CREATER`, `DATE_CREATE`,`PCBS`,`TOTAL_PCBS`, `BOM_VERSION`, IS_RMA, IS_XOUT, PCB_XOUT, `WORK_MOTHER`, `IS_SAMPLE` , `COMMENT` , `STAMP_NUMBER` , " +
                $" `MFGDate` , `FirstDate` , `LastDate`) " +
               $"VALUES ('{work}', '{PO}', '{customer}', '{model}', '{op}',  now(), {numberPCBs - pcbXout }, {numberPCBs}, '{bomversion}', {isRMA}, {isXOUT},  {pcbXout}, '{woMother}', {isSamp} , '{comment}' , '{tempNumber}', '{mfgDate.ToString("yyyy-MM-dd")}' , '{firstDate.ToString("yyyy-MM-dd")}' , '{lasDate.ToString("yyyy-MM-dd")}' ) " +
               $" ON DUPLICATE KEY UPDATE  `PO` = '{PO}', `TOTAL_PCBS` =  {numberPCBs }, `PCBS` =  {numberPCBs - pcbXout },`BOM_VERSION`= '{bomversion}',  IS_RMA = '{isRMA}',IS_XOUT = '{isXOUT}', PCB_XOUT = '{pcbXout}' ,  `IS_SAMPLE` = '{isSamp}' , `COMMENT` = '{comment}' , `STAMP_NUMBER` = '{tempNumber}' , " +
               $" `MFGDate` = '{mfgDate.ToString("yyyy-MM-dd")}' , `FirstDate` = '{firstDate.ToString("yyyy-MM-dd")}' ,  `LastDate` = '{lasDate.ToString("yyyy-MM-dd")}',Month_Finish_PP='{monthFN}';";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`, `CUSTOMER`, `MODEL`, `PCB_QTY`, `WORK_ID_MOTHER`, `CREATER`,`DATE_CREATE`,`NOTE` , `MFGDate` , `FirstDate` , `LastDate`)" +
                $"VALUES ('{work}', '{customer}', '{model}', '{numberPCBs}', '{woMother}', '{op}', now(),'', '{mfgDate.ToString("yyyy-MM-dd")}' , '{firstDate.ToString("yyyy-MM-dd")}' , '{lasDate.ToString("yyyy-MM-dd")}');";

            if (isLinkMother)
                cmd += $"INSERT  INTO `TRACKING_SYSTEM`.`WORK_PROCESS_LINK` (`WORK_ID`, `PROCESS`, `WORK_CHILD`, `PROCESS_CHILD`, `DATETIME`, `OP`, `NOTE`) " +
                    $"VALUES ('{woMother}', '{processMother}', '{work}', '{processChild}', NOW(),'{op}', '{woMother}-{work}') ON DUPLICATE KEY UPDATE  `DATETIME` = NOW();";
            if (!mySQL.InsertDataMySQL(cmd)) return false;
            return true;
        }
        public bool CloseWork(string work, string model, string op)
        {
            string mycmd = "";
            mycmd += $"UPDATE TRACKING_SYSTEM.WORK_ORDER SET `STATUS`='CLOSE' WHERE WORK_ID='{work}';";
            mycmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`, `MODEL`, `CREATER`, `DATE_CREATE`, `NOTE` ) " +
                $" VALUES ('{work}', '{model}', '{op}', now(), 'CLOSE');";
            if (!mySQL.InsertDataMySQL(mycmd)) return false;
            return true;
        }

        public bool DeleteWork(string work, string user, bool IsWorkMother = false)
        {
            string cmd = string.Empty;
            cmd += $"delete from TRACKING_SYSTEM.MOUTER_PROGRAM where WORK_ID = '{work}';";
            cmd += $"delete FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}'; ";
            cmd += $"INSERT INTO `TRACKING_SYSTEM`.`WORK_ORDER_HISTORY` (`WORK_ID`,`CREATER`,`DATE_CREATE`,`NOTE`)" +
                $"VALUES ('{work}',  '{user}', now(),'DELETE');";
            cmd += $"delete FROM TRACKING_SYSTEM.WORK_PROCESS_LINK WHERE WORK_CHILD='{work}';";
            if (!mySQL.InsertDataMySQL(cmd)) return false;

            return true;
        }
        public bool IsWorkID(string work, out string model, out string cusID, out string status, out string process)
        {
            process =  model = cusID = status = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER  A INNER JOIN TRACKING_SYSTEM.PART_MODEL_CONTROL B ON A.MODEL COLLATE UTF8_UNICODE_CI = B.ID_MODEL WHERE WORK_ID='{work}';";

            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            status = dt.Rows[0]["STATUS"].ToString();
            process = dt.Rows[0]["PROCESS"].ToString();
            return true;
        }
        public bool isWorkID(string work, out string model, out string cusID, out string status)
        {
           model = cusID = status = string.Empty;
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}';";

            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            status = dt.Rows[0]["STATUS"].ToString();
            return true;
        }
        
        public bool isWorkID(string work, out string model, out string cusID, out int pcbs, out string status, out string bomversion, out int isRMA, out int isXout, out int pcbXout, out int isSamp , out int tempNumber)
        {
            model = cusID = status = bomversion = string.Empty;
            pcbs = isRMA = isXout = pcbXout = isSamp = tempNumber = 0;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}';");
            if (istableNull(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            pcbs = int.Parse(dt.Rows[0]["TOTAL_PCBS"].ToString());
            status = dt.Rows[0]["STATUS"].ToString();
            bomversion = dt.Rows[0]["BOM_VERSION"].ToString();
            isRMA = int.Parse(dt.Rows[0]["IS_RMA"].ToString());
            isXout = int.Parse(dt.Rows[0]["IS_XOUT"].ToString());
            pcbXout = int.Parse(dt.Rows[0]["PCB_XOUT"].ToString());
            isSamp = int.Parse(dt.Rows[0]["IS_SAMPLE"].ToString());
            tempNumber = int.Parse(dt.Rows[0]["STAMP_NUMBER"].ToString());
            return true;
        }

        public DataTable getModelByCus(string cus)
        {
            return mySQL.GetDataMySQL($"SELECT `ID_MODEL`, `PROCESS` FROM TRACKING_SYSTEM.PART_MODEL_CONTROL WHERE CUSTOMER_ID='{cus}' AND EOL = 0 ORDER BY ID_MODEL;");
        }
        
       
       
        public DataTable getDetailBom(string model, int numbers, string version)
        {

            DataTable dtBill = new DataTable();
            dtBill.Columns.Add("MODEL_ID");
            dtBill.Columns.Add("MAIN_PART", typeof(string));
            dtBill.Columns.Add("INTER_PART", typeof(string));
            dtBill.Columns.Add("QUANTITY", typeof(float));
            dtBill.Columns.Add("REQUIRE", typeof(float));
            dtBill.Columns.Add("Length", typeof(int));
            string cmd = "";
            if (!model.StartsWith( "SEF"))
            {
                cmd = $"SELECT CUSTOMER,A.MODEL,MAIN_PART,B.PART_NUMBER, CS_PART,A.MFG_PART,A.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"A.DESCRIPTION,B.PITCH,B.DIRECTION,B.PACKING FROM  STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.MASTER_PART B ON A.INTER_PART collate utf8_general_ci = B.PART_NUMBER" +
                $" and A.MODEL = '{model}' and A.VERSION = '{version}'; ";
                //return mySql.GetDataMySQL(cmd);
            }
            else
            {
                cmd = $"SELECT CUSTOMER,C.MODEL,MAIN_PART,C.PART_NUMBER, CS_PART,C.MFG_PART,C.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"C.DESCRIPTION,PITCH,DIRECTION,PACKING FROM(SELECT CUSTOMER, MODEL, MAIN_PART, PART_NUMBER, A.CS_PART, B.MFG_PART, B.MFG_NAME, TYPE, LOCATION, LEVEL, PARENT_PART, PROCESS, QUANTITY, SIDE1, SIDE2," +
                $"B.DESCRIPTION FROM STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.AML_LIST B" +
                $" ON B.CUS_PART = A.CS_PART where A.MODEL = '{model}' and A.VERSION = '{version}') C INNER JOIN STORE_MATERIAL_DB.MASTER_PART ON C.PART_NUMBER = MASTER_PART.PART_NUMBER; ";
                //return mySql.GetDataMySQL(cmd);
            }
            //string sql = $"SELECT *  FROM STORE_MATERIAL_DB.BOM_CONTENT WHERE MODEL = '{model}' AND VERSION = '{version}' AND MAIN_PART LIKE '%{model}%';";
            DataTable dtBom = mySQL.GetDataMySQL(cmd);
            string interPart = string.Empty;
            string mainCheck = string.Empty;
            string locationCheck = string.Empty;
            string rate = string.Empty;
            var rs = from r in dtBom.AsEnumerable()
                     group r by new { MainPart = r.Field<string>("MAIN_PART"), Location = r.Field<string>("LOCATION") } into gr
                     select new
                     {
                         gr.Key.MainPart,
                         InterPart = string.Join(" ", gr.Select(i => i.Field<string>("PART_NUMBER"))),
                         Quantity = string.Join("|", gr.Select(a => a.Field<string>("QUANTITY"))),
                         gr.Key.Location
                     };

            foreach (var g in rs)
            {
                float request = 0;
                string[] _quantity = g.Quantity.Split('|');
                for (int i = 0; i < _quantity.Count(); i++)
                {
                    string qtt = _quantity[i];
                    if (!string.IsNullOrEmpty(qtt))
                    {
                        request = float.Parse(qtt) * numbers;
                        break;
                    }
                }
                dtBill.Rows.Add(model, g.MainPart, g.InterPart, request, request,g.InterPart.Length);
            }
            return dtBill;
        }
        public DataTable getBomversion(string model)
        {
            return mySQL.GetDataMySQL($"SELECT  VERSION FROM STORE_MATERIAL_DB.BOM_GENARAL where MODEL_ID= '{model}' and STATUS_BOM = '1' ORDER BY VERSION DESC;");
        }
        #region priority
        public bool isPartNumber(string partNumber)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.MASTER_PART WHERE PART_NUMBER='{partNumber}';");
            if (istableNull(dt)) return false;
            return true;

        }
        #endregion
        public DataTable GeBomForModel(string model, string version)
        {
            string cmd = "";
            if (!model.StartsWith("SEF"))
            {
                cmd = $"SELECT CUSTOMER,A.MODEL,MAIN_PART,B.PART_NUMBER, CS_PART,A.MFG_PART,A.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"A.DESCRIPTION,B.PITCH,B.DIRECTION,B.PACKING FROM  STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.MASTER_PART B ON A.INTER_PART collate utf8_general_ci = B.PART_NUMBER" +
                $" and A.MODEL = '{model}' and A.VERSION = '{version}'; ";
                //return mySql.GetDataMySQL(cmd);
            }
            else
            {
                cmd = $"SELECT CUSTOMER,C.MODEL,MAIN_PART,C.PART_NUMBER, CS_PART,C.MFG_PART,C.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"C.DESCRIPTION,PITCH,DIRECTION,PACKING FROM(SELECT CUSTOMER, MODEL, MAIN_PART, PART_NUMBER, A.CS_PART, B.MFG_PART, B.MFG_NAME, TYPE, LOCATION, LEVEL, PARENT_PART, PROCESS, QUANTITY, SIDE1, SIDE2," +
                $"B.DESCRIPTION FROM STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.AML_LIST B" +
                $" ON B.CUS_PART = A.CS_PART where A.MODEL = '{model}' and A.VERSION = '{version}') C INNER JOIN STORE_MATERIAL_DB.MASTER_PART ON C.PART_NUMBER = MASTER_PART.PART_NUMBER; ";
                //return mySql.GetDataMySQL(cmd);
            }
            DataTable dt = mySQL.GetDataMySQL(cmd);

            return dt;
        }
      
        public bool isDIDofCus(string did, string cus,  out string cusPart)
        {
            cusPart = string.Empty;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.DATA_INPUT where did='{did}' and customer='{cus}' ;");
            if (istableNull(dt)) return false;
            cusPart = dt.Rows[0]["cust_kp_no"].ToString();
            return true;
        }
        public bool isPartBomMaster(string part, out string mfgPart)
        {
            mfgPart = string.Empty;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.MASTER_PART where PART_NUMBER ='{part}' and `TYPE` = 0;");
            if (istableNull(dt)) return false;
            mfgPart = dt.Rows[0]["MFG_PART"].ToString();
            return true;
        }

        #region check Material
        public int getRemainPanacim(string partNumber, string work, string cusID, bool isFox)
        {
            string sql = "";
            if (!isFox)
            {
                sql += $"SELECT PART_NUMBER,SUM(ESTIMATED_QUANTITY) as REMAIN  FROM PanaCIM.dbo.unloaded_reel_view where  AREA='KHOSMT'  and PART_NUMBER='{partNumber}' and USER_DATA='{cusID}' group by PART_NUMBER;";
            }
            else
            {
                sql += $"SELECT  PART_NUMBER,SUM(ESTIMATED_QUANTITY) as REMAIN  FROM PanaCIM.dbo.unloaded_reel_view where  AREA = 'KHOSMT'  and PART_NUMBER = '{partNumber}' and USER_DATA_2 = '{work}' group by PART_NUMBER; ";
            }
            DataTable dt = sqlSever.GetDataSQL(sql);
            if (istableNull(dt)) return 0;
            return int.Parse(dt.Rows[0]["REMAIN"].ToString());
        }
        public DataTable getRateOFModel(string model)
        {
            return mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.PICKUP_RATE_CUS WHERE MODEL = '{model}';");
        }
        public bool isFoxorLux(string CUS)
        {
            DataTable dt = mySQL.GetDataMySQL("SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER;");
            bool contains = false;
            try
            {
                contains = dt.AsEnumerable().Any(row => CUS == row.Field<string>("CUSTOMER_ID") && row.Field<int>("USE_CUS_TEM") == 1);
            }
            catch
            {
                contains = false;
            }
            if (contains) return true;
            return false;

        }
        public DataTable getMaterialReqest(string work, string model, string cus, bool isFoxCoon)
        {
            string sql = string.Empty;

            if (isFoxCoon)
            {
                sql = $"SELECT A.PART_NUMBER, sum( A.QTY ) `QTY` FROM STORE_MATERIAL_DB.REQUEST_EXPORT A  INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID AND A.WORK_ID <> '{work}'  where MODEL = '{model}'  GROUP BY PART_NUMBER;";
            }
            else
            {
                sql = $"SELECT A.PART_NUMBER, sum( A.QTY ) `QTY`  FROM STORE_MATERIAL_DB.REQUEST_EXPORT A  INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID AND A.WORK_ID <> '{work}' where CUSTOMER = '{cus}'  GROUP BY PART_NUMBER;";
            }
            return mySQL.GetDataMySQL(sql);
        }
        public DataTable getMaterialExport(string work, string model, string cus, bool isFoxcoon)
        {
            string sql = string.Empty;

            if (isFoxcoon)
            {
                sql = $@"select   A.PART_NUMBER, SUM(  A.QTY) `QTY`   FROM STORE_MATERIAL_DB.BILL_EXPORT_WH A
                       INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID AND A.WORK_ID <> '{work}' where MODEL = '{model}' group by PART_NUMBER; ";
            }
            else
            {
                sql = $@"select   A.PART_NUMBER, SUM(  A.QTY) `QTY`   FROM STORE_MATERIAL_DB.BILL_EXPORT_WH A
 INNER JOIN TRACKING_SYSTEM.WORK_ORDER B ON A.WORK_ID = B.WORK_ID AND A.WORK_ID <> '{work}' where CUSTOMER = '{cus}' group by PART_NUMBER; ";
            }
            return mySQL.GetDataMySQL(sql);
        }
        public DataTable getMaterialExportEDofWo(string work)
        {
            string sql = $"SELECT PART_NUMBER, QTY FROM STORE_MATERIAL_DB.BILL_EXPORT_WH where WORK_ID = '{work}';";
            return mySQL.GetDataMySQL(sql);
        }
        public DataTable getExportedWork(string work)
        {
            return mySQL.GetDataMySQL($"SELECT B.PART_NUMBER,B.QTY FROM STORE_MATERIAL_DB.BILL_REQUEST_EX A inner join STORE_MATERIAL_DB.BILL_EXPORT_WH B on A.BILL_NUMBER = B.BILL_REQUEST AND A.WORK_ID = B.WORK_ID AND SUB_TYPE = 0 AND A.WORK_ID = '{work}' ;");
        }
        #endregion

        #region Plan
        public DataTable getPlaned(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.LASER_PLAN WHERE WORK_id='{work}';");
            return dt;
        }

       
     
        
       

        #endregion
        public List<string> GetLsMainPartUsing(string model, string version)
        {
            List<string> lsMainPart = new List<string>();
            string sql = $"SELECT * FROM STORE_MATERIAL_DB.BOM_CONTENT WHERE MODEL = '{model}' AND VERSION = '{version}' AND IS_USING = 1  AND  ( `TYPE` = 1 OR `TYPE` = 2 );";
            DataTable dt = mySQL.GetDataMySQL(sql);

            lsMainPart = (from r in dt.AsEnumerable()
                          select r.Field<string>("MAIN_PART")).ToList();
            return lsMainPart;
        }
       
        public DataTable GetLsWork(string model, string work = "")
        {
            string sql = string.IsNullOrEmpty(work) ?
                 $"SELECT *  FROM TRACKING_SYSTEM.WORK_ORDER where MODEL='{model}' ORDER BY ID DESC;"
                : $"SELECT *  FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}'  ORDER BY ID DESC;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public Work GetWorkOrderById( string work)
        {
            string sql = $"SELECT *  FROM TRACKING_SYSTEM.WORK_ORDER where WORK_ID='{work}' ;";
            
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new Work();
            Work wo  = new Work(dt.Rows[0]);
            return wo;
        }

        public DataTable GetLsWorkByPO(string po, string model, bool isCreat= true , string workId=  "")
        {
            string sql = isCreat == true ? $"SELECT *  FROM TRACKING_SYSTEM.WORK_ORDER where PO='{po}' AND MODEL='{model}' ;"
                : $"SELECT *  FROM TRACKING_SYSTEM.WORK_ORDER where PO='{po}' AND MODEL = '{model}' AND  WORK_ID <> '{workId}' ORDER BY ID DESC;";
                
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }


        public DataTable GetModelConfig(string model)
        {
            string sql = $"SELECT * FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where ID_MODEL = '{model}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }

       public List<CusData> GetCustomerDatas(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM STORE_MATERIAL_DB.CUSTOMER_DATA where WO = '{work}';");
            if(istableNull(dt)) return new List<CusData>();

            List<CusData> customerDatas = new List<CusData>();
            foreach (DataRow item in dt.Rows)
            {
                CusData cus = new CusData(item);
                customerDatas.Add(cus);
            }
            return customerDatas;
        }
      
    }
}
