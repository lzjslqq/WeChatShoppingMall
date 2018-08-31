using System.Web;

namespace Utility
{
    public class JsHelper
    {
        private const string ScriptBegin = "<script language='javascript' type='text/javascript'>";
        private const string ScriptEnd = "</script>";

        #region

        public static void RunScript(string js)
        {
            if (string.IsNullOrEmpty(js)) return;

            string value = string.Concat(ScriptBegin, js, ScriptEnd);

            if (!string.IsNullOrEmpty(value))
            {
                HttpContext.Current.Response.Write(value);
            }
        }

        public static void Alert(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            string js = string.Concat("alert('", message, "');");

            RunScript(js);
        }

        public static void Alert(string message, string url)
        {
            if (string.IsNullOrEmpty(message)) return;

            string js = string.Concat("alert('", message, "');");

            if (!string.IsNullOrEmpty(url))
            {
                js = string.Concat(js, "window.location.replace('", url, "');");
            }

            RunScript(js);
        }

        public static void WindowTopLocation(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            string js = string.Concat("window.top.location.replace('", url, "');");

            RunScript(js);
        }

        public static void WindowOpen(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            string js = string.Concat("window.open('", url, "');");

            RunScript(js);
        }

        #endregion

        #region

        public static void EndScript(string js)
        {
            if (string.IsNullOrEmpty(js)) return;

            string value = string.Concat(ScriptBegin, js, ScriptEnd);

            if (!string.IsNullOrEmpty(value))
            {
                HttpContext.Current.Response.Write(value);
                HttpContext.Current.Response.End();
            }
        }

        public static void EndAlert(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            string js = string.Concat("alert('", message, "');");

            EndScript(js);
        }

        public static void EndAlert(string message, string url)
        {
            if (string.IsNullOrEmpty(message)) return;

            string js = string.Concat("alert('", message, "');");

            if (!string.IsNullOrEmpty(url))
            {
                js = string.Concat(js, "window.location.replace('", url, "');");
            }

            EndScript(js);
        }

        public static void EndWindowTopLocation(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            string js = string.Concat("window.top.location.replace('", url, "');");

            EndScript(js);
        }

        public static void EndWindowOpen(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            string js = string.Concat("window.open('", url, "');");

            EndScript(js);
        }

        #endregion
    }
}
