using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.CreateStudentCommand
{
    public class CreateStudentCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Student> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateStudentCommandHandler(IRepositoryAsync<Student> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Student>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
