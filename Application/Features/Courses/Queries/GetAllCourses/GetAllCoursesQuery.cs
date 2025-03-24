using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesQuery : IRequest<Response<List<CourseDto>>>
    {
        public string? Name { get; set; }
        public int? Credits { get; set; }

        public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Response<List<CourseDto>>>
        {
            private readonly IRepositoryAsync<Course> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCoursesQueryHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
            {
                var records = await _repositoryAsync.ListAsync(new CoursesSpecification(request.Name, request.Credits));
                var recordsDto = _mapper.Map<List<CourseDto>>(records);
                return new Response<List<CourseDto>>(recordsDto);
            }
        }
    }
}
