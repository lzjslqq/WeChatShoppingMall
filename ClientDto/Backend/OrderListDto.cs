using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDto.Backend
{
	public class OrderListDto
	{
		public int Id { get; set; }
		public string TradeNo { get; set; }
		public string UserNickname { get; set; }
		public int TotalFee { get; set; }
		public int Payment { get; set; }
		public DateTime CreateTime { get; set; }
		public DateTime PayTime { get; set; }
		public string ShippingName { get; set; }
		public string ShippingCode { get; set; }
	}
}