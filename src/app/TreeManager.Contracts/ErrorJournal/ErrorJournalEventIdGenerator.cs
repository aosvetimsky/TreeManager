namespace TreeManager.Contracts.ErrorJournal
{
    public interface IErrorJournalEventIdGenerator
    {
        long GenerateEventId();
    }
}
