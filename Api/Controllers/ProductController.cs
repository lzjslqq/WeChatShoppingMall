using ClientDto.Api;
using Common;
using Model.Common;
using Service;
using System.Linq;
using System.Web.Http;
using Api.App_Start;

namespace Api.Controllers
{
    public class ProductController : ApiController
    {
        public readonly IProductService _productService;
        public readonly IFeatureService _featureService;

        public ProductController(IProductService productService, IFeatureService featureService)
        {
            _productService = productService;
            _featureService = featureService;
        }

        [HttpGet]
        public ComplexResponse<ProductDetailDto> Detail(int id)
        {
            ProductDetailDto product = new ProductDetailDto();
            if (id > 0)
            {
                product = _productService.Get(id).ToProductDetailDto();
                if (product != null)
                {
                    var files = _productService.GetProductFiles(id, Enums.FileType.All);
                    product.CoverImages = files.Where(f => f.Type == (int)Enums.FileType.DetailBanner).Select(f => f.Url).ToList();
                    product.DetailImages = files.Where(f => f.Type == (int)Enums.FileType.Detail).Select(f => f.Url).ToList();
                    product.FeatureWithValues = _featureService.GetProductFeatureWithValues(id);
                }
            }

            return new ComplexResponse<ProductDetailDto>(1, data: product);
        }

        [HttpGet]
        public ComplexResponse<object> List(int classId, int pageIndex = 1, int pageSize = 10)
        {
            var columns = @"c.Id as ClassId, c.Name as ClassName, p.id, p.Name, p.CoverUrl, p.Description, p.OriginalPrice, p.Price, p.SaleAll, p.SaleMonth,p.CommentAll";
            var tables = @"[Product] p INNER JOIN Class c on c.Id = p.ClassId ";
            var where = "and c.Id = @classId AND c.IsDeleted = 0 AND p.IsDeleted = 0";
            var orderby = "ORDER BY p.SaleAll DESC, p.CreateTime DESC";

            int rowCount;
            var list = _productService.GetPagerList<ProductDetailDto>(columns, tables, where, orderby, out rowCount, pageIndex, pageSize, new { classId });

            var pageInfo = new PageInfo(pageIndex, pageSize, rowCount);

            return new ComplexResponse<object>(1, "成功", new { list, pageInfo });
        }
    }
}