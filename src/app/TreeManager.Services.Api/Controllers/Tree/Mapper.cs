using System.Collections.Generic;
using TreeManager.Contracts.Tree;

namespace TreeManager.Services.Api.Controllers.Tree
{
    public class TreeMapper
    {
        public Transport.Tree ToTreeTransport(TreeDto treeDto)
        {
            var tree = new Transport.Tree
            {
                Name = treeDto.Name,
                Root = ToTreeNodeRecursive(treeDto.Root)
            };
            return tree;
        }

        public TreeDto ToTreeDto(Transport.Tree tree)
        {
            var treeDto = new TreeDto
            {
                Name = tree.Name,
                Root = ToTreeNodeDtoRecursive(tree.Root)
            };

            return treeDto;
        }

        private TreeNodeDto ToTreeNodeDtoRecursive(Transport.TreeNode treeNode)
        {
            var node = new TreeNodeDto
            {
                Id = treeNode.Id,
                Name = treeNode.Name,
                Children = new List<TreeNodeDto>()
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

        private Transport.TreeNode ToTreeNodeRecursive(TreeNodeDto treeNodeDto)
        {
            var node = new Transport.TreeNode
            {
                Id = treeNodeDto.Id,
                Name = treeNodeDto.Name
            };

            if (treeNodeDto.Children != null)
            {
                var children = new List<Transport.TreeNode>();
                foreach (var child in treeNodeDto.Children)
                {
                    children.Add(ToTreeNodeRecursive(child));
                }

                node.Children = children.ToArray();
            }

            return node;
        }
    }
}
