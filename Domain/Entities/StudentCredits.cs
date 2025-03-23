using Domain.Common;

namespace Domain.Entities
{
    public class StudentCredits : AuditableBaseEntity
    {
        public int CreditsCount { get; set; }

        // Foreign key for Student
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
