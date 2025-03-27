using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Enrollments.Queries.GetEnrollmentById
{
    public class GetEnrollmentByIdQuery : IRequest<Response<EnrollmentDto>>
    {
        public int Id { get; set; }

        public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentByIdQuery, Response<EnrollmentDto>>
        {
            private readonly IRepositoryAsync<Enrollment> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllEnrollmentsQueryHandler(IRepositoryAsync<Enrollment> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<EnrollmentDto>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
            {
                var record = await _repositoryAsync.GetByIdAsync(request.Id);
                if (record == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}.");
                }
                else
                {
                    var dto = _mapper.Map<EnrollmentDto>(record);
                    return new Response<EnrollmentDto>(dto);
                }
            }
        }
    }
}
