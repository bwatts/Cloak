using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cloak.Http.Media
{
	[ContractClass(typeof(MediaFormatContract))]
	public abstract class MediaFormat : BufferedMediaTypeFormatter
	{
		protected MediaFormat()
		{
			SetDefaultSupportedEncodings();
		}

		protected MediaFormat(MediaType mediaType) : this()
		{
			Supports(mediaType);
		}

		protected MediaFormat(params MediaType[] mediaTypes) : this()
		{
			Supports(mediaTypes);
		}

		public virtual MediaType PreferredMediaType
		{
			get { return SupportedMediaTypes.Select(supportedMediaType => new MediaType(supportedMediaType)).FirstOrDefault(); }
		}

		public virtual bool HasPreferredMediaType
		{
			get { return SupportedMediaTypes.Any(); }
		}

		public sealed override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			WriteMedia(type, value, writeStream, content);
		}

		public sealed override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			try
			{
				return ReadMedia(type, readStream, content, formatterLogger);
			}
			catch(FormatException exception)
			{
				var message = content != null && content.Headers.ContentType != null
					? Resources.UnableToReadMediaNegotiatedFrom.FormatInvariant(type, content.Headers.ContentType.MediaType)
					: Resources.UnableToReadMedia.FormatInvariant(type);

				throw new MediaFormatException(message.ToString(), exception);
			}
		}

		protected abstract void WriteMedia(Type type, object value, Stream writeStream, HttpContent content);

		protected abstract object ReadMedia(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger);

		protected void SetDefaultSupportedEncodings()
		{
			SupportedEncodings.Clear();

			SupportedEncodings.Add(Encoding.UTF8);
		}

		protected void Supports(MediaType mediaType)
		{
			Contract.Requires(mediaType != null);

			// Adding supported media types in reverse order ensures the most specific ones are at the top of the list.
			//
			// This is important during content formatting and negotiation.

			SupportedMediaTypes.Insert(0, mediaType.ToHeaderValue());
		}

		protected void Supports(IEnumerable<MediaType> mediaTypes)
		{
			Contract.Requires(mediaTypes != null);

			foreach(var mediaType in mediaTypes)
			{
				Supports(mediaType);
			}
		}

		protected void Supports(params MediaType[] mediaTypes)
		{
			Supports(mediaTypes as IEnumerable<MediaType>);
		}
	}

	[ContractClass(typeof(MediaFormatContract<,>))]
	public abstract class MediaFormat<TMedia, TRepresentation> : MediaFormat
	{
		protected MediaFormat() : base()
		{}

		protected MediaFormat(MediaType mediaType) : base(mediaType)
		{}

		protected MediaFormat(params MediaType[] mediaTypes) : base(mediaTypes)
		{}

		public override bool CanWriteType(Type type)
		{
			return type == typeof(TMedia);
		}

		public override bool CanReadType(Type type)
		{
			return type == typeof(TMedia);
		}

		protected override void WriteMedia(Type type, object value, Stream writeStream, HttpContent content)
		{
			WriteRepresentation(ConvertToRepresentation((TMedia) value), writeStream, content);
		}

		protected override object ReadMedia(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			return ConvertFromRepresentation(ReadRepresentation(readStream, content, formatterLogger), formatterLogger);
		}

		protected abstract TRepresentation ConvertToRepresentation(TMedia media);

		protected abstract TMedia ConvertFromRepresentation(TRepresentation representation, IFormatterLogger formatterLogger);

		protected abstract void WriteRepresentation(TRepresentation representation, Stream stream, HttpContent content);

		protected abstract TRepresentation ReadRepresentation(Stream stream, HttpContent content, IFormatterLogger formatterLogger);
	}

	[ContractClassFor(typeof(MediaFormat))]
	internal abstract class MediaFormatContract : MediaFormat
	{
		protected override void WriteMedia(Type type, object value, Stream writeStream, HttpContent content)
		{
 			Contract.Requires(type != null);
			Contract.Requires(writeStream != null);
			Contract.Requires(content != null);
		}

		protected override object ReadMedia(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
 			Contract.Requires(type != null);
			Contract.Requires(readStream != null);
			Contract.Requires(content != null);

			return null;
		}
	}

	[ContractClassFor(typeof(MediaFormat<,>))]
	internal abstract class MediaFormatContract<TMedia, TRepresentation> : MediaFormat<TMedia, TRepresentation>
	{
		protected override TRepresentation ConvertToRepresentation(TMedia media)
		{
			return default(TRepresentation);
		}

		protected override TMedia ConvertFromRepresentation(TRepresentation representation, IFormatterLogger formatterLogger)
		{
			return default(TMedia);
		}

		protected override void WriteRepresentation(TRepresentation representation, Stream stream, HttpContent content)
		{
			Contract.Requires(stream != null);
			Contract.Requires(content != null);
		}

		protected override TRepresentation ReadRepresentation(Stream stream, HttpContent content, IFormatterLogger formatterLogger)
		{
			Contract.Requires(stream != null);
			Contract.Requires(content != null);

			return default(TRepresentation);
		}
	}
}