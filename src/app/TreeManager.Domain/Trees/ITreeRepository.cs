using System.Threading.Tasks;
using TreeManager.Domain.Common;

namespace TreeManager.Domain.Trees
{
    public interface ITreeRepository : IRepository<Tree, int>
    {
        Task<Tree?> GetTreeByName(string treeName);
        Task<int> SaveTree(Tree tree);
    }
}
