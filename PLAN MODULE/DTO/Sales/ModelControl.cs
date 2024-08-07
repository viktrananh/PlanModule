using PLAN_MODULE.DAO.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    internal class ModelControl
    {
        private static ModelControl instance;
        public static ModelControl Instance
        {
            get { if (instance == null) instance = new ModelControl(); return instance; }
            set { instance = value; }
        }

        public static List<Model> LoadLsModel(string cusID)
        {
            DefineModelDAO defineModelDAO = new DefineModelDAO();
            DataTable dt = defineModelDAO.GetLsModel(cusID);
            List<Model> models = new List<Model>();
            foreach (DataRow item in dt.Rows)
            {
                Model model = new Model(item);
                models.Add(model);
            }
            return models;
        }

        public static Model LoadDetailModel(string cusID, string modelID)
        {
            DefineModelDAO defineModelDAO = new DefineModelDAO();
            DataTable dt = defineModelDAO.GetLsModel(cusID, modelID);
            Model model = new Model();

            foreach (DataRow item in dt.Rows)
            {
                model = new Model(item);
                break;
            }
            return model;
        }
    }
}
