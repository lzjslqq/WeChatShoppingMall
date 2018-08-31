namespace ServiceDto
{
	public class ProductSkuDto
	{
		/// <summary>
		/// 商品特征量Id
		/// </summary>
		public int FeatureId { get; set; }

		/// <summary>
		/// 商品特征量
		/// </summary>
		public string FeatureName { get; set; }

		/// <summary>
		/// 商品特征值
		/// </summary>
		public string FeatureValue { get; set; }

		/// <summary>
		/// 商品特征值Id
		/// </summary>
		public int ValueId { get; set; }

		/// <summary>
		/// 库存
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// 价格
		/// </summary>
		public int Price { get; set; }
	}
}