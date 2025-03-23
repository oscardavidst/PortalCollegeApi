using Domain.Common;

namespace Domain.Entities
{
    public class Professor : AuditableBaseEntity
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }

        // Navigations
        public ICollection<ProfessorCourse> ProfessorsCourses { get; set; }
    }
}
