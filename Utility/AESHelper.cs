using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainStr">明文字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">向量</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainStr, string key, string IV)
        {
            if (string.IsNullOrEmpty(plainStr)) return "";
            if (string.IsNullOrEmpty(key)) return "";
            if (string.IsNullOrEmpty(IV)) return "";

            byte[] bKey = Encoding.UTF8.GetBytes(key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = null;
            using (Rijndael aes = Rijndael.Create())
            {
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(byteArray, 0, byteArray.Length);
                            cStream.FlushFinalBlock();
                            encrypt = Convert.ToBase64String(mStream.ToArray());
                        }
                    }
                }
                catch { }
                finally
                {
                    aes.Clear();
                }
            }

            return encrypt;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="IV">向量</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr, string key, string IV)
        {
            if (string.IsNullOrEmpty(encryptStr)) return "";
            if (string.IsNullOrEmpty(key)) return "";
            if (string.IsNullOrEmpty(IV)) return "";

            byte[] bKey = Encoding.UTF8.GetBytes(key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Convert.FromBase64String(encryptStr);

            string decrypt = null;
            using (Rijndael aes = Rijndael.Create())
            {
                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                        {
                            cStream.Write(byteArray, 0, byteArray.Length);
                            cStream.FlushFinalBlock();
                            decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                        }
                    }
                }
                catch { }
                finally
                {
                    aes.Clear();
                }
            }

            return decrypt;
        }
    }
}
