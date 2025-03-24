using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Commands.UpdateStudentCommand
{
    public class UpdateStudentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateStudentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Student> _repositoryAsync;

        public UpdateHotelCommandHandler(IRepositoryAsync<Student> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
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
                record.Email = request.Email ?? record.Email;

                await _repositoryAsync.UpdateAsync(record);
            }

            return new Response<int>(record.Id);
        }
    }
}
