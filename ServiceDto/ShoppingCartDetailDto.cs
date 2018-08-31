namespace ServiceDto
{
	public class ShoppingCartDetailDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string CoverUrl { get; set; }
        public string Name { get; set; }
		public int Num { get; set; }
		public int Price { get; set; }
        public int StoreAmount { get; set; }

		public string FeatureName { get; set; }
		public string FeatureValue { get; set; }
		public int FeatureValueId { get; set; }

        public string PriceStr
        {
            get { return ((float)(Price / 100.00)).ToString("F2"); }
        }
        
	}
}