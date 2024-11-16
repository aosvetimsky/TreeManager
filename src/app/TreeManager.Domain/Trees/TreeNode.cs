using System.Collections.Generic;
using System.Linq;
using TreeManager.Domain.Common;

namespace TreeManager.Domain.Trees
{
    public class TreeNode : Entity<int>
    {
        private readonly List<TreeNode> _children = new();

        public virtual Tree Tree { get; private set; }
        public int TreeId { get; private set; }
        public string Name { get; private set; }
        public int? ParentId { get; private set; }
        public virtual TreeNode Parent { get; private set; }
        public virtual IReadOnlyCollection<TreeNode> Children => _children.AsReadOnly();

        public TreeNode AddChild(string name)
        {
            var child = new TreeNode(Tree, name) { Parent = this };
            _children.Add(child);
            return child;
        }

        public TreeNode AddChild(TreeNode node)
        {
            node.Parent = this;
            _children.Add(node);
            return node;
        }
        public void RemoveChild(string name)
        {
            var node = _children.FirstOrDefault(c => c.Name == name);
            _children.Remove(node);
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        protected TreeNode()
        {
        }

        public TreeNode(Tree tree, string name)
        {
            Tree = tree;
            Name = name;
        }
    }
}
