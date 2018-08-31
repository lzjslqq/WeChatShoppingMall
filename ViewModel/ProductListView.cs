using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
	public class ProductListView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CoverUrl { get; set; }
		public string Code { get; set; }
		public int Price { get; set; }

		public DateTime AddTime { get; set; }
	}
}