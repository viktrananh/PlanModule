using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO
{
    internal class CreateTempDAO
    {
        public bool istableNull(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return true;
            return false;
        }
        public int GetIso8601WeekOfYear(DateTime time)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        public DateTime getTimeServer()
        {
            return DateTime.Parse(DBConnect.getData("SELECT NOW();").Rows[0][0].ToString());
        }
        public DataTable createManuTem(int number, int typeTemp, out int year, out int week)
        {
            string charType = "M";
            if (typeTemp == 0) charType = "M";
            if (typeTemp == 1) charType = "P";
            if (typeTemp == -1) charType = "NG";
            string defaultChar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] arDefaultChar = defaultChar.ToArray();

            DataTable dtSeral = new DataTable();
            dtSeral.Columns.Add("Serial");

            string lastTempdata = typeTemp == -1 ? "00000" : "00000";

            DateTime timeNow = getTimeServer();
            year = timeNow.Year;
            week = GetIso8601WeekOfYear(timeNow);
            DataTable dt = DBConnect.getData($"SELECT * FROM TRACKING_SYSTEM.MVN_TEM_DATA WHERE WEEK_CREATE = '{week}' AND YEAR_CREATE = '{year}' AND TEM_TYPE = '{typeTemp}' ORDER BY ID DESC;");
            if (!istableNull(dt))
            {
                lastTempdata = typeTemp == -1 ? dt.Rows[0]["TEM_END"].ToString().Substring(6) : dt.Rows[0]["TEM_END"].ToString().Substring(5);
            }

            char[] arLastTempdata = lastTempdata.ToCharArray();

            int count = 1;

            int indexstart = 0;

            string lastcharacteroffisrttem = Convert.ToString(arLastTempdata[arLastTempdata.Length - 1]);

            for (int i = 0; i < arDefaultChar.Length; i++)
            {
                if (arDefaultChar[i].ToString() == lastcharacteroffisrttem)
                {
                    indexstart = i;
                    break;
                }
            }

            for (int i = 1; i <= number; i++)
            {
                CreateTem(ref arLastTempdata, ref indexstart, arDefaultChar);
                string tem = typeTemp != -1
                    ? $"{charType}{year.ToString().Substring(2)}{week.ToString("00")}{string.Join("", arLastTempdata)}"
                    : $"{charType}{year.ToString().Substring(3)}{week.ToString("00")}{string.Join("", arLastTempdata)}";
                dtSeral.Rows.Add(tem);
            }
            return dtSeral;
        }
        public void CreateTem(ref char[] tem, ref int currentindex, char[] defaultData)
        {

            if (tem[tem.Length - 1] == 'Z')
            {
                for (int i = tem.Length - 2; i >= 0; i--)
                {
                    if (tem[i] == 'Z') continue;
                    for (int j = 0; j < defaultData.Length; j++)
                    {
                        if (defaultData[j] == tem[i])
                        {
                            tem[i] = defaultData[j + 1];
                            for (int k = i + 1; k < tem.Length; k++)
                            {
                                tem[k] = '0';
                            }
                            currentindex = 0;
                            return;
                        }
                    }
                }
            }
            else
            {

                tem[tem.Length - 1] = defaultData[currentindex + 1];
                currentindex++;
            }

        }
        public DataTable GetDataTempForWeek(int year, int week, int temType)
        {
            DataTable dt = DBConnect.getData($"SELECT TEM_START, TEM_END, QUANTITY, TEM_TYPE FROM TRACKING_SYSTEM.MVN_TEM_DATA where YEAR_CREATE = '{year}' AND WEEK_CREATE = '{week}' AND TEM_TYPE = '{temType}' ;");
            return dt;
        }

        public bool CreatTemInDB(string tempStart, string tempEnd, int year, int week, int tempNums, string usse, int temType)
        {
            string sql = "INSERT INTO `TRACKING_SYSTEM`.`MVN_TEM_DATA` (`TEM_START`, `TEM_END`, `TIME_CREATE`, `YEAR_CREATE`, `WEEK_CREATE`, `QUANTITY`, `OP`, `TEM_TYPE`) " +
              $" VALUES ('{tempStart}', '{tempEnd}',  now() , {year}, '{week}', '{tempNums}', '{usse}', '{temType}');";
            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
        public bool DeleteTemp(string tempStart, string tempEnd, int year, int week)
        {
            string sql = $"DELETE FROM TRACKING_SYSTEM.MVN_TEM_DATA where YEAR_CREATE = '{year}' AND WEEK_CREATE = '{week}' and TEM_START = '{tempStart}' AND TEM_END = '{tempEnd}';";
            if (!DBConnect.InsertMySql(sql)) return false;
            return true;
        }
    }
}
