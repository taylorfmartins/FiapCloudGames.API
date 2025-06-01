using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints.Auth
{
    public static class AuthEndpoints
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Dados e Acesso</param>
        /// <returns>Token</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Token>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static async Task<IResult> Login(IAuthService authService, TokenService tokenService, LoginDto login)
        {
            var user = await authService.AuthenticateAsync(login.Email, login.Password);

            if (user == null)
            {
                return Results.Unauthorized();
            }

            var token = tokenService.GenerateToken(user);

            return Results.Ok(new { Token = token });
        }
    }
}
