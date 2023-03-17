using CustomerWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Service;

using CustomerWebApi.Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CustomerWebApi.Service.Services;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLocationController : ControllerBase
    {
        private readonly ICustomerLocation _customerLocationService;


        public CustomerLocationController(ICustomerLocation customerLocationService)
        {
            _customerLocationService = customerLocationService;

        }

        /// <summary>
        /// Returns a list of locations for a specified customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A list of CustomerLocation objects.</returns>
        [HttpGet("{customerId}")]
        [SwaggerOperation(Summary = "List all locations for a customer", Description = "Returns a list of locations for a specified customer.")]
        [SwaggerResponse(200, "OK", typeof(IEnumerable<CustomerLocation>))]
        [SwaggerResponse(404, "Not Found", typeof(object))]
        public ActionResult<IEnumerable<CustomerLocation>> GetAllLocationsByCustomerId(int customerId)
        {
            var locations = _customerLocationService.GetLocationsByCustomerId(customerId);
            if (locations == null)
            {
                return NotFound(new
                {
                    message = "Location not found for the specified ID"
                });
            }
            return Ok(locations);
        }

        /// <summary>
        /// Adds a new location to the database.
        /// </summary>
        /// <param name="location">The CustomerLocation object to add.</param>
        /// <returns>The newly added CustomerLocation object.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new location", Description = "Adds a new location to the database.")]
        [SwaggerResponse(200, "OK", typeof(CustomerLocation))]
        public ActionResult<CustomerLocation> PostLocation(CustomerLocation location)
        {
            return Ok(_customerLocationService.AddLocation(location));
        }

        /// <summary>
        /// Deletes a location from the database.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <param name="locationId">The ID of the location.</param>
        /// <returns>The deleted CustomerLocation object.</returns>
        [HttpDelete("{id}/{locationId}")]
        [SwaggerOperation(Summary = "Delete a location", Description = "Deletes a location from the database.")]
        [SwaggerResponse(200, "OK", typeof(CustomerLocation))]
        [SwaggerResponse(404, "Not Found", typeof(object))]
        public ActionResult<CustomerLocation> DeleteLocation(int id, int locationId)
        {
            var location = _customerLocationService.GetLocationById(id);
            if (location == null)
            {
                return NotFound(new
                {
                    message = "Location not found for the specified ID"
                });
            }
            _customerLocationService.DeleteLocation(id, locationId);
            return Ok(location);
        }
    }
}



