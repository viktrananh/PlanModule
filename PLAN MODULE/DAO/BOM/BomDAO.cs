using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.BOM
{
    internal class BomDAO : BaseDAO
    {
        public DataTable GetPEBomDetail(string Model, string version, string cusID)
        {
            if(cusID!="SEF")
            {
                string cmd = $"SELECT CUSTOMER,A.MODEL,MAIN_PART,B.PART_NUMBER, CS_PART,A.MFG_PART,A.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"A.DESCRIPTION,B.PITCH,B.DIRECTION,B.PACKING FROM  STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.MASTER_PART B ON A.INTER_PART collate utf8_general_ci = B.PART_NUMBER" +
                $" and A.MODEL = '{Model}' and A.VERSION = '{version}'; ";
                return mySql.GetDataMySQL(cmd);
            } else
            {
                string cmd = $"SELECT CUSTOMER,C.MODEL,MAIN_PART,C.PART_NUMBER, CS_PART,C.MFG_PART,C.MFG_NAME, TYPE,LOCATION,LEVEL,PARENT_PART,PROCESS,QUANTITY,SIDE1,SIDE2," +
                $"C.DESCRIPTION,PITCH,DIRECTION,PACKING FROM(SELECT CUSTOMER, MODEL, MAIN_PART, PART_NUMBER, A.CS_PART, B.MFG_PART, B.MFG_NAME, TYPE, LOCATION, LEVEL, PARENT_PART, PROCESS, QUANTITY, SIDE1, SIDE2," +
                $"B.DESCRIPTION FROM STORE_MATERIAL_DB.BOM_CONTENT A inner join STORE_MATERIAL_DB.AML_LIST B" +
                $" ON B.CUS_PART = A.CS_PART where A.MODEL = '{Model}' and A.VERSION = '{version}') C INNER JOIN STORE_MATERIAL_DB.MASTER_PART ON C.PART_NUMBER = MASTER_PART.PART_NUMBER; ";
                return mySql.GetDataMySQL(cmd);
            }  
        }
       
    }
}
