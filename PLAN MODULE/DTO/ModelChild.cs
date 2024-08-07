using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    public class ModelChild : Model
    {
        public string ModelParent { get; set; }
        public string Process { get; set; }
        public int PcbOnPanel { get; set; }

        public ModelChild() { }
        public ModelChild(DataRow row)
        {
            ModelID = row["ID_MODEL"].ToString();
            CusID = row["CUSTOMER_ID"].ToString();
            CusModel = row["MODEL_NAME"].ToString();
            EOL =  row["EOL"].ToString();
            ModelParent = row["MODEL_PARENT"].ToString();
            Process = row["PROCESS"].ToString();
            PcbOnPanel = int.Parse(row["PCBS"].ToString());
            ModelChilds = ModelChildControl.LoadLsModelChild(ModelID);
        }
    }
}
