using ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDto.Api
{
    public class ProductDetailDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 商品封面文件
        /// </summary>
        public IEnumerable<string> CoverImages { get; set; }

        /// <summary>
        /// 商品详情页轮播图
        /// </summary>
        public IEnumerable<string> BannerImages { get; set; }

        /// <summary>
        /// 商品详情文件
        /// </summary>
        public IEnumerable<string> DetailImages { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 现价
        /// </summary>
        public int Price { get; set; }

        public IEnumerable<FeatureWithValueDto> FeatureWithValues { get; set; }

        public int SaleAll { get; set; }
        public int SaleMonth { get; set; }
        public int CommentAll { get; set; }
        public string CoverUrl { get; set; }
        public int StoreAmount { get; set; }

        public string OriginalPriceStr
        {
            get { return ((float)(OriginalPrice / 100.00)).ToString("F2"); }
        }

        public string PriceStr
        {
            get { return ((float)(Price / 100.00)).ToString("F2"); }
        }
    }
}