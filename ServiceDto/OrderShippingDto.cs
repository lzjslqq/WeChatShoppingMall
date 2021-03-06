﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDto
{
	public class OrderShippingDto
	{
		/// <summary>
		/// 收货人姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 收货人固话
		/// </summary>

		public string Phone { get; set; }

		/// <summary>
		/// 收货人手机
		/// </summary>
		public string Mobile { get; set; }

		public string Province { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		public string PostCode { get; set; }
	}
}