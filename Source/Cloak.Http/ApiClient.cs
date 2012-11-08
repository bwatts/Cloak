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

		public virtual Task<HttpResponseMessage> MakeCallAsync(IHttpCall call)
		{
			Contract.Requires(call != null);

			return call.SendAsync(_httpClientFactory(), MediaFormats);
		}

		public virtual Task<TResult> MakeCallAsync<TResult>(IHttpCall<TResult> call)
		{
			Contract.Requires(call != null);

			return call.SendAsync(_httpClientFactory(), MediaFormats);
		}
	}
}