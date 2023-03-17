using CustomerWebApi.Model;
using CustomerWebApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerWebApi.Service.Services
{
    public class CustomerLocationService : ICustomerLocation
    {


        public  List<CustomerLocation> _locations;

        public CustomerLocationService()
        {
            _locations = new List<CustomerLocation>
        {
            new CustomerLocation { Id = 1, CustomerId = 1, Address = "123 Main St", City = "Anytown", State = "CA", Country = "USA"},
            new CustomerLocation { Id = 2, CustomerId = 1, Address = "456 Maple Ave", City = "Sometown", State = "CA", Country = "USA"  },
            new CustomerLocation { Id = 3, CustomerId = 2, Address = "789 Elm St", City = "Anytown", State = "NY", Country = "USA"},
            new CustomerLocation { Id = 4, CustomerId = 3, Address = "321 Oak Ave", City = "Sometown", State = "NY", Country = "USA"}
        };
        }
        public IEnumerable<CustomerLocation> GetLocationsByCustomerId(int customerId)
        {
            return _locations.Where(l => l.CustomerId == customerId);
        }

        public CustomerLocation GetCustomerLocationById(int id)
        {
            var location = _locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                throw new ArgumentException("Invalid location id");
            }
            return location;
        }
        public CustomerLocation GetLocationById(int id)
        {
            var location = _locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                throw new ArgumentException($"Location with ID {id} not found.");
            }
            return location;
        }


        public CustomerLocation AddLocation(CustomerLocation location)
        {
            _locations.Add(location);

            return location;
        }

        public CustomerLocation UpdateLocation(CustomerLocation location)
        {
            var index = _locations.FindIndex(l => l.Id == location.Id);
            _locations[index] = location;

            return location;
        }

        public CustomerLocation DeleteLocation(int id, int locationId)
        {
            var customer = _locations.FirstOrDefault(c => c.CustomerId == id);
            var location = _locations.FirstOrDefault(l => l.Id == id && l.Id == locationId);

            
            if (_locations.Count(c => c.Id == locationId) == 1)
            {
                // customer has only one location so delete entire customer as well
                _locations.Remove(customer);
                return location;
            }

            //_locations.Remove(location);

            if (_locations.Count(c => c.Id == locationId) == 0)
            {
                return null;
            }

            return null;
       
        }
    }
}
