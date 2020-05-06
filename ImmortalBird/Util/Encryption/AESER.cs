using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encryption
{
    public static class AESER
    {

        #region 加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="iv">向量</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后base64编码字符串</returns>
        public static String Encrypt(String input, String iv, String key)
        {
            Byte[] keyBytes = Convert.FromBase64String(key);
            Byte[] ivBytes = Convert.FromBase64String(iv);
            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.Key = keyBytes;
                rm.IV = ivBytes;
                ICryptoTransform encryptor = rm.CreateEncryptor(rm.Key, rm.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        Byte[] encrypted = msEncrypt.ToArray();
                        return Convert.ToBase64String(encrypted);
                    }
                }
            }
        }
        #endregion

        #region 解密
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="iv">向量</param>
        /// <param name="key">密钥</param>
        /// <returns>加密之后的字符串</returns>
        public static String Decrypt(String input, String iv, String key)
        {
            Byte[] inputBytes = Convert.FromBase64String(input);
            Byte[] keyBytes = Convert.FromBase64String(key);
            Byte[] ivBytes = Convert.FromBase64String(iv);
            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.Key = keyBytes;
                rm.IV = ivBytes;
                ICryptoTransform decryptor = rm.CreateDecryptor(rm.Key, rm.IV);
                using (MemoryStream msDecrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion

    }
}
