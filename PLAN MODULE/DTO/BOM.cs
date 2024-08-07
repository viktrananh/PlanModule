using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE
{
    public class BOM
    {
        public string PartNo  { get; set; }
        public string MFGPart { get; set; }
        public string Location { get; set; }
        public string Qty { get; set; }
    }

    public class BomDetail
    {
        public string MainPart { get; set; }
        public string InterPart { get; set; }
        public string CSPart { get; set; }
        public string MfgPart { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Descripton { get; set; }
        public string MfgName { get; set; }
        public string Size { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Tolerace { get; set; }
        public string Marking { get; set; }
        public string MSL { get; set; }
        public string Rank { get; set; }
        public string Process { get; set; }
        public string Quantity { get; set; }
        public string Side_1 { get; set; }
        public string Siede_2 { get; set; }
        public BomDetail() { }

        public BomDetail(dynamic item)
        {
            this.MainPart = item[1].Value.ToString().Trim();
            this.InterPart = item[2].Value.ToString().Trim();
            this.CSPart = item[3].Value.ToString().Trim();
            this.MfgPart = item[4].Value.ToString().Trim();
            this.Location = item[5].Value.ToString().Trim();
            this.Type = item[6].Value.ToString().Trim();
            this.Descripton = item[7].ToString().Trim();
            this.MfgName = item[8].ToString().Trim();
            this.Size = float.Parse(item[9].ToString().Trim());
            this.Value = item[10].ToString().Trim();
            this.Unit = item[11].ToString().Trim();
            this.Tolerace = float.Parse(item[12].ToString().Trim());
            this.Marking = item[13].ToString().Trim();
            this.MSL = item[14].ToString().Trim();
            this.Rank = item[15].ToString().Trim();
            this.Process = item[16].ToString().Trim();
            this.Quantity = int.Parse(item[17].ToString().Trim());
            this.Side_1 = item[18].ToString().Trim();
            this.Siede_2 = item[19].ToString().Trim();
        }
    }
    public class BomHistory
    {
        public string BomVersion { get; set; }
        public string DateEx { get; set; }
        public string Location { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Content { get; set; }
        public string Creater { get; set; }
        public string Comfirmation { get; set; }

        public BomHistory() { }

    }
}
