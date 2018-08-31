using System;
using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("dbo.[User]")]
	public class User : ModelBase
	{
		public string Name { get; set; }
		public string NickName { get; set; }
		public string OpenId { get; set; }
		public string UnionId { get; set; }

		public int Sex { get; set; }
		public string Phone { get; set; }
		public string AvatarUrl { get; set; }

		/// <summary>
		/// 消费总金额
		/// </summary>
		public int ConsumeFee { get; set; }

		/// <summary>
		/// 余额
		/// </summary>
		public int Fee { get; set; }

		/// <summary>
		/// 用户等级
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// 最近登录时间
		/// </summary>
		public DateTime RecentLoginTime { get; set; }

		/// <summary>
		/// 最近支付时间
		/// </summary>
		public DateTime RecentPayTime { get; set; }

		/// <summary>
		/// Token
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Token过期时间
		/// </summary>
		public DateTime TokenExpireTime { get; set; }
	}
}