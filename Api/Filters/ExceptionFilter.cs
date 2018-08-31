using System;
using System.Collections.Concurrent;
using System.Text;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace Api.Filters
{
	public class ExceptionFilter : FilterAttribute
	{
		public static ConcurrentQueue<string> execptionQueue = new ConcurrentQueue<string>();

		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext.Exception != null)
			{
				var sb = new StringBuilder();
				sb.Append("|Url:");
				if (filterContext.Request.RequestUri.AbsoluteUri != null)
					sb.Append(filterContext.Request.RequestUri.AbsoluteUri);
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

				execptionQueue.Enqueue(sb.ToString());
			}
		}
	}
}