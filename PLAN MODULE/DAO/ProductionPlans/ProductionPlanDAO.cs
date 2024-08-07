using PLAN_MODULE.DTO;
using PLAN_MODULE.DTO.Planed;
using PLAN_MODULE.DTO.Planed.CreatPlan;
using PLAN_MODULE.DTO.ProductionPlans;
using ROSE_Dll.ViewModel.ProducingProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    public class ProductionPlanDAO : BaseDAO
    {


        public List<ProductionPlanCreat> CreatProductionPlan(string work, string model, int Qty, DateTime timeStart, List<ClusterDetailVm> clusterDetailVms)
        {
            string _MachineName = Environment.MachineName;


            DateTime timeNow = getTimeServer();

            DateTime StarndardStart = new DateTime(timeStart.Year, timeStart.Month, timeStart.Day, 8, 0, 0);
            DateTime nextDay = timeStart.AddDays(1);
            DateTime StarndardEnd = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, 7, 59, 59);

            List<ProductionPlanCreat> Ls = new List<ProductionPlanCreat>();

            foreach (var item in clusterDetailVms)
            {

                string planIndex = $"{timeStart.ToString("yyyyMMddHHmmss")}{_MachineName}";
                var cycletime = item.Cycletime;
                var performance = item.Performance;
                double timeProduct = Math.Round((Qty * cycletime * 100) / performance);
                DateTime timeEnd = timeStart.AddSeconds(timeProduct);

                DateTime MfgDateStart = timeStart.Hour < 8 ? timeStart.AddDays(-1).Date : timeStart.Date;
                DateTime NextMfgDateStart = MfgDateStart.AddDays(1);

                DateTime MfgDateEnd = timeEnd.Hour < 8 ? timeEnd.AddDays(-1).Date : timeEnd.Date;
                if (MfgDateStart.Date == MfgDateEnd.Date)
                {
                    ProductionPlanCreat production = new ProductionPlanCreat();
                    production.WorkId = work;
                    production.ModelId = model;

                    production.Count = Qty;
                    production.StartTime = timeStart;
                    production.EndTime = timeEnd;
                    production.MFGDate = MfgDateStart;
                    production.ClusterDetailId = item.Id;
                    production.PlanIndex = planIndex;
                    production.LineId = item.LineId;
                    production.LineName = item.LineName;
                    Ls.Add(production);
                }
                else
                {
                    DateTime lastTime = new DateTime(NextMfgDateStart.Year, NextMfgDateStart.Month, NextMfgDateStart.Day, 7, 59, 59);

                    timeProduct = (lastTime - timeStart).TotalSeconds;

                    int qtyProductInTime = (int)Math.Round((performance * timeProduct) / (cycletime * 100));

                    ProductionPlanCreat production = new ProductionPlanCreat();
                    production.WorkId = work;
                    production.ModelId = model;

                    production.Count = qtyProductInTime;
                    production.StartTime = timeStart;
                    production.EndTime = lastTime;
                    production.MFGDate = MfgDateStart;
                    production.ClusterDetailId = item.Id;

                    production.PlanIndex = planIndex;
                    production.LineId = item.LineId;
                    production.LineName = item.LineName;
                    Ls.Add(production);

                    int RemainQty = Qty - qtyProductInTime;
                    DateTime startTimeNew = new DateTime(NextMfgDateStart.Year, NextMfgDateStart.Month, NextMfgDateStart.Day, 8, 0, 0);
                    var productPlan = CreatProductionPlan(work, model, RemainQty, startTimeNew, new List<ClusterDetailVm>() { item });
                    Ls.AddRange(productPlan);
                }
            }
            return Ls;

        }



        public List<string> GetDeliveryPlan(string work)
        {
            DataTable dt = mySql.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WorkDeliveryPlan where WorkId='{work}';");
            if (istableNull(dt)) return new List<string>();
            List<string> lst = dt.AsEnumerable().Select(x => x.Field<DateTime>("DeliveryDate").ToString("yyyy-MM-dd")).ToList();
            return lst;

        }
        public List<StationModel> GetDefineStationForModel(string model)
        {
            List<StationModel> stationModels = new List<StationModel>();
            DataTable dt = mySQL.GetDataMySQL($"SELECT A.STATION_ID, A.STATION_NAME, A.IS_QUANTITY, A.SIDE_PANACIM, A.TEM_USE, B.ID, B.NOTE FROM TRACKING_SYSTEM.DEFINE_STATION_ID  A " +
                $" INNER JOIN `MASTER`.DEFINE_CLUSTER B ON A.CLUSTER_ID = B.ID  where MODEL_ID = '{model}' ;");
            stationModels = (from r in dt.AsEnumerable()
                             select new StationModel()
                             {
                                 StationID = r.Field<string>("STATION_ID"),
                                 StationName = r.Field<string>("STATION_NAME"),
                                 IsQuantity = r.Field<int>("IS_QUANTITY"),
                                 Side = r.Field<string>("SIDE_PANACIM"),
                                 TemType = r.Field<int>("TEM_USE"),
                                 ClusterID = r.Field<int>("ID"),
                                 ClusterKey = r.Field<string>("NOTE")
                             }).OrderBy(x => x.StationID).ToList();
            return stationModels;
        }
        public int GetPCBExportedForWork(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT SUM(PCBS) FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT where WORK_ID='{work}';");
            if (istableNull(dt)) return 0;
            return string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : int.Parse(dt.Rows[0][0].ToString());
        }
        public DataTable GetTablePlan(DateTime date)
        {
            DataTable dt = mySQL.GetDataMySQL($@"SELECT A.WORK_ID, B.MODEL, C.MODEL_NAME,B.BOM_VERSION, A.QTY, A.SIDE, A.START_TIME, A.END_TIME, A.LINE, A.`PROCESS`, A.TIMES, A.DESCRIPTION, A.`PLAN_INDEX`, A.`CYCLE_TIME`, A.`PRODUCTION_TIME`, C.`PCBS` FROM TRACKING_SYSTEM.WORK_PLANNING_OVERVIEW A 
                        INNER JOIN TRACKING_SYSTEM.WORK_ORDER  B ON A.WORK_ID collate utf8_unicode_ci = B.WORK_ID
                        INNER JOIN TRACKING_SYSTEM.PART_MODEL_CONTROL C ON B.MODEL collate utf8_unicode_ci = C.ID_MODEL where ( MONTH(A.MFG)  = '{date.Month}' OR MONTH(A.MFG)  = '{date.Month - 1}' OR MONTH(A.MFG)  = '{date.Month - 2}') ;");
            return dt;
        }
        public List<PlanDetail> GetPlan(DateTime date)
        {
            List<PlanDetail> plans = new List<PlanDetail>();
            DataTable dt = mySQL.GetDataMySQL($@"SELECT A.WORK_ID, B.MODEL, C.MODEL_NAME,B.BOM_VERSION, A.QTY, A.SIDE, A.START_TIME, A.END_TIME, A.LINE, A.ROUTE, A.TIMES, A.DESCRIPTION FROM TRACKING_SYSTEM.WORK_PLANNING_OVERVIEW A 
                        INNER JOIN TRACKING_SYSTEM.WORK_ORDER  B ON A.WORK_ID collate utf8_unicode_ci = B.WORK_ID
                        INNER JOIN TRACKING_SYSTEM.PART_MODEL_CONTROL C ON B.MODEL collate utf8_unicode_ci = C.ID_MODEL where IS_LASER = 0 AND ( MONTH(A.MFG)  = '{date.Month}' OR MONTH(A.MFG)  = '{date.Month - 1}' OR MONTH(A.MFG)  = '{date.Month - 2}') ;");
            if (istableNull(dt)) return plans;
            plans = (from r in dt.AsEnumerable()
                     select new PlanDetail()
                     {
                         WorkID = r.Field<string>("WORK_ID"),
                         Model = r.Field<string>("MODEL"),
                         CusModel = r.Field<string>("MODEL_NAME"),
                         BomVer = r.Field<string>("BOM_VERSION"),
                         Side = r.Field<string>("SIDE"),
                         Qty = r.Field<int>("QTY"),
                         Line = r.Field<string>("LINE"),
                         StartTime = r.Field<DateTime>("START_TIME"),
                         EndTime = r.Field<DateTime>("END_TIME"),
                         Route = r.Field<string>("ROUTE"),
                         Description = r.Field<string>("DESCRIPTION"),
                         //IndexPlan = r.
                     }).ToList();
            return plans;
        }
        public int GetProductionStation(string work, string station, int pcbOnPanel)
        {
            DataTable DT = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_ID_DETAIL WHERE WORK_ID='{work}' AND STATION = '{station}';");
            if (istableNull(DT)) return 0;
            return int.Parse(DT.Rows[0]["TEM"].ToString()) == 1
                ? int.Parse(DT.Rows[0]["CURRENT_QTY"].ToString()) * pcbOnPanel
                : int.Parse(DT.Rows[0]["CURRENT_QTY"].ToString()) * pcbOnPanel;
        }
        public int GetProductioExported(string work)
        {
            DataTable DT = mySQL.GetDataMySQL($"SELECT SUM(PCBS) FROM TRACKING_SYSTEM.HISTORY_BOX_EXPORT where WORK_ID = '{work}';");
            if (istableNull(DT)) return 0;
            return string.IsNullOrEmpty(DT.Rows[0][0].ToString())
                ? 0
                : int.Parse(DT.Rows[0][0].ToString());
        }
        public List<PlanDetail> GetPlanLine(string line, string route, DateTime date)
        {
            List<PlanDetail> planDetails = new List<PlanDetail>();
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.WORK_PLANNING WHERE LINE = '{line}' AND ROUTE = '{route}' AND MFG ='{date.ToString("yyyy-MM-dd")}';");
            if (istableNull(dt)) return planDetails;

            planDetails = (from r in dt.AsEnumerable()
                           select new PlanDetail()
                           {
                               WorkID = r.Field<string>("WORK_ID"),
                               Side = r.Field<string>("SIDE"),
                               Qty = r.Field<int>("QTY"),
                               Line = r.Field<string>("LINE"),
                               StartTime = r.Field<DateTime>("START_TIME"),
                               EndTime = r.Field<DateTime>("END_TIME"),
                               Route = r.Field<string>("ROUTE")
                           }).ToList();
            return planDetails;
        }
        public int GetPlanTimesWork(string work)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT max(times) FROM TRACKING_SYSTEM.WORK_PLANNING where WORK_ID='{work}';");
            if (istableNull(dt)) return 0;
            return string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : int.Parse(dt.Rows[0][0].ToString());
        }
        public int GetPlanned(string wo, string side)
        {
            string SQL = $"SELECT SUM(QTY_REAL) FROM TRACKING_SYSTEM.WORK_PLANNING where WORK_ID='{wo}' AND SIDE = '{side}' AND `STATUS` = 1 ;";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            if (istableNull(dt)) return 0;
            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString())) return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public DataTable GetClusterModel(string model)
        {
            string sql = $"SELECT B.`NAME`,  A.STATION_NAME FROM TRACKING_SYSTEM.DEFINE_STATION_ID A INNER JOIN " +
                $" MASTER.DEFINE_CLUSTER B  ON  A.CLUSTER_ID = B.ID WHERE A.MODEL_ID='{model}' AND B.ID <> 100;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public List<CycleTime> GetCycleTimePanacim(string model, bool IsSMT)
        {
            List<CycleTime> cycleTimes = new List<CycleTime>();
            string sql = $"SELECT TOP (1000) [MIX_NAME],[TOP_BOTTOM],[CYCLE_TIME] FROM [PANACIM].[dbo].[product_setup] where PCB_NAME='{model}' AND CYCLE_TIME > 0 AND SETUP_VALID_FLAG = 'T';";
            DataTable dt = sqlSever.GetDataSQL(sql);
            if (istableNull(dt)) return cycleTimes;
            foreach (DataRow item in dt.Rows)
            {
                CycleTime cycleTime = new CycleTime(item, IsSMT);
                cycleTimes.Add(cycleTime);
            }
            if (!IsListEmty(cycleTimes))
            {
                cycleTimes = (from r in cycleTimes
                              group r by new { Line = r.LineID, Side = r.Side } into gr
                              select new CycleTime()
                              {
                                  LineID = gr.Key.Line,
                                  Side = gr.Key.Side,
                                  Times = gr.Max(x => x.Times)
                              }).ToList();

            }
            return cycleTimes;
        }
        public List<CycleTime> GetCycleTime(string model, bool IsSMT, string stationName = "")
        {
            List<CycleTime> cycleTimes = new List<CycleTime>();
            string sql = IsSMT
                ? $"SELECT LINE, SIDE, CYCLE_TIME FROM TRACKING_SYSTEM.PRODUCT_CYCLE_TIME WHERE MODEL_ID= '{model}' ;"
                : $"SELECT LINE, SIDE, CYCLE_TIME FROM TRACKING_SYSTEM.PRODUCT_CYCLE_TIME WHERE MODEL_ID= '{model}' ;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            foreach (DataRow item in dt.Rows)
            {
                CycleTime cycleTime = new CycleTime(item, IsSMT);
                cycleTimes.Add(cycleTime);
            }
            return cycleTimes;
        }
        List<DetailTime> DivideShift(List<DetailTime> asa, int qty, int timeProduct, DateTime TimeStart)
        {

            int totaTime = qty * timeProduct;
            //List<DetailTime> asa = new List<DetailTime>();
            int timeShift = 43200;
            int timeRealShift = 39600;
            double _qtyShift = timeRealShift / timeProduct;
            int qtyShift = (int)Math.Ceiling(_qtyShift);
            DateTime timeEndShift = new DateTime();
            if (TimeStart.Hour >= 6 && TimeStart.Hour < 18)
                timeEndShift = new DateTime(TimeStart.Year, TimeStart.Month, TimeStart.Day, 18, 0, 0);
            else
                timeEndShift = new DateTime(TimeStart.Year, TimeStart.Month, TimeStart.Day + 1, 6, 0, 0);

            TimeSpan timeSpan = timeEndShift.Subtract(TimeStart);
            double timePlan = timeSpan.TotalSeconds;

            int firstTime = (int)Math.Ceiling(timePlan);
            int firstTimeReal = firstTime;
            int timeremain = totaTime;
            if (firstTime < timeShift)
            {
                if ((TimeStart.Hour >= 6 && TimeStart.Hour < 12) || (TimeStart.Hour > 18))
                    firstTimeReal = firstTimeReal - 3600;
                int firstQty = (int)Math.Ceiling((double)firstTimeReal / timeProduct);
                totaTime = totaTime - firstTime;
                asa.Add(new DetailTime() { Times = firstTime, Product = firstQty });
            }
            if (totaTime >= timeShift)
            {
                int a = totaTime / timeRealShift;
                int b = totaTime % timeRealShift;
                for (int i = 0; i < a; i++)
                {
                    asa.Add(new DetailTime() { Times = timeShift, Product = qtyShift });
                }
                int rm = asa.Sum(x => x.Product);
                int sumTime = asa.Sum(x => x.Times);
                TimeSpan tsnew = TimeSpan.FromSeconds(sumTime);
                DateTime timeStartNnew = TimeStart.AddDays(tsnew.Days).AddHours(tsnew.Hours).AddMinutes(tsnew.Minutes);
                DivideShift(asa, qty - rm, timeProduct, timeStartNnew);
            }
            else
            {
                asa.Add(new DetailTime() { Times = totaTime, Product = qty });
            }
            return asa;
        }

        public List<TimeFrame> GetTimeFrames(List<TimeFrame> asa, int qty, double timeProduct, DateTime TimeStart, ref DateTime TimeEnd)
        {
            int totaTime = (int)Math.Ceiling(qty * timeProduct);
            //List<DetailTime> asa = new List<DetailTime>();
            int timeShift = 43200;
            //int timeRealShift = 39600;
            double _qtyShift = timeShift / timeProduct;
            int qtyShift = (int)Math.Ceiling(_qtyShift);
            DateTime timeEndShift = new DateTime();
            if (TimeStart.Hour >= 6 && TimeStart.Hour < 18)
                timeEndShift = new DateTime(TimeStart.Year, TimeStart.Month, TimeStart.Day, 18, 0, 0);
            else
                timeEndShift = new DateTime(TimeStart.AddDays(1).Year, TimeStart.AddDays(1).Month, TimeStart.AddDays(1).Day, 6, 0, 0);
            TimeSpan timeSpan = timeEndShift.Subtract(TimeStart);
            double timePlan = timeSpan.TotalSeconds;

            int firstTime = (int)Math.Ceiling(timePlan);
            int firstTimeReal = firstTime;
            int timeremain = totaTime;
            int extraTime = 0;
            int index = 0;
            if (totaTime < firstTime)
            {
                index = asa.Count() + 1;
                TimeSpan ts = TimeSpan.FromSeconds(totaTime);

                timeEndShift = TimeStart.AddDays(ts.Days).AddHours(ts.Hours).AddMinutes(ts.Minutes);
                asa.Add(new TimeFrame() { ID = index, StartTime = TimeStart, EndTime = timeEndShift, Qty = qty });
            }
            else
            {
                if (firstTime < timeShift)
                {
                    if ((TimeStart.Hour >= 6 && TimeStart.Hour < 12) || (TimeStart.Hour > 18))
                    {
                        firstTimeReal = firstTimeReal - 3600;
                    }
                    int firstQty = (int)Math.Ceiling((double)firstTimeReal / timeProduct);
                    totaTime = totaTime - firstTime;
                    index = asa.Count() + 1;
                    asa.Add(new TimeFrame() { ID = index, StartTime = TimeStart, EndTime = timeEndShift, Qty = firstQty });
                    TimeStart = timeEndShift;
                }
                if (totaTime >= timeShift)
                {
                    int a = totaTime / timeShift;
                    int b = totaTime % timeShift;

                    for (int i = 0; i < a; i++)
                    {
                        index = asa.Count() + 1;
                        TimeSpan ts = TimeSpan.FromSeconds(timeShift);
                        timeEndShift = TimeStart.AddDays(ts.Days).AddHours(ts.Hours).AddMinutes(ts.Minutes);
                        asa.Add(new TimeFrame() { ID = index, StartTime = TimeStart, EndTime = timeEndShift, Qty = qtyShift });
                        TimeStart = timeEndShift;
                    }
                    int rm = asa.Sum(x => x.Qty);
                    if (qty - rm > 0)
                    {
                        GetTimeFrames(asa, qty - rm, timeProduct, TimeStart, ref timeEndShift);

                    }
                    else
                    {
                        return asa;
                    }
                }
                else
                {
                    qty = qty - asa.Sum(x => x.Qty);
                    index = asa.Count() + 1;
                    TimeSpan ts = TimeSpan.FromSeconds(totaTime);
                    timeEndShift = TimeStart.AddDays(ts.Days).AddHours(ts.Hours).AddMinutes(ts.Minutes);
                    asa.Add(new TimeFrame() { ID = index, StartTime = TimeStart, EndTime = timeEndShift, Qty = qty });
                }
            }
            TimeEnd = timeEndShift;

            return asa;
        }
        public List<PlanDetail> CreatPlanET(List<CycleTime> cycleTimesSelect, int qty, DateTime timeAction, string firstSide,
            string work, string modelID, string modelName, string bomversion, int pcbOnPanel)
        {
            List<PlanDetail> planDetails = new List<PlanDetail>();
            DateTime timeNow = getTimeServer();
            List<PlanDetail> PlanGlobles = new List<PlanDetail>();
            int timeShift = 43200;
            var qr = (from r in cycleTimesSelect
                      let time = r.Times < 20 ? 20 + 2 : r.Times + 2
                      select new
                      {
                          Line = r.LineID,
                          Side = r.Side,
                          Time = time,
                          TimeProduct = (100 * time) / (pcbOnPanel * 85)
                      }).ToList();
            foreach (var item in qr)
            {

                int totaTime = qty * (int)Math.Ceiling(item.TimeProduct);
                TimeSpan ts = TimeSpan.FromSeconds(totaTime);
                string _line = item.Line;
                string _side = item.Side;
                DateTime timeStart = timeAction;
                DateTime timeStartDetail = timeAction;

                string index = $"{timeNow.ToString("yyyyMMddHHmmss")}{_side}{_line}";
                if (qr.Any(x => x.Line == _line && x.Side != _side))
                {
                    if (_side != firstSide)
                    {
                        double _TimesSideDif = qr.Find(x => x.Line == _line && x.Side != _side).TimeProduct;
                        TimeSpan _ts = TimeSpan.FromSeconds(qty * _TimesSideDif);
                        timeStart = timeStart.AddDays(_ts.Days).AddHours(_ts.Hours).AddMinutes(_ts.Minutes);
                        timeStartDetail = timeStart;
                    }
                }
                DateTime TimeEnd = new DateTime();
                List<TimeFrame> timeFrames = new List<TimeFrame>();
                PlanDetail planGloble = new PlanDetail()
                {
                    WorkID = work,
                    Model = modelID,
                    CusModel = modelName,
                    BomVer = bomversion,
                    Line = item.Line,
                    Side = item.Side,
                    Qty = qty,
                    Description = string.Empty,
                    IndexPlan = index,
                    CycleTime = item.Time,
                    ProductionTime = item.TimeProduct,
                    StartTime = timeStart,
                    TimeFrames = GetTimeFrames(timeFrames, qty, (int)Math.Ceiling(item.TimeProduct), timeStart, ref TimeEnd),
                    EndTime = TimeEnd,
                };
                PlanGlobles.Add(planGloble);
            }
            return PlanGlobles;
        }

        public List<TimeFrame> GetTimeFrames(string index)
        {
            List<TimeFrame> timeFrames = new List<TimeFrame>();
            string sql = $"SELECT * FROM TRACKING_SYSTEM.WORK_PLANNING WHERE PLAN_INDEX = '{index}' AND `STATUS` = 1 ORDER BY ID DESC;";
            DataTable DT = mySQL.GetDataMySQL(sql);
            foreach (DataRow item in DT.Rows)
            {
                TimeFrame timeFrame = new TimeFrame(item);
                timeFrames.Add(timeFrame);
            }
            return timeFrames;
        }

        public int GetRealQtyTime(DateTime timestart, DateTime TimeEnd, string wo, string line, string station)
        {
            string sql = $"SELECT SUM(PASS) + SUM(FAIL)  `SUM_QTY` FROM TRACKING_SYSTEM.LINE_PERFORMANCE WHERE" +
                $" `WORK` = '{wo}' AND LINE = '{line}' AND START_TIME >= '{timestart.ToString("yyyy-MM-dd HH:mm:ss")}' AND START_TIME < '{TimeEnd.ToString("yyyy-MM-dd HH:mm:ss")}' AND STATION='{station}' GROUP BY `WORK`;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return 0;
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable GetPlanGloble(string index)
        {
            DataTable dt = mySQL.GetDataMySQL($@"SELECT A.WORK_ID, B.MODEL, C.MODEL_NAME,B.BOM_VERSION, A.QTY, A.SIDE, A.START_TIME, A.END_TIME, A.LINE, A.`PROCESS`, A.TIMES, A.DESCRIPTION, A.`PLAN_INDEX`, A.`CYCLE_TIME`, A.`PRODUCTION_TIME`, C.`PCBS` FROM TRACKING_SYSTEM.WORK_PLANNING_OVERVIEW A 
                        INNER JOIN TRACKING_SYSTEM.WORK_ORDER  B ON A.WORK_ID collate utf8_unicode_ci = B.WORK_ID
                        INNER JOIN TRACKING_SYSTEM.PART_MODEL_CONTROL C ON B.MODEL collate utf8_unicode_ci = C.ID_MODEL where A.PLAN_INDEX = '{index}';");
            return dt;
        }

        public List<PlanStopConfig> GetConfigPlanStop(string time = "")
        {
            string clause = string.Empty;
            if (string.IsNullOrEmpty(time)) clause = string.Empty;
            else clause = $" WHERE  `TIME` = '{time}'  ";
            string SQL = $"SELECT * FROM TRACKING_SYSTEM.DEFINE_PLAN_STOP {clause};";
            DataTable dt = mySQL.GetDataMySQL(SQL);
            if (istableNull(dt)) return new List<PlanStopConfig>();
            List<PlanStopConfig> planStopConfigs = new List<PlanStopConfig>();
            foreach (DataRow item in dt.Rows)
            {
                PlanStopConfig planStopConfig = new PlanStopConfig(item);
                planStopConfigs.Add(planStopConfig);
            }
            return planStopConfigs;
        }


        public List<PlanDetail> CreatPlanPTHByQuantity(List<CycleTime> cycleTimesSelect, string work, string modelID, string modelName, string bomversion, int qty, DateTime dtStart)
        {

            List<PlanDetail> PlanGlobles = new List<PlanDetail>();
            DateTime timeNow = getTimeServer();
            DateTime timeEnd = new DateTime();

            foreach (var item in cycleTimesSelect)
            {
                string _line = item.LineID;
                string _side = item.Side;
                string index = $"{timeNow.ToString("yyyyMMddHHmmss")}{_side}{_line}";

                double cycleTime = item.Times;

                double timePlan = cycleTime * qty * 100 / 85;
                DateTime timestart = dtStart;

                DateTime _timeend = dtStart.AddSeconds((int)Math.Ceiling(timePlan));

                double timeProductPCB = timePlan / qty;


                List<TimeFrame> timeFrames = new List<TimeFrame>();

                PlanDetail planDetail = new PlanDetail()
                {
                    WorkID = work,
                    Model = modelID,
                    CusModel = modelName,
                    BomVer = bomversion,
                    Line = _line,
                    Side = _side,
                    Qty = qty, //  realTimeL1S1, // lưu SỐ LƯỢNG
                    Description = string.Empty,
                    IndexPlan = index,
                    CycleTime = cycleTime,
                    ProductionTime = timeProductPCB,
                    StartTime = timestart,
                    TimeFrames = GetTimeFrames(timeFrames, qty, timeProductPCB, timestart, ref timeEnd),
                    EndTime = _timeend,

                };
                PlanGlobles.Add(planDetail);

            }
            return PlanGlobles;
        }


        public List<ProductionPlanVM> GetAllProductionPlan(string MFGDate)
        {
            string sql = $"SELECT * FROM MASTER.`View.ProductionPlanDetail` where MFGDate = '{MFGDate}' And State <> -1;";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return new List<ProductionPlanVM>();
            List<ProductionPlanVM> ls = (from r in dt.AsEnumerable()
                                         select new ProductionPlanVM()
                                         {
                                             Id = r.Field<int>("Id"),
                                             WorkId = r.Field<string>("WorkId"),
                                             ModelId = r.Field<string>("ModelId"),
                                             StartTime = r.Field<DateTime>("StartTime"),
                                             EndTime = r.Field<DateTime>("EndTime"),
                                             MFGDate = r.Field<DateTime>("MFGDate"),
                                             Count = r.Field<int>("Count"),
                                             ClusterDetailId = r.Field<int>("ClusterDetailId"),
                                             PlanIndex = r.Field<string>("PlanIndex"),
                                             CycleTime = r.Field<double>("CycleTime"),
                                             Performance = r.Field<double>("Performance"),
                                             ClusterId = r.Field<int>("ClusterId"),
                                             ClusterName = r.Field<string>("ClusterName"),
                                             LineId = r.Field<int>("LineId"),
                                             LineName = r.Field<string>("LineName"),
                                             StageId = r.Field<int>("StageId"),
                                             StageName = r.Field<string>("StageName"),
                                             StationId = r.Field<string>("StationId"),
                                             StationName = r.Field<string>("StationName"),
                                             ProcessId = r.Field<int>("ProcessId"),
                                             ProcessName = r.Field<string>("ProcessName"),
                                             State = r.Field<int>("State")
                                         }).ToList();
            return ls;
        }
        public ProductionPlanVM GetAllProductionPlanById(int Id)
        {
            string sql = $"SELECT A.Id, A.WorkId, A.ModelId, A.StartTime, A.EndTime, A.MFGDate, A.ClusterDetailId,  A.PlanIndex, A.Count, B.CycleTime, B.Performance, B.LineId, C.Name `LineName`, D.ClusterId, E.Name `ClusterName` FROM MASTER.ProductionPlan A " +
                $" INNER JOIN  MASTER.ClusterDetail B ON A.ClusterDetailId = B.Id " +
                $" INNER JOIN  MASTER.LineDefine C on B.LineId = C.Id " +
                $" INNER JOIN MASTER.ClusterInfor D ON B.ClusterInforId = D.Id " +
                $" INNER JOIN  MASTER.Cluster E ON D.ClusterId = E.Id  where A.Id = '{Id}' ;";
            DataTable dt = mySql.GetDataMySQL(sql);
            if (istableNull(dt)) return new ProductionPlanVM();
            ProductionPlanVM ProductionPlanVM = (from r in dt.AsEnumerable()
                                                select new ProductionPlanVM()
                                                {
                                                    Id = r.Field<int>("Id"),
                                                    WorkId = r.Field<string>("WorkId"),
                                                    ModelId = r.Field<string>("ModelId"),
                                                    StartTime = r.Field<DateTime>("StartTime"),
                                                    EndTime = r.Field<DateTime>("EndTime"),
                                                    MFGDate = r.Field<DateTime>("MFGDate"),
                                                    ClusterDetailId = r.Field<int>("ClusterDetailId"),
                                                    PlanIndex = r.Field<string>("PlanIndex"),
                                                    Count = r.Field<int>("Count"),
                                                    ClusterId = r.Field<int>("ClusterId"),
                                                    ClusterName = r.Field<string>("ClusterName"),
                                                    LineId = r.Field<int>("LineId"),
                                                    LineName = r.Field<string>("LineName")
                                                }).FirstOrDefault();
            return ProductionPlanVM;
        }
    }
}
