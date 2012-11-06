using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	public static class HttpContentReading
	{
		public static bool HasFormat(this HttpContent content, MediaFormat format)
		{
			Contract.Requires(content != null);
			Contract.Requires(format != null);

			return content.Headers.ContentType != null
				&& format.SupportedMediaTypes.Any(supportedMediaType => String.Equals(content.Headers.ContentType.MediaType, supportedMediaType.MediaType, StringComparison.OrdinalIgnoreCase));
		}

		public static bool TryReadAsync<T>(this HttpContent content, MediaFormat format, out Task<T> readTask)
		{
			Contract.Requires(content != null);
			Contract.Requires(format != null);

			var canRead = content.HasFormat(format);

			readTask = canRead
				? content.ReadAsAsync<T>(new[] { format })
				: Task.Run(() => default(T));

			return canRead;
		}

		public static Task<T> ReadAsAsync<T>(this HttpContent content, MediaFormat format)
		{
			return content.ReadAsAsync<T>(new[] { format });
		}

		public static Task<object> ReadAnyAsync<T1, T2>(this HttpContent content, IEnumerable<MediaFormat> formats)
		{
			Contract.Requires(content != null);
			Contract.Requires(formats != null);

			Task<object> readTask;

			if(content.TryReadAsync<T1>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T2>(formats, out readTask))
			{
				return readTask;
			}
			else
			{
				throw new ProtocolViolationException(Resources.UnableToReadContentTwoTypes.FormatInvariant(typeof(T1), typeof(T2)));
			}
		}

		public static Task<object> ReadAnyAsync<T1, T2, T3>(this HttpContent content, IEnumerable<MediaFormat> formats)
		{
			Contract.Requires(content != null);
			Contract.Requires(formats != null);

			Task<object> readTask;

			if(content.TryReadAsync<T1>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T2>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T3>(formats, out readTask))
			{
				return readTask;
			}
			else
			{
				throw new ProtocolViolationException(Resources.UnableToReadContentThreeTypes.FormatInvariant(typeof(T1), typeof(T2), typeof(T3)));
			}
		}

		public static Task<object> ReadAnyAsync<T1, T2, T3, T4>(this HttpContent content, IEnumerable<MediaFormat> formats)
		{
			Contract.Requires(content != null);
			Contract.Requires(formats != null);

			Task<object> readTask;

			if(content.TryReadAsync<T1>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T2>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T3>(formats, out readTask))
			{
				return readTask;
			}
			else if(content.TryReadAsync<T4>(formats, out readTask))
			{
				return readTask;
			}
			else
			{
				throw new ProtocolViolationException(Resources.UnableToReadContentFourTypes.FormatInvariant(typeof(T1), typeof(T2), typeof(T3), typeof(T4)));
			}
		}

		private static bool TryReadAsync<T>(this HttpContent content, IEnumerable<MediaFormat> formats, out Task<object> readTask)
		{
			Contract.Requires(content != null);
			Contract.Requires(formats != null);

			var readFormat = formats.FirstOrDefault(format => format.CanReadType(typeof(T)));

			Task<T> typedReadTask = null;

			var contentRead = readFormat != null && content.TryReadAsync(readFormat, out typedReadTask);

			readTask = !contentRead ? null : typedReadTask.ContinueWith<object>(typedReadTask2 => typedReadTask2.Result);

			return contentRead;
		}
	}
}