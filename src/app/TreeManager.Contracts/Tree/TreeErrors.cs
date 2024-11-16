using TreeManager.Contracts.Common;

namespace TreeManager.Contracts.Tree
{
    public static class TreeErrors
    {
        public static readonly OperationError TreeNotFoundError = new("Tree is not found");
        public static readonly OperationError TreeNameAlreadyExistsError = new("Tree name already exists");
        public static readonly OperationError TreeNodeNotFoundError = new("Tree node is not found");
        public static readonly OperationError TreeNodeNameAlreadyExistsForTreeError = new("Tree node already exists in tree");
    }
}
