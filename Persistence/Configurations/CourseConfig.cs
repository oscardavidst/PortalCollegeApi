using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Credits)
                .IsRequired();

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(30);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
