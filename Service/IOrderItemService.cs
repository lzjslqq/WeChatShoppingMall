using Model;
using Service.Base;
using ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public interface IOrderItemService : IBaseService<OrderItem>
	{
		IEnumerable<OrderItemDto> GetOrderItems(int orderId);
        IEnumerable<OrderItemViewDto> GetAllByOrderId(int orderId);
	}
}