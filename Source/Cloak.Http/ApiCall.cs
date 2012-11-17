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
	public sealed class ApiCall : IHttpCall
	{
		private readonly IHttpCall _httpCall;
		private readonly AcceptedTypes _acceptedTypes;
		private readonly bool _ensureSuccessStatusCode;

		public ApiCall(IHttpCall httpCall, AcceptedTypes acceptedTypes = null, bool ensureSuccessStatusCode = true)
		{
			Contract.Requires(httpCall != null);

			_httpCall = httpCall;
			_acceptedTypes = acceptedTypes;
			_ensureSuccessStatusCode = ensureSuccessStatusCode;
		}

		public async Task<HttpResponseMessage> SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			SetAcceptedMediaTypes(client, mediaFormats);

			var response = await _httpCall.SendAsync(client, mediaFormats);

			await CheckApiErrorAsync(response, mediaFormats);

			CheckStatusCode(response);

			return response;
		}

		private void SetAcceptedMediaTypes(HttpClient client, MediaFormats mediaFormats)
		{
			if(_acceptedTypes != null)
			{
				client.AcceptMediaTypes(_acceptedTypes, mediaFormats);
			}
		}

		private async Task CheckApiErrorAsync(HttpResponseMessage response, MediaFormats mediaFormats)
		{
			Task<IApiError> readTask;

			if(mediaFormats.TryReadAsApiErrorAsync(response.Content, out readTask))
			{
				var error = await readTask;

				throw new ApiException(error.Message, error.Code);
			}
		}

		private void CheckStatusCode(HttpResponseMessage response)
		{
			if(_ensureSuccessStatusCode)
			{
				response.EnsureSuccessStatusCode();
			}
		}
	}
}