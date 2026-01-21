using Microsoft.EntityFrameworkCore;
using Bank.Data.Models;
using Bank.Data.Interfaces;

namespace Bank.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context)
        {
            _context = context;
        }

        private string GenerateAccountNumber(int accountId)
        {
            return
                accountId.ToString().PadLeft(4, '0') + "-" +
                (accountId + 1000).ToString().PadLeft(4, '0') + "-" +
                (accountId + 2000).ToString().PadLeft(4, '0');
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            account.AccountNumber = GenerateAccountNumber(account.AccountId);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<AccountType?> GetAccountTypeAsync(int? accountTypeId)
        {
            return await _context.AccountTypes
                .FirstOrDefaultAsync(a => a.AccountTypeId == accountTypeId);
        }

        public async Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<List<Account>> GetAccountByCustomerIdAsync(int customerId)
        {
            return await _context.Dispositions
                .Where(d => d.CustomerId == customerId && d.Type == "OWNER")
                .Include(d => d.Account)
                    .ThenInclude(a => a.AccountTypes)
                .Select(d => d.Account)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountFrequencyAsync(string? accountFrequency)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Frequency == accountFrequency);
        }
    }
}