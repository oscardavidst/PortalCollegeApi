using Application.Exceptions;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProfessorsCourses.Commands.CreateProfessorCourseCommand
{
    public class CreateProfessorCourseCommand : IRequest<Response<int>>
    {
        public int ProfessorId { get; set; }
        public int CourseId { get; set; }
    }

    public class CreateProfessorCourseCommandHandler : IRequestHandler<CreateProfessorCourseCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ProfessorCourse> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateProfessorCourseCommandHandler(IRepositoryAsync<ProfessorCourse> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProfessorCourseCommand request, CancellationToken cancellationToken)
        {
            var recordEqual = await _repositoryAsync.ListAsync(new ProfessorsCoursesSpecification(request.ProfessorId, request.CourseId));
            if (recordEqual.Count > 0)
                throw new ApiException($"El profesor ya dicta esta materia.");

            var records = await _repositoryAsync.ListAsync(new ProfessorsCoursesSpecification(request.ProfessorId, null));
            if (records.Count >= 2)
                throw new ApiException($"El profesor solo puede dictar 2 materias.");

            var newRecord = _mapper.Map<ProfessorCourse>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
