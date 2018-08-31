using Api.Filters;
using Api.Formattings;
using Api.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace Api
{
	public static class WebApiConfig
	{
		public static void Configure(HttpConfiguration config)
		{
			// Message Handlers
			config.MessageHandlers.Add(new CustomErrorMessageHandler());

			// Web API configuration and services

			// Formatters
			var jqueryFormatter = config.Formatters.FirstOrDefault(x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));

			config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
			config.Formatters.Remove(jqueryFormatter);

			foreach (var formatter in config.Formatters)
			{
				formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
			}

			// Filters
			config.Filters.Add(new InvalidModelStateFilterAttribute());

			// If ExcludeMatchOnTypeOnly is true then we don't match on type only which means that we return null if we can't match on anything in the request.
			// This is useful for generating 406 (Not Acceptable) status codes.
			config.Services.Replace(typeof(IContentNegotiator), new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));
			config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(new JsonMediaTypeFormatter()));

			// Remove all the validation providers except for DataAnnotationsModelValidatorProvider
			config.Services.RemoveAll(typeof(ModelValidatorProvider), validator => !(validator is DataAnnotationsModelValidatorProvider));
		}

		public class JsonContentNegotiator : IContentNegotiator
		{
			private readonly JsonMediaTypeFormatter _jsonFormatter;

			public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
			{
				_jsonFormatter = formatter;
				// 解决json序列化时的循环引用问题
				_jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				_jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				_jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter()
				{
					DateTimeFormat = "yyyy-MM-dd hh:mm:ss"
				});
			}

			public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
			{
				var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
				return result;
			}
		}
	}
}