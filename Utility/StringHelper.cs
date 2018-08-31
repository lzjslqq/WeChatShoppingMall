using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using MSA = Microsoft.Security.Application;

namespace Utility
{
    public static class StringHelper
    {
        #region PrefixUrl

        public static string PrefixUrl = ConfigHelper.GetString("PrefixUrl");
        public static string AudioPrefixUrl = ConfigHelper.GetString("AudioPrefixUrl");
        public static string CartoonPrefixUrl = ConfigHelper.GetString("CartoonPrefixUrl");

        #endregion PrefixUrl

        #region 类型转换

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(this object obj, string defaultValue = "")
        {
            if (obj == null) return defaultValue;

            return obj.ToString();
        }

        /// <summary>
        /// 转换为布尔类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            if (obj == null) return false;

            bool i;
            if (bool.TryParse(ToString(obj), out i))
            {
                return i;
            }

            return false;
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            if (obj == null) return 0;

            int i;
            if (int.TryParse(ToString(obj), out i))
            {
                return i;
            }

            return 0;
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this int? obj)
        {
            if (obj == null) return 0;

            if (obj.HasValue)
            {
                return obj.Value;
            }

            return 0;
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(this object obj)
        {
            if (obj == null) return 0;

            long i;
            if (long.TryParse(ToString(obj), out i))
            {
                return i;
            }

            return 0;
        }

        /// <summary>
        /// 转换为浮点型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            if (obj == null) return 0;

            double i = 0;
            if (double.TryParse(ToString(obj), out i))
            {
                return i;
            }

            return 0;
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            if (obj == null) return 0;

            decimal i;
            if (decimal.TryParse(ToString(obj), out i))
            {
                return i;
            }

            return 0;
        }

        /// <summary>
        /// 转换为时间格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            DateTime dt = Convert.ToDateTime("01/01/1900");

            if (IsObjectNullOrEmpty(obj)) return dt;

            if (DateTime.TryParse(ToString(obj), out dt))
            {
                return dt;
            }

            return Convert.ToDateTime("01/01/1900");
        }

        /// <summary>
        /// 转化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this object obj) where T : class
        {
            T t = default(T);

            if (obj != null && obj is T)
            {
                t = (T)obj;
            }

            return t;
        }

        #endregion 类型转换

        #region 类型转换

        /// <summary>
        /// 转换为布尔类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsBool(this object obj, out bool result)
        {
            result = false;

            if (IsObjectNullOrEmpty(obj)) return false;

            return bool.TryParse(ToString(obj), out result);
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsInt(this object obj, out int result)
        {
            result = 0;

            if (IsObjectNullOrEmpty(obj)) return false;

            return int.TryParse(ToString(obj), out result);
        }

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToInt(this int? obj, out int result)
        {
            result = 0;

            if (obj == null) return false;

            bool flag = false;
            if (flag = obj.HasValue)
            {
                result = obj.Value;
            }

            return flag;
        }

        /// <summary>
        /// 转换为长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsLong(this object obj, out long result)
        {
            result = 0;

            if (IsObjectNullOrEmpty(obj)) return false;

            return long.TryParse(ToString(obj), out result);
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object obj, out decimal result)
        {
            result = 0;

            if (IsObjectNullOrEmpty(obj)) return false;

            return decimal.TryParse(ToString(obj), out result);
        }

        /// <summary>
        /// 转换为时间格式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsDateTime(this object obj, out DateTime result)
        {
            result = Convert.ToDateTime("01/01/1900");

            if (IsObjectNullOrEmpty(obj)) return false;

            return DateTime.TryParse(ToString(obj), out result);
        }

        /// <summary>
        /// 转化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool Is<T>(this object obj, out T result) where T : class
        {
            result = default(T);

            if (IsObjectNullOrEmpty(obj)) return false;

            bool flag = false;

            if (flag = (obj != null && obj is T))
            {
                result = (T)obj;
            }

            return flag;
        }

        #endregion 类型转换

        #region 判断是否为空

        /// <summary>
        /// 判断Object是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsObjectNullOrEmpty(this object obj)
        {
            return (obj == null || string.IsNullOrEmpty(ToString(obj)));
        }

        /// <summary>
        /// 先清除空格，再判断String是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsClearBlankNullOrEmpty(this string value, out string result)
        {
            result = value.Trim();

            return string.IsNullOrEmpty(result);
        }

        /// <summary>
        /// 判断DataSet是否为空
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataSet ds)
        {
            return (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0);
        }

        /// <summary>
        /// 判断DataTable是否为空
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            return (dt == null || dt.Rows.Count == 0);
        }

        /// <summary>
        /// 判断DataRow是否为空
        /// </summary>
        /// <param name="drs"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataRow[] drs)
        {
            return (drs == null || drs.Length == 0);
        }

        /// <summary>
        /// 判断string[]是否为空
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string[] list)
        {
            return (list == null || list.Length == 0);
        }

        /// <summary>
        /// 判断T是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T list) where T : class
        {
            return (list == null);
        }

        /// <summary>
        /// 判断IList是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return (list == null || list.Count == 0);
        }

        /// <summary>
        /// 判断IEnumerable是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return (list == null || list.Count() == 0);
        }

        /// <summary>
        /// 判断IDictionary是否为空
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(this IDictionary<K, V> dict)
        {
            return (dict == null || dict.Count == 0);
        }

        /// <summary>
        /// 判断SortedDictionary是否为空
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(this SortedDictionary<K, V> dict)
        {
            return (dict == null || dict.Count == 0);
        }

        /// <summary>
        /// 判断是否泛型默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }

        #endregion 判断是否为空

        #region 字符串格式

        public static string ToLowerTrim(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";

            return value.ToLower().Trim();
        }

        public static string ToLowerTrim(this object obj)
        {
            if (IsObjectNullOrEmpty(obj)) return "";

            return ToString(obj).ToLower().Trim();
        }

        /// <summary>
        /// 字符串结尾添加符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">当value为空时，显示默认值</param>
        /// <param name="mark">符号</param>
        /// <returns></returns>
        public static string SpliceEndString(this string value, string defaultValue, string mark)
        {
            value = value.Trim();
            mark = mark.Trim();

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(mark))
            {
                value = value.EndsWith(mark) ? value : string.Concat(value, mark);
            }

            return value;
        }

        #endregion 字符串格式

        #region 去掉小数点后的0和.

        /// <summary>
        /// 去掉小数点后的0和.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal SubZeroAndDot(this object obj)
        {
            if (obj == null) return 0;

            return SubZeroAndDot(ToString(obj));
        }

        /// <summary>
        /// 去掉小数点后的0和.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal SubZeroAndDot(this string value)
        {
            value = value.Trim();

            if (string.IsNullOrEmpty(value)) return 0;

            if (value.IndexOf(".", StringComparison.Ordinal) > 0)
            {
                return ToDecimal(value.TrimEnd('0').TrimEnd('.'));
            }
            return ToDecimal(value);
        }

        #endregion 去掉小数点后的0和.

        #region 截取字符串

        /// <summary>
        /// 截取字符串长度（字符）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(this string value, int length)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return "";

            if (value.Length > length)
            {
                value = value.Substring(0, length);
            }

            return value;
        }

        /// <summary>
        /// 截取字符串长度（字符）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(this string value, int length, bool Addpoint)
        {
            if (string.IsNullOrEmpty(value) || length <= 0) return "";

            if (value.Length > length)
            {
                value = value.Substring(0, length);

                if (Addpoint)
                {
                    value += "...";
                }
            }

            return value;
        }

        /// <summary>
        /// 截取字符串长度(转换bit)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="addpoint"></param>
        /// <returns></returns>
        public static string SubString(this string value, int length, bool addpoint)
        {
            #region 截取字符串长度(转换bit)

            if (string.IsNullOrEmpty(value) || length <= 0) return "";

            ASCIIEncoding ascii = new ASCIIEncoding();

            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(value);

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                try
                {
                    tempString += value.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > length)
                    break;
            }

            //如果截过则加上半个省略号
            if (addpoint)
            {
                byte[] mybyte = Encoding.Default.GetBytes(value);

                if (mybyte.Length > length)
                {
                    tempString += "...";
                }
            }

            return tempString;

            #endregion 截取字符串长度(转换bit)
        }

        #endregion 截取字符串

        #region 获取手机号码或IP、RemoteHost、ReferUrl、UserAgent

        /// <summary>
        /// 获取ctwap,ctnet或wifi下手机号码或IP
        /// </summary>
        /// <param name="netType"></param>
        /// <returns></returns>
        public static string GetMobileOrIP(out string netType)
        {
            netType = "WIFI";

            string mobile = GetMobile();

            if (!string.IsNullOrEmpty(mobile))
            {
                mobile = mobile.Length > 11 ? mobile.Substring(mobile.Length - 11) : mobile;

                if (mobile.IsMobile())
                {
                    netType = "mobile";
                }
            }

            if (string.Compare(netType, "WIFI", true) == 0)
            {
                mobile = GetIP();
            }

            return mobile;
        }

        /// <summary>
        /// 获取手机号码
        /// </summary>
        public static string GetMobile()
        {
            string value = string.Empty;

            if (HttpContext.Current.Request.Headers["x-up-calling-line-id"] != null && HttpContext.Current.Request.Headers["x-up-calling-line-id"] != "")
            {
                value = HttpContext.Current.Request.Headers["X-Up-Calling-Line-Id"];

                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Trim().TrimStart('+').TrimStart('8').TrimStart('6');
                }
            }

            return value;
        }

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(IP))
            {
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (!RegexHelper.IsIP(IP))
            {
                return "";
            }

            return IP;
        }

        /// <summary>
        /// Remote Host
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteHost()
        {
            string value = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            return !string.IsNullOrEmpty(value) ? value : "";
        }

        /// <summary>
        /// 来源地址
        /// </summary>
        /// <returns></returns>
        public static string GetReferUrl(int length = 0)
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                string url = ToString(HttpContext.Current.Request.UrlReferrer);
                if (!string.IsNullOrEmpty(url))
                {
                    if (length > 0)
                    {
                        return CutString(url, length);
                    }
                    else
                    {
                        return url;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 获取User Agent
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent(int length = 0)
        {
            if (HttpContext.Current.Request.UserAgent != null)
            {
                string ua = ToString(HttpContext.Current.Request.UserAgent);
                if (!string.IsNullOrEmpty(ua))
                {
                    if (length > 0)
                    {
                        return SubString(ua, length, false);
                    }
                    else
                    {
                        return ua;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// UC IMSI
        /// </summary>
        /// <returns></returns>
        public static string GetUCIMSI()
        {
            string Imsi_aes = ""; //手机IMSI信息
            try
            {
                //获取原始URL
                string GetUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString();

                //首先对原始URL进行解码
                string GetUrl_Decoding = UCWebSecret.url_decode(GetUrl);

                if (GetUrl_Decoding.IndexOf("ucstr=") > 0)
                {
                    //截取ucstr参数值
                    string GetUrl_SubStr = UCWebSecret.strSubstring(GetUrl_Decoding, "ucstr=", 1);

                    //获取IMSI参数值
                    string GetSi = null;
                    string[] Get_ucstr = Regex.Split(GetUrl_SubStr, ";", RegexOptions.IgnoreCase); //ucstr拆分为数组
                    foreach (string i in Get_ucstr)
                    {
                        if (i.IndexOf("si") == 0)  //取得其中的si值
                        {
                            GetSi = UCWebSecret.strSubstring(i, "si", 3);
                        }
                    }

                    if (GetSi != "")
                    {
                        //对取得的IMSI值进行Base64解码
                        string Imsi_base64 = UCWebSecret.Base64DeCode(GetSi);

                        //再进行aes解码
                        string Imsi_key = "UCADS!@qkl";//设置密匙
                        byte[] Imsi_Iv = new byte[16]; //设置密匙向量16位
                        Imsi_aes = UCWebSecret.AESDecrypt(Imsi_base64, Imsi_key, Imsi_Iv);
                    }
                }
            }
            catch { }

            return Imsi_aes;
        }

        #region 获取本机IP

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;

            string hostName = Dns.GetHostName();
            if (!string.IsNullOrEmpty(hostName))
            {
                IPHostEntry ipHost = Dns.GetHostEntry(hostName);
                if (ipHost != null)
                {
                    IPAddress[] ipList = ipHost.AddressList;
                    if (ipList != null && ipList.Length > 0)
                    {
                        foreach (IPAddress _IPAddress in ipList)
                        {
                            if (_IPAddress != null && string.Compare(_IPAddress.AddressFamily.ToString(), "InterNetwork", true) == 0)
                            {
                                AddressIP = _IPAddress.ToString();
                                break;
                            }
                        }
                    }
                }
            }

            return AddressIP;
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <param name="IP">默认IP</param>
        /// <returns></returns>
        public static string GetLocalAddressIP(string IP)
        {
            string AddressIP = GetLocalAddressIP();

            if (string.IsNullOrEmpty(AddressIP))
            {
                AddressIP = IP;
            }

            return AddressIP;
        }

        #endregion 获取本机IP

        #region

        public static string GetHost(string host = "")
        {
            try
            {
                if (string.IsNullOrEmpty(host))
                {
                    host = HttpContext.Current.Request.Url.Host;
                }
            }
            catch { }

            return host;
        }

        #endregion 获取手机号码或IP、RemoteHost、ReferUrl、UserAgent

        #endregion 获取手机号码或IP、RemoteHost、ReferUrl、UserAgent

        #region CookieId

        /// <summary>
        /// 获取CookieId
        /// 有效期30分钟
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <returns></returns>
        public static string GetCookieId(string cookieName)
        {
            if (string.IsNullOrEmpty(cookieName)) return "";

            return GetCookieId(cookieName, DateTime.Now.AddMinutes(30));
        }

        /// <summary>
        /// 获取CookieId
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="expires">DateTime.Now.AddMinutes(30)</param>
        /// <returns></returns>
        public static string GetCookieId(string cookieName, DateTime expires)
        {
            if (string.IsNullOrEmpty(cookieName)) return "";

            string cookieId = CookieHelper.Get(cookieName);

            if (string.IsNullOrEmpty(cookieId))
            {
                cookieId = Guid.NewGuid().ToString("N");

                CookieHelper.Set(cookieName, cookieId, expires);
            }

            return cookieId;
        }

        /// <summary>
        /// 获取CookieId
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="name">键值名称</param>
        /// <param name="expires">DateTime.Now.AddMinutes(30)</param>
        /// <returns></returns>
        public static string GetCookieId(string cookieName, string name, DateTime expires)
        {
            if (string.IsNullOrEmpty(cookieName) || string.IsNullOrEmpty(name)) return "";

            return GetCookieId(cookieName, name, "", expires);
        }

        /// <summary>
        /// 获取CookieId
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="name">键值名称</param>
        /// <param name="defaultValue"></param>
        /// <param name="expires">DateTime.Now.AddMinutes(30)</param>
        /// <returns></returns>
        public static string GetCookieId(string cookieName, string name, string defaultValue, DateTime expires)
        {
            if (string.IsNullOrEmpty(cookieName) || string.IsNullOrEmpty(name)) return "";

            string cookieId = string.Empty;

            NameValueCollection values = new NameValueCollection();
            if (CookieHelper.Get(cookieName, out values))
            {
                cookieId = values[name];
            }
            else if (!string.IsNullOrEmpty(defaultValue))
            {
                cookieId = defaultValue;
            }
            else
            {
                cookieId = Guid.NewGuid().ToString("N");
                values = new NameValueCollection();
                values.Add(name, cookieId);
                CookieHelper.Set(cookieName, values, expires);
            }

            return cookieId;
        }

        #endregion CookieId

        #region 时间

        /// <summary>
        /// DateTime时间格式转换为毫秒
        /// </summary>
        /// <param name="dateTime"> DateTime时间格式</param>
        /// <returns>毫秒</returns>
        public static long ConvertMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="dateTime"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertTimeStamp(this DateTime dateTime)
        {
            return (int)(dateTime - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime ConvertDateTime(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            try
            {
                long ticks = 0;
                if (long.TryParse(string.Concat(timeStamp, "0000000"), out ticks))
                {
                    TimeSpan toNow = new TimeSpan(ticks);
                    return dtStart.Add(toNow);
                }
            }
            catch { }

            return dtStart;
        }

        #region 时间变量+1天

        /// <summary>
        /// 时间变量+1天
        /// </summary>
        /// <param name="dateTime">时间变量</param>
        /// <param name="value">天数（+1）</param>
        /// <returns></returns>
        public static string AddDays(this string dateTime, double value = 1)
        {
            DateTime dt = new DateTime();

            if (DateTime.TryParse(dateTime, out dt))
            {
                return dt.AddDays(value).ToString();
            }

            return "";
        }

        #endregion 时间变量+1天

        #endregion 时间

        #region Url

        public static string GetPrefixUrl(this string url, string prefixUrl)
        {
            url = url.Trim();

            if (string.IsNullOrEmpty(url)) return "";

            if (url.ToLower().StartsWith("http://") || url.ToLower().StartsWith("https://")) return url;

            return FileHelper.MergePath("/", new string[] { prefixUrl, url });
        }

        /// <summary>
        /// 获取带有随机数参数的Url
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string GetRandomUrl(this string url)
        {
            if (string.IsNullOrEmpty(url)) return "";

            string paramText = "rnd";
            string paramValue = new Random().Next(10000, 100000).ToString();

            return SpliceUrl(url, paramText, paramValue);
        }

        /// <summary>
        /// 获取带有返回参数的Url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="returnUrl">未通过UrlEncode的参数值</param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public static string GetReturnUrl(this string url, string returnUrl = "", string channelId = "")
        {
            if (string.IsNullOrEmpty(url)) return "";

            if (!string.IsNullOrEmpty(channelId))
            {
                url = url.GetChannelRouteUrl(channelId);
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                try
                {
                    returnUrl = HttpContext.Current.Request.RawUrl;
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = UrlParameterHelper.UrlEncode(returnUrl);

                return SpliceUrl(url, "returnUrl", returnUrl);
            }

            return url;
        }

        #region

        public static string GetChannelRouteUrl(this string url, string paramValue = "")
        {
            if (string.IsNullOrEmpty(url)) return "";

            return GetChannelRouteUrl(url, "cid", paramValue);
        }

        public static string GetChannelRouteUrl(this string url, string paramText, string paramValue = "")
        {
            if (string.IsNullOrEmpty(url)) return "";
            if (string.IsNullOrEmpty(paramText)) return url;

            string l = url.ToLower();
            if (!l.StartsWith("/")
                || l.StartsWith("http://")
                || l.StartsWith("https://")
                || l.Contains(".aspx")
                || l.Contains(".htm")
                || l.Contains(".html")
                || l.Contains(".js")
                || string.Compare(url, "#", true) == 0
                || string.Compare(url, "javascript:;", true) == 0
                || string.Compare(url, "javascript:void(0);", true) == 0)
            {
                return url;
            }

            Regex reg = new Regex(string.Format("(?<begin>\\/)(?<channelName>{0})(?<middle>\\/)(?<channelId>(?!^[a-zA-Z]+$)[0-9a-zA-Z]+)(?<end>(\\/)?(\\?)?)", paramText), RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(paramValue))
            {
                if (reg.IsMatch(url))
                {
                    url = url.Replace(reg, new MatchEvaluator(NoChannelRouteMatchEvaluator));
                }
            }
            else
            {
                if (reg.IsMatch(url))
                {
                    url = url.Replace(reg, new MatchEvaluator(ChannelRouteMatchEvaluator)).Replace("{channelId}", paramValue);
                }
                else
                {
                    string channel = string.Concat("/", paramText, "/", paramValue, "/");
                    if (l.Contains('?'))
                    {
                        string[] list = url.Split('?');
                        if (!list.IsNullOrEmpty())
                        {
                            if (list.Length >= 1)
                            {
                                url = FileHelper.MergePath("/", new string[] { list[0], channel });
                            }
                            if (list.Length >= 2)
                            {
                                url = string.Concat(url, "?", list[1]);
                            }
                        }
                    }
                    else
                    {
                        url = FileHelper.MergePath("/", new string[] { url, channel });
                    }
                }
            }

            return url;
        }

        private static string NoChannelRouteMatchEvaluator(Match match)
        {
            return "";
        }

        private static string ChannelRouteMatchEvaluator(Match match)
        {
            if (match != null && match.Groups.Count > 0)
            {
                return string.Concat(match.Groups["begin"].Value,
                                    match.Groups["channelName"].Value,
                                    match.Groups["middle"].Value,
                                    "{channelId}",
                                    match.Groups["end"].Value);
            }

            return "";
        }

        #endregion

        #region

        /// <summary>
        /// 拼接Url
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="paramList">参数列表</param>
        /// <returns></returns>
        public static string SpliceUrl(this string url, IDictionary<string, object> paramList)
        {
            if (string.IsNullOrEmpty(url) || IsNullOrEmpty<string, object>(paramList)) return "";

            string value = string.Empty;
            foreach (string item in paramList.Keys)
            {
                if (string.IsNullOrEmpty(item)) continue;
                value = ToString(paramList[item]);
                if (string.IsNullOrEmpty(value)) continue;

                url = SpliceUrl(url, item, value);
            }
            return url;
        }

        public static string SpliceUrl(this string url, string paramText, int paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        public static string SpliceUrl(this string url, string paramText, long paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        public static string SpliceUrl(this string url, string paramText, decimal paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        public static string SpliceUrl(this string url, string paramText, double paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        public static string SpliceUrl(this string url, string paramText, bool paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        public static string SpliceUrl(this string url, string paramText, DateTime paramValue)
        {
            return url.SpliceUrl(paramText, paramValue.ToString());
        }

        /// <summary>
        /// 拼接Url
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="paramText">参数</param>
        /// <param name="paramValue">参数值</param>
        /// <returns></returns>
        public static string SpliceUrl(this string url, string paramText, string paramValue)
        {
            if (string.IsNullOrEmpty(url)) return "";
            if (string.IsNullOrEmpty(paramText)) return url;

            Regex reg = new Regex(string.Format("(?<begin>[?|&])(?<param>{0}=[^&]*)(?<end>[&]?)", paramText), RegexOptions.IgnoreCase);
            url = url.Replace(reg, new MatchEvaluator(ParamMatchEvaluator));

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(paramValue))
            {
                if (url.IndexOf("?") == -1)
                {
                    url += string.Format("?{0}={1}", paramText, paramValue);
                }
                else
                {
                    url += string.Format("&{0}={1}", paramText, paramValue);
                }
            }

            if (!string.IsNullOrEmpty(url))
            {
                reg = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
                url = url.Replace(reg, "&");
            }

            if (!string.IsNullOrEmpty(url))
            {
                reg = new Regex("[?]{2,}", RegexOptions.IgnoreCase);
                url = url.Replace(reg, "?");
            }

            if (!string.IsNullOrEmpty(url))
            {
                url = url.Replace("?&", "?");
            }

            return url;
        }

        private static string ParamMatchEvaluator(Match match)
        {
            if (match != null && match.Groups.Count > 0)
            {
                if (!string.IsNullOrEmpty(match.Groups["end"].Value))
                {
                    return match.Groups["begin"].Value;
                }
            }

            return "";
        }

        #endregion

        #endregion Url

        #region 非法字符

        /// <summary>
        /// 过滤非法字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterIllegalParameter(this string input)
        {
            return input;
        }

        /// <summary>
        /// 过滤SQL非法字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterIllegalSQL(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"(exec[\s]|execute[\s]|insert[\s]|delete[\s]|update[\s]|select[\s]|create[\s]table|alter[\s]table|drop[\s]table|master[\s]|truncate[\s]table|declare[\s]|xp_cmdshell|restore[\s]|backup[\s]|net[\s]user|net[\s]localgroup[\s]administrators)";
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);

                if (reg.IsMatch(input))
                {
                    return reg.Replace(input, "");
                }
            }

            return "";
        }

        #endregion 非法字符

        #region Html编码

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return MSA.Encoder.HtmlEncode(input);
            }

            return "";
        }

        #endregion Html编码

        #region 文本

        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="content"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        public static string ConvertTxt(string content, string newValue = "\r\n　　", string oldValue = "\r\n")
        {
            if (string.IsNullOrEmpty(content)) return "";

            return content.Replace("[ 　]+").Replace(oldValue, newValue);
        }

        /// <summary>
        /// 处理内容文本
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ConvertTxt(string content)
        {
            if (string.IsNullOrEmpty(content)) return "";

            content = content.Replace("　", "").Replace(" ", "").Trim().Replace("\r\n", "</p><p>");

            if (!string.IsNullOrEmpty(content))
            {
                content = string.Format("<p>{0}</p>", content);
            }

            return content;
        }

        #endregion 文本

        #region 图片

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="prefixUrl">域名</param>
        /// <param name="defaultUrl">默认图片</param>
        /// <param name="list">多张图片</param>
        /// <returns></returns>
        public static string GetImage(string[] list, string prefixUrl = "", string defaultUrl = "")
        {
            string url = defaultUrl;

            if (!IsNullOrEmpty(list))
            {
                foreach (string item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        url = item;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(prefixUrl) && !string.IsNullOrEmpty(url))
            {
                url = FileHelper.MergePath("/", new string[] { prefixUrl, url });
            }

            return url;
        }

        #endregion 图片

        #region 信息提示

        public static string Alert(string title, string message)
        {
            return string.Format("<div class=\"alert alert-error\" role=\"alert\"><strong>{0}</strong>  {1}</div>", SpliceEndString(title, "提示", "："), message);
        }

        #endregion 信息提示

        #region 清除Html标签

        /// <summary>
        /// 清除Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterHtmlTag(this string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            html = RegexHelper.Replace(html, "<[^>]+>");
            if (string.IsNullOrEmpty(html)) return "";
            return RegexHelper.Replace(html, "&[^;]+;");
        }

        #endregion
    }
}