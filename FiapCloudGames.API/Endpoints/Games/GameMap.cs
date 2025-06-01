namespace FiapCloudGames.API.Endpoints.Game
{
    public static class GameMap
    {
        public static IEndpointRouteBuilder MapGame(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("api/game")
                .WithTags("Game");
            
            groupBuilder.MapGet("/", GameEndpoints.GetAll);
            groupBuilder.MapGet("/{id:int}", GameEndpoints.GetOne);
            groupBuilder.MapPost("/", GameEndpoints.Post);
            groupBuilder.MapPut("/{id:int}", GameEndpoints.Put);
            groupBuilder.MapDelete("/{id:int}", GameEndpoints.Delete);

            return builder;
        }
    }
}
