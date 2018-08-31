using System.Collections.Generic;

namespace ServiceDto
{
    public class OrderDetailViewDto
    {
        public int Id { get; set; }
        public string TradeNo { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 邮费
        /// </summary>
        public int PostFee { get; set; }
        /// <summary>
        /// 实付
        /// </summary>
        public int Payment { get; set; }
        public int Status { get; set; }
        public string UserMessage { get; set; }

        #region 商品列表

        public IEnumerable<OrderItemViewDto> ProductList { get; set; }

        #endregion

        #region 收货地址信息

        public string Receiver { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        #endregion

        #region 优惠券信息

        public int UserCouponId { get; set; }
        public string CouponTitle { get; set; }
        public int CouponFee { get; set; }

        #endregion

        public string TotalFeeStr
        {
            get { return ((float)(TotalFee / 100.00)).ToString("F2"); }
        }
        public string PostFeeStr
        {
            get { return ((float)(PostFee / 100.00)).ToString("F2"); }
        }
        public string PaymentStr
        {
            get { return ((float)(Payment / 100.00)).ToString("F2"); }
        }
        public string CouponFeeStr
        {
            get { return ((float)(CouponFee / 100.00)).ToString("F2"); }
        }

    }
}