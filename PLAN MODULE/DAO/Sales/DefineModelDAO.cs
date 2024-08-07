using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.Sales
{
    internal class DefineModelDAO : BaseDAO
    {
        public DataTable GetLsModel(string cusID, string modelID="")
        {
            string sql = string.IsNullOrEmpty(modelID)
                ? $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL WHERE CUS_ID='{cusID}' ORDER BY MODEL_ID;"
                : $"SELECT * FROM TRACKING_SYSTEM.DEFINE_MODEL where MODEL_ID='{modelID}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }

        public DataTable GetLsModelChild(string modelChild )
        {
            string sql =  $"SELECT * FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where MODEL_PARENT='{modelChild}';";
            DataTable dt = mySQL.GetDataMySQL(sql);
            return dt;
        }
        public bool IsModelChildExist(string model)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT * FROM TRACKING_SYSTEM.PART_MODEL_CONTROL where ID_MODEL='{model}';");
            if(istableNull(dt)) return false;
            return true;
        }
    }
}
