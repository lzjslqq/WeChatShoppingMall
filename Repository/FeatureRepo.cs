using DapperExtension.Core;
using Model;
using ServiceDto;
using System.Collections.Generic;

namespace Repository
{
    public class FeatureRepo : BaseRepo<Feature>
    {
        public FeatureRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<FeatureWithValueDto> GetFeatureWithValues()
        {
            string sql = @" SELECT f.Id as FeatureId,f.Name,v.Id AS ValueId,v.[value] FROM dbo.feature f JOIN dbo.FeatureValue v ON f.Id = v.FeatureId WHERE f.status =1 AND f.isdeleted=0 AND v.status =1 AND v.isdeleted=0";

            return DbManage.Query<FeatureWithValueDto>(sql);
        }

        public IEnumerable<FeatureWithValueDto> GetProductFeatureWithValues(int productId)
        {
            string sql = @"SELECT p.Id as ProductId, f.Id as FeatureId, f.Name, f.DisplayName as DisplayName, 
	fv.Id as ValueId, fv.[Value] as [Value]
FROM FeatureValue fv
INNER JOIN Feature f on f.Id = fv.FeatureId
INNER JOIN ProductSku sku on sku.FeatureValueId = fv.Id
INNER JOIN Product p on p.Id = sku.ProductId
WHERE p.Id = @productId and p.Status = 1";

            return DbManage.Query<FeatureWithValueDto>(sql, new { productId });
        }
    }
}