using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController
        (
            ICustomerService customerService,
            ILogger<CustomerController> logger
        )
        {
            _customerService = customerService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("customer-login")]
        public async Task<ActionResult<CustomerLoginResponseDto>> CustomerLogin([FromBody] CustomerLoginRequestDto request)
        {
            try
            {
                var result = await _customerService.CustomerLoginAsync(request);

                _logger.LogInformation("Login successfull (Customer).");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while login (customer).");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] AdminCreateCustomerRequestDto dto)
        {
            try
            {
                var result = await _customerService.CreateCustomerWithAccount(dto);

                _logger.LogInformation("Customer successfully created (Admin).");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while creating customer (Admin).");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}