using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Courses.Commands.CreateCourseCommand
{
    public class CreateCourseCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public int Credits { get; set; }
    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(IRepositoryAsync<Course> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Course>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
