using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	[Table("[dbo].[ShoppingCart]")]
	public class ShoppingCart : ModelBase
	{
		public int UserId { get; set; }
		public int ProductSkuId { get; set; }
		public int Num { get; set; }
	}
}