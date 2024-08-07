using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO
{
    public class WorkDeliveryPlan
    {
        public int Id { get; set; }
        public string CusId { get; set; }
        public string ModelId { get; set; }
        public string WorkId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Count { get; set; }
        public int Real { get; set; }
        public DateTime DateCreate { get; set; }
        public string Op { get; set; }
        public string Comment { get; set; }

        /// <summary>
        /// T
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 0 : Ko thay đổi
        /// 1 : Tạo mới
        /// 2 : Update
        /// -1 : Xóa
        /// </summary>
        public int Action { get; set; }

        public WorkDeliveryPlan() { }

        public WorkDeliveryPlan(DataRow row)
        {
            Id = int.Parse(row["Id"].ToString());
            CusId = row["CUSTOMER"].ToString();
            ModelId = row["MODEL"].ToString();
            WorkId = row["WorkId"].ToString();
            DeliveryDate = DateTime.Parse(row["DeliveryDate"].ToString());
            Count = int.Parse(row["Count"].ToString());
            Real = int.Parse(row["Real"].ToString());
            DateCreate = DateTime.Parse(row["DateCreat"].ToString());
            Op = row["Op"].ToString();
            Comment = row["Comment"].ToString();
            Status = true;
        }
    }
}
