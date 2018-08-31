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
	public interface IOrderShippingService : IBaseService<OrderShipping>
	{
		OrderShippingDto GetOrderShipping(int orderId);
	}
}