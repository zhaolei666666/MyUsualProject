using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Util.Encryption
{
    public static class MD5ER
    {
        public static string MD5Encryp(string str, string encodingType = "utf-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodingType);
            byte[] b = encoding.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        /// <summary>
        /// MD5签名方法  
        /// </summary>  
        /// <param name="inputText">加密参数</param>  
        /// <returns></returns>  
        public static string MD5EncrypR(string inputText)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(inputText);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
        }
    }
}
