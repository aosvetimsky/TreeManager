using System;

namespace TreeManager.Contracts.ErrorJournal
{
    public class ErrorEntryDto
    {
        public int Id { get; set; }
        public long EventId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string ErrorType { get; set; } = default!;
        public string RequestDetails { get; set; } = default!;
        public string StackTrace { get; set; } = default!;
    }
}
