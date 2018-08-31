
namespace Model.Common
{
    public interface ISimpleResponse<T>
    {
        /// <summary>
        /// 是否成功调用
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        T Data { get; set; }
    }

    /// <summary>
    /// 复杂模式，适用于复杂逻辑判断
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComplexResponse<T> : ISimpleResponse<T>
    {
        /// <summary>
        /// 调用后返回的错误码
        /// </summary>
        ErrorMessage ErrorCode { get; }

        /// <summary>
        /// 操作提示信息
        /// </summary>
        string Message { get; set; }
    }
}
