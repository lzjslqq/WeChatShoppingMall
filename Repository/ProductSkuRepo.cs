using DapperExtension.Core;
using Model;
using System.Collections.Generic;

namespace Repository
{
    public class ProductSkuRepo : BaseRepo<ProductSku>
    {
        public ProductSkuRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<int> GetFeatureValueIds(int productId)
        {
            string sql = @"SELECT FeatureValueId from dbo.ProductSku s WHERE s.ProductId = @productId AND Status = 1 AND IsDeleted = 0 ";

            return DbManage.Query<int>(sql, new { productId });
        }

        public void DeleteProductSku(int productId)
        {
            string sql = @"Delete From dbo.ProductSku  WHERE ProductId = @productId ";

            DbManage.Execute(sql, new { productId });
        }
    }
}