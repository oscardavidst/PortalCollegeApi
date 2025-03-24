using Application.Features.Courses.Commands.CreateCourseCommand;
using Application.Features.Courses.Commands.DeleteCourseCommand;
using Application.Features.Courses.Commands.UpdateCourseCommand;
using Application.Features.Courses.Queries.GetAllCourses;
using Application.Features.Courses.Queries.GetCourseById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CoursesController : BaseApiController
    {
        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllCoursesParameters filter) =>
            Ok(await Mediator.Send(new GetAllCoursesQuery { Name = filter.Name, Credits = filter.Credits }));

        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new GetCourseByIdQuery { Id = id }));

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCourseCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateCourseCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await Mediator.Send(new DeleteCourseCommand() { Id = id }));
    }
}
