using Microsoft.AspNetCore.Http.HttpResults;

namespace FiapCloudGames.API.Endpoints
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
