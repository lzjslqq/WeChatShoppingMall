using System.Collections.Generic;
using ServiceDto;

namespace ClientDto.Backend
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

        public string CoverUrl { get; set; }
        public int StoreAmount { get; set; }

        /// <summary>
        /// 商品属性值集
        /// </summary>
        public IEnumerable<int> AttributeIdSet { get; set; }

        public IEnumerable<FeatureWithValueDto> FeatureWithValues { get; set; }
    }
}