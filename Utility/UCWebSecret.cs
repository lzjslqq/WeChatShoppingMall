using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Utility
{
    public class UCWebSecret
    {
        #region 直接使用.NET中的的库类函数,进行base64加密 和 解密

        //base64加密
        public static string Base64Code(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        //base64解密
        public static string Base64DeCode(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.Default.GetString(bytes);
        }

        #endregion

        #region  aes加密

        //aes加密方法
        public static string AESEncrypt(string sStr, string sKey, byte[] sIv)
        {
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(sKey);//密匙           
                byte[] bIV = sIv;
                byte[] bStr = Encoding.UTF8.GetBytes(sStr);//字符串

                string encrypt = null;
                Rijndael aes = Rijndael.Create();
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(bStr, 0, bStr.Length);
                            cStream.FlushFinalBlock();
                            encrypt = Convert.ToBase64String(mStream.ToArray());
                        }
                    }
                }
                catch { }
                aes.Clear();

                return encrypt;
            }
            catch
            {

                return "";
            }
        }

        #endregion

        #region aes解密

        //aes解密方法
        public static string AESDecrypt(string sStr, string sKey, byte[] sIv)
        {
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(sKey);
                byte[] bIV = sIv;
                byte[] byteArray = Convert.FromBase64String(sStr);

                string OutStr = null;
                Rijndael aes = Rijndael.Create();
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(byteArray, 0, byteArray.Length);
                            cStream.FlushFinalBlock();
                            OutStr = Encoding.UTF8.GetString(mStream.ToArray());
                        }
                    }
                }
                catch
                {
                }

                aes.Clear();
                return OutStr;
            }
            catch
            {

                return "";
            }
        }

        #endregion

        #region url 编码

        //url编码方法
        public static string url_encode(string url)
        {
            string outUrl = null;

            Encoding utf8 = Encoding.UTF8;

            //编码
            outUrl = HttpUtility.UrlEncode(url, utf8);

            return outUrl;
        }

        #endregion

        #region url 解码

        //解码方法
        public static string url_decode(string url)
        {
            string outUrl = null;

            Encoding gb = Encoding.GetEncoding("gb2312");
            Encoding utf8 = Encoding.UTF8;

            //首先进行UTF-8解码
            outUrl = HttpUtility.UrlDecode(url, utf8);
            //对编码再进行一次编码，比对解码是否正确
            string url_decode = HttpUtility.UrlEncode(outUrl, utf8).ToLower();
            //比对结果，如果不同，则进行gb2312解码
            if (url_decode != outUrl)
            {
                outUrl = HttpUtility.UrlDecode(url, gb);
            }

            return outUrl;
        }

        #endregion

        #region 截取字符串

        public static string strSubstring(string GetStr, string Str, int Num)
        {
            string imsi = GetStr.Substring(GetStr.IndexOf(Str) + Num, GetStr.Length - (GetStr.IndexOf(Str) + Num));
            return imsi;
        }
        #endregion
    }
}
