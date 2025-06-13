using BootcampaApp.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BootcampApp.Model;
using WebAPI.RESTModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync();
            List<CustomerREST> customersREST = new List<CustomerREST>();
            foreach (var customer in customers)
            {
                customersREST.Add(new CustomerREST(customer.Id, customer.Name, customer.Phone, customer.Email));
            }
            return Ok(customersREST);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null)
                return NotFound();
            CustomerREST customerREST = new CustomerREST(customer.Id, customer.Name, customer.Phone, customer.Email);
            return Ok(customerREST);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerModel customer)
        {
            await _service.AddAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerModel customer)
        {
            if (id != customer.Id)
                return BadRequest("ID mismatch.");

            await _service.UpdateAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
