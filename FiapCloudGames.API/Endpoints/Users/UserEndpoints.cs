using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints.User
{
    public static class UserEndpoints
    {

        /// <summary>
        /// Busca todos os Usuário
        /// </summary>
        /// <returns>Uma lista de Usuários</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetAll(IUserService service) => Results.Ok(await service.GetAll());

        /// <summary>
        /// Busca um único Usuário pelo Id
        /// </summary>
        /// <param name="id">Id do Usuário</param>
        /// <returns>Usuário</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Core.Entities.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetOne(IUserService service, int id)
        {
            return Results.Ok(await service.GetById(id));
        }

        /// <summary>
        /// Cria um novo Usuário
        /// </summary>
        /// <param name="user">`REQUIRED` Dados de um novo Usuário</param>
        /// <returns>Retorno da Criação</returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
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
        /// Modifica um Usuário existente
        /// </summary>
        /// <param name="id">Id do Usuário a ser modificado</param>
        /// <param name="user">Usuário atualizado</param>
        /// <returns>Ok or NotFound</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Put(IUserService service, int id, [FromBody] UserUpdateDto user)
        {
            try
            {
                var updatedUser = await service.UpdateUserAsync(user);

                return Results.Created($"/user/{updatedUser.Id}", updatedUser);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Deleta um Usuário
        /// </summary>
        /// <param name="id">Id do Usuário a ser deletado</param>
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
