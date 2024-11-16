using TreeManager.Domain.Common;
using TreeManager.Domain.ErrorJournals;

namespace TreeManager.Domain.Trees
{
    public interface IErrorEntryReadOnlyRepository : IReadOnlyRepository<ErrorEntry, int>
    {
    }
}
