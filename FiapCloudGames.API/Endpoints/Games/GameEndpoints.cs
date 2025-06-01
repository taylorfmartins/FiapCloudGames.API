using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Endpoints.Game
{
    public static class GameEndpoints
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>Lista de Todos Jogos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.Game>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetAll(IGameService service) => Results.Ok(await service.GetAll());

        /// <summary>
        /// GetOne
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
        /// Create
        /// </summary>
        /// <param name="game">Jogo</param>
        /// <returns>Jogo</returns>
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
        /// Update
        /// </summary>
        /// <param name="id">Id do Game</param>
        /// <param name="game">Dadps Atualizados</param>
        /// <returns>Jogo</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Entities.Game>))]
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
        /// Delete
        /// </summary>
        /// <param name="id">Id do Jogo</param>
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
