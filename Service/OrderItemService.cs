using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
	public class OrderItemService : BaseService<OrderItem>, IOrderItemService
	{
		public IEnumerable<OrderItemDto> GetOrderItems(int orderId)
		{
			if (orderId <= 0)
				return null;
			using (var cxt = DbContext(DbOperation.Read))
			{
				var repo = new OrderItemRepo(cxt);
				return repo.GetOrderItems(orderId);
			}
		}

	    public IEnumerable<OrderItemViewDto> GetAllByOrderId(int orderId)
        {
            if (orderId <= 0)
                return null;
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new OrderItemRepo(cxt);
                return repo.GetAllByOrderId(orderId);
            }
	    }
	}
}