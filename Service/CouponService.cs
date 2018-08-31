using System.Collections.Generic;
using Model;
using Repository;
using Service.Base;
using ServiceDto;

namespace Service
{
    public class CouponService : BaseService<Coupon>, ICouponService
    {
        public IEnumerable<UserCouponDto> GetNoUseList(int userId, int totalFee)
        {
            if (userId <= 0 && totalFee <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new CouponRepo(cxt);
                return repo.GetNoUseList(userId, totalFee);
            }
        }

        public CouponDto GetDetail(int id, int userId)
        {
            if (id <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new CouponRepo(cxt);
                return repo.GetDetail(id, userId);
            }
        }

        public UserCouponDto GetDefault(int userId, int totalFee)
        {
            if (userId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new CouponRepo(cxt);
                return repo.GetDefault(userId, totalFee);
            }
        }

        public bool AddUserCoupon(int id, int userId)
        {
            if (id <= 0 || userId <= 0)
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();

                var repo = new CouponRepo(cxt);
                var flag = repo.AddUserCoupon(id, userId);

                if (flag) cxt.Commit();
                else cxt.Rollback();

                return flag;
            }
        }

    }
}