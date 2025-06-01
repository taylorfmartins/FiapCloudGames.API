namespace FiapCloudGames.Core.Dtos
{
    /// <summary>
    /// Informações do Jogo
    /// </summary>
    /// <param name="Name">`REQUIRED` Nome do Jogo</param>
    /// <param name="Description">`REQUIRED` Descrição do Jogo (min. 30 caracteres)</param>
    /// <param name="ReleasedDate">`REQUIRED` Data de Lançamento</param>
    /// <param name="Developer">`REQUIRED` Desenvolvedora</param>
    public sealed record GameCreateDto(string Name, string Description, DateTime ReleasedDate, string Developer);
}
