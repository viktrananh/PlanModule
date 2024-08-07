using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLAN_MODULE.DTO.Planed
{
    public class WorkOverviewControl
    {

        private static WorkOverviewControl instance;
        public static WorkOverviewControl Instance
        {
            get { if (instance == null) instance = new WorkOverviewControl(); return WorkOverviewControl.instance; }
            private set { WorkOverviewControl.instance = value; }

        }
       
        public List<WorkOverview> GetDetailBIll( List<StationModel> stationModels, string work, int totalpcbs, int pcbOnPanel)
        {
            List<WorkOverview> workOverviews = new List<WorkOverview>();
            var side = stationModels.Where(x => x.IsQuantity == 1).Select(x => x).ToList();
            foreach (var item in side)
            {

                WorkOverview workOverview = new WorkOverview( work, item.Side, totalpcbs, item.StationName, pcbOnPanel);
                workOverviews.Add(workOverview);
            }
            return workOverviews;
        }
    }
}
