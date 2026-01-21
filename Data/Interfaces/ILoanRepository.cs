using Bank.Data.Models;

namespace Bank.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task CreateLoanAsync(Account account, Loan loan, Transaction transaction);
    }
}