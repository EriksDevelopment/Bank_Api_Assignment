using Bank.Data.Entities;
using Bank.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankDbContext _context;
        public TransactionRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionByAccountIdAsync(int accountId)
        {

            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task SaveTransactionAsync(Account fromAccount, Account toAccount, Transaction fromTransaction, Transaction toTransaction)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Accounts.Update(fromAccount);
                _context.Accounts.Update(toAccount);

                _context.Transactions.AddRange(fromTransaction, toTransaction);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}