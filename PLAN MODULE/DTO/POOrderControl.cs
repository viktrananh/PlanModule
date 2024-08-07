using PLAN_MODULE.DAO;
using PLAN_MODULE.DTO.Planed;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class POOrderControl
    {
        private static POOrderControl instance;
        public static POOrderControl Instance
        {
            get { if (instance == null) instance = new POOrderControl(); return instance; }
            set { instance = value; }
        }
        public POOrderControl() { }

        public static List<POOrder> LoadPOOrders()
        {
            PODao PODAO = new PODao();
            List<POOrder> POs = new List<POOrder>();
            DataTable dt = PODAO.GetPOs();
            foreach (DataRow item in dt.Rows)
            {
                POOrder po = new POOrder(item);
                POs.Add(po);
            }
            return POs;
        }

        public static POOrder LoadPOOrderByName(string POName)
        {
            PODao PODAO = new PODao();
            DataTable dt = PODAO.GetPOByPOName(POName);
            if(PODAO.istableNull(dt)) return new POOrder();
            POOrder po = new POOrder(dt.Rows[0]);
            return po;
        }
    }
}
