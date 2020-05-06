using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Text
{
    public class RandomStr
    {
        #region 创建随机数字串
        /// <summary>
        /// 创建随机数字串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>数字组成的字符串</returns>
        public static string GetRandomNum(int length)
        {
            StringBuilder randomTextBuilder = new StringBuilder(length);
            Random random = new Random();
            string TextChars = "1234567890";
            for (int i = 0; i < length; i++)
            {
                randomTextBuilder.Append(TextChars.Substring(random.Next(TextChars.Length), 1));
            }

            return randomTextBuilder.ToString();
        }
        #endregion

        #region 创建随机字符串
        private static string[] strs = new string[]
                       {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                       };
        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        public static string CreatenNonce_str()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }
        #endregion

        #region 获取时间戳
        /// <summary>
        /// 获取10位的时间戳
        /// </summary>
        /// <returns></returns>
        public static long Get10Time()
        {
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            long t = (long)cha.TotalSeconds;
            return t;
        }

        /// <summary>
        /// 获取13位的时间戳
        /// </summary>
        /// <returns></returns>
        public static long Get13Time()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long t = Convert.ToInt64(ts.TotalMilliseconds);
            return t;
        }
        #endregion

    }
}
