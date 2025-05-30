using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;

namespace FiapCloudGames.API.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("/user")
                .WithOpenApi();

            userGroup.MapGet("/", (IUserService service) => service.GetAll())
                .WithName("GetAll");

            userGroup.MapPost("/", (UserCreateDto user, IUserService service) => service.CreateUserAsync(user));

            return app;
        }
    }
}
