using DevExpress.UnitConversion;
using PLAN_MODULE.DTO.Sales;
using Remotion.Mixins.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.BUS.Sales
{
    internal class DefineModelBUS : BaseBUS
    {
        public bool CreateModel(string model, string cusmodel, string cusID, string gerber, string verBom, string opcontact, string phone, string potential, string possibility, string piority)
        {
            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`DEFINE_MODEL` (`MODEL_ID`, `CUS_MODEL`, `CUS_ID`, `VER_GERBER`, `VER_BOM`, `OP_CONTACT`, `PHONE`, `POTENTIAL`, `POSSIBILITY`, `PIORITY`)" +
                $" VALUES ('{model}', '{cusmodel}', '{cusID}', '{gerber}', '{verBom}', '{opcontact}', '{phone}', '{potential}', '{possibility}', '{piority}')" +
                $" ON DUPLICATE KEY UPDATE `CUS_MODEL` = '{cusmodel}', `CUS_ID` =  '{cusID}' , `VER_GERBER` = '{gerber}', `VER_BOM` = '{verBom}', " +
                $"`OP_CONTACT` = '{opcontact}', `PHONE` = '{phone}', `POTENTIAL` = '{potential}', `POSSIBILITY` = '{possibility}', `PIORITY`  =  '{piority}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false; 
        }


        public bool SaveModelChild(string model, string cusmodel, string process, string cusID, string modelID, string modelPart, string gerber, string bom)
        {
            string sql = $"INSERT INTO `TRACKING_SYSTEM`.`PART_MODEL_CONTROL` (`ID_MODEL`, `MODEL_NAME`, `PROCESS`, `CUSTOMER_ID`, `MODEL_ID`, `MODEL_PARENT`, `VER_GERBER`, `VER_BOM`) VALUES " +
                $"('{model}', '{cusmodel}', '{process}', '{cusID}', '{modelID}', '{modelPart}', '{gerber}', '{bom}')" +
                $" ON DUPLICATE KEY UPDATE  `MODEL_NAME` = '{cusmodel}', `PROCESS` = '{process}' , `CUSTOMER_ID` = '{cusID}', `VER_GERBER` = '{gerber}', `VER_BOM` = '{bom}';";
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        public bool LockModel(List< ModelChild> modelChild , string modelID)
        {
            string sql = string.Empty;
            LoadCMDLockModel(modelChild , modelID, ref sql);
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

        void LoadCMDLockModel(List<ModelChild> modelChild, string modelID , ref string sql)
        {
            foreach (var item in modelChild)
            {
                string Model = item.ModelID;
                if (Model == modelID)
                {
                    sql += $"update TRACKING_SYSTEM.PART_MODEL_CONTROL SET EOL= 1 where ID_MODEL='{modelID}';";

                    foreach (var item1 in item.ModelChilds)
                    {
                        string model1 = item1.ModelID;
                        if (IsListEmty(item1.ModelChilds))
                        {
                            sql += $"update TRACKING_SYSTEM.PART_MODEL_CONTROL SET EOL= 1 where ID_MODEL='{model1}';";

                        }
                        else
                        {
                            LoadCMDLockModel(item1.ModelChilds, modelID, ref sql);

                        }
                    }
                    break;
                }
                else
                {
                    LoadCMDLockModel(item.ModelChilds, modelID, ref sql);
                }

            }
        }
        public bool UnLockModel(List<ModelChild> modelChild, string modelID)
        {
            string sql = string.Empty;

            LoadCMDUnlockModel(modelChild, modelID, ref sql);
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }
        void LoadCMDUnlockModel(List<ModelChild> modelChild, string modelID , ref string sql)
        {

            foreach (var item in modelChild)
            {
                string Model = item.ModelID;
                if (Model == modelID)
                {
                    sql += $"update TRACKING_SYSTEM.PART_MODEL_CONTROL SET EOL= 0 where ID_MODEL='{modelID}';";

                    foreach (var item1 in item.ModelChilds)
                    {
                        string model1 = item1.ModelID;
                        if (IsListEmty(item1.ModelChilds))
                        {
                            sql += $"update TRACKING_SYSTEM.PART_MODEL_CONTROL SET EOL= 0 where ID_MODEL='{model1}';";

                        }
                        else
                        {
                            LoadCMDUnlockModel(item1.ModelChilds, model1, ref sql);

                        }
                    }
                    break;
                }
                else
                {
                    LoadCMDUnlockModel(item.ModelChilds, modelID, ref sql);
                }

            }
        }

        public bool DeleteModel(List<ModelChild> modelChild, string modelID)
        {
            string sql = string.Empty;
            LoadCMDDeleteModel(modelChild, modelID, ref sql);
            if (mySQL.InsertDataMySQL(sql)) return true;
            return false;
        }

        void LoadCMDDeleteModel(List<ModelChild> modelChild, string modelID, ref string sql)
        {


            foreach (var item in modelChild)
            {
                string Model = item.ModelID;
                if (Model == modelID)
                {
                    sql += $"DELETE FROM  TRACKING_SYSTEM.PART_MODEL_CONTROL where ID_MODEL='{modelID}';";

                    foreach (var item1 in item.ModelChilds)
                    {
                        string model1 = item1.ModelID;
                        if (IsListEmty(item.ModelChilds))
                        {
                            sql += $"DELETE FROM  TRACKING_SYSTEM.PART_MODEL_CONTROL where ID_MODEL='{model1}';";

                        }
                        else
                        {
                            LoadCMDDeleteModel(item1.ModelChilds, model1, ref sql);
                        }
                    }
                    break;
                }
                else
                {
                    LoadCMDDeleteModel(item.ModelChilds, modelID, ref sql);
                }

            }

        }
    }
}
