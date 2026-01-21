using Bank.Data.Models;

namespace Bank.Data.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByUserNameAsync(string username);
    }
}