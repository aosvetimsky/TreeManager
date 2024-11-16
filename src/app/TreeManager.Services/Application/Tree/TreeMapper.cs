using TreeManager.Domain.Trees;
using TreeManager.Contracts.Tree;

namespace TreeManager.Services.Application.Tree
{
    internal class TreeMapper
    {
        public Domain.Trees.Tree ToTree(TreeDto treeDto)
        {
            var tree = new Domain.Trees.Tree(treeDto.Name);
            var root = ToTreeNodeRecursive(tree, treeDto.Root);
            tree.SetRootNode(root);
            return tree;
        }

        public TreeDto ToTreeDto(Domain.Trees.Tree tree)
        {
            var treeDto = new TreeDto
            {
                Name = tree.Name,
                Root = ToTreeNodeDtoRecursive(tree.Root)
            };

            return treeDto;
        }

        private TreeNode ToTreeNodeRecursive(Domain.Trees.Tree tree, TreeNodeDto treeNodeDto)
        {
            var node = new TreeNode(tree, treeNodeDto.Name);

            if (treeNodeDto.Children != null)
            {
                foreach (var child in treeNodeDto.Children)
                {
                    node.AddChild(ToTreeNodeRecursive(tree, child));
                }
            }

            return node;
        }

        private TreeNodeDto ToTreeNodeDtoRecursive(TreeNode treeNode)
        {
            var node = new TreeNodeDto
            {
                Name = treeNode.Name,
                Id = treeNode.Id,
                Children = new System.Collections.Generic.List<TreeNodeDto>()
            };

            if (treeNode.Children != null)
            {
                foreach (var child in treeNode.Children)
                {
                    node.Children.Add(ToTreeNodeDtoRecursive(child));
                }
            }

            return node;
        }
    }
}
