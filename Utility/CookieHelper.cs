using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Utility
{
    /// <summary>
    /// 对cookie操作进行封装
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 清除某个Cookie
        /// </summary>
        /// <param name="name">Cookie名称</param>
        public static void Clear(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取某个Cookie值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <returns></returns>
        public static string Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            string str = string.Empty;
            if (cookie != null)
            {
                str = HttpUtility.UrlDecode(cookie.Value);
            }

            return str;
        }

        /// <summary>
        /// 获取Cookie键值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <returns></returns>
        public static NameValueCollection GetCollection(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            NameValueCollection result = null;
            if (cookie != null)
            {
                result = cookie.Values;
            }

            return result;
        }

        /// <summary>
        /// 获取某个Cookie值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="cookieValues">Cookie值</param>
        /// <returns></returns>
        public static bool Get(string name, out string value)
        {
            value = string.Empty;
            bool flag = false;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (flag = (cookie != null))
            {
                value = cookie.Value;
            }

            return flag;
        }

        /// <summary>
        /// 获取Cookie键值
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="values">Cookie键值</param>
        /// <returns></returns>
        public static bool Get(string name, out NameValueCollection values)
        {
            values = null;
            bool flag = false;

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (flag = (cookie != null))
            {
                values = cookie.Values;
            }

            return flag;
        }

        /// <summary>
        /// 添加Cookie（24小时过期）
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="value">Cookie值</param>
        public static void Set(string name, string value)
        {
            Set(name, value, DateTime.Now.AddDays(1.0));
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="value">Cookie值</param>
        /// <param name="expires">过期时间</param>
        public static void Set(string name, string value, DateTime expires)
        {
            if (string.IsNullOrEmpty(name)) return;

            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = HttpUtility.UrlEncode(value, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="values">Cookie键值</param>
        /// <param name="expires">过期时间</param>
        public static void Set(string name, NameValueCollection values)
        {
            Set(name, values, DateTime.Now.AddDays(1.0));
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="name">Cookie名称</param>
        /// <param name="values">Cookie键值</param>
        /// <param name="expires">过期时间</param>
        public static void Set(string name, NameValueCollection values, DateTime expires)
        {
            if (string.IsNullOrEmpty(name)) return;

            HttpCookie cookie = new HttpCookie(name);
            cookie.Values.Add(values);
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public class CookieHelper<T> where T : class
    {
        public static T Get(string name, string key, string IV)
        {
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(key) ||
                string.IsNullOrEmpty(IV))
            {
                return default(T);
            }

            T t = default(T);

            try
            {
                string value = CookieHelper.Get(name);
                if (!string.IsNullOrEmpty(value))
                {
                    value = SecurityHelper.DecryptBase64(value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = AESHelper.AESDecrypt(value, key, IV);
                        if (!string.IsNullOrEmpty(value))
                        {
                            t = SerializeHelper.DeSerialize<T>(value);
                        }
                    }
                }
            }
            catch { }

            return t;
        }

        public static void Set(T t, string name, string key, string IV, DateTime cookieExpires)
        {
            if (t == null ||
                string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(key) ||
                string.IsNullOrEmpty(IV))
            {
                return;
            }

            try
            {
                string value = SerializeHelper.BinarySerialize<T>(t);
                if (!string.IsNullOrEmpty(value))
                {
                    value = AESHelper.AESEncrypt(value, key, IV);
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = SecurityHelper.EncryptBase64(value);
                        if (!string.IsNullOrEmpty(value))
                        {
                            CookieHelper.Set(name, value, cookieExpires);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
