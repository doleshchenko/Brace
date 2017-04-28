using System.IO;
using System.Security.Cryptography;
using Brace.DomainModel.Cryptography;

namespace Brace.Cryptography
{
    public class AesCryptography : ICryptography
    {
        private static readonly byte[] _salt = { 17, 12, 20, 14, 15, 05, 19, 86 };

        public EcryptedData Encrypt(string key, string original)
        {
            using (var aes = Aes.Create())
            {
                var encryptor = aes.CreateEncryptor(CreateKey(key), aes.IV);
                byte[] encrypted;
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(original);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                return new EcryptedData { CipherText = encrypted, Iv = aes.IV };
            }
        }

        public string Decrypt(EcryptedData encryptedData, string key)
        {
            string plaintext;
            using (var aes = Aes.Create())
            {
                aes.Key = CreateKey(key);
                aes.IV = encryptedData.Iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var msDecrypt = new MemoryStream(encryptedData.CipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }


        //32 bytes == 256 bit
        private static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, _salt, iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
    }
}
