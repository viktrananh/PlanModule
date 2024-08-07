using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.BOM
{
    public class Bom
    {
        public string Model { get; set; }

        public string BomVersion { get; set; }

        public string CusID { get; set; }

        public string Work { get; set; }

        public List<BomDetail> BomDetails = new List<BomDetail>();

        public List<BomUsing> BomUsings = new List<BomUsing>();
    }
    public class BomDetail
    {
        public string MainPart { get; set; } // NEW
        public string ParentPart { get; set; }// NEW
        public string InterPart { get; set; }// NEW
        public string CSPart { get; set; }// NEW
        public string MfgPart { get; set; }// NEW
        public string Location { get; set; }// NEW
        public string Type { get; set; }// NEW
        public string Descripton { get; set; }// NEW
        public string MfgName { get; set; }// NEW
        public string Size { get; set; }// NEW
        public string Value { get; set; }// NEW
        public string Unit { get; set; }// NEW
        public string Tolerace { get; set; }// NEW
        public string Marking { get; set; }
        public string MSL { get; set; }
        public string Rank { get; set; }
        public string Process { get; set; }// NEW
        public string Quantity { get; set; }// NEW
        public string Side_1 { get; set; }
        public string Siede_2 { get; set; }
        public int IsUsing { get; set; }
        public double OT { get; set; }// NEW
        public double OA { get; set; }// NEW
        public BomDetail() { }

        public BomDetail(string mainPart, string interPart, string cSPart, string mfgPart, string location, string type, string descripton, string mfgName,
            string size, string value, string unit, string tolerace, string marking, string mSL, string rank, string process, string quantity, string side_1, string siede_2, int isusing, double ot, double oa)
        {
            MainPart = mainPart;
            InterPart = interPart;
            CSPart = cSPart;
            MfgPart = mfgPart;
            Location = location;
            Type = type;
            Descripton = descripton;
            MfgName = mfgName;
            Size = size;
            Value = value;
            Unit = unit;
            Tolerace = tolerace;
            Marking = marking;
            MSL = mSL;
            Rank = rank;
            Process = process;
            Quantity = quantity;
            Side_1 = side_1;
            Siede_2 = siede_2;
            IsUsing = isusing;
            OT = ot;
            OA = oa;
        }

        public BomDetail(DataRow item)
        {
            this.MainPart =  item["MAIN_PART"].ToString().Trim();
            this.ParentPart = item["PARENT_PART"].ToString().Trim();
            this.InterPart =  item["PART_NUMBER"].ToString().Trim();
            this.CSPart =  item["CS_PART"].ToString().Trim();
            this.MfgPart = item["MFG_PART"].ToString().Trim();
            this.Location = item["LOCATION"].ToString().Trim();
            this.Type =  item["TYPE"].ToString().Trim();
            this.Descripton = item["DESCRIPTION"].ToString().Trim();
            this.MfgName = item["MFG_NAME"].ToString().Trim();
            this.Process = item["PROCESS"].ToString().Trim();
            this.Quantity =  item["QUANTITY"].ToString().Trim();
            this.Side_1 = item["SIDE1"].ToString().Trim();
            this.Siede_2 = item["SIDE2"].ToString().Trim();
            }
    }
    public class BomHistory
    {
        public string SubVersion { get; set; }
        public string DateEx { get; set; }
        public string STT { get; set; }
        public string Location { get; set; }
        public string VerGerber { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string Content { get; set; }
        public string Creater { get; set; }
        public string Comfirmation { get; set; }

        public BomHistory() { }

        public BomHistory(string subVersion, string dateEx, string sTT, string location, string verGerber, string before, string after, string content, string creater, string comfirmation)
        {
            SubVersion = subVersion;
            DateEx = dateEx;
            STT = sTT;
            Location = location;
            VerGerber = verGerber;
            Before = before;
            After = after;
            Content = content;
            Creater = creater;
            Comfirmation = comfirmation;
        }

        public BomHistory(DataRow row)
        {
            this.SubVersion = row["VERSION"].ToString();
            this.DateEx = row["DATE_EVENT"].ToString();
            this.STT = row["LOCATION"].ToString();
            this.VerGerber = row["VER_GERBER"].ToString();
            this.Before = row["BEFORE_EX"].ToString();
            this.After = row["AFTER_EX"].ToString();
            this.Content = row["CONTENT"].ToString();
            this.Creater = row["CREATER"].ToString();
            this.Comfirmation = row["COMFIRM_EXCHANGE"].ToString();

        }
    }
    public class BomUsing : BomDetail
    {
        public string TapeWidth { get; set; }
        public string Pitch { get; set; }
        public string Direction { get; set; }
        public int Confirm { get; set; }
        public BomUsing() { }

        public BomUsing(DataRow item)
        {
            this.MainPart = item["MAIN_PART"].ToString().Trim();
            this.InterPart = item["INTER_PART"].ToString().Trim();
            this.CSPart = item["CS_PART"].ToString().Trim();
            this.MfgPart = item["MFG_PART"].ToString().Trim();
            this.Location = item["LOCATION"].ToString().Trim();
            this.Type = item["TYPE"].ToString().Trim();
            this.Descripton = item["DESCRIPTION"].ToString().Trim();
            this.MfgName = item["MFG_NAME"].ToString().Trim();
            this.Size = item["SIZE"].ToString().Trim();
            this.Value = item["VALUE"].ToString().Trim();
            this.Unit = item["UNIT"].ToString().Trim();
            this.Tolerace = item["TOLERANCE"].ToString().Trim();
            this.Marking = item["MARKING"].ToString().Trim();
            this.MSL = item["MSL"].ToString().Trim();
            this.Rank = item["RANK"].ToString().Trim();
            this.Process = item["PROCESS"].ToString().Trim();
            this.Quantity = item["QUANTITY"].ToString().Trim();
            this.Side_1 = item["SIDE1"].ToString().Trim();
            this.Siede_2 = item["SIDE2"].ToString().Trim();
            this.TapeWidth = item["TAPE_WIDTH"].ToString().Trim();
            this.Pitch = item["PITCH"].ToString().Trim();
            this.Direction = item["DIRECTION"].ToString().Trim();
        }

    }
    public class BomContent : Bom
    {
        public List<BomHistory> BomHistories = new List<BomHistory>();
        public List<BomUsing> LsSubPartConfirm = new List<BomUsing>();

    }
}
