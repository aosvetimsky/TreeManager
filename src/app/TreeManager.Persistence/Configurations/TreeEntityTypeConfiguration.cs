using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeManager.Domain.Trees;

namespace TreeManager.Persistence.Configurations;

class TreeEntityTypeConfiguration: IEntityTypeConfiguration<Tree>
{
    public void Configure(EntityTypeBuilder<Tree> treeConfiguration)
    {
        treeConfiguration.ToTable("Trees");

        treeConfiguration.HasKey(x => x.Id);

        treeConfiguration.HasIndex(x => x.Name).IsUnique();

        treeConfiguration.Property(x => x.Name).IsRequired().HasMaxLength(200);

        treeConfiguration.Ignore(x => x.Root);
    }
}
