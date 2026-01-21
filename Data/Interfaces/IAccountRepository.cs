using Bank.Data.Models;

namespace Bank.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<AccountType?> GetAccountTypeAsync(int? accountTypeId);
        Task<Account?> GetAccountByAccountNumberAsync(string accountNumber);
        Task<List<Account>> GetAccountByCustomerIdAsync(int customerId);
        Task<Account?> GetAccountFrequencyAsync(string? accountFrequency);
    }
}