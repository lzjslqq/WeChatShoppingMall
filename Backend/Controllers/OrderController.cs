using Backend.App_Start;
using ClientDto.Backend;
using Common;
using Service;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using System;
using Backend.Filters;
using Model;

namespace Backend.Controllers
{
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;
        public readonly IOrderItemService _orderItemService;
        public readonly IOrderShippingService _orderShippingService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService, IOrderShippingService orderShippingService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _orderShippingService = orderShippingService;
        }

        [AuthorizationFilter]
        public ActionResult Index(int? status)
        {
            ViewData["status"] = status;
            return View();
        }

        [AuthorizationFilter]
        public ActionResult List(int? status, string tradeNo = "", string userNickname = "", string shippingCode = "", int page = 1, int rows = 10)
        {
            ViewData["status"] = status;
            StringBuilder where = new StringBuilder(" and IsDeleted = 0 ");
            if (status.HasValue)
            {
                where.Append(" and Status = @status ");
            }
            if (!string.IsNullOrEmpty(tradeNo))
            {
                where.Append(" and charindex(@tradeNo,tradeNo) > 0");
            }
            if (!string.IsNullOrEmpty(userNickname))
            {
                where.Append(" and charindex(@userNickname,userNickname) > 0");
            }
            if (!string.IsNullOrEmpty(shippingCode))
            {
                where.Append(" and charindex(@shippingCode,shippingCode) > 0");
            }

            int rowCount;
            var list = _orderService.GetPagerList<OrderListDto>(@"Id,TradeNo,UserNickname,TotalFee,Payment,CreateTime,
			PayTime,ShippingName,ShippingCode", "[dbo].[Order]", where.ToString(), "order by CreateTime desc",
            out rowCount, page, rows, new { status, tradeNo, userNickname, shippingCode });
            PageInfo pageinfo = new PageInfo(page, rows, rowCount);
            return Json(new { rows = list, page, total = pageinfo.PageCount }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizationFilter]
        public ActionResult Edit(int id)
        {
            OrderDetailDto orderDetail = new OrderDetailDto();
            if (id > 0)
            {
                var order = _orderService.Get(id);
                if (order != null && order.Id > 0)
                {
                    orderDetail = order.ToOrderDetailDto();
                    orderDetail.OrderStatus = order.Status;
                    orderDetail.OrderItems = _orderItemService.GetOrderItems(id).ToList();
                    if (order.Status != (int)Enums.OrderStatus.未付款 || order.Status != (int)Enums.OrderStatus.交易关闭)
                        orderDetail.OrderShipping = _orderShippingService.GetOrderShipping(id);
                }
            }
            return View(orderDetail);
        }

        [HttpPost]
        [AuthorizationFilter]
        public ActionResult Ship(int id, string name, string code)
        {
            string errorMsg = "发货操作失败";
            bool result = false;
            try
            {
                Order model = _orderService.Get(id);

                if (model != null && model.Id > 0)
                {
                    model.UpdateTime = DateTime.Now;
                    model.DeliveryTime = DateTime.Now;
                    model.ShippingName = name;
                    model.ShippingCode = code;
                    model.Status = (int)Enums.OrderStatus.已发货;

                    result = _orderService.Update(model, p => p.Name == "UpdateTime" || p.Name == "DeliveryTime" || p.Name == "ShippingName" || p.Name == "ShippingCode" || p.Name == "Status");
                }
            }
            catch (Exception)
            {
                return Json(new { res = false, msg = "网络连接错误" });
            }

            if (result) errorMsg = "发货操作成功";

            return Json(new { res = result, msg = errorMsg });
        }
    }
}