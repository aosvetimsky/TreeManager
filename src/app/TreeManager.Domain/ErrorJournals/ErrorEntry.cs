using System;
using TreeManager.Domain.Common;

namespace TreeManager.Domain.ErrorJournals
{
    public class ErrorEntry : Entity<int>
    {
        public long EventId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public string RequestDetails { get; private set; }
        public string StackTrace { get; private set; }
        public string ErrorType { get; private set; }

        public ErrorEntry(long eventId, DateTimeOffset createdAt, string errorType, string requestDetails, string stackTrace)
        {
            EventId = eventId;
            CreatedAt = createdAt;
            RequestDetails = requestDetails ?? throw new ArgumentNullException(nameof(requestDetails));
            StackTrace = stackTrace ?? throw new ArgumentNullException(nameof(stackTrace));
            ErrorType = errorType ?? throw new ArgumentNullException(nameof(errorType));
        }
    }
}
