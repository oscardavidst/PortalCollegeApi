using Domain.Common;

namespace Domain.Entities
{
    public class Enrollment : AuditableBaseEntity
    {
        // Foreign key for Course
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Foreign key for Student
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
