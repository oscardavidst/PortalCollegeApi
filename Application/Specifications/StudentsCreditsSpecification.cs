using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class StudentsCreditsSpecification : Specification<StudentCredits>
    {
        public StudentsCreditsSpecification(int? studentId, int? creditsCount)
        {
            if (creditsCount != null)
                Query.Where(s => s.CreditsCount.Equals(Byte.Parse(creditsCount.ToString()!)));

            if (studentId != null)
                Query.Where(s => s.StudentId.Equals(studentId));
        }
    }
}
