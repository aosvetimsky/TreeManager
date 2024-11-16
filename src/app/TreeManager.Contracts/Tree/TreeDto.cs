using System.Collections.Generic;

namespace TreeManager.Contracts.Tree
{
    public class TreeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required TreeNodeDto Root { get; set; }
    }

    public class TreeNodeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<TreeNodeDto> Children { get; set; }
    }
}
