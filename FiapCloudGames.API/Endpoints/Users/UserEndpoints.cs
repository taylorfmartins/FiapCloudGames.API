using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class UserEndpoints
    {

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>Lista de Todos Usuários</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetAll(IUserService service) => Results.Ok(await service.GetAll());

        /// <summary>
        /// GetOne
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <returns>Usuário</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetOne(IUserService service, int id)
        {
            return Results.Ok(await service.GetById(id));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <returns>Usuário</returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Post(IUserService service, [FromBody] UserCreateDto user)
        {
            try
            {
                var addedUser = await service.CreateUserAsync(user);

                return Results.Created($"/user/{addedUser.Id}", addedUser);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <param name="user">Novos dados</param>
        /// <returns>Usuário</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Put(IUserService service, int id, [FromBody] UserUpdateDto user)
        {
            try
            {
                var updatedUser = await service.UpdateUserAsync(id, user);

                return Results.Created($"/user/{updatedUser.Id}", updatedUser);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <param name="changePassword">Dados e Senha</param>
        /// <returns>Usuário</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> ChangePassword(IUserService service, int id, [FromBody] UserChangePasswordDto changePassword)
        {
            try
            {
                var updatedUser = await service.ChangePasswordAsync(id, changePassword);

                return Results.Created($"/user/{updatedUser.Id}/changePassword", updatedUser);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <returns>Ok or NotFound</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Delete(IUserService service, int id)
        {
            var result = await service.DeleteUserAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        }
    }
}
