namespace Model
{
	public class OrderItem : ModelBase
	{
		/// <summary>
		/// 商品Id
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		///	订单Id
		/// </summary>
		public int OrderId { get; set; }

		public string FeatureName { get; set; }
		public string FeatureValue { get; set; }

		/// <summary>
		/// 购买数量
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// 商品标题
		/// </summary>
		public string Name { get; set; }

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