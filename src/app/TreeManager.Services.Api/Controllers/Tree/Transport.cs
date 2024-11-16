using System.ComponentModel.DataAnnotations;

namespace TreeManager.Services.Api.Controllers.Tree.Transport
{
    public record Tree
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; } = string.Empty;
        [Required]
        public required TreeNode Root { get; set; }
    }

    public record TreeNode
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public TreeNode[] Children { get; set; } = { };
    }

    public record CreateTreeRequest
    {
        [Required]
        public required Tree Tree { get; set; }
    }

    public record CreateTreeResponse
    {
    }

    public record CreateTreeNodeRequest
    {
        public int ParentTreeNodeId { get; set; }
        [Required]
        public required string TreeNodeName { get; set; }
    }

    public record CreateTreeNodeResponse
    {
    }

    public record DeleteTreeNodeRequest
    {
        public int TreeNodeId { get; set; }
    }

    public record DeleteTreeNodeResponse
    {
    }

    public record RenameTreeNodeRequest
    {
        public int TreeNodeId { get; set; }
        [Required]
        public required string TreeNodeName { get; set; }
    }

    public record RenameTreeNodeResponse
    {
    }
}
