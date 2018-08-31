using System;
using Common;
using Utility;
using Model.Common;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;
using Service;
using ServiceDto;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IUserService userService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _userService = userService;
        }

        /// <summary>
        ///  生成订单
        /// </summary>
        [HttpPost]
        public ComplexResponse<object> GenerateOrder([FromBody] GenerateOrderDto dto)
        {
            try
            {
                var msg = Enums.PayErrorMsg.失败;

                var tradeNo = _orderService.GenerateOrder(dto, out msg);
                if (!string.IsNullOrEmpty(tradeNo))
                {
                    msg = Enums.PayErrorMsg.成功;
                }
                LogHelper.Info("\n\n生成订单号：" + tradeNo);

                return new ComplexResponse<object>((int)msg, data: new { tradeNo });
            }
            catch (Exception ex)
            {
                LogHelper.Debug("\n\n生成订单异常：\n", ex);
                return new ComplexResponse<object>((int)Enums.PayErrorMsg.失败, ex.ToString());
            }

        }

        /// <summary>
        ///  查询订单信息，成功后调起支付接口
        /// </summary>
        [HttpGet]
        public ComplexResponse<object> PayOrder(string tradeNo, string token)
        {
            try
            {
                var msg = Enums.PayErrorMsg.失败;
                object data = null;

                if (!string.IsNullOrEmpty(tradeNo))
                {
                    // 校验 token（3rd_session）是否过期
                    var flag = _userService.CheckToken(token);
                    if (flag)
                    {
                        // 查取订单数据（实付费用、openId）
                        var payOrderInfo = _orderService.GetPayOrderInfoByTradeNo(tradeNo);

                        if (!string.IsNullOrEmpty(payOrderInfo.TradeNo) && !string.IsNullOrEmpty(payOrderInfo.OpenId))
                        {
                            // 调起支付接口
                            var test = ConfigurationManager.AppSettings["test"];
                            var appId = ConfigurationManager.AppSettings["appId"];
                            var mchId = ConfigurationManager.AppSettings["mchId"];
                            var body = ConfigurationManager.AppSettings["siteName"];
                            var totalFee = (!string.IsNullOrEmpty(test) && test.Equals("true")) ? 1 : payOrderInfo.TotalFee;
                            var spbillCreateIp = HttpContext.Current.Request.UserHostAddress;
                            var notifyUrl = ConfigurationManager.AppSettings["notifyUrl"] ??
                                            "https://wxapi.xzpfood.com/api/pay/notify";
                            var openId = payOrderInfo.OpenId;
                            var key = ConfigurationManager.AppSettings["appKey"];

                            var requestData = new TenPayV3UnifiedorderRequestData(appId, mchId, body, tradeNo, totalFee,
                                spbillCreateIp, notifyUrl, TenPayV3Type.JSAPI, openId, key, TenPayV3Util.GetNoncestr());
                            UnifiedorderResult result = TenPayV3.Unifiedorder(requestData);

                            LogHelper.Info("\n\n统一下单结果：\n" + JsonHelper.Serialize(result));

                            if (result.IsResultCodeSuccess())
                            {
                                string package = "prepay_id=" + result.prepay_id;
                                string timeStamp = TenPayV3Util.GetTimestamp();
                                string nonceStr = TenPayV3Util.GetNoncestr();
                                string paySign = TenPayV3.GetJsPaySign(appId, timeStamp, nonceStr, package, key);
                                string signType = "MD5";
                                data = new { timeStamp, nonceStr, package, paySign, signType };

                                msg = Enums.PayErrorMsg.成功;
                            }
                        }
                    }
                    else
                    {
                        msg = Enums.PayErrorMsg.用户登录信息过期;
                    }
                }
                else
                {
                    msg = Enums.PayErrorMsg.参数错误;
                }

                return new ComplexResponse<dynamic>((int)msg, msg.ToString(), data);
            }
            catch (Exception ex)
            {
                LogHelper.Debug("\n\n支付异常：\n", ex);
                return new ComplexResponse<object>((int)Enums.PayErrorMsg.失败, ex.ToString());
            }
        }

        [HttpGet]
        public ComplexResponse<object> List(int userId, int type, int pageIndex = 1, int pageSize = 4)
        {
            var msg = ErrorMessage.失败;
            object data = null;

            if (userId > 0)
            {
                // 0-待付款，1-待发货，2-已发货，3-已完成，4-已取消
                var columns = @"o.Id, o.TradeNo, o.TotalFee, o.PostFee, o.Payment, o.Status, o.UserMessage, 
	(SELECT COUNT(1) FROM OrderItem WHERE OrderId=o.Id) as ProductCount";
                var tables = @"[Order] o";
                var where = " AND o.UserId=@userId ";
                var orderby = "ORDER BY o.CreateTime DESC";

                if (type > -1)
                    where += "AND o.Status = @type";
                else
                    where += "AND o.Status != 4";

                int rowCount;
                var list = _orderService.GetPagerList<OrderListViewDto>(columns, tables, where, orderby, out rowCount, pageIndex, pageSize, new { userId, type });
                var pageInfo = new PageInfo(pageIndex, pageSize, rowCount);

                if (list != null && list.Any())
                {
                    foreach (var order in list)
                    {
                        var product = _orderService.GetDefaultProduct(order.Id);
                        order.Product = product;
                    }
                }

                msg = ErrorMessage.成功;
                data = new { list, pageInfo };
            }

            return new ComplexResponse<object>((int)msg, msg.ToString(), data);
        }

        [HttpGet]
        public ComplexResponse<OrderDetailViewDto> Detail(int userId, int orderId)
        {
            var msg = ErrorMessage.失败;
            OrderDetailViewDto data = null;

            if (userId > 0 && orderId > 0)
            {
                data = _orderService.GetDetail(userId, orderId);
                if (data != null)
                {
                    var productList = _orderItemService.GetAllByOrderId(orderId);
                    data.ProductList = productList;
                }
                msg = ErrorMessage.成功;
            }

            return new ComplexResponse<OrderDetailViewDto>((int)msg, msg.ToString(), data);
        }

        // 取消订单
        [HttpGet]
        public ComplexResponse<bool> Cancel(int userId, string tradeNo)
        {
            var msg = ErrorMessage.失败;
            var data = false;

            try
            {
                if (userId > 0 && !string.IsNullOrEmpty(tradeNo) && _orderService.ExistOrder(userId, tradeNo))
                {
                    data = _orderService.CancelOrder(userId, tradeNo);
                    msg = ErrorMessage.成功;
                }

                return new ComplexResponse<bool>((int)msg, msg.ToString(), data);
            }
            catch (Exception ex)
            {
                LogHelper.Debug("\n\n取消订单异常：\n", ex);
                return new ComplexResponse<bool>((int)Enums.PayErrorMsg.失败, ex.ToString());
            }
        }

        [HttpGet]
        public ComplexResponse<OrderStatViewDto> GetStatCountInfo(int userId)
        {
            ErrorMessage msg = ErrorMessage.失败;
            if (userId <= 0)
                msg = ErrorMessage.用户不存在;

            var data = _orderService.GetStatCountInfo(userId) ?? new OrderStatViewDto();
            msg = ErrorMessage.成功;

            return new ComplexResponse<OrderStatViewDto>((int)msg, msg.ToString(), data);
        }

    }
}