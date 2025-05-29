namespace FiapCloudGames.Core.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; } = false;

        public EntityBase()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
