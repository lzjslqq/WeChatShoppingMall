using System;
using System.Collections.Concurrent;
using System.Text;
using System.Web.Mvc;

namespace Backend.Filters
{
	public class ExceptionFilter : FilterAttribute, IExceptionFilter
	{
		public static ConcurrentQueue<string> execptionQueue = new ConcurrentQueue<string>();

		public void OnException(ExceptionContext filterContext)
		{
			if (!filterContext.ExceptionHandled)
			{
				var sb = new StringBuilder();
				sb.Append("|Url:");
				if (filterContext.HttpContext.Request.Url != null)
					sb.Append(filterContext.HttpContext.Request.Url.AbsoluteUri);
				sb.Append("\r\n");
				sb.Append(filterContext.Exception.Message.Replace("<", string.Empty).Replace(">", string.Empty));
				sb.Append("\r\n");
				sb.Append(filterContext.Exception.Source);
				sb.Append("\r\n");
				sb.Append(filterContext.Exception.StackTrace);
				sb.Append("\r\n");
				Exception ex = filterContext.Exception.InnerException;
				while (ex != null)
				{
					sb.Append("\r\n-----------------------");
					sb.Append(filterContext.Exception.Message.Replace("<", string.Empty).Replace(">", string.Empty));
					sb.Append("\r\n");
					sb.Append(filterContext.Exception.Source);
					sb.Append("\r\n");
					sb.Append(filterContext.Exception.StackTrace);
					ex = ex.InnerException;
				}

				var sbHeaders = new StringBuilder();
				for (var i = 0; i < filterContext.HttpContext.Request.Headers.Keys.Count; i++)
				{
					var keyName = filterContext.HttpContext.Request.Headers.AllKeys[i];
					var strings = filterContext.HttpContext.Request.Headers.GetValues(keyName);
					if (strings != null)
						sbHeaders.Append(keyName + ":" + strings[0] + " ");
				}

				execptionQueue.Enqueue(sb.ToString());

				filterContext.ExceptionHandled = false;

				//普通请求，返回自定义错误页面
				filterContext.Result = new ContentResult { Content = "系统异常" };
			}
		}
	}
}