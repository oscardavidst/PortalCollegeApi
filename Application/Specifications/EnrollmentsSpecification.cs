using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class EnrollmentsSpecification : Specification<Enrollment>
    {
        public EnrollmentsSpecification(int? courseId, int? studentId)
        {
            if (courseId != null)
                Query.Where(s => s.CourseId.Equals(courseId));

            if (studentId != null)
                Query.Where(s => s.StudentId.Equals(studentId));
        }
    }
}
