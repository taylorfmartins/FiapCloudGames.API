using System.ComponentModel.DataAnnotations;

namespace FiapCloudGames.Core.Dtos
{
    public record UserChangePasswordDto
    {
        /// <summary>
        /// Senha Atual
        /// </summary>
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        [StringLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// Nova Senha
        /// 
        /// Requisitos:
        /// * Mínimo `8` caracteres
        /// * Ter `1` letra minúscula
        /// * Ter `1` letra maiúscula
        /// * Ter `1` caracter especial
        /// </summary>
        /// <example>Abc@1234</example>
        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [StringLength(100)]
        public string NewPassword { get; set; }
    }
}
