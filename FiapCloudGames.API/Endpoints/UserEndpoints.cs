using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var userGroup = app.MapGroup("/user")
                .WithOpenApi();

            userGroup.MapGet("/", (UserService service) => service.GetAll())
                .WithName("GetAll");

            //userGroup.MapPost("/", (UserService service) => service.AddAs)

            return app;
        }
    }
}
