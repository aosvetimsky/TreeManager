using System;

namespace TreeManager.Domain.Tests.Tree
{
    public class TreeDomainTests
    {
        [Fact]
        public void it_creates_tree_domain()
        {
            var tree = new Trees.Tree("tree");
            tree.SetRootNode(new Trees.TreeNode(tree, "root"));
            Assert.NotNull(tree.Root);
        }

        [Fact]
        public void it_receives_exception_when_creating_tree_with_same_nodes()
        {
            var tree = new Trees.Tree("tree");
            var root = new Trees.TreeNode(tree, "root");
            root.AddChild("chield1");
            root.AddChild("chield2");
            Assert.Throws<ArgumentException>(() => tree.SetRootNode(root));
        }
    }
}