using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utility
{
    public class ValidateCodeHelper
    {
        public ValidateCodeHelper(int length)
        {
        }

        ///  <summary>
        ///  生成随机码
        ///  </summary>
        ///  <param  name="length">随机码个数</param>
        ///  <returns></returns>
        public static string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = String.Empty;
            //生成一定长度的验证码
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomcode += code.ToString();
            }
            return randomcode;
        }

        ///  <summary>
        ///  创建随机码图片
        ///  </summary>
        ///  <param  name="randomCode">随机码</param>
        public static byte[] CreateImage(string randomCode)
        {
            Random rand = new Random();
            int randAngle = rand.Next(45, 70); //随机转动角度
            int mapwidth = (int)(randomCode.Length * 22);
            using (Bitmap map = new Bitmap(mapwidth, 49))//创建图片背景
            using (Graphics graph = Graphics.FromImage(map))
            {
                try
                {
                    graph.Clear(Color.White);//清除画面，填充背景 AliceBlue
                    graph.DrawRectangle(new Pen(Color.LightGray, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
                    graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式

                    #region 背景

                    Color[] penColor = new Color[] { Color.LightPink, Color.LightGreen, Color.LightGoldenrodYellow, Color.LightCyan, Color.LightCoral, Color.LightBlue, Color.LightSkyBlue, Color.LightSlateGray, Color.LightSteelBlue, Color.LightYellow, Color.LightSeaGreen, Color.LightSalmon };

                    //背景噪点生成
                    Pen pen = new Pen(Color.LightGray, 0);
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < 50; i++)
                    {
                        x = rand.Next(0, map.Width);
                        y = rand.Next(0, map.Height);
                        pen = new Pen(penColor[i % 11], 0);
                        graph.DrawRectangle(pen, x, y, 1, 1);
                    }

                    //根据坐标画线
                    int x2 = 0;
                    int y2 = 0;
                    Pen linePen = new Pen(Color.Blue, 0);
                    for (int i = 0; i < 3; i++)
                    {
                        x = rand.Next(map.Width / 2);
                        x2 = rand.Next(map.Width / 2, map.Width);
                        y = rand.Next(map.Height);
                        y2 = rand.Next(map.Height);
                        graph.DrawLine(linePen, x, y, x2, y2);
                    }

                    #endregion 背景

                    #region 字体

                    //验证码旋转，防止机器识别
                    char[] chars = randomCode.ToCharArray();//拆散字符串成单字符数组
                    //文字居中
                    StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    //定义颜色
                    Color[] fontColor = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

                    //定义字体
                    string[] font = new string[] { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "Pmingliu", "宋体", "Sans Serif" };
                    int findex = 0;
                    int cindex = 0;
                    float angle = 0;
                    Font f;
                    Brush b;
                    Point dot;
                    for (int i = 0; i < chars.Length; i++)
                    {
                        cindex = rand.Next(8);
                        findex = rand.Next(7);
                        f = new Font(font[findex], 16, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                        b = new SolidBrush(fontColor[cindex]);
                        dot = new Point(16, 24);
                        angle = rand.Next(-randAngle, randAngle);//转动的度数
                        graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                        graph.RotateTransform(angle);
                        graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                        graph.RotateTransform(-angle);//转回去
                        graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
                    }

                    #endregion 字体

                    #region 生成图片

                    //生成图片
                    using (MemoryStream ms = new MemoryStream())
                    {
                        map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //HttpContext.Current.Response.ClearContent();
                        //HttpContext.Current.Response.ContentType = "image/gif";
                        //HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                        return ms.ToArray();
                    }

                    #endregion 生成图片
                }
                finally
                {
                    graph.Dispose();
                    map.Dispose();
                }
            }
        }
    }
}