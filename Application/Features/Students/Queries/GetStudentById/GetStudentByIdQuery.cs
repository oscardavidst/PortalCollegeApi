using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<Response<StudentDto>>
    {
        public int Id { get; set; }

        public class GetAllStudentsQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<StudentDto>>
        {
            private readonly IRepositoryAsync<Student> _repositorySAsync;
            private readonly IRepositoryAsync<StudentCredits> _repositorySCAsync;
            private readonly IRepositoryAsync<Enrollment> _repositoryEAsync;
            private readonly IRepositoryAsync<Course> _repositoryCAsync;
            private readonly IMapper _mapper;

            public GetAllStudentsQueryHandler(
                IRepositoryAsync<Student> repositorySAsync, 
                IRepositoryAsync<StudentCredits> repositorySCAsync, 
                IRepositoryAsync<Enrollment> repositoryEAsync, 
                IRepositoryAsync<Course> repositoryCAsync, 
                IMapper mapper)
            {
                _repositorySAsync = repositorySAsync;
                _repositorySCAsync = repositorySCAsync;
                _repositoryEAsync = repositoryEAsync;
                _repositoryCAsync = repositoryCAsync;
                _mapper = mapper;
            }

            public async Task<Response<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositorySAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    record.StudentCredits = await _repositorySCAsync.GetByIdAsync(record.Id);
                    record.Enrollments = await _repositoryEAsync.ListAsync(new EnrollmentsSpecification(null, record.Id));

                    var dto = _mapper.Map<StudentDto>(record);
                    dto.CreditsCount = record.StudentCredits.CreditsCount;
                    foreach (var enrollment in record.Enrollments)
                    {
                        var course = await _repositoryCAsync.GetByIdAsync(enrollment.CourseId);
                        dto.CoursesEnrollments.Add(_mapper.Map<CourseDto>(course));
                    }
                    return new Response<StudentDto>(dto);
                }
            }
        }
    }
}
