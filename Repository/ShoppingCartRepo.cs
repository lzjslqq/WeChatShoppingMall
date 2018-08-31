using DapperExtension.Core;
using Model;
using ServiceDto;
using System.Collections.Generic;

namespace Repository
{
    public class ShoppingCartRepo : BaseRepo<ShoppingCart>
    {
        public ShoppingCartRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<ShoppingCartDetailDto> GetDetailsByUser(int userId)
        {
            string sql = @" SELECT c.id as cartid, p.Id as ProductId, p.StoreAmount, p.CoverUrl, p.Name,c.Num, ISNULL(s.Price, p.Price) AS Price, f.[Name] as FeatureName,s.FeatureValueId,v.[value] as FeatureValue
				FROM dbo.ShoppingCart c
                JOIN dbo.ProductSku s on c.ProductSkuId = s.Id
                JOIN dbo.Product p ON s.ProductId = p.Id
                JOIN dbo.FeatureValue v ON s.FeatureValueId = v.Id
                JOIN dbo.Feature f ON v.FeatureId = f.Id
				WHERE c.UserId = @userId AND c.IsDeleted=0 ORDER BY c.Sort,c.CreateTime DESC";

            return DbManage.Query<ShoppingCartDetailDto>(sql, new { userId });
        }

        public bool AddToCart(int userId, int productId, int featureValueId, int num)
        {
            var sql = @"DECLARE @@skuId int
SELECT top 1 @@skuId = id from ProductSku where ProductId =@productId and FeatureValueId = @featureValueId
INSERT INTO ShoppingCart(ProductSkuId, num, Status, CreateTime, UpdateTime, IsDeleted, UserId)
values(@@skuId, @num, 1, GETDATE(), GETDATE(), 0, @userId);";

            return DbManage.Execute(sql, new { userId, productId, featureValueId, num }) > 0;
        }

        public bool DeleteBatch(int userId, int[] ids)
        {
            var sql = @"UPDATE ShoppingCart SET IsDeleted = 1 WHERE Id in @ids AND UserId=@userId;";

            return DbManage.Execute(sql, new { userId, ids }) > 0;
        }

        public bool UpdateNum(int userId, int cartId, int num)
        {
            var sql = @"UPDATE ShoppingCart SET Num=@num WHERE Id=@cartId AND UserId=@userId;";

            return DbManage.Execute(sql, new { userId, cartId, num }) > 0;
        }

        public IEnumerable<ShoppingCartDetailDto> GetItemsByCartIds(int[] cartIdArr)
        {
            string sql = @" SELECT c.id as cartid, p.Id as ProductId, p.CoverUrl, p.Name,c.Num, ISNULL(s.Price, p.Price) AS Price, f.[Name] as FeatureName,s.FeatureValueId,v.[value] as FeatureValue
				FROM dbo.ShoppingCart c
                JOIN dbo.ProductSku s on c.ProductSkuId = s.Id
                JOIN dbo.Product p ON s.ProductId = p.Id
                JOIN dbo.FeatureValue v ON s.FeatureValueId = v.Id
                JOIN dbo.Feature f ON v.FeatureId = f.Id
				WHERE c.IsDeleted=0 AND c.id in @cartIdArr ORDER BY c.Sort,c.CreateTime DESC";

            return DbManage.Query<ShoppingCartDetailDto>(sql, new { cartIdArr });
        }

        public int GetCartCount(int userId)
        {
            var sql = @"Select count(1) from [dbo].[ShoppingCart] where UserId=@userId AND Status=1 AND IsDeleted=0;";
            return DbManage.ExecuteScalar<int>(sql, new { userId });
        }

    }
}