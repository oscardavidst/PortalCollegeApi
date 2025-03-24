using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Professors.Queries.GetProfessorById
{
    public class GetProfessorByIdQuery : IRequest<Response<ProfessorDto>>
    {
        public int Id { get; set; }

        public class GetAllProfessorsQueryHandler : IRequestHandler<GetProfessorByIdQuery, Response<ProfessorDto>>
        {
            private readonly IRepositoryAsync<Professor> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllProfessorsQueryHandler(IRepositoryAsync<Professor> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<ProfessorDto>> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositoryAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    var dto = _mapper.Map<ProfessorDto>(record);
                    return new Response<ProfessorDto>(dto);
                }
            }
        }
    }
}
