using System.Web;

namespace Utility
{
    public class UrlParameterHelper
    {
        #region 判断Url参数是否为空

        /// <summary>
        /// 判断Url参数是否为空（Url编码解密）
        /// true: 为空
        /// false:不为空
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value">结果</param>
        /// <returns></returns>
        public static bool IsDecodingParamsNullOrEmpty(string paramName, out string value)
        {
            value = string.Empty;
            bool flag = false;

            try
            {
                value = GetDecodingParams(paramName);
                flag = string.IsNullOrEmpty(value);
            }
            catch { }

            return flag;
        }

        /// <summary>
        /// 判断Url参数是否为空
        /// true: 为空
        /// false:不为空
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value">结果</param>
        /// <returns></returns>
        public static bool IsParamsNullOrEmpty(string paramName, out string value)
        {
            value = string.Empty;
            bool flag = false;

            try
            {
                value = GetParams(paramName);
                flag = string.IsNullOrEmpty(value);
            }
            catch { }

            return flag;
        }

        #endregion 判断Url参数是否为空

        #region 获取Url参数

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingParams(string name)
        {
            return UrlDecode(GetParams(name));
        }

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingParams(string name, string defaultValue)
        {
            return UrlDecode(GetParams(name, defaultValue));
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetParams(string name)
        {
            return GetParams(name, "");
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetParams(string name, string defaultValue)
        {
            string value = HttpContext.Current.Request.Params[name];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();
            }

            return StringHelper.FilterIllegalParameter(value);
        }

        #endregion 获取Url参数

        #region

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingForm(string name)
        {
            return UrlDecode(GetForm(name));
        }

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingForm(string name, string defaultValue)
        {
            return UrlDecode(GetForm(name, defaultValue));
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetForm(string name)
        {
            return GetForm(name, "");
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetForm(string name, string defaultValue)
        {
            string value = HttpContext.Current.Request.Form[name];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();
            }

            return StringHelper.FilterIllegalParameter(value);
        }

        #endregion

        #region

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingQueryString(string name)
        {
            return UrlDecode(GetQueryString(name));
        }

        /// <summary>
        /// 获取Url参数（Url编码解密）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDecodingQueryString(string name, string defaultValue)
        {
            return UrlDecode(GetQueryString(name, defaultValue));
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetQueryString(string name)
        {
            return GetQueryString(name, "");
        }

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetQueryString(string name, string defaultValue)
        {
            string value = HttpContext.Current.Request.QueryString[name];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();
            }

            return StringHelper.FilterIllegalParameter(value);
        }

        #endregion

        #region

        /// <summary>
        /// 加密Url参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            return HttpUtility.UrlEncode(value);
        }

        /// <summary>
        /// 解密Url参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlDecode(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            return HttpUtility.UrlDecode(value);
        }

        #endregion
    }
}