using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Enrollments.Commands.DeleteEnrollmentCommand
{
    public class DeleteEnrollmentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteEnrollmentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Enrollment> _repositoryAsync;

        public DeleteHotelCommandHandler(IRepositoryAsync<Enrollment> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
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
