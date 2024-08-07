using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    internal class ModelConfigControl
    {
        private static ModelConfigControl instance;
        public static ModelConfigControl Instance
        {
            get { if (instance == null) instance = new ModelConfigControl(); return ModelConfigControl.instance; }
            private set { ModelConfigControl.instance = value; }

        }

        public static ModelConfig LoadModelConfig(string model)
        {
            CreateWorkDAO createWorkDAO = new CreateWorkDAO();
            DataTable dt = createWorkDAO.GetModelConfig(model);
            ModelConfig modelConfig = new ModelConfig(dt);
            return modelConfig;

        }
    }
}
