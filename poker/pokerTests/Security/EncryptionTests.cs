using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Security.Tests
{
    [TestClass()]
    public class EncryptionTests
    {
        [TestMethod()]
        public void EncryptDecryptTest()
        {
            string key = Program.key;
            string iv = Program.iv;
            string data = "Hey Im Willi!";
            string encrypt = Encryption.Encrypt(data, key, iv);
            string decrypt = Decryption.Decrypt(encrypt, key, iv);
            Assert.AreEqual(data, decrypt);
        }
    }
}