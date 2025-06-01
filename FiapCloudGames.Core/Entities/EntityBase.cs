namespace FiapCloudGames.Core.Entities
{
    public class EntityBase
    {
        /// <summary>
        /// Identificador Único
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data de Criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data da última atualização
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public EntityBase()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
