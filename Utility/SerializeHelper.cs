using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Utility
{
    public class SerializeHelper
    {
        /// <summary>
        /// 二进制序列化成一个字符串
        /// </summary>
        /// <returns>序列化代码</returns>
        public static string BinarySerialize<T>(T t)
        {
            if (t == null) return string.Empty;

            string result = string.Empty;

            BinaryFormatter ser = new BinaryFormatter();

            using (MemoryStream mStream = new MemoryStream())
            {
                ser.Serialize(mStream, t);
                byte[] buf = mStream.ToArray();
                if (buf.Length > 0)
                {
                    try
                    {
                        result = Convert.ToBase64String(buf);
                    }
                    catch { }
                }
                mStream.Close();
            }

            return result;
        }
        /// <summary>
        /// 将字符串反序列化
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value)) return default(T);

            T t = default(T);

            byte[] binary = null;

            try
            {
                binary = Convert.FromBase64String(value);
            }
            catch { }

            if (binary != null && binary.Length > 0)
            {
                BinaryFormatter ser = new BinaryFormatter();

                using (MemoryStream mStream = new MemoryStream(binary))
                {
                    object obj = ser.Deserialize(mStream);
                    if (obj is T)
                    {
                        t = (T)obj;
                    }
                    mStream.Close();
                }
            }

            return t;
        }
    }
}
