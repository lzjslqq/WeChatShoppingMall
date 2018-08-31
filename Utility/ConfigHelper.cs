using System;
using System.Configuration;

namespace Utility
{
    /// <summary>
    /// web.config操作类
    /// </summary>
    public class ConfigHelper
    {
        public static T GetSection<T>(string sectionName) where T : class
        {
            return ConfigurationManager.GetSection(sectionName).To<T>();
        }

        #region

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        /// <summary>
        /// 得到AppSettings中的配置bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(string key)
        {
            var value = GetString(key);

            if (string.IsNullOrEmpty(value)) return false;

            return value.ToBool();
        }

        /// <summary>
        /// 得到AppSettings中的配置decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            var value = GetString(key);

            if (string.IsNullOrEmpty(value)) return 0;

            return value.ToDecimal();
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            var value = GetString(key);

            if (string.IsNullOrEmpty(value)) return 0;

            int i = 0;
            if (int.TryParse(value, out i))
            {
                return i;
            }

            return defaultValue;
        }

        #endregion
    }
}