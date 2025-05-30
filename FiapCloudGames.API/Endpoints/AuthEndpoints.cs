namespace FiapCloudGames.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var authGroup = app.MapGroup("/auth")
                .WithOpenApi();

            //authGroup.MapPost("/login", () => { })

            return app;
        }
    }
}
