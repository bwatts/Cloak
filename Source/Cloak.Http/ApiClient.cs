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

		public ApiClient(MediaFormats mediaFormats, Func<HttpClient> httpClientFactory)
		{
			Contract.Requires(mediaFormats != null);
			Contract.Requires(httpClientFactory != null);

			MediaFormats = mediaFormats;
			_httpClientFactory = httpClientFactory;
		}

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