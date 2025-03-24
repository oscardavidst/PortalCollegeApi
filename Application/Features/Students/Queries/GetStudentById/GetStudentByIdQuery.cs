using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<Response<StudentDto>>
    {
        public int Id { get; set; }

        public class GetAllStudentsQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<StudentDto>>
        {
            private readonly IRepositoryAsync<Student> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllStudentsQueryHandler(IRepositoryAsync<Student> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositoryAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    var dto = _mapper.Map<StudentDto>(record);
                    return new Response<StudentDto>(dto);
                }
            }
        }
    }
}
