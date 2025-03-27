using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAdress)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                throw new ApiException($"No hay un usuario registrado con el email {request.Email}");
            }

            var result = await _signInManager.PasswordSignInAsync(usuario.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Las credenciales del usuario {request.Email} no son válidas.");
            }

            JwtSecurityToken jwtSercurityToken = await GenerateJWTToken(usuario);
            AuthenticationResponse authResponse = new AuthenticationResponse()
            {
                Id = usuario.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSercurityToken),
                Email = usuario.Email,
                UserName = usuario.UserName
            };

            var rolesList = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);
            authResponse.Roles = rolesList.ToList();
            authResponse.IsVerified = usuario.EmailConfirmed;

            var refreshToken = GenerateRefreshToken(ipAdress);
            authResponse.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(authResponse, $"Usuario {usuario.UserName} autenticado!");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin, Roles rol)
        {
            var usuarioConElMismoUserName = await _userManager.FindByNameAsync(request.UserName);
            if (usuarioConElMismoUserName != null)
            {
                throw new ApiException($"El usuario {request.UserName} ya existe.");
            }
            var usuario = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.Name,
                Lastname = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var usuarioConElMismoCorreo = await _userManager.FindByEmailAsync(request.Email);
            if (usuarioConElMismoCorreo != null)
            {
                throw new ApiException($"El usuario con Email {request.Email} ya existe.");
            }
            else
            {
                var result = await _userManager.CreateAsync(usuario, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, rol.ToString());
                    return new Response<string>(usuario.Id, message: $"Usuario {request.UserName} ({request.Email}) registrado exitosamente.");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }

        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser usuario)
        {
            var userClaims = await _userManager.GetClaimsAsync(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            var rolesClaims = new List<Claim>();
            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim("roles", role));
            }

            string ipAddress = IpHelper.GetIpAdress();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("uid", usuario.Id),
                new Claim("ip", ipAddress)
            }.Union(userClaims).Union(rolesClaims);

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCrypoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCrypoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public async Task<Response<AuthenticationResponse>> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                var claims = principal.Identities.FirstOrDefault()?.Claims.ToList();
                AuthenticationResponse authResponse = new AuthenticationResponse()
                {
                    Id = claims?.Where(c => c.Type.Contains("uid")).Select(c => c.Value).FirstOrDefault() ?? "",
                    Email = claims?.Where(c => c.Type.Contains("email")).Select(c => c.Value).FirstOrDefault() ?? "",
                    UserName = claims?.Where(c => c.Type.Contains("nameidentifier")).Select(c => c.Value).FirstOrDefault() ?? "",
                    Roles = [claims?.Where(c => c.Type.Contains("role")).Select(c => c.Value).FirstOrDefault() ?? ""],
                    IsVerified = true,
                    JWToken = token
                };
                return new Response<AuthenticationResponse>(authResponse, "Token válido.");
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ApiException("El token ha expirado.");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new ApiException("La firma del token no es válida.");
            }
            catch (Exception ex)
            {
                throw new ApiException($"Token inválido: {ex.Message}");
            }
        }
    }
}
