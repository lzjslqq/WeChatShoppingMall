using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class SessionCookieHelper<T> where T : class
    {
        #region

        public static T Get(string sessionName, string cookieName, string cookieKey, string cookieIV)
        {
            if (string.IsNullOrEmpty(sessionName) ||
                string.IsNullOrEmpty(cookieName) ||
                string.IsNullOrEmpty(cookieKey) ||
                string.IsNullOrEmpty(cookieIV))
            {
                return default(T);
            }

            T t = SessionHelper<T>.Get(sessionName);

            #region Get Cookie

            if (t == null)
            {
                t = CookieHelper<T>.Get(cookieName, cookieKey, cookieIV);
            }

            #endregion

            return t;
        }

        public static void Save(T t, string sessionName, string cookieName, string cookieKey, string cookieIV)
        {
            Save(t, sessionName, cookieName, cookieKey, cookieIV, DateTime.Now.AddMonths(1));
        }

        public static void Save(T t, string sessionName, string cookieName, string cookieKey, string cookieIV, DateTime cookieExpires)
        {
            if (t == null ||
                string.IsNullOrEmpty(sessionName) ||
                string.IsNullOrEmpty(cookieName) ||
                string.IsNullOrEmpty(cookieKey) ||
                string.IsNullOrEmpty(cookieIV))
            {
                return;
            }

            SessionHelper<T>.Set(sessionName, t);

            #region Set Cookie

            CookieHelper<T>.Set(t, cookieName, cookieKey, cookieIV, cookieExpires);

            #endregion
        }

        public static void Clear(string sessionName, string cookieName)
        {
            if (!StringHelper.IsObjectNullOrEmpty(SessionHelper.Get(sessionName)))
            {
                SessionHelper.Remove(sessionName);
            }
            CookieHelper.Clear(cookieName);
        }

        #endregion
    }
}
