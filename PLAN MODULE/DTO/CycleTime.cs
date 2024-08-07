using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class CycleTime
    {
        public string LineID { get; set; }
        public string Side { get; set; }
        public double Times { get; set; }
        public bool Use { get; set; }
        public CycleTime() { }

        public CycleTime(DataRow row, bool isSMT)
        {
            LineID = isSMT ? row["MIX_NAME"].ToString().Split('-')[0].Substring(1) : row["LINE"].ToString();
            Side = isSMT ?  row["TOP_BOTTOM"].ToString() : row["SIDE"].ToString();
            Times = isSMT ?  double.Parse( row["CYCLE_TIME"].ToString()) : double.Parse(row["CYCLE_TIME"].ToString());
        }

    }

}
