using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");
            builder.HasKey(e => e.Id);

            // Foreign key for Course
            builder.HasOne(e => e.Course)
                .WithMany(p => p.Enrollments)
                .HasForeignKey(t => t.CourseId)
                .IsRequired()
                .HasConstraintName("FK_Enrollments_Courses");

            // Foreign key for Student
            builder.HasOne(pc => pc.Student)
                .WithMany(p => p.Enrollments)
                .HasForeignKey(t => t.StudentId)
                .IsRequired()
                .HasConstraintName("FK_Enrollments_Students");

            builder.Property(t => t.CreatedBy)
                .HasMaxLength(30);

            builder.Property(t => t.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
