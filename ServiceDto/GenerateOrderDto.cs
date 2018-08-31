using System.Collections.Generic;

namespace ServiceDto
{
	public class GenerateOrderDto
	{
		public int Id { get; set; }
		public string CartIds { get; set; }
		public int UserId { get; set; }
		public string UserNickname { get; set; }
		public int TotalFee { get; set; }
		public int PostFee { get; set; }
		public int UserCouponId { get; set; }
        public int Payment { get; set; }
        public string ShippingName { get; set; }
        public string UserMessage { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public string PostCode { get; set; }
		public string Receiver { get; set; }
		public string Mobile { get; set; }
		public IEnumerable<OrderItemDto> OrderItemDtos { get; set; }
	}
}