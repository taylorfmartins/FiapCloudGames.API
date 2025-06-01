namespace FiapCloudGames.API.Endpoints.Game
{
    public static class GameMap
    {
        public static IEndpointRouteBuilder MapGame(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("api/game")
                .WithTags("Game")
                .RequireAuthorization();
            
            groupBuilder.MapGet("/", GameEndpoints.GetAll);
            groupBuilder.MapGet("/{id:int}", GameEndpoints.GetOne);
            groupBuilder.MapPost("/", GameEndpoints.Post)
                .RequireAuthorization("Admin");
            groupBuilder.MapPut("/{id:int}", GameEndpoints.Put)
                .RequireAuthorization("Admin");
            groupBuilder.MapDelete("/{id:int}", GameEndpoints.Delete)
                .RequireAuthorization("Admin");

            return builder;
        }
    }
}
