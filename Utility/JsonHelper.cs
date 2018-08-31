using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Utility
{
    public static class JsonHelper
    {
        /// <summary>
        ///  将对象序列化成 Json 格式字符串
        /// </summary>
        /// <param name="obj">解析的对象</param>
        /// <param name="customDateTimeFormat">是否将时间格式转化为自定义格式</param>
        /// <returns></returns>
        public static string Serialize(this object obj, bool customDateTimeFormat = true)
        {
            return customDateTimeFormat
                ? JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" })
                : JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        ///  将对象序列化成 Json 格式字符串
        /// </summary>
        /// <param name="obj">解析的对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        ///  将 Json 字符串解析成一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr)
        {
            return string.IsNullOrEmpty(jsonStr) ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}