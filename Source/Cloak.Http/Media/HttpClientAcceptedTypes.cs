using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpClientAcceptedTypes
	{
		public static void AcceptMediaType(this HttpClient httpClient, MediaTypeHeaderValue mediaType)
		{
			Contract.Requires(httpClient != null);
			Contract.Requires(mediaType != null);

			httpClient.AcceptMediaType(new MediaTypeWithQualityHeaderValue(mediaType.MediaType));
		}

		public static void AcceptMediaType(this HttpClient httpClient, MediaTypeWithQualityHeaderValue mediaType)
		{
			Contract.Requires(httpClient != null);
			Contract.Requires(mediaType != null);

			httpClient.DefaultRequestHeaders.Accept.Add(mediaType);
		}

		public static void AcceptMediaType(this HttpClient httpClient, MediaFormat mediaFormat)
		{
			Contract.Requires(httpClient != null);
			Contract.Requires(mediaFormat != null);

			if(mediaFormat.HasPreferredMediaType)
			{
				httpClient.AcceptMediaType(mediaFormat.PreferredMediaType.ToHeaderValue());
			}
		}

		public static void AcceptMediaTypes(this HttpClient httpClient, AcceptedTypes acceptedTypes, MediaFormats mediaFormats)
		{
			Contract.Requires(httpClient != null);
			Contract.Requires(acceptedTypes != null);
			Contract.Requires(mediaFormats != null);

			foreach(var mediaFormat in acceptedTypes.GetAcceptedMediaFormats(mediaFormats))
			{
				httpClient.AcceptMediaType(mediaFormat);
			}
		}
	}
}