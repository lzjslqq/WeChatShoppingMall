using Model;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
	public interface IFeatureService : IBaseService<Feature>
	{
		IEnumerable<FeatureWithValueDto> GetFeatureWithValues();

        IEnumerable<FeatureWithValueDto> GetProductFeatureWithValues(int productId);
        
	}
}