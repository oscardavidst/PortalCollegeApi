using Domain.Common;

namespace Domain.Entities
{
    public class ProfessorCourse : AuditableBaseEntity
    {
        // Foreign key for Professor
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        // Foreign key for Course
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
