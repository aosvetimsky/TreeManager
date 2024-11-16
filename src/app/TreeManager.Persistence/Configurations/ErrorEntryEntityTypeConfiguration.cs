using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreeManager.Domain.ErrorJournals;

namespace TreeManager.Persistence.Configurations;

class ErrorEntryEntityTypeConfiguration : IEntityTypeConfiguration<ErrorEntry>
{
    public void Configure(EntityTypeBuilder<ErrorEntry> errorEntryConfiguration)
    {
        errorEntryConfiguration.ToTable("ErrorEntries");

        errorEntryConfiguration.HasKey(x => x.Id);

        errorEntryConfiguration.Property(x => x.EventId).IsRequired();
        errorEntryConfiguration.Property(x => x.CreatedAt).IsRequired();
        errorEntryConfiguration.Property(x => x.StackTrace).IsRequired();
        errorEntryConfiguration.Property(x => x.RequestDetails).IsRequired();
        errorEntryConfiguration.Property(x => x.ErrorType).IsRequired();
    }
}
