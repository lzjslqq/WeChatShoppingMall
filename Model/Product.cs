using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("Product")]
    public class Product : ModelBase
    {
        /// <summary>
        /// 货号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 分类（现在暂时直接指定，后期分类可以同时具有多种时加关系表）
        /// </summary>
        public int ClassId { get; set; }

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

        /// <summary>
        /// 总销量
        /// </summary>
        public int SaleAll { get; set; }

        /// <summary>
        /// 月销量
        /// </summary>
        public int SaleMonth { get; set; }

        /// <summary>
        /// 总点击量
        /// </summary>
        public int ClickAll { get; set; }

        /// <summary>
        /// 月点击量
        /// </summary>
        public int ClickMonth { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentAll { get; set; }

        /// <summary>
        ///  商品库存量
        /// </summary>
        public int StoreAmount { get; set; }
    }
}