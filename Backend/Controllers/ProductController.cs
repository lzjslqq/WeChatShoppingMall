using Backend.App_Start;
using ClientDto.Backend;
using Common;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Backend.Filters;
using Utility;

namespace Backend.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService _productService;
        public readonly IFeatureService _featureService;

        public ProductController(IProductService productService, IFeatureService featureService)
        {
            _productService = productService;
            _featureService = featureService;
        }

        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizationFilter]
        public ActionResult List(string name, string code, int page = 1, int rows = 10)
        {
            int rowCount;
            StringBuilder where = new StringBuilder("and Status =1 ");
            if (!string.IsNullOrEmpty(name))
            {
                where.Append("and charindex(@name,Name)> 0");
            }
            if (!string.IsNullOrEmpty(code))
            {
                where.Append("and charindex(@code,Code)> 0");
            }

            var list = _productService.GetPagerList<ProductListDto>("Id,Name,Code,ClassId,Price,Summary,CreateTime", "[dbo].[Product]", where.ToString(), "order by CreateTime desc", out rowCount, page, rows, new { name, code });
            PageInfo pageinfo = new PageInfo(page, rows, rowCount);
            return Json(new { rows = list, page = page, total = pageinfo.PageCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizationFilter]
        public ActionResult Edit(int id = 0)
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
                    product.AttributeIdSet = _productService.GetFeatureValueIds(id);
                    product.FeatureWithValues = _featureService.GetFeatureWithValues();
                }
            }
            else
            {
                product.CoverImages = new List<string>();
                product.DetailImages = new List<string>();
                product.AttributeIdSet = new List<int>();
                product.FeatureWithValues = _featureService.GetFeatureWithValues();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        [AuthorizationFilter]
        public ActionResult Edit(ProductDetailDto dto, string bannerImgList, string coverImg, string detailImg)
        {
            try
            {
                dto.CoverUrl = coverImg;
                dto.DetailImages = new List<string> { detailImg };
                dto.CoverImages = bannerImgList.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (dto.Id == 0)
                {
                    _productService.Add(dto);
                }
                else
                    _productService.UpdateDetail(dto);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [AuthorizationFilter]
        public ActionResult Delete(int id)
        {
            string errorMsg = "删除失败";
            bool result = false;

            try
            {
                result = _productService.Delete(new Product { Id = id });
                if (result) errorMsg = "删除成功";
            }
            catch (Exception)
            {
                return Json(new { res = result, msg = errorMsg });
            }
            return Json(new { res = result, msg = errorMsg });
        }

        [HttpPost]
        [AuthorizationFilter]
        public ActionResult UploadImg(HttpPostedFileBase file, int type)
        {
            string msg = string.Empty;
            string filePath = string.Empty;
            string imgUrl = string.Empty;

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = FileHelper.CreateRandomFileNameByDateTime(type, extension);
                    var path = Server.MapPath("~/Upload/Product");
                    imgUrl = string.Format("http://{0}:{1}/Upload/Product/{2}", Request.Url.Host, Request.Url.Port, fileName);
                    filePath = Path.Combine(path, fileName);
                    FileHelper.CreateDirectory(path);
                    file.SaveAs(filePath);
                }
            }
            catch (Exception)
            {
                msg = "网络连接失败";
                return Json(new { msg });
            }

            return Json(new { msg = "ok", imgUrl });
        }
    }
}