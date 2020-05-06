using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Util.Other
{
    public class XMLHelper
    {
        #region 序列化
        public static string Serializer<T>(T t)
        {
            //StringBuilder sb = new StringBuilder();
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8))
                {
                    XmlSerializerFactory xmlSerializerFactory = new XmlSerializerFactory();
                    string name = t.GetType().Name;

                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //Add an empty namespace and empty value
                    ns.Add("", "");

                    XmlSerializer xmlSerializer = xmlSerializerFactory.CreateSerializer(typeof(T));

                    xmlSerializer.Serialize(xw, t);

                    // 去除BOM
                    byte[] buffer = ms.ToArray();

                    if (buffer.Length <= 3)
                    {
                        return Encoding.UTF8.GetString(buffer);
                    }
                    byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };
                    if (buffer[0] == bomBuffer[0] && buffer[1] == bomBuffer[1] && buffer[2] == bomBuffer[2])
                    {
                        return Encoding.UTF8.GetString(buffer, 3, buffer.Length - 3);
                    }
                    return Encoding.UTF8.GetString(buffer);
                }
            }
        }
        #endregion

        #region 反序列化
        public static T Deserialize<T>(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T obj = (T)xs.Deserialize(reader);
                return obj;
            }
        }
        #endregion
    }
}
