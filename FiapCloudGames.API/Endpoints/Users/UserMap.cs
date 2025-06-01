using FiapCloudGames.API.Endpoints.Game;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class UserMap
    {
        public static IEndpointRouteBuilder MapUser(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("api/user")
                .WithTags("User");

            groupBuilder.MapGet("/", UserEndpoints.GetAll)
                .RequireAuthorization("Admin");
            
            groupBuilder.MapGet("/{id:int}", UserEndpoints.GetOne)
                .RequireAuthorization("Admin");
            
            groupBuilder.MapPost("/", UserEndpoints.Post);
            
            groupBuilder.MapPut("/{id:int}", UserEndpoints.Put)
                .RequireAuthorization();
            
            groupBuilder.MapPost("/{id:int}/changePassword", UserEndpoints.ChangePassword)
                .RequireAuthorization();

            groupBuilder.MapPost("/{id:int}/changeRole/{role}", UserEndpoints.ChangeRole)
                .RequireAuthorization("Admin");

            groupBuilder.MapDelete("/{id:int}", UserEndpoints.Delete)
                .RequireAuthorization("Admin");

            return builder;
        }
    }
}
