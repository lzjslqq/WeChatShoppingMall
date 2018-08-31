using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Utility
{
    public class SecurityHelper
    {
        #region 手机号码

        /// <summary>
        /// 获取加密手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncryptPhone(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length == 11)
            {
                value = value.Insert(3, "****").Remove(7, 4);
            }

            return value;
        }

        #endregion 手机号码

        #region 加密

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptSHA1(string str)
        {
            if (StringHelper.IsClearBlankNullOrEmpty(str, out str)) return "";

            SHA1 sha1 = SHA1.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] sha1Buffer = sha1.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in sha1Buffer)
            {
                sb.Append(b.ToString("x2").ToUpperInvariant());
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncryptMD5(string str)
        {
            if (StringHelper.IsClearBlankNullOrEmpty(str, out str)) return "";

            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2").ToUpperInvariant());
            }
            return sb.ToString();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptMD5(string str, string key)
        {
            if (StringHelper.IsClearBlankNullOrEmpty(str, out str)
                || StringHelper.IsClearBlankNullOrEmpty(key, out key)) return "";

            str = string.Concat(str, key);
            return EncryptMD5(str);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptMD5(string str, string separator, string key)
        {
            if (StringHelper.IsClearBlankNullOrEmpty(str, out str) || StringHelper.IsClearBlankNullOrEmpty(separator, out separator) || StringHelper.IsClearBlankNullOrEmpty(key, out key)) return "";

            str = string.Concat(str, separator, key);
            return EncryptMD5(str);
        }

        #endregion 加密

        #region

        #region

        public static string EncryptBase64XorBase64(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            return EncryptBase64(EncryptXor(EncryptBase64(value), key));
        }

        public static string DecryptBase64XorBase64(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            return DecryptBase64(EncryptXor(DecryptBase64(value), key));
        }

        public static string EncryptXor(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            try
            {
                byte[] v = Encoding.UTF8.GetBytes(value);
                byte[] t = Encoding.UTF8.GetBytes(key);

                for (int i = 0; i < v.Length; i++)
                {
                    for (int j = 0; j < t.Length; j++)
                    {
                        v[i] = Convert.ToByte(v[i] ^ t[j]);
                    }
                }

                return Encoding.UTF8.GetString(v);//.TrimEnd('\0');
            }
            catch { }

            return "";
        }

        public static string EncryptBase64(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    if (bytes != null && bytes.Length > 0)
                    {
                        return Convert.ToBase64String(bytes);
                    }
                }
            }
            catch { }

            return "";
        }

        public static string DecryptBase64(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] bytes = Convert.FromBase64String(value);
                    if (bytes != null && bytes.Length > 0)
                    {
                        return Encoding.UTF8.GetString(bytes);
                    }
                }
            }
            catch { }

            return "";
        }

        #endregion

        #region Url

        public static string EncryptBase64XorBase64Url(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            return EncryptBase64Url(EncryptXor(EncryptBase64(value), key));
        }

        public static string DecryptBase64XorBase64Url(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            return DecryptBase64(EncryptXor(DecryptBase64Url(value), key));
        }

        public static string EncryptBase64Url(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    if (bytes != null && bytes.Length > 0)
                    {
                        return Convert.ToBase64String(bytes).Replace('+', '.').Replace('/', '-').Replace('=', '_');
                    }
                }
            }
            catch { }

            return "";
        }

        public static string DecryptBase64Url(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace('_', '=').Replace('.', '+').Replace('-', '/');

                    byte[] bytes = Convert.FromBase64String(value);
                    if (bytes != null && bytes.Length > 0)
                    {
                        return Encoding.UTF8.GetString(bytes);
                    }
                }
            }
            catch { }

            return "";
        }

        #endregion

        #endregion

        #region url

        public static string EncryptUrl(string url, string key)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key)) return "";

            string result = string.Empty;

            try
            {
                result = SecurityHelper.EncryptBase64XorBase64Url(url, key);
                if (!string.IsNullOrEmpty(result))
                {
                    result = UrlParameterHelper.UrlEncode(result);
                }
            }
            catch { }

            return result;
        }

        public static string DecryptUrl(string value, string key)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key)) return "";

            string url = string.Empty;

            try
            {
                value = UrlParameterHelper.UrlDecode(value);
                if (!string.IsNullOrEmpty(value))
                {
                    url = SecurityHelper.DecryptBase64XorBase64Url(value, key);
                }
            }
            catch
            {
            }

            return url;
        }

        #endregion
    }
}