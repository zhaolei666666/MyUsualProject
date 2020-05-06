using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Util.Text
{
    public class UnicodeToChinese
    {
        public static string Decode(string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
                return string.Empty;

            string outStr = unicodeString;

            Regex re = new Regex("\\\\u[0123456789abcdef]{4}|&#x[0123456789abcdef]{4}|&#[0123456789]{5}", RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(unicodeString);
            
            foreach (Match ma in mc)
            {
                outStr = outStr.Replace(ma.Value, ConverUnicodeStringToChar(ma.Value).ToString());
            }
            return outStr;
        }
        private static char ConverUnicodeStringToChar(string str)
        {
            char outStr = Char.MinValue;
            if (str.StartsWith("&#x")) //16进制
                outStr = (char)int.Parse(str.Remove(0, 3), System.Globalization.NumberStyles.HexNumber);
            else if (str.StartsWith("\\u")) //16进制
                outStr = (char)int.Parse(str.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            else //10进制
                outStr = (char)int.Parse(str.Remove(0, 2), System.Globalization.NumberStyles.Integer);
            return outStr;
        }
    }
}
