using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Bank.Data.Entities;
using Bank.Data.Interfaces;

namespace Bank.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ILoanRepository _loanRepo;

        public LoanService
        (
            IAccountRepository accountRepo,
            ILoanRepository loanRepo
        )
        {
            _accountRepo = accountRepo;
            _loanRepo = loanRepo;
        }

        public async Task<AdminCreateCustomerLoanResponseDto> CreateLoanAsync(AdminCreateCustomerLoanRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ToAccountNumber))
                throw new ArgumentException("Account number must be provided.");

            var account = await _accountRepo.GetAccountByAccountNumberAsync(dto.ToAccountNumber);
            if (account == null)
                throw new ArgumentException("Account not found.");

            if (dto.Duration <= 0)
                throw new ArgumentException("Loan duration must be atleast 1 month.");

            var payment = dto.Amount / dto.Duration;

            var loan = new Loan
            {
                AccountId = account.AccountId,
                Amount = dto.Amount,
                Duration = dto.Duration,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Payments = payment,
                Status = "Running"
            };


            account.Balance += dto.Amount;

            var transaction = new Transaction
            {
                AccountId = account.AccountId,
                Date = loan.Date,
                Type = "Credit",
                Operation = "Loan",
                Amount = dto.Amount,
                Balance = account.Balance,
                Symbol = "LN",
                Bank = "Nordea",
                Account = account.AccountNumber
            };

            await _loanRepo.CreateLoanAsync(account, loan, transaction);

            return new AdminCreateCustomerLoanResponseDto
            {
                Message = $"Loan created successfully.",
                ToAccountNumber = account.AccountNumber,
                Amount = dto.Amount,
                Date = loan.Date,
                Duration = dto.Duration,
                Status = loan.Status,
                EndDate = loan.Date.AddMonths(dto.Duration),
                PaymentPerMonth = dto.Amount / dto.Duration
            };
        }
    }
}