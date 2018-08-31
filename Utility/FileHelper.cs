using System;
using System.IO;
using System.Web;

namespace Utility
{
    public class FileHelper
    {
        /// <summary>
        /// 生成时间格式“yyyyMMddHHmmss”+随机数字+标识数字文件名（不包括后缀）
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string CreateRandomFileNameByDateTime(int sign, string extension)
        {
            return CreateRandomFileNameByDateTime("yyyyMMddHHmmss", 10000, 100000, sign, extension);
        }

        /// <summary>
        /// 生成时间文件夹
        /// </summary>
        /// <param name="format">yyyyMMddHHmmss</param>
        /// <returns></returns>
        public static string CreateFolderNameByDateTime(string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return DateTime.Now.ToString(format);
            }

            return "";
        }

        /// <summary>
        /// 生成时间+随机数字+标识数字文件名（不包括后缀）
        /// </summary>
        /// <param name="format">yyyyMMddHHmmss</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue">
        ///     随机数最小值（可取该下界值）
        ///     随机数最大值（不可取该上界值）
        /// </param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static string CreateRandomFileNameByDateTime(string format, int minValue, int maxValue, int sign)
        {
            if (!string.IsNullOrEmpty(format) && maxValue > minValue)
            {
                return string.Concat(DateTime.Now.ToString(format), new Random().Next(minValue, maxValue), sign > 0 ? sign.ToString() : "");
            }

            return "";
        }

        /// <summary>
        /// 生成时间+随机数字+标识数字文件名（包括后缀）
        /// </summary>
        /// <param name="format">yyyyMMddHHmmss</param>
        /// <param name="minValue">随机数最小值（可取该下界值）</param>
        /// <param name="maxValue">随机数最大值（不可取该上界值）</param>
        /// <param name="sign"></param>
        /// <param name="extension">文件后缀（.txt）</param>
        /// <returns></returns>
        public static string CreateRandomFileNameByDateTime(string format, int minValue, int maxValue, int sign, string extension)
        {
            if (!string.IsNullOrEmpty(format) && maxValue > minValue && !string.IsNullOrEmpty(extension))
            {
                extension = extension.Trim();

                if (!string.IsNullOrEmpty(extension))
                {
                    extension = extension.StartsWith(".") ? extension : string.Concat(".", extension);

                    if (extension.Length > 1)
                    {
                        return string.Concat(DateTime.Now.ToString(format), new Random().Next(minValue, maxValue), sign > 0 ? sign.ToString() : "", extension);
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 合并路径
        /// </summary>
        /// <param name="separator">相对路径："/"、物理路径："\\"</param>
        /// <param name="pathList">不要单独使用http(s)：//，可使用http(s):// + 域名、相对路径等</param>
        /// <returns></returns>
        public static string MergePath(string separator, string[] pathList)
        {
            char c;
            if (string.IsNullOrEmpty(separator)) return "";
            if (!char.TryParse(separator, out c)) return "";
            if (pathList == null || pathList.Length == 0) return "";

            string result = string.Empty;

            string value = string.Empty;

            foreach (string path in pathList)
            {
                if (StringHelper.IsClearBlankNullOrEmpty(path, out value)) continue;

                value = value.TrimEnd(c);

                if (!string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = value;
                    }
                    else
                    {
                        result += value.StartsWith(separator) || value.StartsWith(".") ? value : string.Concat(separator, value);
                    }
                }
            }

            if (!string.IsNullOrEmpty(result) && !result.Contains("."))
            {
                result = string.Concat(result, separator);
            }

            return result;
        }

        /// <summary>
        /// 获取文件后缀
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return "";

            string result = string.Empty;

            fileName = fileName.Trim();

            if (!string.IsNullOrEmpty(fileName) &&
                fileName.Contains("."))
            {
                int index = fileName.LastIndexOf(".", StringComparison.Ordinal);
                int length = fileName.Length;

                result = fileName.Substring(index, length - index);
            }

            return result;
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="path">文件目录 + 文件名 + 后缀（相对路径）</param>
        /// <param name="filePath">文件目录（相对路径）</param>
        /// <param name="fileName">文件名 + 后缀（相对路径）</param>
        /// <returns></returns>
        public static bool GetFileInfo(string path, out string filePath, out string fileName)
        {
            filePath = string.Empty;
            fileName = string.Empty;

            if (string.IsNullOrEmpty(path)) return false;

            try
            {
                int index = path.LastIndexOf("/", StringComparison.Ordinal);
                if (index > 0)
                {
                    filePath = path.Substring(0, index + 1);
                    fileName = path.Substring(index + 1);
                }

                return (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(fileName));
            }
            catch
            {
                // ignored
            }

            return false;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirPath">目录</param>
        public static void CreateDirectory(string dirPath)
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 获取目录下所有文件列表
        /// </summary>
        /// <param name="dirPath">目录</param>
        /// <returns></returns>
        public static string[] GetFiles(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                return Directory.GetFiles(dirPath);
            }

            return null;
        }

        /// <summary>
        /// 返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
        /// </summary>
        /// <param name="path">Web 服务器的虚拟路径。</param>
        /// <returns>与 path 相对应的物理文件路径。</returns>
        public static string MapPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return "";

            return HttpContext.Current.Server.MapPath(path);
        }

        /// <summary>
        /// 获取文件物理路径
        /// </summary>
        /// <param name="dirPath">文件目录(物理路径)</param>
        /// <param name="fileName">文件名（包括后缀）</param>
        /// <returns>文件路径(物理路径)</returns>
        public static string GetPhysicalPath(string dirPath, string fileName)
        {
            if (string.IsNullOrEmpty(dirPath)) return "";
            if (string.IsNullOrEmpty(fileName)) return "";

            return MergePath("\\", new[] { dirPath, fileName });
        }

        /// <summary>
        /// 检查是否存在文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否</returns>
        public static bool FileExists(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            return File.Exists(HttpContext.Current.Server.MapPath(path));
        }

        public static string ReadFile(string filePath)
        {
            string content = string.Empty;

            try
            {
                if (FileExists(filePath))
                {
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(filePath), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                        {
                            content = sr.ReadToEnd();
                            sr.Close();
                            fs.Close();
                        }
                    }
                }
            }
            catch { content = null; }

            return content;
        }

        public static string ReadFile(string filePath, string chapterName)
        {
            string content = string.Empty;

            try
            {
                if (FileExists(filePath))
                {
                    if (!StringHelper.IsClearBlankNullOrEmpty(chapterName, out chapterName))
                    {
                        chapterName = chapterName.Replace("　", "").Replace(" ", "");
                    }

                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(filePath), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
                        {
                            string str = "";
                            string result = "";
                            while (!sr.EndOfStream)
                            {
                                str = sr.ReadLine();
                                if (!StringHelper.IsClearBlankNullOrEmpty(str, out result))
                                {
                                    result = result.Replace("　", "").Replace(" ", "");
                                    if (string.Compare(chapterName, result, true) != 0)
                                    {
                                        content = str + "\r\n";
                                        break;
                                    }
                                }
                            }

                            content += sr.ReadToEnd();
                            sr.Close();
                            fs.Close();
                        }
                    }
                }
            }
            catch { content = null; }

            return content;
        }
    }
}