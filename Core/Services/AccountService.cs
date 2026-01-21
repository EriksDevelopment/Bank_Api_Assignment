using Bank.Core.Interfaces;
using Bank.Data.Dtos;
using Bank.Data.Models;
using Bank.Data.Interfaces;

namespace Bank.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IDispositionRepository _dispositionRepo;

        public AccountService
        (
            IAccountRepository accountRepo,
            IDispositionRepository dispositionRepo
        )
        {
            _accountRepo = accountRepo;
            _dispositionRepo = dispositionRepo;
        }

        public async Task<List<ShowAccountsResponseDto>> GetCustomerAccountAsync(int customerId)
        {
            var account = await _accountRepo.GetAccountByCustomerIdAsync(customerId);

            return account.Select(a => new ShowAccountsResponseDto
            {
                AccountNumber = a.AccountNumber,
                Account = a.AccountTypes?.TypeName ?? "Standard transaction account",
                Balance = a.Balance
            }).ToList();
        }

        public async Task<CreateAccountResponseDto> CreateAccountAsync(CreateAccountRequestDto dto, int customerId, string accountFrequency)
        {
            if (string.IsNullOrWhiteSpace(dto.AccountFrequency))
                throw new ArgumentException("Account frequency must be provided, either monthly or weekly");

            var frequency = await _accountRepo.GetAccountFrequencyAsync(accountFrequency);
            if (frequency == null)
                throw new ArgumentException("invalid account frequency, must be either monthly or weekly");

            var accountType = await _accountRepo.GetAccountTypeAsync(dto.AccountTypeId);
            if (accountType == null)
                throw new ArgumentException("Invalid account type, provide 1 for Standard transaction account or 2 for Savings account.");

            var account = new Account
            {
                Balance = 0,
                Frequency = dto.AccountFrequency,
                AccountTypesId = dto.AccountTypeId,
                Created = DateOnly.FromDateTime(DateTime.Now)
            };

            await _accountRepo.CreateAccountAsync(account);

            var disposition = new Disposition
            {
                CustomerId = customerId,
                AccountId = account.AccountId,
                Type = "OWNER"
            };

            await _dispositionRepo.CreateDispositionAsync(disposition);

            return new CreateAccountResponseDto
            {
                Message = "Account successfully created.",
                Account = accountType.TypeName,
                Balance = account.Balance,
                AccountNumber = account.AccountNumber,
                Created = account.Created
            };
        }
    }
}