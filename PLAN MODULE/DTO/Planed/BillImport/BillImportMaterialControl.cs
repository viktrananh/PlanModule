using PLAN_MODULE.DAO.PLAN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.BillImport
{
    internal class BillImportMaterialControl
    {
        private static BillImportMaterialControl instance;
        public static BillImportMaterialControl Instance
        {
            get { if (instance == null) instance = new BillImportMaterialControl(); return BillImportMaterialControl.instance; }
            private set { BillImportMaterialControl.instance = value; }

        }

        public static List<BillImportMaterial> LoadListBillImportMaterial()
        {
            List<BillImportMaterial> billImportMaterials = new List<BillImportMaterial>();

            BillImportMaterialDAO billImportMaterialDAO = new BillImportMaterialDAO();
            DataTable dt = billImportMaterialDAO.GetLsBill();
            foreach (DataRow item in dt.Rows)
            {
                BillImportMaterial billImportMaterial = new BillImportMaterial(item);
                billImportMaterials.Add(billImportMaterial);
            }
            return billImportMaterials;
        }
    }
}
