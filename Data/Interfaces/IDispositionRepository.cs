using Bank.Data.Models;

namespace Bank.Data.Interfaces
{
    public interface IDispositionRepository
    {
        Task<Disposition> CreateDispositionAsync(Disposition disposition);
    }
}