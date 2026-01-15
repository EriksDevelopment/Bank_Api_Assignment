using Microsoft.EntityFrameworkCore;
using Bank.Data.Entities;
using Bank.Data.Interfaces;

namespace Bank.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankDbContext _context;

        public CustomerRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Emailaddress == email);
        }

        public async Task<bool> CustomerOwnsAccountAsync(int customerId, int accountId)
        {
            return await _context.Dispositions
                .AnyAsync(d =>
                    d.CustomerId == customerId &&
                    d.AccountId == accountId &&
                    d.Type == "OWNER");
        }
    }
}