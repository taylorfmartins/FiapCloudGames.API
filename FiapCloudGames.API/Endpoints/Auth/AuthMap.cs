namespace FiapCloudGames.API.Endpoints.Auth
{
    public static class AuthMap
    {
        public static IEndpointRouteBuilder MapAuth(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("api/auth")
                .WithTags("Authentication");

            groupBuilder.MapPost("/", AuthEndpoints.Login);

            return groupBuilder;
        }
    }
}
