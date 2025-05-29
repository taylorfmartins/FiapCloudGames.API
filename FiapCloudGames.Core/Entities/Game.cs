namespace FiapCloudGames.Core.Entities
{
    public class Game : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Developer { get; set; }
    }
}
