namespace Common
{
    public class Enums
    {
        #region 商品分类

        /// <summary>
        /// 商品分类
        /// </summary>
        public enum ProductClass
        {
            Chicken = 0, Duck = 1, Gift = 2
        };

        /// <summary>
        /// 商品分类显示中文
        /// </summary>
        public enum ProductClassDisplay
        {
            鸡产品 = 0, 鸭产品 = 1, 伴手礼 = 2
        }

        #endregion 商品分类

        #region 商品图片

        /// <summary>
        /// 商品分类
        /// </summary>
        public enum FileType
        {
            All = 0, DetailBanner = 1, Detail = 2
        };

        #endregion 商品图片

        #region 订单状态

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus
        {
            未付款 = 0, 未发货 = 1, 已发货 = 2, 交易成功 = 3, 交易关闭 = 4
        }

        #endregion 订单状态

        #region 支付方式

        public enum PayType
        {
            微信支付 = 0
        }

        #endregion 支付方式

        #region 支付错误信息

        public enum PayErrorMsg
        {
            成功 = 1,
            失败 = 0,
            参数错误 = 10001,
            用户不存在 = 10100,
            用户无此优惠券或已使用 = 10200,
            优惠金额错误 = 10300,
            优惠券无效 = 10301,
            用户登录信息过期 = 10401,
        }

        #endregion 支付错误信息
    }
}