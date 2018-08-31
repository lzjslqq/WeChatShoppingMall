using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Utility
{
    public class RouteDataHelper
    {
        #region

        public static IDictionary<string, object> Init(RequestContext requestContext)
        {
            if (requestContext == null) return null;

            IDictionary<string, object> routeParams = null;

            RouteData data = requestContext.RouteData;
            if (data != null)
            {
                RouteValueDictionary values = data.Values;
                if (values != null && values.Count > 0)
                {
                    object obj = null;
                    foreach (string key in values.Keys)
                    {
                        if (string.Compare(key, "controller", true) == 0 || string.Compare(key, "action", true) == 0) continue;

                        obj = values[key];
                        if (!obj.IsObjectNullOrEmpty())
                        {
                            if (routeParams == null)
                            {
                                routeParams = new Dictionary<string, object>();
                            }

                            Set(routeParams, key, obj);
                        }
                    }
                }
            }

            return routeParams;
        }

        public static string Get(IDictionary<string, object> routeParams, string key)
        {
            if (routeParams.IsNullOrEmpty<string, object>() || string.IsNullOrEmpty(key)) return "";

            object obj = null;

            key = key.ToLower();
            if (routeParams.ContainsKey(key))
            {
                obj = routeParams[key];
            }

            return StringHelper.ToString(obj).FilterIllegalParameter();
        }

        public static void Set(IDictionary<string, object> routeParams, string key, object value)
        {
            if (routeParams == null || string.IsNullOrEmpty(key) || value.IsObjectNullOrEmpty()) return;

            key = key.ToLower();

            if (routeParams.ContainsKey(key))
            {
                routeParams[key] = value;
            }
            else
            {
                routeParams.Add(key, value);
            }
        }

        #endregion
    }
}