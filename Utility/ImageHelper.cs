using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace Utility
{
    public class ImageHelper
    {
        #region 保存远程图片

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="url">远程图片地址</param>
        /// <param name="method">get/post</param>
        /// <param name="dirPath">文件保存目录(相对路径)</param>
        /// <param name="sign"></param>
        /// <param name="filePath">文件路径（物理路径）</param>
        /// <param name="fileName">文件名（包括后缀）</param>
        /// <returns></returns>
        public static bool Save(string url, string method, string dirPath, int sign, out string filePath, out string fileName)
        {
            bool flag = false;

            filePath = string.Empty;
            fileName = string.Empty;
            if (string.IsNullOrEmpty(url)) return flag;
            if (string.IsNullOrEmpty(dirPath)) return flag;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = (string.IsNullOrEmpty(method) ? "get" : method);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        dirPath = FileHelper.MapPath(dirPath);
                        fileName = FileHelper.CreateRandomFileNameByDateTime(sign, ".jpg");
                        filePath = FileHelper.GetPhysicalPath(dirPath, fileName);
                        Save(stream, dirPath, fileName);
                        stream.Close();
                        stream.Dispose();
                    }
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }

                flag = true;
            }
            catch
            {

            }

            return flag;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="stream">stream</param>
        /// <param name="filePath">文件保存目录（物理路径）</param>
        /// <param name="fileName">文件名（包括后缀）</param>
        /// <returns></returns>
        public static bool Save(Stream stream, string dirPath, string fileName)
        {
            if (string.IsNullOrEmpty(dirPath)) return false;

            bool flag = false;

            try
            {
                FileHelper.CreateDirectory(dirPath);

                string uploadPath = FileHelper.GetPhysicalPath(dirPath, fileName);
                using (Image image = Image.FromStream(stream))
                {
                    image.Save(uploadPath);
                }

                flag = true;
            }
            catch
            {

            }

            return flag;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strBase64">二进制流</param>
        /// <param name="dirPath">文件保存目录（物理路径）</param>
        /// <param name="fileName">文件名（包括后缀名称）</param>
        /// <returns></returns>
        public static bool Save(string strBase64, string dirPath, string fileName)
        {
            if (string.IsNullOrEmpty(dirPath)) return false;

            bool flag = false;

            try
            {
                FileHelper.CreateDirectory(dirPath);

                string uploadPath = FileHelper.GetPhysicalPath(dirPath, fileName);

                byte[] buf = Convert.FromBase64String(strBase64);

                using (MemoryStream ms = new MemoryStream(buf))
                {
                    using (Image image = Image.FromStream(ms))
                    {
                        image.Save(uploadPath);
                    }
                }

                flag = true;
            }
            catch
            {

            }

            return flag;
        }

        public static bool Upload(string strBase64, string filePath)
        {
            if (string.IsNullOrEmpty(strBase64)) return false;
            if (string.IsNullOrEmpty(filePath)) return false;

            bool isSuccess = true;

            try
            {
                byte[] buf = Convert.FromBase64String(strBase64);
                using (MemoryStream ms = new MemoryStream(buf))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(buf, 0, buf.Length);
                    }
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        #endregion

        #region 生成缩略图

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式(HW=指定高宽缩放;W=指定宽，高按比例;H=指定高，宽按比例;Cut=指定高宽裁减)</param>
        /// <returns></returns>
        public static bool MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            bool flag = false;

            try
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case "HW"://指定高宽缩放（可能变形）                
                        break;
                    case "W"://指定宽，高按比例
                        if (originalImage.Width < width)
                        {
                            towidth = originalImage.Width;
                            toheight = originalImage.Height;
                        }
                        else
                        {
                            toheight = originalImage.Height * width / originalImage.Width;
                        }
                        break;
                    case "H"://指定高，宽按比例
                        if (originalImage.Height < height)
                        {
                            toheight = originalImage.Height;
                            towidth = originalImage.Width;
                        }
                        else
                        {
                            towidth = originalImage.Width * height / originalImage.Height;
                        }
                        break;
                    case "Cut"://指定高宽裁减（不变形）                
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }

                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(System.Drawing.Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                    new System.Drawing.Rectangle(x, y, ow, oh),
                    System.Drawing.GraphicsUnit.Pixel);

                try
                {
                    string Path = thumbnailPath.Substring(0, thumbnailPath.LastIndexOf('\\'));
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }

                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    flag = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flag;
        }

        #endregion
    }
}
