using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PetrofexSystem.Messaging
{
    public class MessageEncryption
    {
        public byte[] Encrypt(string dataToEncrypt, byte[] key)
        {
            byte[] encrypted;
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
                    }
                    encrypted = encryptionMemoryStream.ToArray();
                }
            }
            return encrypted;
        }

        public byte[] EncryptBytes(byte[] dataToEncrypt, byte[] key)
        {
            using (var encryptionAlgorithm = new DESCryptoServiceProvider())
            {
                var encryptor = encryptionAlgorithm.CreateEncryptor(key, key);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);                        
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        public string Decrypt(byte[] encryptedData, byte[] key)
        {
            string decrypted;
            using (var encryptionAlgorithm = new DESCryptoServiceProvider())
            {
                var decryptor = encryptionAlgorithm.CreateDecryptor(key, key);
                using (var memoryStream = new MemoryStream(encryptedData))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            decrypted = reader.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }

        

        public byte[] DecryptBytes(byte[] encryptedData, byte[] key)
        {
            using (var encryptionAlgorithm = new DESCryptoServiceProvider())
            {
                var decryptor = encryptionAlgorithm.CreateDecryptor(key, key);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                    }
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
