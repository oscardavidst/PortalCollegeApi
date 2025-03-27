using Application.Features.Enrollments.Commands.CreateEnrollmentCommand;
using Application.Features.Enrollments.Commands.DeleteEnrollmentCommand;
using Application.Features.Enrollments.Queries.GetAllEnrollments;
using Application.Features.Enrollments.Queries.GetEnrollmentById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class EnrollmentsController : BaseApiController
    {
        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllEnrollmentsParameters filter) =>
            Ok(await Mediator.Send(new GetAllEnrollmentsQuery { StudentId = filter.StudentId, CourseId = filter.CourseId }));

        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new GetEnrollmentByIdQuery { Id = id }));

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateEnrollmentCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await Mediator.Send(new DeleteEnrollmentCommand() { Id = id }));
    }
}
