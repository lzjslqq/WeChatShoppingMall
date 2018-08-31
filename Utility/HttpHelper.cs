using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Utility
{
    public class HttpHelper
    {
        #region

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">Get/Post</param>
        /// <param name="result">返回结果</param>
        /// <returns></returns>
        public static bool Send(string url, string method, out string result)
        {
            bool flag = false;

            result = string.Empty;

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(method)) return flag;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.KeepAlive = false;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                        flag = true;
                    }
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flag;
        }

        #endregion

        #region

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="sendInfo">请求对象</param>
        /// <param name="HttpReceiveInfo">返回结果</param>
        /// <returns></returns>
        public static bool Send(HttpSendInfo sendInfo, out HttpReceiveInfo receiveInfo)
        {
            bool flag = false;

            receiveInfo = new HttpReceiveInfo();

            string url = sendInfo.Url;

            if (sendInfo == null || string.IsNullOrEmpty(url)) return flag;

            try
            {
                #region

                if (sendInfo.IsHttps)
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteCertificateValidationCallback);
                }

                #endregion

                #region 创建httpWebRequest对象

                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;
                if (httpRequest == null)
                {
                    throw new ArgumentNullException(
                        string.Format("Invalid url string: {0}", url)
                        );
                }

                #endregion

                #region 填充httpWebRequest的基本信息

                httpRequest.UserAgent = sendInfo.UserAgent;
                httpRequest.ContentType = sendInfo.ContentType;
                httpRequest.Method = sendInfo.Method;

                #endregion

                #region 填充要post的内容

                string data = sendInfo.Data;
                if (!string.IsNullOrEmpty(data))
                {
                    Encoding encoding = Encoding.GetEncoding(sendInfo.RequestEncoding);
                    byte[] byteArray = encoding.GetBytes(data);
                    httpRequest.ContentLength = byteArray.Length;
                    using (Stream stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }
                }

                #endregion

                #region 发送post请求到服务器并读取服务器返回信息

                using (HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(sendInfo.ResponseEncoding)))
                    {
                        receiveInfo.Result = reader.ReadToEnd();
                        flag = true;
                    }
                }

                if (httpRequest != null)
                {
                    httpRequest.Abort();
                }

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flag;
        }

        public static bool RemoteCertificateValidationCallback(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        }

        #endregion

        #region

        /// <summary>
        /// 获取参数，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <param name="method">GET/POST</param>
        /// <returns></returns>
        public static SortedDictionary<string, string> GetRequest(string method = "GET")
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            NameValueCollection coll = null;
            if (string.Compare(method, "POST", true) == 0)
            {
                coll = HttpContext.Current.Request.Form;
            }
            else
            {
                coll = HttpContext.Current.Request.QueryString;
            }

            if (coll != null)
            {
                string[] keys = coll.AllKeys;
                if (keys != null && keys.Length > 0)
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        sArray.Add(keys[i], UrlParameterHelper.GetForm(keys[i]));
                    }
                }
            }

            return sArray;
        }
        
        #endregion
    }

    public class HttpSendInfo
    {
        private string _userAgent = "";
        /// <summary>
        /// User Agent
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }
        private string _contentType = "application/json";
        /// <summary>
        /// Content Type
        /// </summary>
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }
        private string _requestEncoding = "utf-8";
        /// <summary>
        /// Request Encoding
        /// </summary>
        public string RequestEncoding
        {
            get { return _requestEncoding; }
            set { _requestEncoding = value; }
        }
        private string _responseEncoding = "utf-8";
        /// <summary>
        /// Response Encoding
        /// </summary>
        public string ResponseEncoding
        {
            get { return _responseEncoding; }
            set { _responseEncoding = value; }
        }
        private string _data;
        /// <summary>
        /// 请求数据
        /// param1=&param2=&param3=
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
        private string _url;
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _method = "Post";
        /// <summary>
        /// Get/Post
        /// </summary>
        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }
        private bool isHttps = false;
        /// <summary>
        /// Https/Http
        /// </summary>
        public bool IsHttps
        {
            get { return isHttps; }
            set { isHttps = value; }
        }
    }

    public class HttpReceiveInfo
    {
        private string _result;
        /// <summary>
        /// 返回结果
        /// </summary>
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
