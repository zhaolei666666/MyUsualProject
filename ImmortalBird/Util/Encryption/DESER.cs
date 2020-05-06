using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encryption
{
    public static class DESER
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="message">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string message, string key)
        {
            var des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(key);
            //des.Mode = CipherMode.ECB;

            byte[] inputByteArray = Encoding.UTF8.GetBytes(message);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            ms.Close();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();

        }

        //DES解密
        public static string Decrypt(string content, string key)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                // 密钥
                provider.Key = Encoding.UTF8.GetBytes(key);
                // 偏移量
                provider.IV = Encoding.UTF8.GetBytes(key);
                byte[] buffer = new byte[content.Length / 2];
                for (int i = 0; i < (content.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(content.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num2;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    stream.Close();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception) { return ""; }
        }
    }
}
