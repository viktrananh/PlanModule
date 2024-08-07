using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    internal class ModelConfig
    {
        public string ModelID { get; set; }
        public string CusModel { get; set; }
        public string CusID { get; set; }
        public string Process { get; set; }
        public string ModelParent { get; set; }
        public string ProductLine { get; set; }
        public ModelConfig() { }

        public ModelConfig(DataTable dt)
        {
            ModelID = dt.Rows[0]["ID_MODEL"].ToString();
            CusModel = dt.Rows[0]["MODEL_NAME"].ToString();
            CusID = dt.Rows[0]["CUSTOMER_ID"].ToString();
            Process = dt.Rows[0]["PROCESS"].ToString();
            ModelParent = dt.Rows[0]["MODEL_PARENT"].ToString();
            ProductLine = dt.Rows[0]["MODEL_ID"].ToString();
        }

    }
}
