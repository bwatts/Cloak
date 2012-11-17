using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Cloak.Http
{
	public class ApiClient
	{
		private readonly Func<HttpClient> _httpClientFactory;

		public ApiClient(Uri baseAddress, Func<HttpClient> httpClientFactory, MediaFormats mediaFormats)
		{
			Contract.Requires(baseAddress != null);
			Contract.Requires(httpClientFactory != null);
			Contract.Requires(mediaFormats != null);

			BaseAddress = baseAddress;
			_httpClientFactory = httpClientFactory;
			MediaFormats = mediaFormats;
		}

		public Uri BaseAddress { get; private set; }

		public MediaFormats MediaFormats { get; private set; }

		public virtual async Task<HttpResponseMessage> MakeCallAsync(IHttpCall call)
		{
			Contract.Requires(call != null);

			using(var httpClient = CreateHttpClient())
			{
				return await call.SendAsync(httpClient, MediaFormats);
			}
		}

		public virtual async Task<TResult> MakeCallAsync<TResult>(IHttpCall<TResult> call)
		{
			Contract.Requires(call != null);

			using(var httpClient = CreateHttpClient())
			{
				return await call.SendAsync(httpClient, MediaFormats);
			}
		}

		private HttpClient CreateHttpClient()
		{
			var httpClient = _httpClientFactory();

			httpClient.BaseAddress = BaseAddress;

			return httpClient;
		}
	}
}