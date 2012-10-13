using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Cloak.Http.Calls
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ApiClientCalls
	{
		public static Task<HttpResponseMessage> SendAsync(
			this ApiClient client,
			Func<HttpClient, Task<HttpResponseMessage>> callSendAsync,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCall(callSendAsync);

			return client.MakeCallAsync(new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode));
		}

		public static Task<TResult> SendAsync<TResult>(
			this ApiClient client,
			Func<HttpClient, Task<HttpResponseMessage>> callSendAsync,
			Func<HttpContent, Task<TResult>> readResult,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCall(callSendAsync);

			var apiCall = new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode);

			return client.MakeCallAsync(new HttpCall<TResult>(apiCall, readResult));
		}

		public static Task<TResult> SendAsync<TResult>(
			this ApiClient client,
			Func<HttpClient, Task<HttpResponseMessage>> callSendAsync,
			Func<HttpContent, MediaFormats, Task<TResult>> readResult,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCall(callSendAsync);

			var apiCall = new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode);

			return client.MakeCallAsync(new HttpCall<TResult>(apiCall, readResult));
		}

		public static Task<HttpResponseMessage> SendContentAsync(
			this ApiClient client,
			object content,
			Func<HttpClient, HttpContent, Task<HttpResponseMessage>> callSendAsync,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCallWithContent(content, callSendAsync);

			return client.MakeCallAsync(new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode));
		}

		public static Task<TResult> SendContentAsync<TResult>(
			this ApiClient client,
			object content,
			Func<HttpClient, HttpContent, Task<HttpResponseMessage>> callSendAsync,
			Func<HttpContent, Task<TResult>> readResult,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCallWithContent(content, callSendAsync);

			var apiCall = new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode);

			return client.MakeCallAsync(new HttpCall<TResult>(apiCall, readResult));
		}

		public static Task<TResult> SendContentAsync<TResult>(
			this ApiClient client,
			object content,
			Func<HttpClient, HttpContent, Task<HttpResponseMessage>> callSendAsync,
			Func<HttpContent, MediaFormats, Task<TResult>> readResult,
			AcceptedTypes acceptedTypes = null,
			bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(client != null);

			var httpCall = new HttpCallWithContent(content, callSendAsync);

			var apiCall = new ApiCall(httpCall, acceptedTypes, ensureSuccessStatusCode);

			return client.MakeCallAsync(new HttpCall<TResult>(apiCall, readResult));
		}
	}
}