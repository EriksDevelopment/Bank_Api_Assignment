using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<CustomerController> _logger;
        public TransactionController
        (
            ITransactionService transactionService,
            ILogger<CustomerController> logger
        )
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("accounts/{accountNumber}/transactions")]
        public async Task<ActionResult<List<ShowAllTransactionsResponseDto>>> GetTransactions(string accountNumber)
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("customerId")!.Value);

                var transaction = await _transactionService.GetAccountTransactionsAsync(customerId, accountNumber);

                _logger.LogInformation("Transactions successfully retrieved");
                return Ok(transaction);
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
                _logger.LogError(ex, "Something went wrong while retrieving all transactions");
                return StatusCode(500, "Something went wrong");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create-transaction")]
        public async Task<ActionResult<CreateTransactionResponseDto>> CreateTransaction(CreateTransactionRequestDto dto)
        {
            try
            {
                var customerId = int.Parse(User.FindFirst("CustomerId")!.Value);

                var result = await _transactionService.CreateTransactionAsync(customerId, dto);

                _logger.LogInformation("Transaction successfully created.");
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while making a transaction");
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}