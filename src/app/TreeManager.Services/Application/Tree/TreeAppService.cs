using System.Linq;
using System.Threading.Tasks;
using TreeManager.Contracts.Common;
using TreeManager.Contracts.Tree;
using TreeManager.Domain.Trees;

namespace TreeManager.Services.Application.Tree
{
    internal class TreeAppService : ITreeAppService
    {
        private readonly ITreeRepository _treeRepository;
        private readonly ITreeNodeRepository _treeNodeRepository;
        private readonly TreeMapper _treeMapper;

        public TreeAppService(ITreeRepository treeRepository, ITreeNodeRepository treeNodeRepository, TreeMapper treeMapper)
        {
            _treeRepository = treeRepository;
            _treeNodeRepository = treeNodeRepository;
            _treeMapper = treeMapper;
        }

        public async Task<OperationResult<int>> CreateTree(TreeDto treeDto)
        {
            var existingTree = await _treeRepository.GetTreeByName(treeDto.Name);

            if (existingTree != null)
            {
                return OperationResult<int>.Failed(TreeErrors.TreeNameAlreadyExistsError);
            }

            var tree = _treeMapper.ToTree(treeDto);

            await _treeRepository.SaveTree(tree);

            await _treeRepository.UnitOfWork.SaveEntitiesAsync();

            return OperationResult<int>.Succeeded(tree.Id);
        }

        public async Task<TreeDto?> FindTree(string treeName)
        {
            var tree = await _treeRepository.GetTreeByName(treeName);
            return tree == null ? null : _treeMapper.ToTreeDto(tree);
        }

        public async Task<OperationResult> CreateTreeNode(string treeName, int parentTreeNodeId, string name)
        {
            var tree = await _treeRepository.GetTreeByName(treeName);

            if (tree == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNotFoundError);
            }

            var treeNode = tree.FindNode(parentTreeNodeId);

            if (treeNode == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNodeNotFoundError);
            }

            if (tree.FlattenNodes().Any(n => n.Name == name))
            {
                return OperationResult.Failed(TreeErrors.TreeNodeNameAlreadyExistsForTreeError);
            }

            treeNode.AddChild(name);

            await _treeRepository.SaveTree(tree);

            await _treeRepository.UnitOfWork.SaveEntitiesAsync();

            return OperationResult.Succeeded();
        }

        public async Task<OperationResult> DeleteTreeNode(string treeName, int treeNodeId)
        {
            var tree = await _treeRepository.GetTreeByName(treeName);

            if (tree == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNotFoundError);
            }

            var treeNode = tree.FindNode(treeNodeId);

            if (treeNode == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNodeNotFoundError);
            }

            treeNode.Parent.RemoveChild(treeNode.Name);

            await _treeRepository.SaveTree(tree);

            await _treeRepository.UnitOfWork.SaveEntitiesAsync();

            return OperationResult.Succeeded();
        }

        public async Task<OperationResult> UpdateTreeNode(string treeName, int treeNodeId, string name)
        {
            var tree = await _treeRepository.GetTreeByName(treeName);

            if (tree == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNotFoundError);
            }

            var treeNode = tree.FindNode(treeNodeId);

            if (treeNode == null)
            {
                return OperationResult.Failed(TreeErrors.TreeNodeNotFoundError);
            }

            if (tree.FlattenNodes().Any(n => n.Name == name && n.Id != treeNodeId))
            {
                return OperationResult.Failed(TreeErrors.TreeNodeNameAlreadyExistsForTreeError);
            }

            tree.RenameNode(treeNodeId, name);

            await _treeRepository.SaveTree(tree);

            await _treeRepository.UnitOfWork.SaveEntitiesAsync();

            return OperationResult.Succeeded();
        }
    }
}
