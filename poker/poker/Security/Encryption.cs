﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace poker.Security
{
    public static class Encryption
    {
        public static string Encrypt(string prm_text_to_encrypt, string prm_key, string prm_iv)
        {
            var sToEncrypt = prm_text_to_encrypt;

            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256,
            };

            var key = Convert.FromBase64String(prm_key);
            var IV = Convert.FromBase64String(prm_iv);

            var encryptor = rj.CreateEncryptor(key, IV);

            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            var encrypted = msEncrypt.ToArray();

            return (Convert.ToBase64String(encrypted));
        }

        public static string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
