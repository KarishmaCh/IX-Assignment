using CustomerWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using CustomerWebApi.Service;
using CustomerWebApi.Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomer _customerService;

        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all customers")]
        public IEnumerable<Customer> GetAll()
        {
            return _customerService.GetAllCustomers();
        }

        /// <summary>
        /// Retrieves specific customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a specific customer by ID.")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound("Customer not found for the specified ID.");
            }
            return customer;
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customer">The customer data to add.</param>
        [HttpPost]
        [SwaggerOperation(Summary = "Adds a new customer.")]
        [ProducesResponseType(typeof(Customer), 200)]
        public IActionResult Create(Customer customer)
        {
            return Ok(_customerService.AddCustomer(customer));
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="newCust">The updated customer data.</param>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing customer.")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(204)]
        public ActionResult Update(int id, Customer newCust)
        {
            if (newCust.Id != null)
            {
                Customer customer = _customerService.UpdateCustomer(id, newCust);
                return Ok(customer);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an existing customer.")]
        [ProducesResponseType(typeof(Customer), 200)]
        public ActionResult<Customer> Delete(int id)
        {
            Customer customer = _customerService.DeleteCustomer(id);
            return Ok(customer);
        }
    }
}
