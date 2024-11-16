using TreeManager.Domain.Trees;
using TreeManager.Persistence.Common;

namespace TreeManager.Persistence.Repositories
{
    internal class TreeNodeRepository : EfRepository<AppDbContext, TreeNode, int>, ITreeNodeRepository
    {
        private readonly AppDbContext _db;
        public TreeNodeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
