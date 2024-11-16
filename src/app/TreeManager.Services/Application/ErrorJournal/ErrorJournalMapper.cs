using TreeManager.Contracts.ErrorJournal;
using TreeManager.Domain.ErrorJournals;

namespace TreeManager.Services.Application.ErrorJournal
{
    public class ErrorJournalMapper
    {
        public ErrorEntryDto ToDto(ErrorEntry entry) => new ErrorEntryDto
        {
            Id = entry.Id,
            RequestDetails = entry.RequestDetails,
            ErrorType = entry.ErrorType,
            StackTrace = entry.StackTrace,
            CreatedAt = entry.CreatedAt,
            EventId = entry.EventId
        };
    }
}
