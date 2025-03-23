using Domain.Common;

namespace Domain.Entities
{
    public class Course : AuditableBaseEntity
    {
        public required string Name { get; set; }
        public Byte Credits { get; set; }

        // Navigations
        public ICollection<ProfessorCourse> ProfessorsCourses { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
