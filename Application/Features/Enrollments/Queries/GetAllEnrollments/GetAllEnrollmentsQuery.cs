using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Enrollments.Queries.GetAllEnrollments
{
    public class GetAllEnrollmentsQuery : IRequest<Response<List<EnrollmentDto>>>
    {
        public int? CourseId { get; set; }
        public int? StudentId { get; set; }

        public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, Response<List<EnrollmentDto>>>
        {
            private readonly IRepositoryAsync<Enrollment> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllEnrollmentsQueryHandler(IRepositoryAsync<Enrollment> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<EnrollmentDto>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
            {
                var records = await _repositoryAsync.ListAsync(new EnrollmentsSpecification(request.CourseId, request.StudentId));
                var recordsDto = _mapper.Map<List<EnrollmentDto>>(records);
                return new Response<List<EnrollmentDto>>(recordsDto);
            }
        }
    }
}
