using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Other
{
    public static class IDCardHelper
    {
        #region 检测身份证的合法性
        public static bool Check(string cardId)
        {
            string num = cardId.ToUpper();
            int[] factorArr = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            char[] parityBit = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            char[] varArray = new char[18];
            var lngProduct = 0;
            int intCheckDigit = 0;
            var intStrLen = num.Length;
            var idNumber = num;
            if ((intStrLen != 15) && (intStrLen != 18))
            {
                return false;
            }

            for (int i = 0; i < intStrLen; i++)
            {
                varArray[i] = (char)Convert.ToInt32(idNumber[i]);
                if (((varArray[i] - 48) < 0 || (varArray[i] - 48) > 9) && (i != 17))
                {
                    return false;
                }
                else if (i < 17)
                {
                    varArray[i] = (char)((varArray[i] - 48) * factorArr[i]);
                }
            }
            if (intStrLen == 18)
            {
                var date8 = idNumber.Substring(6, 8);
                if (isDate8(date8) == false)
                {
                    return false;
                }
                for (int i = 0; i < 17; i++)
                {
                    lngProduct = lngProduct + varArray[i];
                }
                intCheckDigit = parityBit[lngProduct % 11];
                if (varArray[17] != intCheckDigit)
                {
                    return false;
                }
            }
            else
            {
                var date6 = idNumber.Substring(6, 6);
                if (isDate6(date6) == false)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 位身份证日期检测--年/月--6位
        /// <summary>
        /// 15位身份证日期检测--年/月--6位
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        static bool isDate6(string sDate)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(sDate, "^[0-9]{6}$"))
            {
                return false;
            }
            int year = Convert.ToInt32(sDate.Substring(0, 4));
            int month = Convert.ToInt32(sDate.Substring(4, 2));
            if (year < 1700 || year > 2500) return false;
            if (month < 1 || month > 12) return false;
            return true;
        }
        #endregion

        #region 18位身份证日期检测--年/月/日--8位
        /// <summary>
        /// 18位身份证日期检测--年/月/日--8位
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        static bool isDate8(string sDate)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(sDate, "^[0-9]{8}$"))
            {
                return false;
            }
            int year = Convert.ToInt32(sDate.Substring(0, 4));
            int month = Convert.ToInt32(sDate.Substring(4, 2));
            int day = Convert.ToInt32(sDate.Substring(6, 2));

            int[] iaMonthDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (year < 1700 || year > 2500) return false;
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) iaMonthDays[1] = 29;
            if (month < 1 || month > 12) return false;
            if (day < 1 || day > iaMonthDays[month - 1]) return false;
            return true;
        }
        #endregion

        #region 根据身份证号码获取年龄、生日
        public static DateTime? GetBirthday(string cardId)
        {
            bool result = Check(cardId);
            if (result)
            {
                int year = Convert.ToInt32(cardId.Substring(6, 4));
                int month = Convert.ToInt32(cardId.Substring(10, 2));
                int day = Convert.ToInt32(cardId.Substring(12, 2));
                return new DateTime(year, month, day);
            }
            return null;
        }
        #endregion
    }
}
