using System;
using Dapper;
using DapperExtension.Core;
using Model;
using ServiceDto;
using System.Data;
using System.Linq;

namespace Repository
{
    public class OrderRepo : BaseRepo<Order>
    {
        public OrderRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public int CheckParamsValid(GenerateOrderDto dto)
        {
            var procParams = new DynamicParameters();
            procParams.Add("@UserId", dto.UserId);
            procParams.Add("@TotalFee", dto.TotalFee);
            procParams.Add("@Payment", dto.Payment);
            procParams.Add("@UserCouponId", dto.UserCouponId);
            procParams.Add("@PostFee", dto.PostFee);
            procParams.Add("@ROut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            DbManage.Execute("[dbo].[Order_ParamsCheck]", procParams, CommandType.StoredProcedure);

            return procParams.Get<int>("@ROut");
        }

        public int InsertOrderShipping(OrderShipping model)
        {
            string sql = @" INSERT INTO [dbo].[OrderShipping](OrderId, Name, Mobile, Province, City, District, Address,
						PostCode, Sort, Status, Creator, CreateTime, IsDeleted) values( @OrderId, @Receiver, @Mobile,
						@Province, @City, @District, @Address, @PostCode, 0, 1, 'admin', GETDATE(), 0) ";

            return DbManage.Execute(sql, new { OrderId = model.OrderId, Receiver = model.Name, Mobile = model.Mobile, Province = model.Province, City = model.City, District = model.District, Address = model.Address, PostCode = model.PostCode });
        }

        public bool UpdateUserCoupon(int userCouponId)
        {
            string sql = " UPDATE dbo.[UserCoupon] SET IsUsed = 1 WHERE Id = @userCouponId AND IsUsed = 0";

            return DbManage.Execute(sql, new { userCouponId }) == 1;
        }

        public bool DeleteShoppingCart(int[] cartIdArr)
        {
            string sql = " UPDATE dbo.[ShoppingCart] SET IsDeleted = 1 WHERE Id in @cartIdArr";

            return DbManage.Execute(sql, new { cartIdArr }) > 0;
        }
        public int InsertOrderItem(OrderItemDto dto)
        {
            //string sql = @" INSERT INTO [dbo].[OrderItem]( [OrderId], [ProductId], [FeatureName],[FeatureValue], [Num], [Name], [TotalFee], [Price], [Sort], [Status], [Creator], [CreateTime], [IsDeleted] ) Select @OrderId, s.ProductId, f.Name , v.Value,@Num,p.Name,@TotalFee,@Price, 0, 1, NULL, @date, 0  from dbo.[Product] p join dbo.[ProductSku] s on p.id = s.Productid JOIN dbo.[FeatureValue] v on s.FeatureValueId = v.Id JOIN dbo.[Feature] f ON f.id = v.FeatureId Where p.Status =1 and Isdeleted = 0 ";
            string sql = @" INSERT INTO [dbo].[OrderItem]( [OrderId], [ProductId], [FeatureName],[FeatureValue], [Num], [Name], [TotalFee], [ImgUrl], [Price], [Sort], [Status], [Creator], [CreateTime], [IsDeleted] ) select @OrderId, @ProductId, @FeatureName , @FeatureValue,@Num, @Name,@TotalFee, p.CoverUrl, @Price, 0, 1, NULL, @date, 0 from dbo.[Product] p where p.Id=@ProductId;";

            return DbManage.Execute(sql, new { OrderId = dto.OrderId, ProductId = dto.ProductId, FeatureName = dto.FeatureName, FeatureValue = dto.FeatureValue, Num = dto.Num, Name = dto.Name, TotalFee = dto.TotalFee, Price = dto.Price, date = DateTime.Now });
        }

        public OrderItemViewDto GetDefaultProduct(int orderId)
        {
            string sql = @" SELECT TOP 1 * FROM [OrderItem] oi WHERE oi.OrderId =@orderId ORDER BY oi.Id DESC;";

            return DbManage.Query<OrderItemViewDto>(sql, new { orderId }).FirstOrDefault();
        }

        public OrderDetailViewDto GetDetail(int userId, int orderId)
        {
            string sql = @"SELECT o.Id, o.TradeNo, o.TotalFee, o.PostFee, o.Payment, o.Status, o.UserMessage, o.UserCouponId, c.Title as CouponTitle, c.RebateFee as CouponFee,
	os.Name as Receiver, os.Mobile, os.Province, os.City, os.District, os.Address, os.PostCode
from [Order] o
LEFT JOIN OrderShipping os on os.OrderId=o.Id
LEFT JOIN UserCoupon uc on uc.Id=o.UserCouponId
LEFT JOIN Coupon c on c.Id=uc.CouponId
WHERE o.UserId=@userId AND o.Id = @orderId;";

            return DbManage.Query<OrderDetailViewDto>(sql, new { userId, orderId }).FirstOrDefault();
        }

        public string GetTradeNo(long orderId)
        {
            string sql = " Select TradeNo from [dbo].[Order] where Id=@orderId";

            return DbManage.ExecuteScalar<string>(sql, new { orderId });
        }

        public PayOrderDto GetPayOrderInfoByTradeNo(string tradeNo)
        {
            string sql = @"SELECT o.TradeNo,  o.Payment as TotalFee, u.OpenId
from [Order] o
LEFT JOIN [dbo].[User] u on u.Id=o.UserId
WHERE o.TradeNo=@tradeNo;";

            return DbManage.Query<PayOrderDto>(sql, new { tradeNo }).FirstOrDefault();
        }

        public bool UpdateOrderStatus(string tradeNo, int status)
        {
            string sql = " UPDATE dbo.[Order] SET Status = @status WHERE TradeNo = @tradeNo";

            return DbManage.Execute(sql, new { tradeNo, status }) == 1;
        }
        
        public bool ExistOrder(int userId, string tradeNo)
        {
            string sql = "SELECT count(1) from dbo.[Order] o inner join dbo.[User] u on u.Id=o.UserId where u.Id=@userId and o.TradeNo=@tradeNo";

            return DbManage.ExecuteScalar<int>(sql, new { tradeNo, userId }) > 0;
        }

        public OrderStatViewDto GetStatCountInfo(int userId)
        {
            string sql = @"SELECT 	(SELECT COUNT(1) FROM [Order] WHERE UserId=2 AND IsDeleted=0 AND Status=0) AS NotPayCount,
	(SELECT COUNT(1) FROM [Order] WHERE UserId=@userId AND IsDeleted=0 AND Status=1) AS NotDeliverCount,
	(SELECT COUNT(1) FROM [Order] WHERE UserId=@userId AND IsDeleted=0 AND Status=2) AS DeliveredCount,
	(SELECT COUNT(1) FROM [Order] WHERE UserId=@userId AND IsDeleted=0 AND Status=3) AS FinishedCount";

            return DbManage.Query<OrderStatViewDto>(sql, new { userId }).FirstOrDefault();
        }

    }
}