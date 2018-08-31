using System.Collections.Generic;
using Model;
using Service.Base;
using ServiceDto;

namespace Service
{
    public interface ICouponService : IBaseService<Coupon>
    {
        IEnumerable<UserCouponDto> GetNoUseList(int userId, int totalFee);
        CouponDto GetDetail(int id, int userId);
        UserCouponDto GetDefault(int userId, int totalFee);
        bool AddUserCoupon(int id, int userId);
    }
}