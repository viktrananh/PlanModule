using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Laser
{
    public class LaserMakingData
    {
        public string Code { get; set; }
        public int TemType { get; set; }
        public DateTime TimeMark { get; set; }
        public string WorkId { get; set; }

        public LaserMakingData() { }
        public LaserMakingData(DataRow row)
        {
            Code = row["code"].ToString();
            TemType = int.Parse( row["TPYE"].ToString());
            TimeMark = DateTime.Parse(row["DATE"].ToString()) ;
            WorkId = row["WORK"].ToString();

        }

    }
}
