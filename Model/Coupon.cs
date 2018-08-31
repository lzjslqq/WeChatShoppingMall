using Dapper.Contrib.Extensions;
using System;

namespace Model
{
	/// <summary>
	/// 优惠券
	/// </summary>
	[Table("dbo.Coupon")]
	public class Coupon : ModelBase
	{
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 图片地址
		/// </summary>
		public string ImgUrl { get; set; }

		/// <summary>
		/// 使用条件：生效时间
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// 使用条件：结束时间
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// 使用条件：最小金额（分）
		/// </summary>
		public int MinFee { get; set; }

		/// <summary>
		/// 使用条件：最小数量
		/// </summary>
		public int MinNum { get; set; }

		/// <summary>
		/// 使用条件：用户最小等级
		/// </summary>
		public int MinUserLevel { get; set; }

		/// <summary>
		/// 适用商品关系表ID
		/// </summary>
		public int CouponProductId { get; set; }

		/// <summary>
		/// 减免金额（分）
		/// </summary>
		public int RebateFee { get; set; }

		/// <summary>
		/// 优先级
		/// </summary>
		public int Priority { get; set; }

		/// <summary>
		/// 用户最多可拥有数量
		/// </summary>
		public int MaxOwnLimit { get; set; }

		/// <summary>
		/// 用户领取后有效期：天
		/// </summary>
		public int ValidDays { get; set; }

		/// <summary>
		/// 是否长期有效
		/// </summary>
		public int IsPersistent { get; set; }
	}
}