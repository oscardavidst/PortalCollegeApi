using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProfessorsCourses.Commands.DeleteProfessorCourseCommand
{
    public class DeleteProfessorCourseCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteProfessorCourseCommand, Response<int>>
    {
        private readonly IRepositoryAsync<ProfessorCourse> _repositoryAsync;

        public DeleteHotelCommandHandler(IRepositoryAsync<ProfessorCourse> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteProfessorCourseCommand request, CancellationToken cancellationToken)
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
