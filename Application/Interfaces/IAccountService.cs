using Application.DTOs.Users;
using Application.Enums;
using Application.Wrappers;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAdress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin, Roles rol);
    }
}
