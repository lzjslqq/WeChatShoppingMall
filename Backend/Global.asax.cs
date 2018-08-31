using Backend.App_Start;
using Backend.Filters;
using Service.Base;
using Service.Core;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;

namespace Backend
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			ViewEngines.Engines.RemoveAt(0);

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			LogHelper.Initialise();
			AutoMapperConfig.Instance.Initialise();
			//AutofacBootStrapper.Instance.Initialise(Assembly.GetExecutingAssembly());
			AutofacConfig.Initialize();

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

		protected void Application_Stop()
		{
			CurrentContext.ClearUser();
		}
	}
}