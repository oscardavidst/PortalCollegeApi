using Application.Features.Professors.Commands.CreateProfessorCommand;
using Application.Features.Professors.Commands.DeleteProfessorCommand;
using Application.Features.Professors.Commands.UpdateProfessorCommand;
using Application.Features.Professors.Queries.GetAllProfessors;
using Application.Features.Professors.Queries.GetProfessorById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProfessorsController : BaseApiController
    {
        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllProfessorsParameters filter) =>
            Ok(await Mediator.Send(new GetAllProfessorsQuery { Name = filter.Name, LastName = filter.LastName }));

        [Authorize(Roles = "Administrator,Student,Professor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new GetProfessorByIdQuery { Id = id }));

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProfessorCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateProfessorCommand command) => Ok(await Mediator.Send(command));

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await Mediator.Send(new DeleteProfessorCommand() { Id = id }));
    }
}
