using ClientDto.Backend;
using Common;
using Model;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
    public interface IProductService : IBaseService<Product>
    {
        IEnumerable<ProductFileDto> GetProductFiles(int productId, Enums.FileType fileType);

        IEnumerable<ProductSkuDto> GetProductSku(int productId);

        IEnumerable<int> GetFeatureValueIds(int productId);

        bool Add(ProductDetailDto dto);

        bool UpdateDetail(ProductDetailDto dto);
    }
}