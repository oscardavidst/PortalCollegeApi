using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQuery : IRequest<Response<List<StudentDto>>>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, Response<List<StudentDto>>>
        {
            private readonly IRepositoryAsync<Student> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllStudentsQueryHandler(IRepositoryAsync<Student> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
            {
                var records = await _repositoryAsync.ListAsync(new StudentsSpecification(request.Name, request.LastName, request.Email));
                var recordsDto = _mapper.Map<List<StudentDto>>(records);
                return new Response<List<StudentDto>>(recordsDto);
            }
        }
    }
}
