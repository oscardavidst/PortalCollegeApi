using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class CoursesSpecification : Specification<Course>
    {
        public CoursesSpecification(string? name, int? credits)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Search(s => s.Name, "%" + name + "%");

            if (credits != null)
                Query.Where(s => s.Credits.Equals(Byte.Parse(credits.ToString()!)));
        }
    }
}
