using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Courses.Commands.DeleteCourseCommand
{
    public class DeleteCourseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteCourseCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync;

        public DeleteHotelCommandHandler(IRepositoryAsync<Course> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
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
