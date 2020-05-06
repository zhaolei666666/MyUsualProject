using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Util.HttpUtil
{
    public static class HttpHelper
    {
        #region 一般数据请求
        public static string WebRequest(Method method, string url, string postData)
        {
            return WebRequest(method, url, postData, Encoding.UTF8);
        }

        static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {

            return true;
        }

        public static string WebRequest(Method method, string url, string postData, Dictionary<string, dynamic> header)
        {
            return WebRequest(method, url, postData, Encoding.UTF8, header);
        }

        public static string WebRequest(Method method, string url, string postData, Encoding encoding)
        {
            return WebRequest(method, url, postData, encoding, null);
        }
        public static string WebRequest(Method method, string url, string postData, Encoding encoding, Dictionary<string, dynamic> header)
        {
            string responseData = "";
            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;


            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^https://", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (regex.IsMatch(url))
            {
                ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            }

            if (method == Method.POST || method == Method.PUT)
            {
                if (method == Method.POST)
                {
                    webRequest.ContentType = "application/form-data;charset=utf-8";
                }
                if (method == Method.PUT)
                {
                    webRequest.ContentType = "html/xml";
                }
                if (header != null)
                {
                    foreach (var item in header)
                    {
                        if (item.Key == "Date")
                        {
                            webRequest.Date = DateTime.Parse(item.Value);
                        }
                        else if (item.Key == "ContentType")
                        {
                            webRequest.ContentType = item.Value;
                        }
                        else
                        {
                            webRequest.Headers[item.Key] = item.Value;
                        }
                    }
                }
                byte[] Bytes = encoding.GetBytes(postData);
                webRequest.ContentLength = Bytes.Length;

                using (Stream requestWriter = webRequest.GetRequestStream())
                {
                    try
                    {
                        requestWriter.Write(Bytes, 0, Bytes.Length);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        requestWriter.Close();
                    }
                }
            }

            responseData = WebResponseGet(webRequest, encoding);

            webRequest = null;

            return responseData;

        }

        /// <summary>
        /// 访问指定页面地址
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string WebRequest(Method method, string url, IDictionary<string, string> parameters)
        {
            return WebRequest(method, url, ToQueryString(parameters));
        }

        public static string WebRequest(Method method, string url, IDictionary<string, string> parameters, System.Text.Encoding encoding)
        {
            return WebRequest(method, url, ToQueryString(parameters), encoding);
        }

        #endregion

        #region 带有文字和图片的请求
        /// <summary>
        /// 带有文件/图片的请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string WebRequest(Method method, string url, IDictionary<string, string> parameters, FileItem fileItem, Encoding encoding)
        {
            string responseData = "";
            HttpWebRequest webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.KeepAlive = true;
            webRequest.UserAgent = "Mozilla/5.0 (Windows; U; zh-CN)";

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

            if (method == Method.POST || method == Method.PUT)
            {
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

                using (Stream requestWriter = webRequest.GetRequestStream())
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        string start = "--" + boundary;
                        string end = start + "--";

                        for (int i = 0; i < parameters.Count; i++)
                        {
                            sb.Append(start + "\r\n");
                            sb.Append("Content-Disposition: form-data; name=\"" + parameters.ElementAt(i).Key + "\"\r\n\r\n");
                            sb.Append(parameters.ElementAt(i).Value + "\r\n");
                        }
                        sb.Append(start + "\r\n");
                        sb.Append("Content-Disposition: form-data; name=\"" + fileItem.Name + "\"; filename=\"" + fileItem.FileName + "\"\r\n");
                        sb.Append("Content-Type: " + fileItem.ContentType + "\r\n\r\n");
                        string content = sb.ToString();
                        requestWriter.Write(Encoding.UTF8.GetBytes(content), 0, content.Length);
                        requestWriter.Write(fileItem.Content, 0, fileItem.Content.Length);
                        string endContent = "\r\n" + end;
                        requestWriter.Write(Encoding.UTF8.GetBytes(endContent), 0, endContent.Length);

                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        requestWriter.Close();
                    }
                }
            }

            responseData = WebResponseGet(webRequest, encoding);

            webRequest = null;

            return responseData;
        }

        #endregion

        #region 获取返回数据
        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <param name="webRequest"></param>
        /// <returns></returns>
        private static string WebResponseGet(HttpWebRequest webRequest, System.Text.Encoding encoding)
        {
            HttpWebResponse res = null;

            try
            {
                res = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            using (StreamReader responseReader = new StreamReader(res.GetResponseStream(), encoding))
            {
                string responseData = responseReader.ReadToEnd();
                responseReader.Close();
                return responseData;
            }
        }
        #endregion

        #region 将字符串字典里的数据转换成URL查询字符串格式

        /// <summary>
        /// 将字符串字典里的数据转换成URL查询字符串格式。
        /// </summary>
        /// <param name="dict">字符串字典。</param>
        /// <returns></returns>
        public static string ToQueryString(IDictionary<string, string> dict)
        {
            if (dict == null)
                return string.Empty;

            if (dict.Count == 0)
                return string.Empty;

            var buffer = new StringBuilder();
            var count = 0;
            var end = false;

            foreach (var key in dict.Keys)
            {
                if (count == dict.Count - 1) end = true;

                if (end)
                    buffer.AppendFormat("{0}={1}", key, dict[key]);
                else
                    buffer.AppendFormat("{0}={1}&", key, dict[key]);

                count++;
            }

            return buffer.ToString();
        }

        #endregion
    }

    public enum Method { GET, POST, PUT, DELETE };

    #region 文件信息
    public class FileItem
    {
        public string ContentType
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public byte[] Content
        {
            get;
            set;
        }
    }
    #endregion 
}
