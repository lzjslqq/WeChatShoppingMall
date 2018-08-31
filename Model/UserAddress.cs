using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("[dbo].[UserAddress]")]
	public class UserAddress : ModelBase
	{
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