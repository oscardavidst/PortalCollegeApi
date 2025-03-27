using Application.Features.ProfessorsCourses.Commands.CreateProfessorCourseCommand;
using Application.Features.ProfessorsCourses.Commands.DeleteProfessorCourseCommand;
using Application.Features.ProfessorsCourses.Queries.GetAllProfessorsCourses;
using Application.Features.ProfessorsCourses.Queries.GetProfessorCourseById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfessorsCoursesController : BaseApiController
    {
        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllProfessorsCoursesParameters filter) =>
            Ok(await Mediator.Send(new GetAllProfessorsCoursesQuery { ProfessorId = filter.ProfessorId, CourseId = filter.CourseId }));

        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new GetProfessorCourseByIdQuery { Id = id }));

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProfessorCourseCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await Mediator.Send(new DeleteProfessorCourseCommand() { Id = id }));
    }
}
