using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Util.HttpUtil
{
    public class HttpHelperRaw
    {
        public static string HttpPost(string url, string postDataStr)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream requestStream = req.GetRequestStream();

                StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("utf-8"));

                streamWriter.Write(postDataStr);
                streamWriter.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resStream = res.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, Encoding.GetEncoding("utf-8"));

                string resString = sr.ReadToEnd();
                sr.Close();
                resStream.Close();

                return resString;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return str;
            }

        }

        public static string HttpGet(string url, string getDataStr)
        {
            try
            {
                string getUrl = url + (getDataStr == "" ? "" : "?") + getDataStr;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(getUrl);
                req.Method = "GET";
                req.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                Stream s = res.GetResponseStream();

                StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8"));

                string resultStr = sr.ReadToEnd();

                sr.Close();
                s.Close();

                return resultStr;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return str;
            }

        }
    }
}
