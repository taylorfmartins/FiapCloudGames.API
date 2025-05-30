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
            
            userGroup.MapGet("/{int}", async (int id, IUserService service) => {
                var user = await service.GetById(id);

                if (user == null)
                    return Results.NotFound();

                return Results.Ok(user);
            })
           .WithName("GetById");

            userGroup.MapPost("/", async (UserCreateDto user, IUserService service) =>
            {
                try
                {
                    var addedUser = await service.CreateUserAsync(user);
                    return Results.Created($"/user/{addedUser.Id}", addedUser);
                }
                catch (ArgumentException e)
                {
                    return Results.BadRequest(new { error = e.Message });
                }
            });

            userGroup.MapDelete("/{id}", async (int id, IUserService service) =>
            {
                await service.DeleteUserAsync(id);

                return Results.NoContent();
            });

            return app;
        }
    }
}
