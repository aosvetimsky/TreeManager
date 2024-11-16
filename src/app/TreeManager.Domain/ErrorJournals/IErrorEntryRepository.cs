using TreeManager.Domain.Common;
using TreeManager.Domain.ErrorJournals;

namespace TreeManager.Domain.Trees
{
    public interface IErrorEntryRepository : IRepository<ErrorEntry, int>
    {
    }
}
