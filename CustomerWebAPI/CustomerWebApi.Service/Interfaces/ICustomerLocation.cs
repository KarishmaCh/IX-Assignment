using CustomerWebApi.Model;
using CustomerWebApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebApi.Service.Interfaces
{
    public interface ICustomerLocation
    {

        IEnumerable<CustomerLocation> GetLocationsByCustomerId(int customerId);
        CustomerLocation GetLocationById(int id);
        CustomerLocation AddLocation(CustomerLocation location);
        CustomerLocation UpdateLocation(CustomerLocation location);
        CustomerLocation DeleteLocation(int id, int customerId);
      
    }
}
