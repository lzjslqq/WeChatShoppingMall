using Dapper.Contrib.Extensions;
using System;

namespace Model
{
    [Table("[dbo].[Order]")]
    public class Order : ModelBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Computed]
        public string TradeNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 实付金额（分）
        /// </summary>
        public int Payment { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PayType { get; set; }

        public int UserCouponId { get; set; }

        /// <summary>
        /// 邮费
        /// </summary>
        public int PostFee { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 交易关闭时间
        /// </summary>
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// 物流名称
        /// </summary>
        public string ShippingName { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string ShippingCode { get; set; }

        /// <summary>
        /// 买家id
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        public string UserNickname { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// 是否留言
        /// </summary>
        public int IsRate { get; set; }
    }
}