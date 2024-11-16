using System;
using System.ComponentModel.DataAnnotations;

namespace TreeManager.Services.Api.Controllers.ErrorJournal
{
    public record SearchErrorJournalRequest
    {
        public string Filter { get; init; } = string.Empty;
        [Required]
        public int Skip { get; init; }
        [Required]
        public int Take { get; init; }
    }

    public record SearchErrorJournalResponse
    {
        public ErrorEntry[] ErrorEntries { get; init; }
    }

    public record ErrorEntry
    {
        public long EventId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string RequestDetails { get; init; } = string.Empty;
        public string StackTrace { get; init; } = string.Empty;
    }
}
