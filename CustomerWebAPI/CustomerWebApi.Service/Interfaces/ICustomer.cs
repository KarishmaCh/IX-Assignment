using CustomerWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebApi.Service.Interfaces
{
   
    public interface ICustomer
    {

        IEnumerable<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
        Customer AddCustomer(Customer customer);
        Customer UpdateCustomer(int id,Customer customer);
        Customer DeleteCustomer(int id);
     

    }
}
