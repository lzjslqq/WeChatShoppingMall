using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Utility;

namespace Api.Handlers
{
	public class CustomErrorMessageHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
			{
				HttpResponseMessage response = responseToCompleteTask.Result;
				HttpError error = null;
				if (response.TryGetContentValue<HttpError>(out error))
				{
					//添加自定义错误处理
					//error.Message = "Your Customized Error Message";
				}

				if (error != null)
				{
					//获取抛出自定义异常，有拦截器统一解析
					throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
					{
						//封装处理异常信息，返回指定JSON对象
						Content = new StringContent(JsonHelper.Serialize(new { errorCode = responseToCompleteTask.Result.StatusCode, errorMessage = error.Message }), Encoding.UTF8, "application/json"),
						ReasonPhrase = "Exception"
					});
				}
				else
				{
					return response;
				}
			});
		}
	}
}