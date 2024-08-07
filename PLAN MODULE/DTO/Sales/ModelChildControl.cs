using PLAN_MODULE.DAO.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    internal class ModelChildControl
    {
        private static ModelChildControl instance;
        public static ModelChildControl Instance
        {
            get { if (instance == null) instance = new ModelChildControl(); return instance; }
            set { instance = value; }
        }


        public static List<ModelChild> LoadLsModelChild(string modelID)
        {
            List<ModelChild> modelChildren = new List<ModelChild>();
            DefineModelDAO defineModelDAO = new DefineModelDAO();
            DataTable dt = defineModelDAO.GetLsModelChild(modelID);
            if (defineModelDAO.istableNull(dt)) return new List<ModelChild>() ;
            foreach (DataRow item in dt.Rows)
            {
                ModelChild modelChild = new ModelChild(item);
                modelChildren.Add(modelChild);
            }
            return modelChildren;
        }
    }
}
