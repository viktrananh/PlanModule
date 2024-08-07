using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    public class Model
    {
        public string ModelID { get; set; }
        public string CusModel { get; set; }
        public string CusID { get; set; }
        public string Gerber { get; set; }
        public string VerBom { get; set; }
        public string OPContact { get; set; }
        public string Phone { get; set; }
        public string Potential { get; set; }
        public string Possibility { get; set; }
        public string Pioirity { get; set; }
        public string EOL { get; set; }
        public int isIATF { get; set; }
        public List<ModelChild> ModelChilds = new List<ModelChild>();
        public Model() { }
        public Model(DataRow row)
        {
            ModelID = row["MODEL_ID"].ToString();
            CusModel = row["CUS_MODEL"].ToString();
            CusID = row["CUS_ID"].ToString();
            Gerber = row["VER_GERBER"].ToString();
            VerBom = row["VER_BOM"].ToString();
            OPContact = row["OP_CONTACT"].ToString();
            Phone = row["PHONE"].ToString();
            Potential = row["POTENTIAL"].ToString();
            Possibility = row["POSSIBILITY"].ToString();
            Pioirity = row["PIORITY"].ToString();
            EOL = int.Parse(row["EOL"].ToString()) == 1 ? "CLOSE" : "OPEN";
            isIATF = int.Parse(row["IATF"].ToString()) ;
            ModelChilds = ModelChildControl.LoadLsModelChild(ModelID);
        }
    }
}
 