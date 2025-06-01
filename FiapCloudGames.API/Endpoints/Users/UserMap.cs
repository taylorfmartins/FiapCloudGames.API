using FiapCloudGames.API.Endpoints.Game;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class UserMap
    {
        public static IEndpointRouteBuilder MapUser(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("api/user");

            groupBuilder.MapGet("/", UserEndpoints.GetAll);
            groupBuilder.MapGet("/{id:int}", UserEndpoints.GetOne);
            groupBuilder.MapPost("/", UserEndpoints.Post);
            groupBuilder.MapPut("/{id:int}", UserEndpoints.Put);
            groupBuilder.MapDelete("/{id:int}", UserEndpoints.Delete);

            return builder;
        }
    }
}
