using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class TimeFrame
    {
        public int ID { get; set; }
        public int Qty { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeFrame() { }

        public TimeFrame(DataRow dataRow)
        {
            ID = int.Parse(dataRow["PLAN_ID"].ToString());
            Qty = int.Parse(dataRow["QTY_REAL"].ToString());
            StartTime = DateTime.Parse(dataRow["START_TIME"].ToString());
            EndTime = DateTime.Parse(dataRow["END_TIME"].ToString());
        }
    }
}
