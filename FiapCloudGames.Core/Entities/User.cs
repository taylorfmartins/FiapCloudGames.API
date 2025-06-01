namespace FiapCloudGames.Core.Entities
{
    public class User : EntityBase
    {
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail do Usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do Usuário criptografada
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Nível de acesso do usuário
        /// </summary>
        public string Role { get; set; }
    }
}
