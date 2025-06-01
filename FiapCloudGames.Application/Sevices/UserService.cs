using FiapCloudGames.Core.Dtos;
using FiapCloudGames.Core.Entities;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;
using System.Data;
using System.Text.RegularExpressions;

namespace FiapCloudGames.Application.Sevices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        public async Task<List<User>> GetAll() => await _userRepository.GetAllAsync();

        public async Task<User> GetById(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User> CreateUserAsync(UserCreateDto userDto, string role = "user")
        {
            if (string.IsNullOrEmpty(userDto.Name))
                throw new ArgumentException("Nome do usuário não pode estar em branco");

            if (!IsValidEmail(userDto.Email))
                throw new ArgumentException("Formato de e-mail inválido");

            if (!IsValidPassword(userDto.Password))
                throw new ArgumentException("A senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais");

            User user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = _encryptionService.Encrypt(userDto.Password),
                Role = role
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
            user.PasswordHash = _encryptionService.Encrypt(userDto.Password);

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

            if (_encryptionService.Decrypt(user.PasswordHash) != userDto.Password)
                throw new ArgumentException("A senha atual informada não está correta");

            user.PasswordHash = _encryptionService.Encrypt(userDto.NewPassword);

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Padrão de email válido
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPassword(string password)
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
    }
}
