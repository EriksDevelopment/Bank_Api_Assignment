using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<CustomerController> _logger;
        public AccountController
        (
            IAccountService accountService,
            ILogger<CustomerController> logger
        )
        {
            _accountService = accountService;
            _logger = logger;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("accounts")]
        public async Task<ActionResult<List<ShowAccountsResponseDto>>> GetAccounts()
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("CustomerId")!.Value);

                var accounts = await _accountService.GetCustomerAccountAsync(customerId);

                _logger.LogInformation("Accounts successfully retrieved");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when retrieving accounts");
                return StatusCode(500, "Something went wrong");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create-account")]
        public async Task<ActionResult<CreateAccountResponseDto>> CreateAccount([FromBody] CreateAccountRequestDto dto)
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("CustomerId")!.Value);

                var result = await _accountService.CreateAccountAsync(dto, customerId, dto.AccountFrequency);

                _logger.LogInformation("Account successfully created.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while creating account");
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}