using Bank.Data.Entities;
using Bank.Data.Interfaces;

namespace Bank.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly BankDbContext _context;

        public LoanRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task CreateLoanAsync(Account account, Loan loan, Transaction transaction)
        {
            _context.Accounts.Update(account);
            _context.Loans.Add(loan);
            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
        }
    }
}