using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(s => s.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(s => s.CreatedBy)
                .HasMaxLength(30);

            builder.Property(s => s.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
