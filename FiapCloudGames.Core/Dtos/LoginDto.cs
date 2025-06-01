namespace FiapCloudGames.Core.Dtos
{
    public record LoginDto
    {
        /// <summary>
        /// E-mail do Usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha de Acesso
        /// </summary>
        public string Password { get; set; }
    }
}
