using DapperExtension.Core;
using Model;
using ServiceDto;
using System.Collections.Generic;

namespace Repository
{
    public class ProductRepo : BaseRepo<Product>
    {
        public ProductRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<ProductFileDto> GetProductFiles(int productId, int fileType)
        {
            string sql = "select Url,Type from dbo.[File]  where  IsDeleted = 0 and Status = 1 and ProductId = @productId ";

            if (fileType > 0)
            {
                sql += "and Type = @fileType";
            }

            return DbManage.Query<ProductFileDto>(sql, new { productId, fileType });
        }

        public IEnumerable<ProductSkuDto> GetProductSku(int productId)
        {
            string sql = @"SELECT f.Id as FeatureId,v.Id as ValueId,f.Name as FeatureName,v.[Value] as FeatureValue, sku.Num , ISNULL(sku.Price, p.Price) AS Price
							from dbo.ProductSku sku JOIN dbo.Product p ON sku.ProductId = p.Id
							JOIN dbo.FeatureValue v ON sku.FeatureValueId = v.Id
							JOIN dbo.Feature f ON v.FeatureId = f.Id
							WHERE sku.ProductId = @productId AND sku.IsDeleted=0 AND sku.Status =1 AND v.IsDeleted=0 AND v.Status=1
							AND f.IsDeleted=0 AND f.Status =1 AND p.Status=1 AND p.IsDeleted=0 ";
            return DbManage.Query<ProductSkuDto>(sql, new { productId });
        }

        public bool AddAmount(int productId, int num)
        {
            var sql = @"Update Product Set StoreAmount=StoreAmount+@num where Id=@productId;";
            return DbManage.Execute(sql, new { productId, num }) > 0;
        }

        public IEnumerable<OrderItemDto> GetProductsByTradeNo(string tradeNo)
        {
            var sql = @"SELECT oi.ProductId as ProductId, oi.Num FROM OrderItem oi
INNER JOIN [Order] o on oi.orderId=o.id
WHERE o.tradeNo = @tradeNo;";
            return DbManage.Query<OrderItemDto>(sql, new { tradeNo });
        }

    }
}