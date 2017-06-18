using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace poker.Security
{
    public static class Decryption
    {
        public static string Decrypt(string prm_text_to_decrypt, string prm_key, string prm_iv)
        {

            var sEncryptedString = prm_text_to_decrypt;

            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256,
            };

            var key = Convert.FromBase64String(prm_key);
            var IV = Convert.FromBase64String(prm_iv);

            var decryptor = rj.CreateDecryptor(key, IV);

            var sEncrypted = Convert.FromBase64String(sEncryptedString);

            var fromEncrypt = new byte[sEncrypted.Length];

            var msDecrypt = new MemoryStream(sEncrypted);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            return (Encoding.ASCII.GetString(fromEncrypt).Replace("\0", string.Empty));
        }
    }
}
