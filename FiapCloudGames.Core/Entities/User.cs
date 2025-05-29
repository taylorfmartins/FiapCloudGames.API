namespace FiapCloudGames.Core.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<string> AcquiredGameIds { get; set; } = new List<string>();
        public string Role { get; set; }
    }
}
