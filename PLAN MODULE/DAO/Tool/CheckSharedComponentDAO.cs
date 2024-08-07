using PLAN_MODULE.DTO.BOM;
using PLAN_MODULE.DTO.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DAO.Tool
{
    public class CheckSharedComponentDAO : BaseDAO
    {
        public List<CheckSharedCombonentBOM> GetLsModelBom(string cusID, string process)
        {
            DataTable dt = mySQL.GetDataMySQL($"SELECT A.MODEL_ID, A.VERSION FROM STORE_MATERIAL_DB.BOM_GENARAL A INNER JOIN" +
                $" TRACKING_SYSTEM.PART_MODEL_CONTROL B ON A.MODEL_ID COLLATE UTF8_UNICODE_CI = B.ID_MODEL WHERE B.CUSTOMER_ID = '{cusID}' AND B.`PROCESS` = '{process}' AND A.STATUS_BOM = '1';");
            if (istableNull(dt)) return new List<CheckSharedCombonentBOM>();
            List<CheckSharedCombonentBOM> ls = (from r in dt.AsEnumerable()
                                                select new CheckSharedCombonentBOM()
                                                {
                                                    ModelID = r.Field<string>("MODEL_ID"),
                                                    BomVer = r.Field<string>("VERSION"),
                                                    State = false
                                                }).ToList();
            return ls;
        }

        public List<DTO.BOM.BomDetail> GetLsBomDetail(List<CheckSharedCombonentBOM> _lsBom, string cusID)
        {
            List<DTO.BOM.BomDetail> ls = new List<DTO.BOM.BomDetail>();
            foreach (var item in _lsBom)
            {
                if (!item.State) continue;
                string model = item.ModelID;
                string bomver = item.BomVer;
                Bom bom = BomControl.LoadBomContent(model, cusID, "", bomver);
                List<DTO.BOM.BomDetail> lsSub = bom.BomDetails;
                ls = ls.Union(lsSub).ToList();
            }
            return ls;
        }
    }
}
