using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class StudentCreditsConfig : IEntityTypeConfiguration<StudentCredits>
    {
        public void Configure(EntityTypeBuilder<StudentCredits> builder)
        {
            builder.ToTable("Students_Credits");
            builder.HasKey(e => e.Id);

            // Foreign key for Student
            builder.HasOne(pc => pc.Student)
                .WithMany(p => p.StudentsCredits)
                .HasForeignKey(t => t.StudentId)
                .IsRequired()
                .HasConstraintName("FK_StudentCredits_Students");

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(30);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
