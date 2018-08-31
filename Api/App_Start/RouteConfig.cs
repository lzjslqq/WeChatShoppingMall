using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;

namespace Api.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(HttpConfiguration config)
		{
			var routes = config.Routes;

			config.MapHttpAttributeRoutes();

			routes.MapHttpRoute(
				"DefaultHttpRoute",
				"api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
				//constraints: new { id = new GuidRouteConstraint() }
				);
		}
	}
}