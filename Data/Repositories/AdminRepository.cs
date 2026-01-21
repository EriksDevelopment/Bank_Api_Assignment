using Microsoft.EntityFrameworkCore;
using Bank.Data.Models;
using Bank.Data.Interfaces;

namespace Bank.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BankDbContext _context;

        public AdminRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByUserNameAsync(string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.User_Name == username);
        }
    }
}