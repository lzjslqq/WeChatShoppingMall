using Api.App_Start;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Utility;
using System.Threading;
using Api.Filters;

namespace Api
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			var config = GlobalConfiguration.Configuration;

			RouteConfig.RegisterRoutes(config);
			WebApiConfig.Configure(config);
			AutofacConfig.Initialize(config);
			AutoMapperConfig.Instance.Initialise();
			LogHelper.Initialise();

			GlobalConfiguration.Configuration.EnsureInitialized();

			ThreadPool.QueueUserWorkItem((a) =>
			{
				string error = "";

				while (true)
				{
					//判断一下队列中是否有数据
					if (ExceptionFilter.execptionQueue.Count > 0)
					{
						if (ExceptionFilter.execptionQueue.TryDequeue(out error))
						{
							LogHelper.Error(error);
						}
						else
						{
							Thread.Sleep(3000);
						}
					}
					else
					{
						//如果队列中没有数据，休息
						Thread.Sleep(3000);
					}
				}
			});
		}
	}
}