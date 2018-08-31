using System;
using System.Web;

namespace Utility
{
    public class SessionHelper
    {
        /// <summary>
        /// 添加Session，有效期为20分钟
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        public static void Set(string name, object value)
        {
            if (string.IsNullOrEmpty(name)) return;

            HttpContext.Current.Session[name] = value;
            HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        /// <param name="timeout">有效期（分钟）</param>
        public static void Set(string name, object value, int timeout)
        {
            if (string.IsNullOrEmpty(name)) return;

            HttpContext.Current.Session[name] = value;
            HttpContext.Current.Session.Timeout = timeout;
        }

        /// <summary>
        /// 读取某个Session值
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns>Session值</returns>
        public static object Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 删除某个Session
        /// </summary>
        /// <param name="name">Session名称</param>
        public static void Remove(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            HttpContext.Current.Session[name] = null;
        }

        /// <summary>
        /// 清除所有Session
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }

    public class SessionHelper<T> where T : class
    {
        public static T Get(string name)
        {
            T t = default(T);

            object obj = SessionHelper.Get(name);
            if (obj != null && obj is T)
            {
                t = (T)obj;
            }

            return t;
        }

        public static void Set(string name, T t)
        {
            if (!string.IsNullOrEmpty(name) && t != null)
            {
                SessionHelper.Set(name, t);
            }
        }
    }

    public class ReturnUrlHelper
    {
        public static string GetSession(string name, string state)
        {
            string value = StringHelper.ToString(SessionHelper.Get(name));
            if (!string.IsNullOrEmpty(value))
            {
                string[] list = value.Split('@');
                if (!list.IsNullOrEmpty() && list.Length == 2
                    && !string.IsNullOrEmpty(state) && string.Compare(state, list[0], true) == 0)
                {
                    return list[1];
                }
            }

            return "";
        }

        public static void SetSession(string name, string state, string returnUrl)
        {
            if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(returnUrl))
            {
                string value = string.Concat(state, "@", returnUrl);
                SessionHelper.Set(name, value);
            }
        }
    }
}