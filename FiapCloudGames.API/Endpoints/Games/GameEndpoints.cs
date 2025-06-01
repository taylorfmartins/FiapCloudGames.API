using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints.Game
{
    public static class GameEndpoints
    {
        /// <summary>
        /// Busca todos os Jogos
        /// </summary>
        /// <returns>Uma lista de Jogos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.Game>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetAll(IGameService service) => Results.Ok(await service.GetAll());

        /// <summary>
        /// Busca um único Jogo pelo Id
        /// </summary>
        /// <param name="id">Id do Jogo</param>
        /// <returns>Jogo</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Core.Entities.Game))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetOne(IGameService service, int id)
        {
            return Results.Ok(await service.GetById(id));
        }

        /// <summary>
        /// Cria um novo Jogo
        /// </summary>
        /// <param name="game">`REQUIRED` Dados de um novo Jogo</param>
        /// <returns>Retorno da Criação</returns>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<Core.Entities.Game>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Post(IGameService service, [FromBody] GameCreateDto game)
        {
            try
            {
                var addedGame = await service.CreateGameAsync(game);

                return Results.Created($"/game/{addedGame.Id}", addedGame);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Modifica um Jogo existente
        /// </summary>
        /// <param name="id">Id do Game a ser modificado</param>
        /// <param name="game">Jogo atualizado</param>
        /// <returns>Ok or NotFound</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Put(IGameService service, int id, [FromBody] GameUpdateDto game)
        {
            try
            {
                var updatedGame = await service.UpdateGameAsync(game);

                return Results.Created($"/game/{updatedGame.Id}", updatedGame);
            }
            catch (ArgumentException e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// Deleta um Jogo
        /// </summary>
        /// <param name="id">Id do Jogo a ser deletado</param>
        /// <returns>Ok or NotFound</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> Delete(IGameService service, int id)
        {
            var result = await service.DeleteGameAsync(id);
            return result ? Results.NoContent() : Results.NotFound();
        }
    }
}
