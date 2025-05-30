using FiapCloudGames.Core.Services;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class DeleteUserEndpoint
    {
        public static IEndpointRouteBuilder MapDeleteUser(this IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", async (int id, IUserService service) => {
                var result = await service.DeleteUserAsync(id);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteUser")
            .WithOpenApi();

            return app;
        }
    }
} 