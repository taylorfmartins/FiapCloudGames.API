namespace FiapCloudGames.API.Endpoints.User
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("/user")
                .WithOpenApi();

            userGroup.MapCreateUser();
            userGroup.MapDeleteUser();
            userGroup.MapGetAllUsers();
            userGroup.MapGetUserById();

            return app;
        }
    }
}
