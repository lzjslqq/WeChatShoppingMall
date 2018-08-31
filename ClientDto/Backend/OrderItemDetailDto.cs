using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDto.Backend
{
	public class OrderItemDetailDto
	{
		/// <summary>
		/// 商品名
		/// </summary>
		public int ProductName { get; set; }

		/// <summary>
		/// 商品属性
		/// </summary>
		public string FeatureValue { get; set; }

		/// <summary>
		/// 购买数量
		/// </summary>
		public int Num { get; set; }

		/// <summary>
		/// 商品标题
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 商品价格（分）
		/// </summary>
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