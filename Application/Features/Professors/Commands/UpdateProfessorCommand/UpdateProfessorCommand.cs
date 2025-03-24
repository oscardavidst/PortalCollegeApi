using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Professors.Commands.UpdateProfessorCommand
{
    public class UpdateProfessorCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateProfessorCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Professor> _repositoryAsync;

        public UpdateHotelCommandHandler(IRepositoryAsync<Professor> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateProfessorCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
            }
            else
            {
                record.Name = request.Name ?? record.Name;
                record.LastName = request.LastName ?? record.LastName;

                await _repositoryAsync.UpdateAsync(record);
            }

            return new Response<int>(record.Id);
        }
    }
}
