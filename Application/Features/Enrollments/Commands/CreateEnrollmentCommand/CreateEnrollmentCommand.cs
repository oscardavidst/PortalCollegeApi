using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Enrollments.Commands.CreateEnrollmentCommand
{
    public class CreateEnrollmentCommand : IRequest<Response<int>>
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Enrollment> _repositoryAsync;
        private readonly IRepositoryAsync<Student> _repositorySAsync;
        private readonly IRepositoryAsync<Course> _repositoryCAsync;
        private readonly IRepositoryAsync<StudentCredits> _repositorySCAsync;
        private readonly IMapper _mapper;

        public CreateEnrollmentCommandHandler(
            IRepositoryAsync<Enrollment> repositoryAsync, 
            IRepositoryAsync<Student> repositorySAsync, 
            IRepositoryAsync<Course> repositoryCAsync, 
            IRepositoryAsync<StudentCredits> repositorySCAsync, 
            IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _repositorySAsync = repositorySAsync;
            _repositoryCAsync = repositoryCAsync;
            _repositorySCAsync = repositorySCAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repositorySAsync.GetByIdAsync(request.StudentId);
            if (student == null)
                throw new ApiException($"No existe el estudiante con Id {request.StudentId}.");

            var course = await _repositoryCAsync.GetByIdAsync(request.CourseId);
            if (course == null)
                throw new ApiException($"No existe la materia con Id {request.CourseId}.");

            var recordEqual = await _repositoryAsync.ListAsync(new EnrollmentsSpecification(request.CourseId, request.StudentId));
            if (recordEqual.Count > 0)
                throw new ApiException($"Esta inscripción ya existe.");

            var enrollmetsStudent = await _repositoryAsync.ListAsync(new EnrollmentsSpecification(null, request.StudentId));
            if (enrollmetsStudent.Count >= 3)
                throw new ApiException($"El estudiante solo puede inscribir hasta 3 materias.");

            var studentCredits = await _repositorySCAsync.FirstOrDefaultAsync(new StudentsCreditsSpecification(request.StudentId, null));
            if (studentCredits == null) {
                var newStudetCredits = _mapper.Map<StudentCredits>(new StudentCreditsRequest(request.StudentId, course.Credits));
                await _repositorySCAsync.AddAsync(newStudetCredits);
            } else
            {
                studentCredits.CreditsCount += course.Credits;
                await _repositorySCAsync.UpdateAsync(studentCredits);
            }

            var newRecord = _mapper.Map<Enrollment>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
