using ClientDto.Backend;
using Common;
using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public IEnumerable<ProductFileDto> GetProductFiles(int productId, Enums.FileType fileType)
        {
            if (productId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ProductRepo(cxt);
                return repo.GetProductFiles(productId, (int)fileType);
            }
        }

        public IEnumerable<ProductSkuDto> GetProductSku(int productId)
        {
            if (productId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ProductRepo(cxt);
                return repo.GetProductSku(productId);
            }
        }

        public IEnumerable<int> GetFeatureValueIds(int productId)
        {
            if (productId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ProductSkuRepo(cxt);
                return repo.GetFeatureValueIds(productId);
            }
        }

        public bool Add(ProductDetailDto dto)
        {
            using (var cxt = DbContext(DbOperation.Write))
            {
                if (dto.Id == 0)
                {
                    cxt.BeginTransaction();
                    var repo = new ProductRepo(cxt);
                    var product = new Product
                    {
                        Code = dto.Code,
                        Name = dto.Name,
                        CoverUrl = dto.CoverUrl,
                        ClassId = dto.ClassId,
                        Summary = dto.Summary,
                        Description = dto.Description,
                        OriginalPrice = dto.OriginalPrice,
                        Price = dto.Price,
                        StoreAmount = dto.StoreAmount,
                        UpdateTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        IsDeleted = 0,
                        Status = 1
                        //SaleAll = dto.SaleAll
                        //CommentAll = dto.CommentAll
                    };
                    long id = repo.Insert(product);

                    if (id > 0)
                    {
                        var fileRepo = new FileRepo(cxt);
                        var fileList = new List<File>();

                        if (!string.IsNullOrEmpty(dto.DetailImages.FirstOrDefault()))
                        {
                            var detailImg = new File
                            {
                                ProductId = (int)id,
                                Url = dto.DetailImages.FirstOrDefault(),
                                Type = (int)Common.Enums.FileType.Detail,
                                UpdateTime = DateTime.Now,
                                CreateTime = DateTime.Now,
                                IsDeleted = 0,
                                Status = 1
                            };
                            fileList.Add(detailImg);
                        }

                        if (dto.CoverImages.Any())
                        {
                            foreach (var img in dto.CoverImages)
                            {
                                var coverImg = new File
                                {
                                    ProductId = (int)id,
                                    Url = img,
                                    Type = (int)Enums.FileType.DetailBanner,
                                    UpdateTime = DateTime.Now,
                                    CreateTime = DateTime.Now,
                                    IsDeleted = 0,
                                    Status = 1
                                };
                                fileList.Add(coverImg);
                            }
                        }

                        if (fileList.Any())
                        {
                            fileRepo.Insert(fileList);
                        }

                        var skuRepo = new ProductSkuRepo(cxt);
                        var skuList = new List<ProductSku>();
                        foreach (var featureValueId in dto.AttributeIdSet)
                        {
                            var sku = new ProductSku
                            {
                                ProductId = (int)id,
                                FeatureValueId = featureValueId,
                                Num = dto.StoreAmount,
                                Price = dto.Price,
                                UpdateTime = DateTime.Now,
                                CreateTime = DateTime.Now,
                                IsDeleted = 0,
                                Status = 1
                            };
                            skuList.Add(sku);
                        }
                        skuRepo.Insert(skuList);
                        cxt.Commit();
                        return true;
                    }
                    else
                    {
                        cxt.Rollback();
                        return false;
                    }
                }

                return false;
            }
        }

        public bool UpdateDetail(ProductDetailDto dto)
        {
            using (var cxt = DbContext(DbOperation.Write))
            {
                if (dto.Id > 0)
                {
                    cxt.BeginTransaction();
                    var repo = new ProductRepo(cxt);
                    var product = new Product
                    {
                        Id = dto.Id,
                        Code = dto.Code,
                        Name = dto.Name,
                        CoverUrl = dto.CoverUrl,
                        ClassId = dto.ClassId,
                        Summary = dto.Summary,
                        Description = dto.Description,
                        OriginalPrice = dto.OriginalPrice,
                        Price = dto.Price,
                        StoreAmount = dto.StoreAmount,
                        UpdateTime = DateTime.Now,
                    };

                    //var propertyNames = product.GetType().GetProperties().Select(p => p.Name).ToList();
                    var propertyNames = "Code,Name,CoverUrl,ClassId,Summary,Description,OriginalPrice,Price,StoreAmount,UpdateTime".Split(',');
                    if (repo.Update(product, p => propertyNames.Contains(p.Name)))
                    {
                        var fileRepo = new FileRepo(cxt);
                        fileRepo.DeleteFilesOfProduct(dto.Id);
                        var fileList = new List<File>();
                        if (!string.IsNullOrEmpty(dto.DetailImages.FirstOrDefault()))
                        {
                            var detailImg = new File
                            {
                                ProductId = dto.Id,
                                Url = dto.DetailImages.FirstOrDefault(),
                                Type = (int)Common.Enums.FileType.Detail,
                                UpdateTime = DateTime.Now,
                                CreateTime = DateTime.Now,
                                IsDeleted = 0,
                                Status = 1
                            };
                            fileList.Add(detailImg);
                        }

                        if (dto.CoverImages.Any())
                        {
                            foreach (var img in dto.CoverImages)
                            {
                                var coverImg = new File
                                {
                                    ProductId = dto.Id,
                                    Url = img,
                                    Type = (int)Enums.FileType.DetailBanner,
                                    UpdateTime = DateTime.Now,
                                    CreateTime = DateTime.Now,
                                    IsDeleted = 0,
                                    Status = 1
                                };
                                fileList.Add(coverImg);
                            }
                        }

                        if (fileList.Any())
                        {
                            fileRepo.Insert(fileList);
                        }

                        var skuRepo = new ProductSkuRepo(cxt);
                        skuRepo.DeleteProductSku(dto.Id);
                        var skuList = new List<ProductSku>();
                        foreach (var featureValueId in dto.AttributeIdSet)
                        {
                            var sku = new ProductSku
                            {
                                ProductId = dto.Id,
                                FeatureValueId = featureValueId,
                                Num = dto.StoreAmount,
                                Price = dto.Price,
                                UpdateTime = DateTime.Now,
                                CreateTime = DateTime.Now,
                                IsDeleted = 0,
                                Status = 1
                            };
                            skuList.Add(sku);
                        }
                        skuRepo.Insert(skuList);
                        cxt.Commit();

                        return true;
                    }
                    else
                    {
                        cxt.Rollback();
                        return false;
                    }
                }

                return false;
            }
        }
    }
}