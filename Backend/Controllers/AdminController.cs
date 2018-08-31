using System;
using System.Web.Mvc;
using Backend.Filters;
using Model.Common;
using Service;
using Service.Base;

namespace Backend.Controllers
{
    public class AdminController : BaseController
    {
        public readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region 1. 登录/登出

        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///  登录校验
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckLogin(string userName, string userPwd)
        {
            try
            {
                // 1. 判空
                if (string.IsNullOrEmpty(userName))
                    return Json(new { status = false, msg = "请输入用户名！" }, JsonRequestBehavior.AllowGet);
                if (string.IsNullOrEmpty(userPwd))
                    return Json(new { status = false, msg = "请输入密码！" }, JsonRequestBehavior.AllowGet);

                // 2. 判缓存
                if (CurrentUser != null && CurrentUser.UserId > 0)
                    return Json(new { status = true, msg = "成功", redirect = "/admin/index" }, JsonRequestBehavior.AllowGet);

                // 3. 判正误
                var user = _adminService.GetByUserName(userName);
                if (user == null)
                {
                    return Json(new { status = false, msg = "用户名不存在！" }, JsonRequestBehavior.AllowGet);
                }
                else if (!user.LogPwd.Equals(userPwd))
                {
                    return Json(new { status = false, msg = "密码输入有误！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CurrentContext.SetUser(new CurrentUser { UserId = (int)user.Id, UserName = user.LogName });
                    return Json(new { status = true, msg = "成功", redirect = "/admin/index" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("checklogin error:", ex);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            CurrentContext.ClearUser();
            Session.RemoveAll();
            return Redirect("/admin/login");
        }

        #endregion

    }
}