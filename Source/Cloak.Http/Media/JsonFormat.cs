using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cloak.Http.Media
{
	public abstract class JsonFormat<TMedia> : MediaFormat<TMedia, JToken>
	{
		protected JsonFormat() : base()
		{}

		protected JsonFormat(MediaType mediaType) : base(mediaType)
		{}

		protected JsonFormat(params MediaType[] mediaTypes) : base(mediaTypes)
		{}

		protected override void WriteRepresentation(JToken representation, Stream stream, HttpContent content)
		{
			using(var writer = new StreamWriter(stream))
			using(var jsonWriter = new JsonTextWriter(writer))
			{
				representation.WriteTo(jsonWriter);
			}
		}

		protected override JToken ReadRepresentation(Stream stream, HttpContent content, IFormatterLogger formatterLogger)
		{
			try
			{
				using(var reader = new StreamReader(stream))
				using(var jsonReader = new JsonTextReader(reader))
				{
					return JToken.ReadFrom(jsonReader);
				}
			}
			catch(JsonException exception)
			{
				formatterLogger.LogError(exception.Source, exception);

				throw new FormatException(Resources.StreamIsInvalidJsonToken, exception);
			}
		}
	}
}