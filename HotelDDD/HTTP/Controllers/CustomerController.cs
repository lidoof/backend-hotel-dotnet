using HotelDDD.Domain.Customer;
using Microsoft.AspNetCore.Mvc;

namespace HotelDDD.HTTP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                var customerId = await _customerService.CreateCustomerAsync(customer);
                return Ok(new { CustomerId = customerId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to create customer.");
            }
        }

        [HttpGet("GetCustomer/{customerId}")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            try
            {
                var customer = await _customerService.GetCustomerAsync(customerId);
                if (customer != null)
                {
                    return Ok(customer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to retrieve customer.");
            }
        }



        [HttpGet("GetCustomerWallet/{customerId}")]
        public async Task<IActionResult> GetCustomerWallet(Guid customerId)
        {
            try
            {
                var wallet = await _customerService.GetCustomerWalletAsync(customerId);
                if (wallet != null)
                {
                    return Ok(new { WalletId = wallet.Id, Balance = wallet.Balance, Currency = wallet.PreferredCurrency });
                }
                return NotFound("Wallet not found for the given customer id.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("UpdateCustomer/{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
                if (updatedCustomer != null)
                {
                    return Ok(updatedCustomer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to update customer.");
            }
        }

        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(customerId);
                if (result)
                {
                    return Ok("Customer deleted successfully.");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to delete customer.");
            }
        }

        public class AuthenticateRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            try
            {
                var result = await _customerService.AuthenticateAsync(request.Email, request.Password);

                if (result)
                {
                    return Ok(new {message= "authentification succesfful"});
                }

                return Unauthorized(new {error = "identifiant incorrect"});
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to authenticate");
            }
        }
    }
}