using System;

namespace ClientDto.Backend
{
	public class ProductListDto
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Code { get; set; }
		public int Price { get; set; }
		public string Summary { get; set; }

		public DateTime CreateTime { get; set; }
	}
}