namespace FiapCloudGames.Core.Dtos
{
    public sealed record GameUpdateDto(string Name, string Description, DateTime ReleasedDate, string Developer);
}
