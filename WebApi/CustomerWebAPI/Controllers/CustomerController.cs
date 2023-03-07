using CustomerWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {


        private static List<Customer> _customers = new List<Customer>()
        {
            new Customer() {Id = 1, Name = "John Doe", Locations = new List<CustomerLocation>{new CustomerLocation(){Id = 1, Address = "123 Main St.", City = "Anytown", State = "CA", ZipCode = 90210}}},
            new Customer() {Id = 2, Name = "Jane Smith", Locations = new List<CustomerLocation>{new CustomerLocation(){Id = 2, Address = "231 Maple Ave.", City = "Somewhere", State = "NY", ZipCode = 12345}, new CustomerLocation(){Id = 3, Address = "332 Pine St.", City = "Another", State = "NY", ZipCode = 67890}}}
        };

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        [HttpGet]
        

         [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of all customers.", typeof(IEnumerable<Customer>))]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return _customers;
        }

        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer object.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the requested customer.", typeof(Customer))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer object to create.</param>
        /// <returns>The created customer object.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the newly created customer.", typeof(Customer))]
        public IActionResult Create(Customer customer)
        {
            // assign new id to customer
            int newCustomerId = _customers.Max(c => c.Id) + 1;
            customer.Id = newCustomerId;

            _customers.Add(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="updatedCustomer">The updated customer object.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Customer successfully updated.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid customer ID or missing fields.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
        public IActionResult Update(int id, Customer updatedCustomer)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            //update fields from updated customer
            if (!string.IsNullOrEmpty(updatedCustomer?.Name))
            {
                customer.Name = updatedCustomer.Name;
            }
            if (updatedCustomer?.Locations != null && updatedCustomer.Locations.Any())
            {
                customer.Locations = updatedCustomer.Locations;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Customer successfully deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Customer not found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Cannot delete a customer with associated locations.")]
        public IActionResult Delete(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            if (customer.Locations != null && customer.Locations.Any())
            {
                return BadRequest("Cannot delete a customer with associated locations");
            }

            _customers.Remove(customer);

            return NoContent();
        }
    }
}
