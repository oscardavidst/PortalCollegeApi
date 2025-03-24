using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Professors.Commands.CreateProfessorCommand
{
    public class CreateProfessorCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public class CreateProfessorCommandHandler : IRequestHandler<CreateProfessorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Professor> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateProfessorCommandHandler(IRepositoryAsync<Professor> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<Professor>(request);
            var data = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(data.Id);
        }
    }
}
