using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlReadCustomer;

namespace TestSqlReadCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerController cust = new CustomerController();

            int cid = 15;

            if (!cust.Delete(cid))
            {
                Console.WriteLine("DELETE FAILED!");
            }
            else
                Console.WriteLine("DELETE SUCCESSFUL!");


            // Customer c = new Customer();
            // c.Id = 16;
            // c.Name = "SDMax";
            // c.City = "Mason";
            // c.State = "OH";
            // c.IsCorpAcct = true;
            // c.CreditLimit = 1000;
            // c.Active = true;

            //if (!cust.Update(c))
            // {
            //     Console.WriteLine("UPDATE FAILED!");
            // }

            //The call from the exe to the dll

            //List<Customer> customers = cust.List();
            //List<Customer> customers = cust.SearchByCreditLimitRange(300000,1000000);
            //List<Customer> customers = cust.SearchByName("LPA");
            //foreach (Customer customer in customers)
            //{
            //    Console.WriteLine($"{customer.Id} | {customer.Name} | {customer.City} | {customer.State} | {customer.CreditLimit}");
            //}

            //Customer customer = cust.Get(2);
            //Console.WriteLine($"{customer.Id} | {customer.Name} | {customer.City} | {customer.State}");

            //SqlOrder ord = new SqlOrder();

            ////The call from the exe to the dll
            //List<Order> orders = ord.List();

            //foreach (Order order in orders)
            //{
            //    Console.WriteLine($"{order.Date} | {order.Amount} | {order.CustomerId}");
            //}

            Console.ReadLine();
        }
    }
}
