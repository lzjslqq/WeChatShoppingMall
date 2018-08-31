namespace ServiceDto
{
    public class UserAddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IsDefault { get; set; }
        public string Receiver { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}