using Domain.Common;

namespace Domain.Entities
{
    public class Student : AuditableBaseEntity
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        // Navigations
        public ICollection<Enrollment> Enrollments { get; set; }
        public StudentCredits StudentCredits { get; set; }
    }
}
