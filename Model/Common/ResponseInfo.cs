using Utility;

namespace Model.Common
{
	/// <summary>
	/// 简单模式，适用于添加修改删除
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SimpleResponse<T> : ISimpleResponse<T>
	{
		/// <summary>
		/// 是否成功调用
		/// </summary>
		public bool Success { get; private set; }

		/// <summary>
		/// 返回的数据
		/// </summary>
		public T Data { get; set; }

		public SimpleResponse(bool success, T data = default(T))
		{
			this.Success = success;
			this.Data = data;
		}
	}

	/// <summary>
	/// 复杂模式，适用于复杂逻辑判断
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ComplexResponse<T> : SimpleResponse<T>, IComplexResponse<T>
	{
		/// <summary>
		/// 调用后返回的错误码
		/// </summary>
		public ErrorMessage ErrorCode { get; private set; }

		/// <summary>
		/// 操作提示信息
		/// </summary>
		public string Message { get; set; }

		public ComplexResponse(int code = -1, string message = "", T data = default(T))
			: base(code == 1, data)
		{
			ErrorMessage errorMsg = default(ErrorMessage);
			if (!EnumHelper.TryParsebyValue<ErrorMessage>(code, out errorMsg))
			{
				errorMsg = ErrorMessage.未知错误;
			}
			ErrorCode = (ErrorMessage)code;
			this.Message = string.IsNullOrEmpty(message) ? errorMsg.ToString() : message;
		}
	}
}