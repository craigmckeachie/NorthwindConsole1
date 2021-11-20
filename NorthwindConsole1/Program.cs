using System;
using System.Linq;

namespace NorthwindConsole1
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetCustomers();
            //GetCustomer();
            //UpdateCustomer();
            //InsertCustomer();
            //DeleteCustomer();
            GetCustomerWithOrders();

            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        static void GetCustomers()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                //var customers = context.Customers.OrderBy(c=> c.CompanyName).ToList();
                var customers = from c in context.Customers
                                orderby c.CompanyName descending
                                select c;
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.CompanyName);
                }
            }
        }

        static void GetCustomer()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "ISLAT";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                //var customer = (from c in context.Customers
                //                where c.CustomerID == customerID
                //                select c).FirstOrDefault();
                Console.WriteLine(customer.CompanyName);
            }
        }

        static void GetCustomerWithOrders()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "ISLAT";
                //Lazy Loading
                //var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                //Eagerly loading
                var customer = context.Customers.Include("Orders").FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                //var customer = (from c in context.Customers
                //                where c.CustomerID == customerID
                //                select c).FirstOrDefault();
                Console.WriteLine(customer.CompanyName);
                foreach(Order order in customer.Orders)
                {
                    Console.WriteLine(order.OrderID);
                }
            }
        }

        static void UpdateCustomer()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "ISLAT";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                customer.CompanyName = "Island Trading";
                context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "ISLAT";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                Console.WriteLine(customer.CompanyName);
            }
        }

        static void InsertCustomer()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;

                var customer = new Customer() { CustomerID = "CRAIG", CompanyName = "Funny Ant, LLC" };
                context.Entry(customer).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }

            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "CRAIG";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                Console.WriteLine(customer.CompanyName);
            }
        }

        static void DeleteCustomer()
        {
            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "CRAIG";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                Console.WriteLine(customer.CompanyName);
                context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            using (var context = new NorthwindContext())
            {
                context.Database.Log = Console.WriteLine;
                var customerID = "CRAIG";
                var customer = context.Customers.FirstOrDefault<Customer>(c => c.CustomerID == customerID);
                Console.WriteLine(customer == null);
            }
        }
    }
}
