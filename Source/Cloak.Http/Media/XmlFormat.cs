﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;

namespace Cloak.Http.Media
{
	public abstract class XmlFormat<TMedia> : MediaFormat<TMedia, XElement>
	{
		protected XmlFormat() : base(MediaType.XmlTypes)
		{}

		protected XmlFormat(IEnumerable<MediaType> supportedMediaTypes) : base(supportedMediaTypes.Concat(MediaType.XmlTypes))
		{}

		protected XmlFormat(params MediaType[] supportedMediaTypes) : this(supportedMediaTypes as IEnumerable<MediaType>)
		{}

		protected XmlFormat(MediaType preferredMediaType) : base(preferredMediaType, MediaType.XmlTypes)
		{}

		protected XmlFormat(MediaType preferredMediaType, IEnumerable<MediaType> otherMediaTypes) : base(preferredMediaType, otherMediaTypes.Concat(MediaType.XmlTypes))
		{}

		protected XmlFormat(MediaType preferredMediaType, params MediaType[] otherMediaTypes) : this(preferredMediaType, otherMediaTypes as IEnumerable<MediaType>)
		{}

		protected override void WriteRepresentation(XElement representation, Stream stream, HttpContent content)
		{
			var settings = new XmlWriterSettings
			{
				Encoding = SelectCharacterEncoding(content.Headers),
				Indent = true,
				OmitXmlDeclaration = true,
				CloseOutput = false
			};

			using(var xmlWriter = XmlWriter.Create(stream, settings))
			{
				representation.WriteTo(xmlWriter);
			}
		}

		protected override XElement ReadRepresentation(Stream stream, HttpContent content, IFormatterLogger formatterLogger)
		{
			try
			{
				return XDocument.Load(stream).Root;
			}
			catch(XmlException exception)
			{
				formatterLogger.LogError(exception.SourceUri, exception);

				throw new FormatException(Resources.StreamIsInvalidXmlDocument, exception);
			}
		}
	}
}