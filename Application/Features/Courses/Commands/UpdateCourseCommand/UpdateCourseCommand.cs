using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Credits { get; set; }
    }

    public class UpdateHotelCommandHandler : IRequestHandler<UpdateCourseCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Course> _repositoryAsync;

        public UpdateHotelCommandHandler(IRepositoryAsync<Course> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
            }
            else
            {
                record.Name = request.Name ?? record.Name;
                record.Credits = request.Credits != null ? Byte.Parse(request.Credits.ToString()) : record.Credits;

                await _repositoryAsync.UpdateAsync(record);
            }

            return new Response<int>(record.Id);
        }
    }
}
