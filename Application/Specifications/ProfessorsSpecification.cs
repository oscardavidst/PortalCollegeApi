using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class ProfessorsSpecification : Specification<Professor>
    {
        public ProfessorsSpecification(string fullName)
        {
            if (!string.IsNullOrEmpty(fullName))
                Query
                    .Search(t => t.Name, "%" + fullName + "%")
                    .Search(p => p.LastName, "%" + fullName + "%");
        }
    }
}
