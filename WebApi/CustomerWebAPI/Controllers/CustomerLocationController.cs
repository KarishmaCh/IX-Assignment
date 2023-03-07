using CustomerWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLocationController : ControllerBase
    {
    

        private static readonly List<CustomerLocation> _customerLocations = new List<CustomerLocation>
        {
            new CustomerLocation { Id = 1, CustomerId = 1, Name = "Location 1", Address = "123 Main St.", City = "Anytown", State = "CA", ZipCode = 12345 },
            new CustomerLocation { Id = 2, CustomerId = 1, Name = "Location 2", Address = "456 Elm St.", City = "Otherville", State = "NY", ZipCode = 67890 },
            new CustomerLocation { Id = 3, CustomerId = 2, Name = "Location 3", Address = "789 Oak St.", City = "Smallville", State = "TX", ZipCode = 54321 }
        };

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerLocation> Get(int id)
        {
            var customerLocation = _customerLocations.FirstOrDefault(cl => cl.Id == id);
            if (customerLocation == null)
            {
                return NotFound();
            }

            return customerLocation;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerLocation> UpdateLocation(int id, CustomerLocation updatedLocation)
        {
            if (ModelState.IsValid)
            {
                var locationToUpdate = _customerLocations.FirstOrDefault(l => l.Id == id);

                if (locationToUpdate == null)
                {
                    return NotFound();
                }

                locationToUpdate.Name = updatedLocation.Name;
                locationToUpdate.Address = updatedLocation.Address;
                locationToUpdate.City = updatedLocation.City;
                locationToUpdate.State = updatedLocation.State;
                locationToUpdate.ZipCode = updatedLocation.ZipCode;

                return Ok(locationToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

     
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var location = _customerLocations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _customerLocations.Remove(location);
            return NoContent();
        }

        [HttpDelete("{customerId}/locations/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult DeleteCustomerLocation(int customerId, int id)
        {
            var customer = _customerLocations.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var location = _customerLocations.FirstOrDefault(l => l.Id == id && l.CustomerId == customerId);

            if (location == null)
            {
                return NotFound();
            }

            if (_customerLocations.Count(c => c.CustomerId == customerId) == 1)
            {
                // customer has only one location so delete entire customer as well
                _customerLocations.Remove(customer);
                return NoContent();
            }

            _customerLocations.Remove(location);

            if (_customerLocations.Count(c => c.CustomerId == customerId) == 0)
            {
                return Ok("All locations for the customer have been deleted.");
            }

            return NoContent();
        }
    }
}