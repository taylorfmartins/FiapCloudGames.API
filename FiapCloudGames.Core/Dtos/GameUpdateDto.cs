using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Core.Dtos
{
    /// <summary>
    /// Informações para atualização de um Jogo existente
    /// </summary>
    public record GameUpdateDto
    {
        /// <summary>
        /// Nome do Jogo
        /// </summary>
        /// <example>Exploradores do Cosmos</example>
        [Required(ErrorMessage = "O nome do jogo é obrigatório.")]
        [StringLength(200)]
        public string Name { get; init; }

        /// <summary>
        /// Descrição detalhada sobre o jogo, com no mínimo 30 caracteres
        /// </summary>
        /// <example>Uma aventura espacial épica através de galáxias desconhecidas em busca da verdade sobre uma antiga civilização.</example>
        [StringLength(2000, MinimumLength = 30, ErrorMessage = "A descrição deve ter entre 30 e 2000 caracteres.")]
        public string Description { get; init; }

        /// <summary>
        /// Data em que o jogo foi ou será lançado
        /// </summary>
        [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
        public DateTime ReleasedDate { get; init; }

        /// <summary>
        /// Empresa ou estúdio que desenvolveu o jogo
        /// </summary>
        /// <example>Estúdios Supernova</example>
        [Required(ErrorMessage = "O nome da desenvolvedora é obrigatório.")]
        [StringLength(100)]
        public string Developer { get; init; }
    }
}
