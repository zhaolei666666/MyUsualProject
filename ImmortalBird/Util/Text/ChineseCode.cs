using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Text
{
    /// <summary>
    /// �������������֤��
    /// </summary>
    public class ChineseCode
    {
        /**/
        /*
        �˺����ں��ֱ��뷶Χ���������������Ԫ�ص�ʮ�������ֽ����飬ÿ���ֽ��������һ�����֣�����
        �ĸ��ֽ�����洢��object�����С�
        ������strlength��������Ҫ�����ĺ��ָ���
        */
        private static object[] CreateRegionCode(int strlength)
        {
            //����һ���ַ������鴢�溺�ֱ�������Ԫ��
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //����һ��object��������
            object[] bytes = new object[strlength];

            /**/
            /*ÿѭ��һ�β���һ��������Ԫ�ص�ʮ�������ֽ����飬���������bject������
         ÿ���������ĸ���λ�����
         ��λ���1λ����λ���2λ��Ϊ�ֽ������һ��Ԫ��
         ��λ���3λ����λ���4λ��Ϊ�ֽ�����ڶ���Ԫ��
        */
            for (int i = 0; i < strlength; i++)
            {
                //��λ���1λ
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //��λ���2λ
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//��������������������ӱ�������ظ�ֵ
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //��λ���3λ
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //��λ���4λ
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //���������ֽڱ����洢���������������λ��
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //�������ֽڱ����洢���ֽ�������
                byte[] str_r = new byte[] { byte1, byte2 };

                //��������һ�����ֵ��ֽ��������object������
                bytes.SetValue(str_r, i);

            }

            return bytes;

        }

        public static string GetChinese(int length)
        {
            //��ȡGB2312����ҳ����
            Encoding gb = Encoding.GetEncoding("gb2312");

            //���ú�������4��������ĺ��ֱ���
            object[] bytes = CreateRegionCode(length);

            //���ݺ��ֱ�����ֽ������������ĺ���
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(gb.GetString((byte[])Convert.ChangeType(bytes[i], typeof(byte[]))));
            }
           
            //����Ŀ���̨
            return  sb.ToString();
        }      
    
    }
}
