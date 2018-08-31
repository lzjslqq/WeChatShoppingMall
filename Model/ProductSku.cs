using Dapper.Contrib.Extensions;

namespace Model
{
	[Table("ProductSku")]
	public class ProductSku : ModelBase
	{
		public int ProductId { get; set; }
		public int FeatureValueId { get; set; }
		public int Num { get; set; }
		public int Price { get; set; }
	}
}