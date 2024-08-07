using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    public class WorkOverview
    {
        LineAppointmentDAO lineAppointmentDAO = new LineAppointmentDAO();

        public string  Work { get; set; }
        public string Side { get; set; }
        public int Request { get; set; }
        public int Production { get; set; }
        public int Planned { get; set; }
        public int Exported { get; set; }
        public int Remain { get; set; }
        
        public WorkOverview() { }

        public WorkOverview(string work, string side, int request, string stationName, int pcbOnPanel )
        {
            int producttion = lineAppointmentDAO.GetProductionStation(work, stationName, pcbOnPanel);
            int exported = lineAppointmentDAO.GetProductioExported(work);
            int planned = lineAppointmentDAO.GetPlanned(work, side);
            this.Work = work;
            this.Side = side;
            this.Request = request;
            this.Planned = planned;
            this.Production = producttion;
            this.Exported = exported;
            this.Remain = producttion - exported;
        }
    }
}
