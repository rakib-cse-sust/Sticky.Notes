using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sticky.Notes.Domain.Entities;

namespace Sticky.Notes.Persistence.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(e => e.NoteName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
