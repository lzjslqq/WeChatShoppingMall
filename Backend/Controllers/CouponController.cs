using Backend.App_Start;
using ClientDto.Backend;
using Common;
using Model;
using Service;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Backend.Filters;
using Utility;

namespace Backend.Controllers
{
	public class CouponController : Controller
	{
		public readonly ICouponService _couponService;

		public CouponController(ICouponService couponService)
		{
			_couponService = couponService;
		}

        [AuthorizationFilter]
		public ActionResult Index()
		{
			return View();
		}

        [AuthorizationFilter]
		public ActionResult List(string title, int page = 1, int rows = 10)
		{
			int rowCount;
			StringBuilder where = new StringBuilder("and Status =1 and IsDeleted = 0 ");
			if (!string.IsNullOrEmpty(title))
			{
				where.Append("and charindex(@title,Title)> 0");
			}

			var list = _couponService.GetPagerList<CouponListDto>("Id,Title,Description,ImgUrl,StartTime,EndTime,MinFee,MinNum,MinUserLevel,RebateFee,Priority,MaxOwnLimit,IsPersistent,ValidDays,CreateTime ", "[dbo].[Coupon]", where.ToString(), "order by CreateTime desc", out rowCount, page, rows, new { title });
			PageInfo pageinfo = new PageInfo(page, rows, rowCount);
			return Json(new { rows = list, page = page, total = pageinfo.PageCount }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
        [AuthorizationFilter]
		public ActionResult Edit(int id = 0)
		{
			CouponDetailDto coupon = new CouponDetailDto();
			if (id > 0)
			{
				coupon = _couponService.Get(id).ToCouponDetailDto();
				if (coupon != null)
				{
				}
			}

			return View(coupon);
		}

        [HttpPost]
        [AuthorizationFilter]
		public ActionResult Edit(CouponDetailDto dto)
		{
			string errorMsg = "优惠券保存失败";
			bool result = false;
			Coupon model = model = dto.ToCoupon();
			model.MinFee = dto.MinFee * 100;
			model.RebateFee = dto.RebateFee * 100;

			try
			{
				if (dto.Id > 0)
				{
					model.UpdateTime = DateTime.Now;
					var propertyNames = dto.GetType().GetProperties().Select(p => p.Name).ToList();
					result = _couponService.Update(model, p => propertyNames.Contains(p.Name));
				}
				else
				{
					model.CreateTime = model.UpdateTime = DateTime.Now;
					model.Creator = "admin";
					model.Status = 1;
					model.Sort = 0;
					model.IsDeleted = 0;
					long newId = _couponService.Insert(model);
					result = newId > 0;
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error("Coupon/Edit [post] Error", ex);
				return Json(new { res = false, msg = "网络连接错误" });
			}

			if (result) errorMsg = "优惠券保存成功";

			return Json(new { res = result, msg = errorMsg });
		}

        [HttpPost]
        [AuthorizationFilter]
		public ActionResult Delete(int id)
		{
			string errorMsg = "删除失败";
			bool result = false;

			try
			{
				result = _couponService.Delete(new Coupon { Id = id });
				if (result) errorMsg = "删除成功";
			}
			catch (Exception)
			{
				return Json(new { res = result, msg = errorMsg });
			}
			return Json(new { res = result, msg = errorMsg });
		}
	}
}