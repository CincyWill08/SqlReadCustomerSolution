using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlReadCustomer
{
    public class SqlOrder
    {
        public List<Order> List()
        {
            //Make a connection to the server and database

            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The Connection Didn't Open.");
                return null;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "select * from [order]";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Result has no rows");
                return null;
            }

            List<Order> orders = new List<Order>();

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));
                decimal amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                int customerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));

                Order order = new Order();
                order.Id = id;
                order.Date = date;
                order.Amount = amount;
                order.CustomerId = customerId;

                orders.Add(order);

            }

            conn.Close();  //good practice to close since Sql Connection is high overhead
            return orders;
        }
    }
}
