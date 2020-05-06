using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Text
{
    public static class RegExtHelper
    {
        /// <summary>
        /// 用的时候要看 MatchType 这个枚举
        /// </summary>
        /// <param name="type">比对什么</param>
        /// <param name="wantToVerifyStr">要验证的字符串</param>
        /// <returns></returns>
        public static bool MatchResult(int type, string wantToVerifyStr)
        {
            bool flag = false;
            string regStr = string.Empty;

            switch (type)
            {
                case (int)MatchType.电话号码:
                    regStr = @"^(\d{3.4}-)\d{7,8}$";
                    break;
                case (int)MatchType.身份证号:
                    regStr = @"^\d{15}|\d{18}$";
                    break;
                case (int)MatchType.Email地址:
                    regStr = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                    break;
                case (int)MatchType.全是汉字:
                    regStr = @"^[\u4e00-\u9fa5]{0,}$";
                    break;
                case (int)MatchType.全是英文字符:
                    regStr = @"^[A-Za-z]+$";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(regStr))
                flag = System.Text.RegularExpressions.Regex.IsMatch(wantToVerifyStr, regStr);

            return flag;
        }

    }

    public enum MatchType
    {
        /// <summary>
        ///  正确格式：xxx/xxxx-xxxxxxx/xxxxxxxx
        /// </summary>
        电话号码 = 1,

        /// <summary>
        /// 验证身份证号（15位或18位数字)
        /// </summary>
        身份证号 = 2,

        /// <summary>
        /// 验证Email地址
        /// </summary>
        Email地址 = 3,

        全是汉字 = 4,

        全是英文字符 = 5,
    }

}
