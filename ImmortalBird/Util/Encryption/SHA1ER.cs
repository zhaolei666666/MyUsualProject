using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encryption
{
    public static class SHA1ER
    {
        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SHA1Encrypt(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}