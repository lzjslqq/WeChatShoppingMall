using System.Collections.Generic;
using System.Linq;
using DapperExtension.Core;
using Model;
using ServiceDto;

namespace Repository
{
    public class CouponRepo : BaseRepo<Coupon>
    {
        public CouponRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<UserCouponDto> GetNoUseList(int userId, int totalFee)
        {
            var sql = @"
SELECT uc.Id, u.Id as UserId, c.Id as CouponId, c.Title, c.StartTime, c.EndTime, c.MinFee, c.RebateFee, c.MaxOwnLimit, '未使用' as status,
	(case WHEN c.MinFee <= @totalFee THEN 1 ELSE 0 END) as CanUse
FROM UserCoupon uc
INNER JOIN Coupon c on c.Id=uc.CouponId AND uc.Status=1
INNER JOIN [User] u on u.Id=uc.UserId 
WHERE uc.Status=1 AND uc.IsDeleted=0 
    AND u.Id = @userId and uc.isUsed = 0 and  DATEDIFF(d, GETDATE(), c.EndTime) >=0
ORDER BY uc.CreateTime ASC;";
            return DbManage.Query<UserCouponDto>(sql, new { userId, totalFee });
        }

        public CouponDto GetDetail(int id, int userId)
        {
            var sql = @"SELECT TOP 1 c.Id, c.Title, c.StartTime, c.EndTime, c.MinFee, c.RebateFee, c.MaxOwnLimit, c.Status,
	CASE WHEN DATEDIFF(d, GETDATE(), c.EndTime) >=0 THEN 1 ELSE 0 END as CanUse,
	(SELECT count(1) FROM UserCoupon uc WHERE uc.CouponId=c.Id AND uc.Status=1 AND IsDeleted=0 AND IsUsed=0 AND uc.UserId=@userId ) as OwnCount
FROM Coupon c 
WHERE c.Id =@id;";
            return DbManage.Query<CouponDto>(sql, new { id, userId }).FirstOrDefault();
        }

        public UserCouponDto GetDefault(int userId, int totalFee)
        {
            var sql = @"SELECT TOP 1 uc.Id, u.Id as UserId, c.Id as CouponId, c.Title, c.StartTime, c.EndTime, c.MinFee, c.RebateFee, c.MaxOwnLimit, c.Status
FROM UserCoupon uc
INNER JOIN Coupon c on c.Id=uc.CouponId
INNER JOIN [User] u on u.Id=uc.UserId 
WHERE uc.isUsed=0 AND uc.IsDeleted=0 
        and u.Id = @userId AND c.MinFee<= @totalFee AND DATEDIFF(d, GETDATE(), c.EndTime) >=0
ORDER BY c.RebateFee DESC, uc.CreateTime ASC;";
            return DbManage.Query<UserCouponDto>(sql, new { userId, totalFee }).FirstOrDefault();
        }

        public bool AddUserCoupon(int id, int userId)
        {
            var sql = @"insert into [dbo].[UserCoupon] (userid,couponid, status, createtime, updatetime, isdeleted, isUsed) values(@userId, @id, 1, getdate(), getdate(), 0, 0);";
            return DbManage.Execute(sql, new { id, userId }) > 0;
        }

    }
}