using DapperExtension.Core;
using Model;
using ServiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class OrderShippingRepo : BaseRepo<OrderShipping>
	{
		public OrderShippingRepo(IDbContext dbContext)
			: base(dbContext)
		{
		}

		public OrderShippingDto GetOrderShipping(int orderId)
		{
			string sql = @" SELECT top 1 * FROM  dbo.OrderShipping WHERE OrderId = @orderId ";

			return DbManage.Query<OrderShippingDto>(sql, new { orderId }).FirstOrDefault();
		}
	}
}