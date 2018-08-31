using log4net;
using System;

namespace Utility
{
	public class LogHelper
	{
		private static ILog log = LogManager.GetLogger("LogFileAppender");

		public static void Initialise()
		{
			//log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath(@"\Logs\log4net.config")));
			log4net.Config.XmlConfigurator.Configure();
		}

		#region 通过 log4net 记录日志

		/// <summary>
		/// 调试信息
		/// </summary>
		/// <param name="content">内容</param>

		public static void Debug(string content)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Debug(content);
		}

		/// <summary>
		/// 调试信息
		/// </summary>
		/// <param name="content">内容</param>
		/// <param name="exception">错误</param>

		public static void Debug(string content, Exception exception)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Debug(content, exception);
		}

		/// <summary>
		/// 信息
		/// </summary>
		/// <param name="content">内容</param>
		public static void Info(string content)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Info(content);
		}

		/// <summary>
		/// 信息
		/// </summary>
		/// <param name="content">内容</param>
		/// <param name="exception">错误</param>
		public static void Info(string content, Exception exception)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Info(content, exception);
		}

		/// <summary>
		/// 错误信息
		/// </summary>
		/// <param name="content">内容</param>
		public static void Error(string content)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Error(content);
		}

		/// <summary>
		/// 错误信息
		/// </summary>
		/// <param name="content">内容</param>
		/// <param name="exception">错误</param>
		public static void Error(string content, Exception exception)
		{
			if (string.IsNullOrEmpty(content)) return;

			log.Error(content, exception);
		}

		#endregion 通过 log4net 记录日志
	}
}