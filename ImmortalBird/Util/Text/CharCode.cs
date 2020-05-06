using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Text
{
    public class CharCode
    {
        public static string GetRandomChar(int length)
        {
            StringBuilder randomTextBuilder = new StringBuilder(length);
            Random random = new Random();
            string TextChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < length; i++)
            {
                randomTextBuilder.Append(TextChars.Substring(random.Next(TextChars.Length), 1));
            }

            return randomTextBuilder.ToString();

        }
    }
}
