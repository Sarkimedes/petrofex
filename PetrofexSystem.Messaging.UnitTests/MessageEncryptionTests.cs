using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetrofexSystem.Messaging.UnitTests
{
    [TestClass]
    public class MessageEncryptionTests
    {
        [TestMethod]
        public void Encrypt_CalledWithValidKey_CanBeDecryptedUsingSameKey()
        {
            var cryptoServiceProvider = new DESCryptoServiceProvider();
            var message = "This is a test";
            var key = cryptoServiceProvider.Key;
            
            var messageEncryption = new MessageEncryption();
            var encrypted = messageEncryption.Encrypt(message, key);
            var decrypted = messageEncryption.Decrypt(encrypted, key);

            Assert.AreNotEqual(message, encrypted);
            Assert.AreNotEqual(encrypted, decrypted);
            Assert.AreEqual(message, decrypted);
        }
    }
}
