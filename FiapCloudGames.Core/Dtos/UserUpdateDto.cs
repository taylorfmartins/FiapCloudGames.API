using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Core.Dtos
{
    /// <summary>
    /// Informações para a atualização de um Usuário existente
    /// </summary>
    public record UserUpdateDto
    {
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        /// <example>Taylor Figueira Martins</example>
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100)]
        public string Name { get; init; }

        /// <summary>
        /// E-mail do Usuário
        /// </summary>
        /// <example>taylor@fiap.com.br</example>
        [Required(ErrorMessage = "O e-mail do usuário é obrigatório.")]
        [StringLength(100)]
        public string Email { get; init; }

        /// <summary>
        /// Senha de Acesso
        /// 
        /// Requisitos:
        /// * Mínimo `8` caracteres
        /// * Ter `1` letra minúscula
        /// * Ter `1` letra maiúscula
        /// * Ter `1` caracter especial
        /// </summary>
        /// <example>Abc@1234</example>
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100)]
        public string Password { get; init; }
    }
}
