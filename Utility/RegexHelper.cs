using System.Text.RegularExpressions;

namespace Utility
{
    public static class RegexHelper
    {
        /// <summary>
        /// 验证是否手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMobile(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            Regex reg = new Regex(@"^((\+86)|(86))?(1)\d{10}$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 验证是否IP
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIP(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            Regex reg = new Regex(@"^(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)$");
            return reg.IsMatch(value);
        }

        /// <summary>
        /// 获取仅保留中文、英文、数字的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCnEnNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            Regex reg = new Regex("[^\u4e00-\u9fa5a-zA-Z0-9]+");
            if (reg != null)
            {
                value = reg.Replace(value, "");
            }

            return value;
        }

        #region

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string Replace(this string input, string pattern, string replacement = "")
        {
            if (string.IsNullOrEmpty(input)) return "";

            return Replace(input, new Regex(pattern), replacement);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Replace(this string input, string pattern, string replacement, RegexOptions options)
        {
            if (string.IsNullOrEmpty(input)) return "";

            return Replace(input, new Regex(pattern, options), replacement);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static string Replace(this string input, string pattern, MatchEvaluator evaluator)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (evaluator == null) return input;

            return Replace(input, new Regex(pattern), evaluator);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="evaluator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Replace(this string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (evaluator == null) return input;

            return Replace(input, new Regex(pattern, options), evaluator);
        }

        #endregion

        #region

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="reg"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string Replace(this string input, Regex reg, string replacement)
        {
            if (string.IsNullOrEmpty(input)) return "";

            if (reg != null)
            {
                input = reg.Replace(input, replacement);
            }

            return input;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="reg"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static string Replace(this string input, Regex reg, MatchEvaluator evaluator)
        {
            if (string.IsNullOrEmpty(input)) return "";
            if (evaluator == null) return input;

            if (reg != null)
            {
                input = reg.Replace(input, evaluator);
            }

            return input;
        }

        #endregion
    }
}
