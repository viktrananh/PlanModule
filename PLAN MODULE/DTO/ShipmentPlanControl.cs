using DevComponents.DotNetBar.SuperGrid;
using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.PlanExport
{
    internal class ShipmentPlanControl : ShipmentPlanDAO
    {
        private static ShipmentPlanControl instance;

        public static ShipmentPlanControl Instance
        {
            get { if (instance == null) instance = new ShipmentPlanControl(); return instance; }
            private set { instance = value; }
        }

        private ShipmentPlanControl() { }

        public List<ShipmentView> LoadListWorkExport(DateTime date)
        {
            List<ShipmentPlan> workExports = new List<ShipmentPlan>();
            List<ShipmentView> a = new List<ShipmentView>();
            DataTable dt = GetAllShipmentPlan(date);
            foreach (DataRow item in dt.Rows)
            {
                ShipmentView shipmentView = new ShipmentView(item);
                a.Add(shipmentView);
            }



            //workExports = (from r in a
            //               group r by new { r.Work, r.Model, r.CusID, r.CusCode, r.CusModel } into gr
            //               select new ShipmentPlan()
            //               {
            //                   WorkID = gr.Key.Work,
            //                   CusID = gr.Key.CusID,
            //                   ModelID = gr.Key.Model,
            //                   CusModel = gr.Key.CusModel,
            //                   CusCode = gr.Key.CusCode,
            //                   Request = gr.Sum(x => x.Request),
            //                   Real = gr.Sum(x => x.Real),
            //                   BoxC = gr.Sum(x => x.BoxC),
            //                   BoxP = gr.Sum(x => x.BoxP),
            //                   TimelinesDate = (from b in gr
            //                                    group b by b.DateReal into subGr
            //                                    let request = subGr.Sum(b => b.Request)
            //                                    let real  = subGr.Sum(b => b.Real)
            //                                    select new Timeline()
            //                                    {
            //                                        TimeExport = subGr.Key.ToString("yyyy-MM-dd"),
            //                                        Request = request,
            //                                        Real =real,
            //                                        Diffrent = request - real,
            //                                        BoxC = subGr.Sum(x=>x.BoxC),
            //                                        BoxP = subGr.Sum(x=>x.BoxP)
            //                                    }).OrderBy(x => x.TimeExport).ToList()
            //               }).ToList();
            return a;
        }

    }
}
