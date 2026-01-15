using Bank.Data.Entities;
using Bank.Data.Interfaces;

namespace Bank.Data.Repositories
{
    public class DispositionRepository : IDispositionRepository
    {
        private readonly BankDbContext _context;

        public DispositionRepository(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Disposition> CreateDispositionAsync(Disposition disposition)
        {
            _context.Dispositions.Add(disposition);
            await _context.SaveChangesAsync();
            return disposition;
        }
    }
}