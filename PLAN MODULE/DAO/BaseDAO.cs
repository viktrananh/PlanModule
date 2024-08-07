using DevExpress.PivotGrid.OLAP;
using DevExpress.XtraSpreadsheet.Import.OpenXml;
using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Laser;
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
    public class BaseDAO
    {
        //mySql
        public ROSE_Dll.sqlClass mySQL = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "TRACKING_SYSTEM", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass mySql = new ROSE_Dll.sqlClass("mariadbsv1.mvn", "TRACKING_SYSTEM", "admin", "ManuAdmin$123");
        public ROSE_Dll.sqlClass sqlSever = new ROSE_Dll.sqlClass("192.168.10.253\\PANACIM", "PANACIM", "sa", "PANASONIC1!");
        public bool istableNull(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;
            return false;
        }
        public void LoadModel(ref ComboBox combo)
        {
            System.Data.DataTable dt = mySql.GetDataMySQL("SELECT ID_MODEL FROM TRACKING_SYSTEM.PART_MODEL_CONTROL;");
            combo.DataSource = dt;
            combo.DisplayMember = "ID_MODEL";
            combo.ValueMember = "ID_MODEL";
        }
        public void LoadCluster(ref ComboBox combo)
        {
            System.Data.DataTable dt = mySql.GetDataMySQL("SELECT Name FROM MASTER.Cluster;");
            combo.DataSource = dt;
            combo.DisplayMember = "Name";
            combo.ValueMember = "Name";
        }
        public bool IsListEmty<T>(List<T> myList)
        {
            if (myList != null && myList.Count != 0) return false;
            return true;
        }
        public void LoadCustomer(ComboBox cbo)
        {
            System.Data.DataTable dt = DBConnect.getData("SELECT CUSTOMER_NAME, CUSTOMER_ID FROM TRACKING_SYSTEM.DEFINE_CUSTOMER ORDER BY CUSTOMER_NAME;");
            cbo.DataSource = dt;
            cbo.DisplayMember = "CUSTOMER_NAME";
            cbo.ValueMember = "CUSTOMER_ID";
        }
        public List<ProcessDefine> GetAllProcess()
        {
            string sql = $"SELECT * FROM MASTER.DEFINE_PROCESS where station_name is not null;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<ProcessDefine>();
            List<ProcessDefine> ls = (from r in dt.AsEnumerable()
                                      select new ProcessDefine()
                                      {
                                          Id = r.Field<int>("Id"),
                                          Name = r.Field<string>("Name"),

                                      }).ToList();
            return ls;
        }
        public bool IsWorkCreatedManuData(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT COUNT(WORK_ID) FROM STORE_MATERIAL_DB.DATA_INPUT WHERE WORK_ID='{work}';");
            if (dt.Rows[0][0].ToString() != "0") return true;
            return false;
        }
        public bool IsWorkHasLink(string wo, out string workLink)
        {
            workLink = string.Empty;
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_PROCESS_LINK WHERE WORK_ID='{wo}';");
            if (istableNull(dt)) return false;
            workLink = dt.Rows[0]["WORK_CHILD"].ToString();
            return true;
        }
        public bool IsWorkRunning(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ID_DETAIL where WORK_ID='{work}';");
            if (istableNull(dt)) return false;
            return true;
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(DBConnect.getData("SELECT NOW();").Rows[0][0].ToString());
        }
        public bool isWorkID(string work, out string model, out string cusID, out int pcbs, out string status, out string bomversion,
            out int rma, out int xout, out int pcbXout, out string modelName, out int pcbOnPanel, out string PRC)
        {
            model = cusID = status = bomversion = modelName = PRC = string.Empty;
            pcbs = rma = xout = pcbXout = pcbOnPanel = 0;
            DataTable dt = DBConnect.getData($@"SELECT 
                    A.WORK_ID,
                    A.PO,
                    A.CUSTOMER,
                    A.MODEL,
                    A.PCBS,
                    A.`STATUS`,
                    A.IS_RMA,
                    A.BOM_VERSION,
                    A.IS_XOUT,
                    A.PCB_XOUT,
                    B.MODEL_NAME,
                    B.PCBS `PCBONPANEL`,
                    B.PROCESS
                 FROM TRACKING_SYSTEM.WORK_ORDER A INNER JOIN  TRACKING_SYSTEM.PART_MODEL_CONTROL B ON A.MODEL collate utf8_unicode_ci = B.ID_MODEL where WORK_ID = '{work}'; ");
            if (istableNull(dt)) return false;
            model = dt.Rows[0]["MODEL"].ToString();
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            pcbs = int.Parse(dt.Rows[0]["PCBS"].ToString());
            status = dt.Rows[0]["STATUS"].ToString();
            bomversion = dt.Rows[0]["BOM_VERSION"].ToString();
            rma = int.Parse(dt.Rows[0]["IS_RMA"].ToString());
            xout = int.Parse(dt.Rows[0]["IS_XOUT"].ToString());
            pcbXout = int.Parse(dt.Rows[0]["PCB_XOUT"].ToString());
            modelName = dt.Rows[0]["MODEL_NAME"].ToString();
            pcbOnPanel = int.Parse(dt.Rows[0]["PCBONPANEL"].ToString());
            PRC = dt.Rows[0]["PROCESS"].ToString();
            return true;
        }

        public bool IsWorkID(string work, out string modelWork, out string modelParent, out string modelGenaral, out string cusID, out int pcbs, out string status, out string bomversion,
          out int rma, out int xout, out int pcbXout, out string modelName, out int pcbOnPanel, out string PRC, out int bySet, out int IsGroup)
        {
            modelWork = modelParent = modelGenaral = cusID = status = bomversion = modelName = PRC = string.Empty;
            pcbs = rma = xout = pcbXout = pcbOnPanel = bySet= IsGroup= 0;
            DataTable dt = DBConnect.getData($@"SELECT 
                    A.WORK_ID,
                    A.PO,
                    A.CUSTOMER,
                    A.MODEL,
                    A.PCBS,
                    A.`STATUS`,
                    A.IS_RMA,
                    A.BOM_VERSION,
                    A.IS_XOUT,
                    A.PCB_XOUT,
                    B.MODEL_NAME,
                    B.PCBS,
                    B.PROCESS,
                    B.MODEL_ID,
                    B.MODEL_PARENT,
                    B.BY_SET,
                    B.IS_GROUP_MODEL
                 FROM TRACKING_SYSTEM.WORK_ORDER A INNER JOIN  TRACKING_SYSTEM.PART_MODEL_CONTROL B ON A.MODEL collate utf8_unicode_ci = B.ID_MODEL where WORK_ID = '{work}'; ");
            if (istableNull(dt)) return false;
            modelWork = dt.Rows[0]["MODEL"].ToString();
            modelParent = dt.Rows[0]["MODEL_PARENT"].ToString();
            modelGenaral = dt.Rows[0]["MODEL_ID"].ToString();
            cusID = dt.Rows[0]["CUSTOMER"].ToString();
            pcbs = int.Parse(dt.Rows[0]["PCBS"].ToString());
            status = dt.Rows[0]["STATUS"].ToString();
            bomversion = dt.Rows[0]["BOM_VERSION"].ToString();
            rma = int.Parse(dt.Rows[0]["IS_RMA"].ToString());
            xout = int.Parse(dt.Rows[0]["IS_XOUT"].ToString());
            pcbXout = int.Parse(dt.Rows[0]["PCB_XOUT"].ToString());
            modelName = dt.Rows[0]["MODEL_NAME"].ToString();
            pcbOnPanel = int.Parse(dt.Rows[0]["PCBS"].ToString());
            PRC = dt.Rows[0]["PROCESS"].ToString();
            bySet = int.Parse( dt.Rows[0]["BY_SET"].ToString());
            IsGroup = int.Parse(dt.Rows[0]["IS_GROUP_MODEL"].ToString());
            return true;
        }
        //public bool isFoxorLux(string CUS)
        //{
        //    DataTable dt = DBConnect.getData("SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER;");
        //    bool contains = dt.AsEnumerable().Any(row => CUS == row.Field<String>("CUSTOMER_ID") && row.Field<int>("USE_CUS_TEM") == 1);
        //    if (contains) return true;
        //    return false;

        //}
        public string GetModelMother()
        {
            return "";
        }
        public bool checkWork(string work, out string model, out string cusID, out string cusModel, out string cusCode, out int totalPCB, out string statusWo, out int pcbs, out string po , out int exported)
        {
            model = cusID = cusModel = cusCode = statusWo = po = string.Empty;
            totalPCB = exported =  0;
            pcbs = 0;
            string sql = $@"SELECT* FROM TRACKING_SYSTEM.WORK_ORDER A
                INNER JOIN TRACKING_SYSTEM.BOX_PACKING B ON A.MODEL = B.MODEL_ID
                INNER JOIN  TRACKING_SYSTEM.PART_MODEL_CONTROL C ON A.MODEL COLLATE UTF8_UNICODE_CI = C.ID_MODEL where A.work_id = '{work}'; ";
            DataTable DT = DBConnect.getData(sql);
            if (istableNull(DT)) return false;
               model = DT.Rows[0]["MODEL"].ToString();
            cusID = DT.Rows[0]["CUSTOMER"].ToString();
            cusModel = DT.Rows[0]["MODEL_NAME"].ToString();
            cusCode = DT.Rows[0]["CUS_CODE"].ToString();
            statusWo = DT.Rows[0]["STATUS"].ToString();
            pcbs = int.Parse(DT.Rows[0]["PCBS"].ToString());
            totalPCB = int.Parse(DT.Rows[0]["TOTAL_PCBS"].ToString());
            po = DT.Rows[0]["PO"].ToString();
            exported = int.Parse(DT.Rows[0]["EXPORT"].ToString());
            return true;
        }
        public DataTable GetLsModel(string cusID, string modelID = "")
        {
            string sql = string.IsNullOrEmpty(modelID)
                ? $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL WHERE CUS_ID='{cusID}' ORDER BY MODEL_ID;"
                : $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL where MODEL_ID='{modelID}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public DataTable GetModelById( string modelID )
        {
            string sql =  $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL where MODEL_ID='{modelID}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public DataTable getModelByCus(string cus)
        {
            return mySQL.GetDataMySQL($"SELECT DISTINCT ID_MODEL FROM TRACKING_SYSTEM.PART_MODEL_CONTROL WHERE CUSTOMER_ID='{cus}' AND EOL = 0 ORDER BY ID_MODEL;");
        }
        public DataTable GetCustomer()
        {
            return mySQL.GetDataMySQL($"SELECT CUSTOMER_NAME, CUSTOMER_ID FROM TRACKING_SYSTEM.DEFINE_CUSTOMER WHERE EOL<>1;");
        }
        public List<CycleTime> GetCycleTimeForModel(string model)
        {
            List<CycleTime> cycleTimes = new List<CycleTime>();
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.PRODUCT_CYCLE_TIME where MODEL_ID='{model}';");
            if (istableNull(dt)) return cycleTimes;
            cycleTimes = (from r in dt.AsEnumerable()
                          select new CycleTime()
                          {
                              Side = r.Field<string>("SIDE"),
                              LineID = r.Field<string>("LINE"),
                              Times = r.Field<double>("CYCLE_TIME"),
                              //Performance = r.Field<double>("PERFORMANCE")
                          }).ToList();
            return cycleTimes;
        }
        public bool isFoxorLux(string CUS)
        {
            DataTable dt = mySQL.GetDataMySQL("SELECT * FROM TRACKING_SYSTEM.DEFINE_CUSTOMER;");
            bool contains = false;
            try
            {
                contains = dt.AsEnumerable().Any(row => CUS == row.Field<String>("CUSTOMER_ID") && row.Field<int>("USE_CUS_TEM") == 1);
            }
            catch
            {
                contains = false;
            }
            if (contains) return true;
            return false;
        }
        public bool IsGroupModel(string model, out int NumberModelGroup)
        {
            NumberModelGroup = 0;
            string sql = $"SELECT DISTINCT B.MODEL_ID FROM TRACKING_SYSTEM.PART_MODEL_CONTROL A " +
                $"INNER JOIN TRACKING_SYSTEM.MIX_MODEL_ORDER B ON A.ID_MODEL collate utf8_unicode_ci = B.GROUP_MODEL where A.ID_MODEL='{model}' AND A.IS_GROUP_MODEL = 1 ;";

            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            NumberModelGroup = dt.Rows.Count;
            return true;
        }
        public string getMarkInfor(string work)
        {
            string rs = "...";
            DataTable dt = mySQL.GetDataMySQL($"SELECT count(*) count,tpye FROM TRACKING_SYSTEM.MVN_LASER_MARKING_DATA where work = '{work}' group by tpye;");
            if (istableNull(dt)) return rs;
            rs = "";
            foreach (DataRow item in dt.Rows)
            {
                rs += item["tpye"].ToString().Contains("1") ? "Panel:" + item["count"].ToString() + ". " : "Pcb:" + item["count"].ToString() + ". ";
            }
            return rs;
        }

   

    }
}
