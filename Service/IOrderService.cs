using Common;
using Model;
using Service.Base;
using ServiceDto;

namespace Service
{
	public interface IOrderService : IBaseService<Order>
	{
        OrderDetailViewDto GetDetail(int userId, int orderId);

        bool CancelOrder(int userId, string tradeNo);

		Enums.PayErrorMsg CheckParamsValid(GenerateOrderDto dto);

		string GenerateOrder(GenerateOrderDto dto, out Enums.PayErrorMsg msg);

	    OrderItemViewDto GetDefaultProduct(int orderId);

	    PayOrderDto GetPayOrderInfoByTradeNo(string tradeNo);

	    bool UpdateOrderStatus(string tradeNo, int status);

	    bool ExistOrder(int userId, string tradeNo);

	    OrderStatViewDto GetStatCountInfo(int userId);
	}
}