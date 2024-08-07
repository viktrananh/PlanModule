using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.PLAN
{
    public class CreateBillToolsDAO : BaseDAO
    {
        public bool IsPartNumberTools(string part, out string mfgPart, out string description)
        {
            mfgPart = description=  string.Empty;
            string sql = $" SELECT * FROM STORE_MATERIAL_DB.MASTER_PART WHERE  PART_NUMBER='{part}' AND MASTER_TYPE = 0;";
            DataTable dt = mySQL.GetDataMySQL(sql);
            if (istableNull(dt)) return false;
            mfgPart = dt.Rows[0]["MFG_PART"].ToString();
            description = dt.Rows[0]["DESCRIPTION"].ToString();
            return true;
        }
    }
}
