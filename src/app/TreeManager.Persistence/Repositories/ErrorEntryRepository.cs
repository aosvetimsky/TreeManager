using System.Threading.Tasks;
using TreeManager.Domain.ErrorJournals;
using TreeManager.Persistence;
using TreeManager.Persistence.Common;
using TreeManager.Persistence.Exceptions;

namespace TreeManager.Domain.Trees
{
    internal class ErrorEntryRepository : EfRepository<AppDbContext, ErrorEntry, int>, IErrorEntryRepository, IErrorEntryReadOnlyRepository
    {
        private readonly AppDbContext _db;
        public ErrorEntryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task DeleteAsync(int id)
        {
            var entry = await _db.FindAsync<ErrorEntry>(id) ?? throw new EntityNotFoundException<ErrorEntry>(id);
            _db.Remove(entry);
        }
        public async Task<ErrorEntry?> FindAsync(int id) => await _db.FindAsync<ErrorEntry>(id);
        public async Task<ErrorEntry> AddAsync(ErrorEntry entity) => (await _db.AddAsync(entity)).Entity;
        public async Task<ErrorEntry> UpdateAsync(ErrorEntry entity)
        {
            _db.Attach(entity);
            return entity;
        }
    }
}
