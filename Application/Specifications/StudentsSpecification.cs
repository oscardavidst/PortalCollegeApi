using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class StudentsSpecification : Specification<Student>
    {
        public StudentsSpecification(string? name, string? lastName, string? email)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Search(s => s.Name, "%" + name + "%");

            if (!string.IsNullOrEmpty(lastName))
                Query.Search(s => s.LastName, "%" + lastName + "%");

            if (!string.IsNullOrEmpty(email))
                Query.Search(s => s.Email, email);
        }
    }
}
