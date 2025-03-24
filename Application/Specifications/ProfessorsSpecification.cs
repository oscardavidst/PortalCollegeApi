using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ProfessorsSpecification : Specification<Professor>
    {
        public ProfessorsSpecification(string? name, string? lastName)
        {
            if (!string.IsNullOrEmpty(name))
                Query.Search(s => s.Name, "%" + name + "%");

            if (!string.IsNullOrEmpty(lastName))
                Query.Search(s => s.LastName, "%" + lastName + "%");
        }
    }
}
