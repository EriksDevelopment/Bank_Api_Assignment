using Bank.Data.Entities;

namespace Bank.Data.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByUserNameAsync(string username);
    }
}