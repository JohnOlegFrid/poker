using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace poker.Security
{
    public static class Key
    {
        public static void GenerateKeyIV(out string key, out string IV)
        {
            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256,
            };
            rj.GenerateKey();
            rj.GenerateIV();

            key = Convert.ToBase64String(rj.Key);
            IV = Convert.ToBase64String(rj.IV);
        }
    }
}
