using Dapper.Contrib.Extensions;
using System;

namespace Model
{
	public class ModelBase
    {
        /// <summary>
        /// 排序
        /// </summary>
        [IdentityKey]
		public long Id { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		public int Sort { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 创建者
		/// </summary>
		public string Creator { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime { get; set; }

		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 软删除标志
		/// </summary>
		public int IsDeleted { get; set; }
	}
}