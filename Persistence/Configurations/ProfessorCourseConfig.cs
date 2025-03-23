using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProfessorCourseConfig : IEntityTypeConfiguration<ProfessorCourse>
    {
        public void Configure(EntityTypeBuilder<ProfessorCourse> builder)
        {
            builder.ToTable("Professors_Courses");
            builder.HasKey(professorCourse => professorCourse.Id);

            // Foreign key for Professor
            builder.HasOne(professorCourse => professorCourse.Professor)
                .WithMany(professor => professor.ProfessorsCourses)
                .HasForeignKey(professorCourse => professorCourse.ProfessorId)
                .IsRequired()
                .HasConstraintName("FK_ProfessorsCourses_Professors");

            // Foreign key for Course
            builder.HasOne(professorCourse => professorCourse.Course)
                .WithMany(course => course.ProfessorsCourses)
                .HasForeignKey(professorCourse => professorCourse.CourseId)
                .IsRequired()
                .HasConstraintName("FK_ProfessorsCourses_Courses");

            builder.Property(professorCourse => professorCourse.CreatedBy)
                .HasMaxLength(30);

            builder.Property(professorCourse => professorCourse.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
