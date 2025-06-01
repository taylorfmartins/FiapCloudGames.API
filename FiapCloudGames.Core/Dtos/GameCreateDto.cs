namespace FiapCloudGames.Core.Dtos
{
    /// <summary>
    /// Informações do Jogo
    /// </summary>
    /// <param name="name">`REQUIRED` Nome do Jogo</param>
    /// <param name="Description">Descrição do jogo</param>
    /// <param name="ReleasedDate">`REQUIRED` Data de Lançamento</param>
    /// <param name="Developer">Desenvolvedora do Jogo</param>
    public sealed record GameCreateDto(string? Name, string? Description, DateTime ReleasedDate, string? Developer);
    //public record GameCreateDto
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public DateTime ReleasedDate { get; set; }
    //    public string Developer { get; set; }
    //}
}
