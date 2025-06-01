namespace FiapCloudGames.Core.Entities
{
    public class Game : EntityBase
    {
        /// <summary>
        /// Nome do Jogo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição do Jogo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data de Lançamento
        /// </summary>
        public DateTime ReleasedDate { get; set; }

        /// <summary>
        /// Desenvolvedora responsável pelo Jogo
        /// </summary>
        public string Developer { get; set; }
    }
}
