namespace Common
{
	public class PageInfo
	{
		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public int ItemCount { get; set; }

		public int PageCount { get { return ItemCount % PageSize == 0 ? ItemCount / PageSize : ((ItemCount / PageSize) + 1); } }

		public PageInfo()
		{
			PageIndex = 1;
			PageSize = 10;
		}

		public PageInfo(int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
		}

		public PageInfo(int pageIndex, int pageSize, int itemCount)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			ItemCount = itemCount;
		}
	}
}