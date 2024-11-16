using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeManager.Domain.Trees;
using TreeManager.Persistence.Common;

namespace TreeManager.Persistence.Repositories
{
    internal class TreeRepository : EfRepository<AppDbContext, Tree, int>, ITreeRepository
    {
        private readonly AppDbContext _db;
        private readonly ITreeNodeRepository _treeNodeRepository;
        public TreeRepository(AppDbContext db, ITreeNodeRepository treeNodeRepository) : base(db)
        {
            _db = db;
            _treeNodeRepository = treeNodeRepository;
        }

        public async Task<Tree?> GetTreeByName(string treeName)
        {
            var tree = await Context.Trees.FirstOrDefaultAsync(t => t.Name == treeName);
            if (tree == null)
            {
                return null;
            }

            var treeNodes = await Context.TreeNodes.Where(tn => tn.TreeId == tree.Id).ToListAsync();
            var root = treeNodes.FirstOrDefault(tn => !tn.ParentId.HasValue);
            tree.SetRootNode(root);

            return tree == null ? null : tree;
        }

        public async Task<int> SaveTree(Tree tree)
        {
            if (tree.Id > 0)
            {
                Context.Trees.Attach(tree);
                var flatten = new List<TreeNode>(tree.FlattenNodes());

                var treeNodes = await Context.TreeNodes.Where(tn => tn.TreeId == tree.Id).ToListAsync();

                var nodesToUpdate = (from fn in flatten
                                     join tn in treeNodes on fn.Id equals tn.Id
                                     select new { Existing = tn, New = fn}).ToArray();

                var nodesToAdd = flatten.Where(n => !treeNodes.Any(tn => tn.Id == n.Id)).ToArray();

                var nodesToDelete = treeNodes.Where(n => !flatten.Any(tn => tn.Id == n.Id)).ToArray();

                foreach (var node in nodesToUpdate)
                {
                    Context.Entry(node.Existing).CurrentValues.SetValues(node.New);
                }

                foreach (var node in nodesToAdd)
                {
                    await Context.AddAsync(node);
                }

                foreach (var node in nodesToDelete)
                {
                    Context.Remove(node);
                }

                return tree.Id;
            }
            else
            {
                var flatten = new List<TreeNode>(tree.FlattenNodes());

                await Context.TreeNodes.AddRangeAsync(flatten);

                tree = (await Context.Trees.AddAsync(tree)).Entity;
                return tree.Id;
            }
        }

        private void BuildTreeRecursive(TreeNode node, List<TreeNode> treeNodes)
        {
            var subNodes = treeNodes.Where(tn => tn.ParentId == node.Id);
            foreach (var subNode in subNodes)
            {
                node.AddChild(subNode.Name);
                BuildTreeRecursive(subNode, treeNodes);
            }
        }
    }
}
