using Common;

namespace ServiceDto
{
    public class OrderListViewDto
    {
        public int Id { get; set; }
        public string TradeNo { get; set; }
        public int TotalFee { get; set; }
        public int PostFee { get; set; }
        public int Payment { get; set; }
        public int Status { get; set; }
        public string StatusName
        {
            get
            {
                var result = "";
                switch (Status)
                {
                    case (int)Enums.OrderStatus.未付款:
                        result = "待付款";
                        break;
                    case (int)Enums.OrderStatus.未发货:
                        result = "待发货";
                        break;
                    case (int)Enums.OrderStatus.已发货:
                        result = "已发货";
                        break;
                    case (int)Enums.OrderStatus.交易成功:
                        result = "已完成";
                        break;
                    case (int)Enums.OrderStatus.交易关闭:
                        result = "已取消";
                        break;
                }
                return result;
            }
        }
        public string UserMessage { get; set; }
        /// <summary>
        ///  商品（种类）数量
        /// </summary>
        public int ProductCount { get; set; }

        #region 商品信息（一条）

        public OrderItemViewDto Product { get; set; }

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

    }
}