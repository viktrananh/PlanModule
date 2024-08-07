using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    class MainPart_infor
    {

        public string workID { get; set; }
        public string mainPart { get; set; }
        public string partNumber { get; set; }
        public string csPart { get; set; }
        public string mfgPart { get; set; }
        public int qtyBom { get; set; }
        public int qtyRequest { get; set; }
    }
    public class Part_infor
    {
        public string workID { get; set; }
        public string part { get; set; }
        public int qtyBom { get; set; }
        public int qtyRequest { get; set; }
    }
    
    public class work_remain_Part
    {
        //WORK_ID,PART_NUMBER,QTY_REMAIN, IS_BOOK
        public string WORK_ID { get; set; }
        public string main_part { get; set; }
        public int qtyMainrequest { get; set; }
        public int qtyMainExport { get; set; }
        public int qtyMainRemain { get; set; }
        public string PART_NUMBER { get; set; }
        public int? qtyPartExport { get; set; }
        public int? qtyPartRemain { get; set; }
    }
    public class work_remain_SPart
    {
        //WORK_ID,PART_NUMBER,QTY_REMAIN, IS_BOOK
        public string WORK_ID { get; set; }
        public string main_part { get; set; }
        public string PART_NUMBER { get; set; }
        public int? qtyPartRemain { get; set; }
    }
    class WorkOrder
    {
        public static string stateClose = "CLOSE";
        public string WorkID { get; set; }
        public string ModelID { get; set; }
        public int totalPcs { get; set; }
        public int isRMA { get; set; }
        public string bomVersion { get; set; }
        public string state { get; set; }
        public int PCBOnPanel { get; set; }
        public int NumberTemp { get;set; }

    }
}
