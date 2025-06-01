using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;
using System.Text.RegularExpressions;

namespace FiapCloudGames.Application.Sevices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;

        public UserService(IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<List<User>> GetAll() => await _userRepository.GetAllAsync();

        public async Task<User> GetById(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User> CreateUserAsync(UserCreateDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
                throw new ArgumentException("Nome do usuário não pode estar em branco");

            if (userDto.Name.Equals(userDto.Email, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Nome do usuário não pode ser igual ao e-mail");

            if (!IsValidEmail(userDto.Email))
                throw new ArgumentException("Formato de e-mail inválido");

            if (!IsValidPassword(userDto.Password))
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais");

            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = _passwordHashingService.HashPassword(userDto.Password),
                Role = "User"
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(userDto.Name))
                throw new ArgumentException("Nome do usuário não pode estar em branco");

            if (!IsValidEmail(userDto.Email))
                throw new ArgumentException("Formato de e-mail inválido");

            if (!IsValidPassword(userDto.Password))
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais");

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PasswordHash = _passwordHashingService.HashPassword(userDto.Password);

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> ChangePasswordAsync(int id, UserChangePasswordDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(userDto.Password))
                throw new ArgumentException("A senha atual não pode estar vazia");

            if (string.IsNullOrEmpty(userDto.NewPassword))
                throw new ArgumentException("A nova senha não pode estar vazia");

            if (!IsValidPassword(userDto.NewPassword))
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais");

            if (_passwordHashingService.VerifyPassword(userDto.Password, user.PasswordHash))
                throw new ArgumentException("A senha atual informada não está correta");

            user.PasswordHash = _passwordHashingService.HashPassword(userDto.NewPassword);

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> ChangeRole(int id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (!IsValidRole(role))
                throw new ArgumentException("O nível de acesso informado não existe");

            user.Role = role;

            var updatedUser = await _userRepository.UpdateAsync(user);

            return updatedUser != null;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Padrão de email válido
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // Mínimo 8 caracteres
            if (password.Length < 8)
                return false;

            // Deve conter pelo menos uma letra maiúscula
            if (!Regex.IsMatch(password, "[A-Z]"))
                return false;

            // Deve conter pelo menos uma letra minúscula
            if (!Regex.IsMatch(password, "[a-z]"))
                return false;

            // Deve conter pelo menos um número
            if (!Regex.IsMatch(password, "[0-9]"))
                return false;

            // Deve conter pelo menos um caractere especial
            if (!Regex.IsMatch(password, "[!@#$%^&*(),.?:{}|<>]"))
                return false;

            return true;
        }

        public bool IsValidRole(string role)
        {
            if (role.Equals("Admin") || role.Equals("User"))
                return true;
            return false;
        }
    }
}
