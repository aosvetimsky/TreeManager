using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeManager.Domain.Trees;

namespace TreeManager.Persistence.Configurations;

class TreeNodeEntityTypeConfiguration : IEntityTypeConfiguration<TreeNode>
{
    public void Configure(EntityTypeBuilder<TreeNode> treeNodeConfiguration)
    {
        treeNodeConfiguration.ToTable("TreeNodes");

        treeNodeConfiguration.HasKey(x => x.Id);

        treeNodeConfiguration.Property(x => x.Name).IsRequired().HasMaxLength(200);

        //treeNodeConfiguration.HasOne(x => x.Tree);

        treeNodeConfiguration.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);

        //treeNodeConfiguration.HasMany(x => x.Children).WithOne().HasForeignKey("ParentId");
    }
}
