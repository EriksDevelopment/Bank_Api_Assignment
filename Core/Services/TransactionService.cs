using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Bank.Data.Entities;
using Bank.Data.Interfaces;

namespace Bank.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionService
        (
            ICustomerRepository customerRepo,
            IAccountRepository accountRepo,
            ITransactionRepository transactionRepo
        )
        {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task<List<ShowAllTransactionsResponseDto>> GetAccountTransactionsAsync(int customerId, string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be empty.");

            var account = await _accountRepo.GetAccountByAccountNumberAsync(accountNumber);
            if (account == null)
                throw new UnauthorizedAccessException("Account number not found");

            var ownsAccount = await _customerRepo.CustomerOwnsAccountAsync(customerId, account.AccountId);
            if (!ownsAccount)
                throw new UnauthorizedAccessException("Invalid account Id.");

            var transaction = await _transactionRepo.GetTransactionByAccountIdAsync(account.AccountId);


            return transaction.Select(t => new ShowAllTransactionsResponseDto
            {
                AccountNumber = t.AccountNavigation.AccountNumber,
                Date = t.Date,
                Type = t.Type,
                Operation = t.Operation,
                Amount = t.Amount,
                Balance = t.Balance,
                Symbol = t.Symbol ?? "Unknown",
                Bank = t.Bank ?? "Unknown",
                Account = t.Account ?? "Unknown"
            }).ToList();
        }

        public async Task<CreateTransactionResponseDto> CreateTransactionAsync(int customerId, CreateTransactionRequestDto dto)
        {
            var fromAccount = await _accountRepo.GetAccountByAccountNumberAsync(dto.FromAccountNumber);
            if (fromAccount == null)
                throw new ArgumentException("From account not found.");

            var toAccount = await _accountRepo.GetAccountByAccountNumberAsync(dto.ToAccountNumber);
            if (toAccount == null)
                throw new ArgumentException("To account not found.");

            var ownsAccount = await _customerRepo.CustomerOwnsAccountAsync(customerId, fromAccount.AccountId);
            if (!ownsAccount)
                throw new UnauthorizedAccessException("Invalid account number");

            if (fromAccount.Balance < dto.Amount)
                throw new InvalidOperationException("Not enough money on account.");

            fromAccount.Balance -= dto.Amount;
            toAccount.Balance += dto.Amount;

            var transactionDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var fromTransaction = new Transaction
            {
                AccountId = fromAccount.AccountId,
                Amount = -dto.Amount,
                Balance = fromAccount.Balance,
                Type = "Debit",
                Operation = "Collection from Another Bank",
                Date = transactionDate,
                Account = toAccount.AccountNumber
            };

            var toTransaction = new Transaction
            {
                AccountId = toAccount.AccountId,
                Amount = dto.Amount,
                Balance = toAccount.Balance,
                Type = "Credit",
                Operation = "Collection from Another Bank",
                Date = transactionDate,
                Account = fromAccount.AccountNumber
            };

            await _transactionRepo.SaveTransactionAsync(fromAccount, toAccount, fromTransaction, toTransaction);

            return new CreateTransactionResponseDto
            {
                Message = "Transaction comleted successfully.",
                FromAccountNumber = fromAccount.AccountNumber,
                ToAccountNumber = toAccount.AccountNumber,
                Amount = dto.Amount,
                Date = transactionDate
            };
        }
    }
}