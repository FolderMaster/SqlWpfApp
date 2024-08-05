using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Files;

namespace ViewModel.Dependencies
{
    public class AesEncryptionService : IEncryptionService
    {
        private static string _filePath = "key.key";

        private static DataProtectionScope _scope = DataProtectionScope.CurrentUser;

        private static byte[] _entropy = Encoding.Default.GetBytes("Entropy");

        private readonly IFileService _fileService;

        public AesEncryptionService(IFileService fileService) =>
            _fileService = fileService;

        public byte[] Encrypt(byte[] data)
        {
            using (var aes = Aes.Create())
            {
                CreateOrLoadKey(aes, _filePath);
                aes.GenerateIV();
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(aes.IV, 0, aes.IV.Length);
                        using (var cryptoStream = new CryptoStream(memoryStream,
                            encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                        }
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            using (var aes = Aes.Create())
            {
                LoadKey(aes, _filePath);
                GetIv(aes, data);
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream,
                            decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, aes.IV.Length, data.Length - aes.IV.Length);
                        }
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private void GetIv(Aes aes, byte[] data)
        {
            var iv = new byte[aes.BlockSize / 8];
            Array.Copy(data, 0, iv, 0, iv.Length);
            aes.IV = iv;
        }

        private void CreateOrLoadKey(Aes aes, string filePath)
        {
            if (_fileService.IsPathExists(filePath))
            {
                LoadKey(aes, filePath);
            }
            else
            {
                CreateKey(aes);
                SaveKey(aes, filePath);
            }
        }

        private void SaveKey(Aes aes, string filePath)
        {
            var data = ProtectedData.Protect(aes.Key, _entropy, _scope);
            _fileService.Save(filePath, data);
        }

        private void LoadKey(Aes aes, string filePath)
        {
            var data = _fileService.Load(filePath);
            aes.Key = ProtectedData.Unprotect(data, _entropy, _scope);
        }

        private void CreateKey(Aes aes) => aes.GenerateKey();
    }
}
