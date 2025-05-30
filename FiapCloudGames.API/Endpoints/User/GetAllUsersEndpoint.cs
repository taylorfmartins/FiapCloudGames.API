using FiapCloudGames.Core.Services;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class GetAllUsersEndpoint
    {
        public static IEndpointRouteBuilder MapGetAllUsers(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", (IUserService service) => service.GetAll())
                .WithName("GetAllUsers")
                .WithOpenApi();

            return app;
        }
    }
} 