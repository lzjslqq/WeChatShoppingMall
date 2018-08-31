using Model.Common;
using Service.Base;
using System.Web.Mvc;

namespace Backend.Filters
{
	public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
	{
		// 当前登录用户
		private ICurrentUser _currentUser;

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			_currentUser = CurrentContext.GetUser();
			//  登陆状态
			if (_currentUser == null || _currentUser.UserId == 0)
			{
				// 没有登录返回登录界面

				filterContext.Result = new RedirectResult("/admin/login");
				return;
			}
		}
	}
}