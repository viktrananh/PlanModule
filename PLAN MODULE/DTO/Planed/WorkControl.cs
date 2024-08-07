using PLAN_MODULE.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed
{
    internal class WorkControl
    {
        private static WorkControl instance;
        public static WorkControl Instance
        {
            get { if (instance == null) instance = new WorkControl(); return instance; }
            set { instance = value; }
        }
        public WorkControl() { }

        public static List<Work> LoadLsWork(string model, string work = "")
        {
            CreateWorkDAO createWorkDAO = new CreateWorkDAO();
            List<Work> works = new List<Work>();
            DataTable dt = createWorkDAO.GetLsWork(model, work);
            foreach (DataRow item in dt.Rows)
            {
                Work wo = new Work(item);
                works.Add(wo);
            }
            return works;
        }
    }
}
