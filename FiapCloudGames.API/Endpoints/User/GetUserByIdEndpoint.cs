using FiapCloudGames.Core.Services;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class GetUserByIdEndpoint
    {
        public static IEndpointRouteBuilder MapGetUserById(this IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (int id, IUserService service) => {
                var user = await service.GetById(id);
                return user is null ? Results.NotFound() : Results.Ok(user);
            })
            .WithName("GetUserById")
            .WithOpenApi();

            return app;
        }
    }
} 