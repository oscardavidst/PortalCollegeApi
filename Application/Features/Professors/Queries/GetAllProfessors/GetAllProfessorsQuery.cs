using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Professors.Queries.GetAllProfessors
{
    public class GetAllProfessorsQuery : IRequest<Response<List<ProfessorDto>>>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public class GetAllProfessorsQueryHandler : IRequestHandler<GetAllProfessorsQuery, Response<List<ProfessorDto>>>
        {
            private readonly IRepositoryAsync<Professor> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllProfessorsQueryHandler(IRepositoryAsync<Professor> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<ProfessorDto>>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
            {
                var records = await _repositoryAsync.ListAsync(new ProfessorsSpecification(request.Name, request.LastName));
                var recordsDto = _mapper.Map<List<ProfessorDto>>(records);
                return new Response<List<ProfessorDto>>(recordsDto);
            }
        }
    }
}
