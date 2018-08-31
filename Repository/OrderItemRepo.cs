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
	public class OrderItemRepo : BaseRepo<OrderItem>
	{
		public OrderItemRepo(IDbContext dbContext)
			: base(dbContext)
		{
		}

	    public IEnumerable<OrderItemViewDto> GetAllByOrderId(int orderId)
        {
            string sql = @"SELECT * FROM [OrderItem] WHERE OrderId=@orderId ORDER BY Id DESC";

            return DbManage.Query<OrderItemViewDto>(sql, new { orderId });
	    }

		public IEnumerable<OrderItemDto> GetOrderItems(int orderId)
		{
			string sql = @" SELECT Name,FeatureName,FeatureValue,Num,Price,TotalFee,ImgUrl from dbo.OrderItem WHERE orderid = @orderId ";

			return DbManage.Query<OrderItemDto>(sql, new { orderId });
		}
	}
}