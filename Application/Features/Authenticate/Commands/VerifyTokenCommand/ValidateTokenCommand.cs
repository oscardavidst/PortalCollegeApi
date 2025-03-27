using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System.Security.Claims;

namespace Application.Features.Authenticate.Commands.ValidateTokenCommand
{
    public class ValidateTokenCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string Token { get; set; }
    }

    public class VerifyTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Response<AuthenticationResponse>>
    {
        private readonly IAccountService _accountService;

        public VerifyTokenCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<Response<AuthenticationResponse>> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.ValidateTokenAsync(request.Token);
        }
    }
}
