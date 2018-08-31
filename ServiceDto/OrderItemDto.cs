namespace ServiceDto
{
	public class OrderItemDto
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string FeatureName { get; set; }

		/// <summary>
		/// 商品属性值
		/// </summary>
		public string FeatureValue { get; set; }

		/// <summary>
		/// 购买数量
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// 单价
		/// </summary>

		public int Price { get; set; }

		/// <summary>
		/// 总金额（分）
		/// </summary>
		public int TotalFee { get; set; }

		/// <summary>
		/// 商品图片地址
		/// </summary>
		public string ImgUrl { get; set; }
	}
}