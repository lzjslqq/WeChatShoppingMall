using System;
using System.Collections.Generic;
using Common;
using Service;
using System.Web.Http;
using Model.Common;
using ServiceDto;

namespace Api.Controllers
{
    public class CouponController : ApiController
    {
        public readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        // 用户未使用的券
        [HttpGet]
        public ComplexResponse<IEnumerable<UserCouponDto>> NoUseList(int userId, int totalFee)
        {
            var msg = ErrorMessage.失败;
            IEnumerable<UserCouponDto> data = null;

            if (userId > 0 && totalFee > 0)
            {
                data = _couponService.GetNoUseList(userId, totalFee);
                msg = ErrorMessage.成功;
            }

            return new ComplexResponse<IEnumerable<UserCouponDto>>((int)msg, msg.ToString(), data);
        }

        [HttpGet]
        public dynamic List(int userId, int type, int pageIndex = 1, int pageSize = 10)
        {
            // 0-未使用，1-已使用，2-已过期
            var columns = @"uc.Id, u.Id as UserId, c.Id as CouponId, c.Title, c.StartTime, c.EndTime, c.MinFee, c.RebateFee, c.MaxOwnLimit, '{0}' as status";
            var tables = @"UserCoupon uc
INNER JOIN Coupon c on c.Id=uc.CouponId AND uc.Status=1
INNER JOIN [User] u on u.Id=uc.UserId";
            var where = "AND uc.Status=1 AND uc.IsDeleted=0 AND u.Id = @userId ";
            var orderby = "";
            switch (type)
            {
                case 0:
                    columns = String.Format(columns, "未使用");
                    where += "and uc.isUsed = 0 and  DATEDIFF(d, GETDATE(), c.EndTime) >=0";
                    orderby = "order by uc.CreateTime ASC";
                    break;
                case 1:
                    columns = String.Format(columns, "已使用");
                    where += "and uc.isUsed = 1";
                    orderby = "order by uc.CreateTime DESC";
                    break;
                case 2:
                    columns = String.Format(columns, "已过期");
                    where += "and uc.isUsed = 0 and  DATEDIFF(d, GETDATE(), c.EndTime) < 0";
                    orderby = "order by uc.CreateTime DESC";
                    break;
            }

            int rowCount;
            var list = _couponService.GetPagerList<UserCouponDto>(columns, tables, where, orderby, out rowCount, pageIndex, pageSize, new { userId });

            var pageInfo = new PageInfo(pageIndex, pageSize, rowCount);
            return new { list, pageInfo };
        }

        /// <summary>
        ///  优惠券详情
        /// </summary>
        [HttpGet]
        public CouponDto Detail(int id, int userId = 0)
        {
            var info = _couponService.GetDetail(id, userId);
            return info;
        }

        [HttpGet]
        public bool Add(int id, int userId)
        {
            var flag = _couponService.AddUserCoupon(id, userId);
            return flag;
        }

        /// <summary>
        ///  用户最优优惠券
        /// </summary>
        [HttpGet]
        public UserCouponDto Default(int userId, int totalFee)
        {
            var info = _couponService.GetDefault(userId, totalFee);
            return info;
        }
    }
}