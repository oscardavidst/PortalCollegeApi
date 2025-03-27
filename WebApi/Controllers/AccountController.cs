using Application.DTOs.Users;
using Application.Enums;
using Application.Features.Authenticate.Commands.AuthenticateCommand;
using Application.Features.Authenticate.Commands.RegisterCommand;
using Application.Features.Authenticate.Commands.ValidateTokenCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Application.Features.Students.Commands.CreateStudentCommand;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticationAsync(AuthenticationRequest request)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
                IpAddress = GenerateIpAddress()
            }));
        }

        [HttpPost("register")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await Mediator.Send(new RegisterCommand
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Origin = Request.Headers["origin"],
                Rol = request.Rol
            }));
        }

        [HttpPost("registerStudent")]
        public async Task<IActionResult> RegisterStudentAsync(RegisterStudentRequest request)
        {
            var registeredStudent = Ok(await Mediator.Send(new RegisterCommand
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Origin = Request.Headers["origin"],
                Rol = Roles.Student.ToString()
            }));

            if (registeredStudent.StatusCode == StatusCodes.Status200OK)
            {
                var student = Ok(await Mediator.Send(new CreateStudentCommand()
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                }));
            }

            return registeredStudent;
        }

        [HttpGet("validateToken")]
        public async Task<IActionResult> ValidateTokenAsync([FromQuery] ValidateTokenRequest request) =>
            Ok(await Mediator.Send(new ValidateTokenCommand { Token = request.Token }));

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
