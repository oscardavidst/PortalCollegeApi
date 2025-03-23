using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class StudentsSpecification : Specification<Student>
    {
        public StudentsSpecification(string fullName)
        {
            if (!string.IsNullOrEmpty(fullName))
                Query
                    .Search(s => s.Name, "%" + fullName + "%")
                    .Search(s => s.LastName, "%" + fullName + "%");
        }
    }
}
