using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class CreateUserEndpoint
    {
        public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (UserCreateDto user, IUserService service) => {
                try
                {
                    var addedUser = await service.CreateUserAsync(user);
                    return Results.Created($"/user/{addedUser.Id}", addedUser);
                }
                catch (ArgumentException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
            })
            .WithName("CreateUser")
            .WithOpenApi();

            return app;
        }
    }
} 