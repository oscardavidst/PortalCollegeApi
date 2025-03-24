using Application.DTOs.Users;
using Application.Enums;
using Application.Wrappers;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAdress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin, Roles rol);
        Task<Response<ClaimsPrincipal>> ValidateTokenAsync(string token);
    }
}
