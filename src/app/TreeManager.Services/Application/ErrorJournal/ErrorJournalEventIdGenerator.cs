using System;
using TreeManager.Contracts.ErrorJournal;

namespace TreeManager.Services.Application.ErrorJournal
{
    internal class ErrorJournalEventIdGenerator : IErrorJournalEventIdGenerator
    {
        public long GenerateEventId() => new Random().NextInt64(); // not sure how else it should be generated
    }
}
