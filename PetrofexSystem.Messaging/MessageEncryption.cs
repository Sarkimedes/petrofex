using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PetrofexSystem.Messaging
{
    class MessageEncryption
    {
        public byte[] Encrypt(string dataToEncrypt, byte[] key)
        {
            using (var encryptionAlgorithm = new DESCryptoServiceProvider())
            {
                var encryptor = encryptionAlgorithm.CreateEncryptor(key, key);
                using (var encryptionMemoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(encryptionMemoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(dataToEncrypt, Encoding.UTF8);
                        }
                        return encryptionMemoryStream.ToArray();
                    }
                }
            }
        }

        public string Decrypt(byte[] encryptedData, byte[] key)
        {
            string data = string.Empty;
            using (var encryptionAlgorithm = new DESCryptoServiceProvider())
            {
                var decryptor = encryptionAlgorithm.CreateDecryptor(key, key);
                using (var memoryStream = new MemoryStream(encryptedData))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }
            }
            return data;
        }
    }
}
