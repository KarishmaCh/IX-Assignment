using CustomerWebApi.Model;
using CustomerWebApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebApi.Service.Services
{
    public class CustomerService : ICustomer
    {

        private static List<Customer>? _customers = null;
  

        public CustomerService()
        {
            _customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "karishma ch", Locations = new List<CustomerLocation>
                {
                    new CustomerLocation { Id = 1, CustomerId = 1, Address = "123 Main St", City = "Anytown", State = "CA"},
                    new CustomerLocation { Id = 2, CustomerId = 1, Address = "456 Elm St", City = "Anytown", State = "CA" }
                }},
                new Customer { Id = 2, Name = "Jane Smith", Locations = new List<CustomerLocation>
                {
                    new CustomerLocation { Id = 3, CustomerId = 2, Address = "789 Oak St", City = "Anytown", State = "CA" }
                }},
                new Customer { Id = 3, Name = "Bob Johnson", Locations = new List<CustomerLocation>() }
            };


        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer? GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer AddCustomer(Customer customer)
        {

            _customers.Add(customer);
            return customer;

        }

        public Customer UpdateCustomer(int id, Customer newCustomer)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);


            //update fields from updated customer
            if (!string.IsNullOrEmpty(newCustomer?.Name))
            {
                customer.Name = newCustomer.Name;

                if (newCustomer?.Locations != null && newCustomer.Locations.Any())
                {
                    customer.Locations = newCustomer.Locations;
                }
                return customer;
            }


            return null;
        }


        public Customer DeleteCustomer(int id)
        {

            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return null;
            }

            if (customer.Locations != null && customer.Locations.Any())
            {
                return null;
            }

            _customers.Remove(customer);
            return customer;


        }
    }
}



