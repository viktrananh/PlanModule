﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Planed.PlanExport
{
    public class ShipmentView
    {
        public string BillNumber { get; set; }
        public string Work { get; set; }
        public string CusID { get; set; }
        public string Model { get; set; }
        public string CusModel { get; set; }
        public string CusCode { get; set; }
        public DateTime DateExport { get; set; }
        public DateTime DateReal { get; set; }
        public int Request { get; set; }
        public int Real { get; set; }
        public int BoxC { get; set; }
        public int BoxP { get; set; }
        public ShipmentView() { }
        public ShipmentView(DataRow dataRow)
        {
            BillNumber = dataRow["BILL_NUMBER"].ToString();
            Work = dataRow["WORK_ID"].ToString();
            CusID = dataRow["CUSTOMER_ID"].ToString();
            Model = dataRow["MODEL_ID"].ToString();
            CusModel = dataRow["CUS_MODEL"].ToString();
            CusCode = dataRow["CUS_CODE"].ToString();
            DateExport = DateTime.Parse( dataRow["DATE_EXPORT"].ToString());
            DateReal = DateTime.Parse(dataRow["DATE_REAL"].ToString());

            Request = string.IsNullOrEmpty( dataRow["NUMBER_REQUEST"].ToString()) ? 0 : int.Parse(dataRow["NUMBER_REQUEST"].ToString());
            Real = string.IsNullOrEmpty(dataRow["EXPORTS_REAL"].ToString()) ? 0 : int.Parse(dataRow["EXPORTS_REAL"].ToString());
            BoxC = string.IsNullOrEmpty(dataRow["BOX_C"].ToString()) ? 0 : int.Parse(dataRow["BOX_C"].ToString());
            BoxP = string.IsNullOrEmpty(dataRow["BOX_P"].ToString()) ? 0 : int.Parse(dataRow["BOX_P"].ToString());

        }
    }
}