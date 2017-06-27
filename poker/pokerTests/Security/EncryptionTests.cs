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

        [TestMethod()]
        public void EncryptDecryptNotSameTest()
        {
            string key = Program.key;
            string iv = Program.iv;
            string data1 = "Hey Im Willi!";
            string data2 = "Hey Im Eliran!";
            string encrypt1 = Encryption.Encrypt(data1, key, iv);
            string encrypt2 = Encryption.Encrypt(data2, key, iv);
            string decrypt1 = Decryption.Decrypt(encrypt1, key, iv);
            string decrypt2 = Decryption.Decrypt(encrypt2, key, iv);
            Assert.AreNotEqual(data1, decrypt2);
            Assert.AreNotEqual(data2, decrypt1);
        }

        [TestMethod()]
        public void EncryptPasswordGood()
        {
            string key = Program.key;
            string iv = Program.iv;
            string pass = "1234";
            string encry = Encryption.EncryptPassword(pass);
            Assert.AreNotEqual(pass, encry);
            Assert.AreEqual(Encryption.EncryptPassword(pass), encry);
        }

        [TestMethod()]
        public void EncryptPasswordBad()
        {
            string key = Program.key;
            string iv = Program.iv;
            string pass1 = "1234";
            string pass2 = "12345";
            string encry1 = Encryption.EncryptPassword(pass1);
            string encry2 = Encryption.EncryptPassword(pass2);
            Assert.AreNotEqual(encry1, encry2);
        }
    }
}