using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bank.Data.Dtos;
using Bank.Core.Interfaces;

namespace Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILogger<AdminController> _logger;

        public LoanController
        (
            ILoanService loanService,
            ILogger<AdminController> logger
        )
        {
            _loanService = loanService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-loan")]
        public async Task<ActionResult<AdminCreateCustomerLoanResponseDto>> CreateLoan(AdminCreateCustomerLoanRequestDto dto)
        {
            try
            {
                var result = await _loanService.CreateLoanAsync(dto);

                _logger.LogInformation("Loan successfully created.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while creating loan");
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}