namespace ServiceDto
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int Payment { get; set; }
        public int PostFee { get; set; }
        public int IsDeleted { get; set; }
        public int Status { get; set; }
        public string Receiver  { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public int CouponId { get; set; }
        public string CouponTitle { get; set; }
    }
}