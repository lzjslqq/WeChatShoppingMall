namespace Model
{
	public class Address : ModelBase
	{
		/// <summary>
		/// 收货人姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 收货人固话
		/// </summary>

		public string Phone { get; set; }

		/// <summary>
		/// 收货人手机
		/// </summary>
		public string Mobile { get; set; }

		public string Province { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Detail { get; set; }
		public string PostCode { get; set; }
	}
}