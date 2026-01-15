using Bank.Data.Entities;

namespace Bank.Data.Interfaces
{
    public interface IDispositionRepository
    {
        Task<Disposition> CreateDispositionAsync(Disposition disposition);
    }
}