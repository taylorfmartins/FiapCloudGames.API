namespace FiapCloudGames.API.Endpoints.Game
{
    public static class GameEndpoints
    {
        public static IEndpointRouteBuilder MapGameEndpoints(this IEndpointRouteBuilder app)
        {
            var gameGroup = app.MapGroup("/game")
                .WithOpenApi();

            gameGroup.MapGet("", GetAll);

            return app;
        }

        public static async Task<IResult> GetAll()
        {
            return TypedResults.Ok();
        }
    }
}
