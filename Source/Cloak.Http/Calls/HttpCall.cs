using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Cloak.Http.Calls
{
	public sealed class HttpCall : IHttpCall
	{
		private readonly Func<HttpClient, Task<HttpResponseMessage>> _callSendAsync;

		public HttpCall(Func<HttpClient, Task<HttpResponseMessage>> callSendAsync)
		{
			Contract.Requires(callSendAsync != null);

			_callSendAsync = callSendAsync;
		}

		public Task<HttpResponseMessage> SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			return _callSendAsync(client);
		}
	}

	public sealed class HttpCall<TResult> : IHttpCall<TResult>
	{
		private readonly IHttpCall _baseCall;
		private readonly Func<HttpContent, Task<TResult>> _readResult;
		private readonly Func<HttpContent, MediaFormats, Task<TResult>> _readResultWithFormats;

		public HttpCall(IHttpCall baseCall, Func<HttpContent, Task<TResult>> readResult)
		{
			Contract.Requires(baseCall != null);
			Contract.Requires(readResult != null);

			_baseCall = baseCall;
			_readResult = readResult;
		}

		public HttpCall(IHttpCall baseCall, Func<HttpContent, MediaFormats, Task<TResult>> readResultWithFormats)
		{
			Contract.Requires(baseCall != null);
			Contract.Requires(readResultWithFormats != null);

			_baseCall = baseCall;
			_readResultWithFormats = readResultWithFormats;
		}

		public async Task<TResult> SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			var response = await _baseCall.SendAsync(client, mediaFormats);

			return await ReadResult(response.Content, mediaFormats);
		}

		private Task<TResult> ReadResult(HttpContent content, MediaFormats mediaFormats)
		{
			return _readResult != null ? _readResult(content) : _readResultWithFormats(content, mediaFormats);
		}
	}
}