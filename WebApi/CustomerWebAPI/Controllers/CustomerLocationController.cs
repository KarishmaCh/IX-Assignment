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

        /// <summary>
        /// Gets the customer location with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the customer location to retrieve.</param>
        /// <returns>The customer location with the specified ID.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The customer location with the specified ID.", typeof(CustomerLocation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No customer location was found with the specified ID.")]
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


        /// <summary>
        /// Updates the specified customer location.
        /// </summary>
        /// <param name="id">The ID of the customer location to update.</param>
        /// <param name="updatedLocation">The updated customer location.</param>
        /// <returns>The updated customer location.</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The updated customer location.", typeof(CustomerLocation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request model is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No customer location was found with the specified ID.")]
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

        /// <summary>
        /// Deletes the customer location with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the customer location to delete.</param>
        /// <returns>A 204 No Content response indicating that the customer location was deleted successfully.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The customer location was deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No customer location was found with the specified ID.")]
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

        /// <summary>
        /// Deletes a specific customer location of a customer with the specified ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer whose location is to be deleted.</param>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A 204 No Content response indicating that the customer location was deleted successfully.</returns>
        [HttpDelete("{customerId}/locations/{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The customer location was deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No customer or customer location was found with the specified IDs.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The customer has only one location, so the entire customer was deleted.")]
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