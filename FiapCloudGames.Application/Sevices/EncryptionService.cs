using FiapCloudGames.Core.Services;
using System.Security.Cryptography;

namespace FiapCloudGames.Application.Sevices
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] encryptionKey;
        private readonly byte[] encryptIv;

        public EncryptionService()
        {
            var encryptionKey = "dbO6FqQWdF1jYQRCInxLbYNckBo9XqOANo0lzjn+zYg=";
            var encryptIv = "mAAzdKyGkCtgJp1a9QeZCw==";

            this.encryptionKey = Convert.FromBase64String(encryptionKey);
            this.encryptIv = Convert.FromBase64String(encryptIv);

            // Verificar se a chave e o IV têm tamanhos válidos
            if (this.encryptionKey.Length != 16 && this.encryptionKey.Length != 24 && this.encryptionKey.Length != 32)
            {
                throw new ArgumentException("Chave inválida. AES suporta chaves de 128, 192 ou 256 bits.");
            }
            if (this.encryptIv.Length != 16)
            {
                throw new ArgumentException("IV inválido. AES suporta um IV de 128 bits.");
            }
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptIv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    var array = ms.ToArray();
                    return Convert.ToBase64String(array);
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = encryptIv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                var cipherTextBytes = Convert.FromBase64String(cipherText);
                using (var ms = new MemoryStream(cipherTextBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
