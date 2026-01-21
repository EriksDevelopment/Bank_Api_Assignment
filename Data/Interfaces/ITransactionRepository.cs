using Bank.Data.Models;

namespace Bank.Data.Interfaces
{
    public interface ITransactionRepository
    {

        Task<List<Transaction>> GetTransactionByAccountIdAsync(int accountId);
        Task SaveTransactionAsync(Account fromAccount, Account toAccount, Transaction fromTransaction, Transaction toTransaction);
    }
}