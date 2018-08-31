using System.Web.Mvc;
using Model.Common;
using Utility;

namespace Backend.Controllers
{
    public class BaseController : Controller
    {
        public static CurrentUser CurrentUser
        {
            get { return SessionHelper<CurrentUser>.Get("Current_User"); }
        }

    }
}