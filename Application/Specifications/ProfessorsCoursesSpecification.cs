using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ProfessorsCoursesSpecification : Specification<ProfessorCourse>
    {
        public ProfessorsCoursesSpecification(int? professorId, int? courseId)
        {
            if (professorId != null)
                Query.Where(s => s.ProfessorId.Equals(professorId));

            if (courseId != null)
                Query.Where(s => s.CourseId.Equals(courseId));
        }
    }
}
