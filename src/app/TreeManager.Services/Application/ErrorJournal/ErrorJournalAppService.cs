using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreeManager.Contracts.ErrorJournal;
using TreeManager.Domain.ErrorJournals;
using TreeManager.Domain.Trees;

namespace TreeManager.Services.Application.ErrorJournal
{
    internal class ErrorJournalAppService : IErrorJournalAppService
    {
        private readonly IErrorEntryRepository _errorEntryRepository;
        private readonly IErrorEntryReadOnlyRepository _errorEntryReadOnlyRepository;
        private readonly IErrorJournalEventIdGenerator _errorJournalEventIdGenerator;
        private readonly ErrorJournalMapper _errorJournalMapper;
        public ErrorJournalAppService(IErrorEntryRepository errorEntryRepository, IErrorEntryReadOnlyRepository errorEntryReadOnlyRepository, 
            IErrorJournalEventIdGenerator errorJournalEventIdGenerator, ErrorJournalMapper errorJournalMapper)
        {
            _errorEntryRepository = errorEntryRepository;
            _errorEntryReadOnlyRepository = errorEntryReadOnlyRepository;
            _errorJournalEventIdGenerator = errorJournalEventIdGenerator;
            _errorJournalMapper = errorJournalMapper;
        }
        public async Task<int> CreateErrorEntry(ErrorEntryDto errorEntry)
        {
            var eventId = _errorJournalEventIdGenerator.GenerateEventId();
            var entry = new ErrorEntry(eventId, errorEntry.CreatedAt, errorEntry.ErrorType, errorEntry.RequestDetails, errorEntry.StackTrace);
            await _errorEntryRepository.AddAsync(entry);
            await _errorEntryRepository.UnitOfWork.SaveEntitiesAsync();
            return entry.Id;
        }
        public async Task<ErrorEntryDto?> GetErrorEntryById(int entryId)
        {
            var entry = await _errorEntryRepository.FindAsync(entryId);

            if (entry == null)
            {
                return null;
            }

            return _errorJournalMapper.ToDto(entry);
        }
        public async Task<IReadOnlyList<ErrorEntryDto>> GetErrorEntriesRange(int skip, int take, string searchPattern)
        {
            var query = _errorEntryReadOnlyRepository.GetQueryable();

            if (!string.IsNullOrEmpty(searchPattern))
            {
                query = query.Where(e => e.EventId.ToString() == searchPattern || e.RequestDetails == searchPattern || e.StackTrace == searchPattern);
            }

            query = query.Skip(skip).Take(take);

            var entries = await query.ToListAsync();

            return entries.Select(_errorJournalMapper.ToDto).ToImmutableList();
        }
    };
}
