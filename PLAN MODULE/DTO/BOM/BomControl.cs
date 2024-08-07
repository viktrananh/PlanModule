using PLAN_MODULE.DAO.BOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.BOM
{
    internal class BomControl
    {

        private static BomControl instance;
        public static BomControl Instance
        {
            get { if (instance == null) instance = new BomControl(); return instance; }
            set { instance = value; }
        }
        public BomControl() { }
        public static BomContent LoadBomContent(string Model, string cusID, string work, string Version)
        {
            BomDAO bomDAO = new BomDAO();
            BomContent bomContent = new BomContent();
            bomContent.Model = Model;
            bomContent.BomVersion = Version;
            bomContent.Work = work;
            bomContent.CusID = cusID;
            //List<StandardIQC> standardIQCs = createBomDAO.GetStandardIQC(cusID);



            //-------------------------Bom PE -------------------------
            DataTable dtBomdetail = bomDAO.GetPEBomDetail(Model, Version, cusID);

            foreach (DataRow item in dtBomdetail.Rows)
            {
                BomDetail bom = new BomDetail(item);
                bomContent.BomDetails.Add(bom);
            }
           
            return bomContent;
        }

    }
}
