using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProfessorConfig : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professors");
            builder.HasKey(professor => professor.Id);

            builder.Property(professor => professor.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(professor => professor.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(professor => professor.CreatedBy)
                .HasMaxLength(30);

            builder.Property(professor => professor.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
