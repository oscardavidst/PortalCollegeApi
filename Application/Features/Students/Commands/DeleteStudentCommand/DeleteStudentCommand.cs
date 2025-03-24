using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.DeleteStudentCommand
{
    public class DeleteStudentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteStudentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Student> _repositoryAsync;

        public DeleteHotelCommandHandler(IRepositoryAsync<Student> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
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
