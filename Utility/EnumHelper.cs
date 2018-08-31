using System;

namespace Utility
{
    public class EnumHelper
    {
        /// <summary>
        /// 名称转换化枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="t">输出枚举</param>
        /// <returns>是否成功</returns>
        public static bool TryParsebyName<T>(object name, out T t)
        {
            t = default(T);

            if (StringHelper.IsObjectNullOrEmpty(name)) return false;

            return TryParsebyName(name.ToString(""), out t);
        }

        /// <summary>
        /// 名称转换化枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="t">输出枚举</param>
        /// <returns>是否成功</returns>
        public static bool TryParsebyName<T>(string name, out T t)
        {
            t = default(T);
            name = name.ToLower().Trim();
            if (string.IsNullOrEmpty(name)) return false;

            bool result = Enum.IsDefined(typeof(T), name);

            if (result)
            {
                object obj = Enum.Parse(typeof(T), name);
                if (obj is T)
                {
                    t = (T)obj;
                }
            }

            return result;
        }

        /// <summary>
        /// 值转换化枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="t">输出枚举</param>
        /// <returns>是否成功</returns>
        public static bool TryParsebyValue<T>(object value, out T t)
        {
            t = default(T);

            if (StringHelper.IsObjectNullOrEmpty(value)) return false;

            return TryParsebyValue(value.ToString(""), out t);
        }

        /// <summary>
        /// 值转换化枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="t">输出枚举</param>
        /// <returns>是否成功</returns>
        public static bool TryParsebyValue<T>(string value, out T t)
        {
            t = default(T);
            value = value.ToLower().Trim();
            if (string.IsNullOrEmpty(value)) return false;

            int i;
            if (int.TryParse(value, out i))
            {
                return TryParsebyValue(i, out t);
            }

            return false;
        }

        /// <summary>
        /// 值转换化枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="t">输出枚举</param>
        /// <returns>是否成功</returns>
        public static bool TryParsebyValue<T>(int value, out T t)
        {
            t = default(T);

            bool flag = Enum.IsDefined(typeof(T), value);

            if (flag)
            {
                object obj = Enum.Parse(typeof(T), value.ToString());
                if (obj is T)
                {
                    t = (T)obj;
                }
            }

            return flag;
        }
    }
}
