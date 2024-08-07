using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string OpContact { get; set; }
        public string Information { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Customer() { }

        public Customer(DataRow dataRow)
        {
            CustomerName = dataRow["CUSTOMER_NAME"].ToString();
            CustomerID = dataRow["CUSTOMER_ID"].ToString();
            CompanyName = dataRow["COMPANY_NAME"].ToString();
            OpContact = dataRow["REPRESENTATIVE"].ToString();
            Information =   dataRow["INFORMATION"].ToString();
            Address = dataRow["ADDRESS"].ToString();
            Phone   = dataRow["PHONE"].ToString();
            Email =  dataRow["EMAIL"].ToString();
        }
    }
}
