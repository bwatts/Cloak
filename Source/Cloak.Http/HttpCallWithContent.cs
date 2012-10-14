using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Cloak.Http
{
	public sealed class HttpCallWithContent : IHttpCall
	{
		private readonly object _content;
		private readonly Func<HttpClient, HttpContent, Task<HttpResponseMessage>> _callSendAsync;

		public HttpCallWithContent(object content, Func<HttpClient, HttpContent, Task<HttpResponseMessage>> callSendAsync)
		{
			Contract.Requires(callSendAsync != null);

			_content = content;
			_callSendAsync = callSendAsync;
		}

		public Task<HttpResponseMessage> SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			return _callSendAsync(client, mediaFormats.GetHttpContent(_content));
		}
	}
}