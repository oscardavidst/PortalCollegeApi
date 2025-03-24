using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<Response<CourseDto>>
    {
        public int Id { get; set; }

        public class GetAllCoursesQueryHandler : IRequestHandler<GetCourseByIdQuery, Response<CourseDto>>
        {
            private readonly IRepositoryAsync<Course> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllCoursesQueryHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositoryAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    var dto = _mapper.Map<CourseDto>(record);
                    return new Response<CourseDto>(dto);
                }
            }
        }
    }
}
