using PLAN_MODULE.DAO.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    internal class CustomerControl
    {
        private static CustomerControl instance;
        public static CustomerControl Instance
        {
            get { if (instance == null) instance = new CustomerControl(); return instance; }
            set { instance = value; }
        }
        public CustomerControl() { }

        public static List<Customer> LoadCustomer()
        {
           List<Customer> customers = new List<Customer>();
           DefineCustomerDAO define = new DefineCustomerDAO();
            DataTable dt = define.GetAllCustomer();
            foreach (DataRow item in dt.Rows)
            {
                Customer customer = new Customer(item);
                customers.Add(customer);
            }
            return customers;
        }
    }
}
