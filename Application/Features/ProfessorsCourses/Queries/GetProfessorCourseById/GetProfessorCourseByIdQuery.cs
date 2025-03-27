using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProfessorsCourses.Queries.GetProfessorCourseById
{
    public class GetProfessorCourseByIdQuery : IRequest<Response<ProfessorCourseDto>>
    {
        public int Id { get; set; }

        public class GetAllProfessorsCoursesQueryHandler : IRequestHandler<GetProfessorCourseByIdQuery, Response<ProfessorCourseDto>>
        {
            private readonly IRepositoryAsync<ProfessorCourse> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllProfessorsCoursesQueryHandler(IRepositoryAsync<ProfessorCourse> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<ProfessorCourseDto>> Handle(GetProfessorCourseByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositoryAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    var dto = _mapper.Map<ProfessorCourseDto>(record);
                    return new Response<ProfessorCourseDto>(dto);
                }
            }
        }
    }
}
