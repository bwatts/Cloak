using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	public class MediaFormats : IReadOnlyList<MediaFormat>
	{
		private readonly IList<MediaFormat> _formats;
		private readonly MediaFormat _apiErrorFormat;

		public MediaFormats(IList<MediaFormat> formats)
		{
			Contract.Requires(formats != null);

			_formats = formats;

			_apiErrorFormat = _formats.FirstOrDefault(format => format.CanReadType(typeof(ApiError)));
		}

		public MediaFormats(params MediaFormat[] formats) : this(formats as IList<MediaFormat>)
		{}

		public MediaFormat this[int index]
		{
			get { return _formats[index]; }
		}

		public int Count
		{
			get { return _formats.Count; }
		}

		public IEnumerator<MediaFormat> GetEnumerator()
		{
			return _formats.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public MediaFormat FirstCanReadOrDefault(Type contentType)
		{
			Contract.Requires(contentType != null);

			return _formats.FirstOrDefault(format => format.CanReadType(contentType));
		}

		public MediaFormat FirstCanWriteOrDefault(Type contentType)
		{
			Contract.Requires(contentType != null);

			return _formats.FirstOrDefault(format => format.CanWriteType(contentType));
		}

		public MediaFormat FirstCanRead(Type contentType)
		{
			Contract.Requires(contentType != null);

			var selectedFormat = FirstCanReadOrDefault(contentType);

			if(selectedFormat == null)
			{
				throw new ApiException(Resources.NoMediaFormatCanReadType.FormatInvariant(contentType));
			}

			return selectedFormat;
		}

		public MediaFormat FirstCanWrite(Type contentType)
		{
			Contract.Requires(contentType != null);

			var selectedFormat = FirstCanWriteOrDefault(contentType);

			if(selectedFormat == null)
			{
				throw new ApiException(Resources.NoMediaFormatCanWriteType.FormatInvariant(contentType));
			}

			return selectedFormat;
		}

		public bool TryReadAsApiErrorAsync(HttpContent content, out Task<ApiError> readTask)
		{
			var isApiError = content != null && IsApiError(content);

			readTask = isApiError ? ReadAsApiErrorAsync(content) : null;

			return isApiError;
		}

		public HttpContent GetHttpContent(object content)
		{
			return content == null ? GetEmptyContent() : GetObjectContent(content);
		}

		private bool IsApiError(HttpContent content)
		{
			return _apiErrorFormat != null && content.HasFormat(_apiErrorFormat);
		}

		private Task<ApiError> ReadAsApiErrorAsync(HttpContent content)
		{
			return content.ReadAsAsync<ApiError>(_apiErrorFormat);
		}

		private static HttpContent GetEmptyContent()
		{
			return new StreamContent(new MemoryStream());
		}

		private HttpContent GetObjectContent(object content)
		{
			var contentType = content.GetType();

			return new ObjectContent(contentType, content, FirstCanWrite(contentType));
		}
	}
}