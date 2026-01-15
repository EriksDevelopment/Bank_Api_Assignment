using Bank.Data.Entities;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> GetByEmailAsync(string email);
        Task<bool> CustomerOwnsAccountAsync(int customerId, int accountId);
    }
}