using TreeManager.Contracts.ErrorJournal;

namespace TreeManager.Services.Api.Controllers.ErrorJournal
{
    public class ErrorJournalMapper
    {
        public ErrorEntry ToTransport(ErrorEntryDto e) => new ErrorEntry
        {
            CreatedAt = e.CreatedAt,
            EventId = e.EventId,
            RequestDetails = e.RequestDetails,
            StackTrace = e.StackTrace
        };
    }
}
