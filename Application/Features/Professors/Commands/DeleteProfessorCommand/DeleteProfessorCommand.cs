using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Professors.Commands.DeleteProfessorCommand
{
    public class DeleteProfessorCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteProfessorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Professor> _repositoryAsync;

        public DeleteHotelCommandHandler(IRepositoryAsync<Professor> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteProfessorCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(record);
            }

            return new Response<int>(record.Id);
        }
    }
}
