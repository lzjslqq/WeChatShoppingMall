using Dapper.Contrib.Extensions;

namespace Model
{
    [Table("File")]
    public class File : ModelBase
    {
        public int ProductId { get; set; }

        /// <summary>
        /// 文件链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public int Type { get; set; }

        public string Summary { get; set; }
    }
}