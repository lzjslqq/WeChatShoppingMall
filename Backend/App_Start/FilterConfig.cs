using Backend.Filters;
using System.Web.Mvc;

namespace Backend.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new HandleErrorAttribute());
			filters.Add(new ExceptionFilter());
		}
	}
}