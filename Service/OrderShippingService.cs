using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class OrderShippingService : BaseService<OrderShipping>, IOrderShippingService
	{
		public OrderShippingDto GetOrderShipping(int orderId)
		{
			if (orderId <= 0)
				return null;
			using (var cxt = DbContext(DbOperation.Read))
			{
				var repo = new OrderShippingRepo(cxt);
				return repo.GetOrderShipping(orderId);
			}
		}
	}
}