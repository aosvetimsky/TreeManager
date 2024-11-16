using System.Threading.Tasks;
using TreeManager.Contracts.Common;

namespace TreeManager.Contracts.Tree
{
    public interface ITreeAppService
    {
        Task<OperationResult<string>> CreateTree(TreeDto tree);
        Task<TreeDto?> FindTree(string treeName);
        Task<OperationResult> CreateTreeNode(string treeName, int parentTreeNodeId, string name);
        Task<OperationResult> UpdateTreeNode(string treeName, int treeNodeId, string name);
        Task<OperationResult> DeleteTreeNode(string treeName, int treeNodeId);
    }
}
