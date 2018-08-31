using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
	public class FeatureService : BaseService<Feature>, IFeatureService
    {
        public IEnumerable<FeatureWithValueDto> GetFeatureWithValues()
        {
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new FeatureRepo(cxt);
                return repo.GetFeatureWithValues();
            }
        }

        public IEnumerable<FeatureWithValueDto> GetProductFeatureWithValues(int productId)
        {
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new FeatureRepo(cxt);
                return repo.GetProductFeatureWithValues(productId);
            }
        }
	}
}