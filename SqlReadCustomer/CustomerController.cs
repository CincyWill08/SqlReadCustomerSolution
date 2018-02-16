using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SqlReadCustomer
{


    public class CustomerController
    {
        public bool Delete(int customerId)
        {
            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The Connection Didn't Open.");
                return false;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "delete from customer where Id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Id", customerId));
            int recsAffected = cmd.ExecuteNonQuery();
            if (recsAffected != 1)
            {
                Console.WriteLine("Delete failed.");
                return false;
            }

            conn.Close();  //good practice to close since Sql Connection is high overhead
            return true;

        }

        public bool Update(Customer customer)
        {
         //Make a connection to the server and database

            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {   
                Console.WriteLine("The Connection Didn't Open.");
                return false;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "update customer set "
                    + " Name = @Name, city = @City, state = @State, " 
                    + " IsCorpAcct = @IsCorpAcct, CreditLimit = @CreditLimit, "
                    + " Active = @Active where id = @Id;";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Id",customer.Id));
            cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
            cmd.Parameters.Add(new SqlParameter("@City", customer.City));
            cmd.Parameters.Add(new SqlParameter("@State", customer.State));
            cmd.Parameters.Add(new SqlParameter("@IsCorpAcct", customer.IsCorpAcct));
            cmd.Parameters.Add(new SqlParameter("@CreditLimit", customer.CreditLimit));
            cmd.Parameters.Add(new SqlParameter("@Active", customer.Active));
            int recsAffected = cmd.ExecuteNonQuery();
            if (recsAffected != 1)
            {
                Console.WriteLine("Insert failed.");
                return false;
            }
            conn.Close();  //good practice to close since Sql Connection is high overhead
            return true;
        }

        public bool Insert(Customer customer)
        {
            //Make a connection to the server and database

            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The Connection Didn't Open.");
                return false;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "insert into customer "
                + " (name, city, state, IsCorpAcct, CreditLimit, Active) "
                + " values (@name, @city, @state, @IsCorpAcct, @CreditLimit, @Active);";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
            cmd.Parameters.Add(new SqlParameter("@City", customer.City));
            cmd.Parameters.Add(new SqlParameter("@State", customer.State));
            cmd.Parameters.Add(new SqlParameter("@IsCorpAcct", customer.IsCorpAcct));
            cmd.Parameters.Add(new SqlParameter("@CreditLimit", customer.CreditLimit));
            cmd.Parameters.Add(new SqlParameter("@Active", customer.Active));
            int recsAffected = cmd.ExecuteNonQuery();
            if (recsAffected != 1)
            {
                Console.WriteLine("Insert failed.");
                return false;
            }
            conn.Close();  //good practice to close since Sql Connection is high overhead
            return true;
        }

        public List<Customer> SearchByName(string partName)
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

            string sql = "select * from customer where name like '%' + @partialName + '%' order by name;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pname = new SqlParameter("@partialName", partName);
            cmd.Parameters.Add(pname);
              
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Result has no rows");
                return null;
            }

            List<Customer> customers = new List<Customer>();

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer();
                customer.Id = id;
                customer.Name = name;
                customer.City = city;
                customer.State = state;
                customer.IsCorpAcct = isCorpAcct;
                customer.CreditLimit = creditLimit;
                customer.Active = active;

                customers.Add(customer);

            }

            conn.Close();  //good practice to close since Sql Connection is high overhead
            return customers;
        }

        public List<Customer> SearchByCreditLimitRange(int lower, int upper)
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

            string sql = "select * from Customer"
                + " where creditLimit between @lowerc1 and @upperc1"
                + " order by CreditLimit desc";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter plower = new SqlParameter("@lowerc1", lower);
            SqlParameter pupper = new SqlParameter("@upperc1", upper);
            cmd.Parameters.Add(plower);
            cmd.Parameters.Add(pupper);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Result has no rows");
                return null;
            }

            List<Customer> customers = new List<Customer>();

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer();
                customer.Id = id;
                customer.Name = name;
                customer.City = city;
                customer.State = state;
                customer.IsCorpAcct = isCorpAcct;
                customer.CreditLimit = creditLimit;
                customer.Active = active;

                customers.Add(customer);

            }

            conn.Close();  //good practice to close since Sql Connection is high overhead
            return customers;
        }

        public Customer Get(int CustomerId)
        {
            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The Connection Didn't Open.");
                return null;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "select * from customer where Id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = CustomerId;
            cmd.Parameters.Add(pId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine($"Customer {CustomerId} not found");
                return null;
            }
            reader.Read();

            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string city = reader.GetString(reader.GetOrdinal("City"));
            string state = reader.GetString(reader.GetOrdinal("State"));
            bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
            int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
            bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

            Customer customer = new Customer();
            customer.Id = id;
            customer.Name = name;
            customer.City = city;
            customer.State = state;
            customer.IsCorpAcct = isCorpAcct;
            customer.CreditLimit = creditLimit;
            customer.Active = active;

            return customer;

        }

        public List<Customer> List()
        {
            //Make a connection to the server and database

            string connStr = @"server=STUDENT03\SQLEXPRESS;database=SqlTutorial;Trusted_connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if(conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The Connection Didn't Open.");
                return null;
            }
            //Console.WriteLine("The Connection Open Was Successful.");

            string sql = "select * from customer";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("Result has no rows");
                return null;
            }

            List<Customer> customers = new List<Customer>();

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer();
                customer.Id = id;
                customer.Name = name;
                customer.City = city;
                customer.State = state;
                customer.IsCorpAcct = isCorpAcct;
                customer.CreditLimit = creditLimit;
                customer.Active = active;

                customers.Add(customer);
                
            }

            conn.Close();  //good practice to close since Sql Connection is high overhead
            return customers;
        }
    }
}
