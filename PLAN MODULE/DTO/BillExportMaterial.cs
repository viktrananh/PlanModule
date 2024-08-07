using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{

    public class BillExportMaterial
    {
        public static int XuatKeHoach = 0;
        public static int XuatPhatSinh = 1;
        public static int XuatGiaCong = 2;
        public static int XuatTheoFile = 3;

        public static int phiêuSMT = 5;
        public static int phiêuFAT = 4;
        public class subPart_Request
        {
            public string subpartNo { get; set; }
            public string mainPart { get; set; }
            public string unit { get; set; }
            public int qtyRequest { get; set; }
        }
        public class Export_Part_Fact
        {
            public string partNo { get; set; }
            public string unit { get; set; }
            public int qtyExport { get; set; }
            public List<didExport> didExports { get; set; }
        }
        public class didExport
        {
            public string did { get; set; }
            public string partNo { get; set; }
            public string cusNo { get; set; }
            public string mfgNo { get; set; }
            public string lotCode { get; set; }
            public string dateCode { get; set; }
            public string exPridate { get; set; }
            public int qty { get; set; }
        }
        public static T Cast<T>(object target, T example)
        {
            return (T)target;
        }
        public struct billStatusDefine
        {
            public int value;
            public string status;
        }
        public List<billStatusDefine> billStatuses = new List<billStatusDefine>
        {
            new billStatusDefine{value=-1,status="Đã hủy"},
            new billStatusDefine{value=0,status="Tạo phiếu"},
            new billStatusDefine{value=1,status="Đã duyệt"},
            new billStatusDefine{value=2,status="Đã xuất"},
            new billStatusDefine{value=3,status="Hoàn thành"}

        };

        public string BillId { get; set; }
        public string ModelId { get; set; }
        public string CusId { get; set; }
        public string WorkId { get; set; }
        public int BillType { get; set; }
        public string BillTypeName { get; set; }
        public int ExportType { get; set; }
        public string ExportTypeName { get; set; }

        public int BillStatus { get; set; }
        public string BillStatusName { get; set; }

        public int Pcbs { get; set; }
        public DateTime CreateTime { get; set; }
        public string BomVersion { get; set; }
        public string OP { get; set; }
        public string Comment { get; set; }
        public string Note { get; set; }
        public List<Export_Part_Fact> export_Part_s { get; set; }
        public List<subPart_Request> SubPart_Requests { get; set; }
        public List<BillExportMaterialDetail> BillExportMaterialDetails { get; set; }

    }

   }
