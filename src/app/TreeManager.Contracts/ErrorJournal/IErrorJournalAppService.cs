using System.Collections.Generic;
using System.Threading.Tasks;

namespace TreeManager.Contracts.ErrorJournal
{
    public interface IErrorJournalAppService
    {
        public Task<int> CreateErrorEntry(ErrorEntryDto errorEntry);
        public Task<ErrorEntryDto?> GetErrorEntryById(int entryId);
        public Task<IReadOnlyList<ErrorEntryDto>> GetErrorEntriesRange(int skip, int take, string searchPattern);
    }
}
