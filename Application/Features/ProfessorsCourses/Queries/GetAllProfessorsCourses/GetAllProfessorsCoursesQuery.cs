using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProfessorsCourses.Queries.GetAllProfessorsCourses
{
    public class GetAllProfessorsCoursesQuery : IRequest<Response<List<ProfessorCourseDto>>>
    {
        public int? ProfessorId { get; set; }
        public int? CourseId { get; set; }

        public class GetAllProfessorsCoursesQueryHandler : IRequestHandler<GetAllProfessorsCoursesQuery, Response<List<ProfessorCourseDto>>>
        {
            private readonly IRepositoryAsync<ProfessorCourse> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllProfessorsCoursesQueryHandler(IRepositoryAsync<ProfessorCourse> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<ProfessorCourseDto>>> Handle(GetAllProfessorsCoursesQuery request, CancellationToken cancellationToken)
            {
                var records = await _repositoryAsync.ListAsync(new ProfessorsCoursesSpecification(request.ProfessorId, request.CourseId));
                var recordsDto = _mapper.Map<List<ProfessorCourseDto>>(records);
                return new Response<List<ProfessorCourseDto>>(recordsDto);
            }
        }
    }
}
