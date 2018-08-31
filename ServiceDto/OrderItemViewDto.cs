namespace ServiceDto
{
    public class OrderItemViewDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Num { get; set; }
        public int TotalFee { get; set; }
        public string FeatureName { get; set; }
        public string FeatureValue { get; set; }
        public string ImgUrl { get; set; }

        public string PriceStr
        {
            get { return ((float)(Price / 100.00)).ToString("F2"); }
        }
        public string TotalFeeStr
        {
            get { return ((float)(TotalFee / 100.00)).ToString("F2"); }
        }

    }
}