using System;
using System.Collections.Generic;
using System.Linq;
using TreeManager.Domain.Common;

namespace TreeManager.Domain.Trees
{
    public class Tree : Entity<int>
    {
        private IReadOnlyCollection<TreeNode> _flattenNodes;

        public TreeNode Root { get; private set; }
        public string Name { get; private set; }

        protected Tree() { }
        public Tree(string name) : this()
        {
            Name = name ?? throw new ArgumentNullException();
        }

        public IReadOnlyCollection<TreeNode> FlattenNodes()
        {
            var list = new List<TreeNode>();
            list.Add(Root);
            ToFlatten(Root, list);
            return list;
        }
        public void SetRootNode(TreeNode node)
        {
            Root = node ?? throw new ArgumentNullException(nameof(node));

            if (FlattenNodes().GroupBy(n => n.Name).Count() > 1)
            {
                throw new ArgumentException("Root contains nodes with the same names");
            }
        }

        public TreeNode? FindNode(int nodeId) => FlattenNodes().FirstOrDefault(n => n.Id == nodeId);

        public void RenameNode(int nodeId, string name)
        {
            var node = FindNode(nodeId);
            if (node == null)
            {
                throw new InvalidOperationException($"Node {nodeId} is not found");
            }

            node.UpdateName(name ?? throw new ArgumentNullException());
        }

        private void ToFlatten(TreeNode treeNode, List<TreeNode> list)
        {
            if (treeNode.Children != null)
            {
                foreach (var child in treeNode.Children)
                {
                    list.Add(child);
                    ToFlatten(child, list);
                }
            }
        }
    }
}
