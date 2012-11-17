using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	public sealed class MediaType : IEquatable<MediaType>, IComparable<MediaType>
	{
		#region Operators

		public static bool operator ==(MediaType x, MediaType y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		public static bool operator !=(MediaType x, MediaType y)
		{
			return !(x == y);
		}

		public static bool operator >(MediaType x, MediaType y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(MediaType x, MediaType y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(MediaType x, MediaType y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(MediaType x, MediaType y)
		{
			return x.CompareTo(y) <= 0;
		}
		#endregion

		#region Known

		public static readonly MediaType Unspecified = new MediaType("");
		public static readonly MediaType ApplicationXWwwFormUrlEncoded = new MediaType("application/x-www-form-urlencoded");
		public static readonly MediaType ApplicationJson = new MediaType("application/json");
		public static readonly MediaType ApplicationXml = new MediaType("application/xml");
		public static readonly MediaType TextJson = new MediaType("text/json");
		public static readonly MediaType TextXml = new MediaType("text/xml");
		public static readonly MediaType Html = new MediaType("text/html");
		public static readonly MediaType Xhtml = new MediaType("application/xhtml+xml");

		// Each of these subsets is ordered from most to least specific. This ensures the most relevant match during content negotiation.

		public static IEnumerable<MediaType> JsonTypes
		{
			get
			{
				yield return ApplicationJson;
				yield return TextJson;
			}
		}

		public static IEnumerable<MediaType> XmlTypes
		{
			get
			{
				yield return ApplicationXml;
				yield return TextXml;
			}
		}

		public static IEnumerable<MediaType> HtmlTypes
		{
			get
			{
				yield return Xhtml;
				yield return Html;
			}
		}
		#endregion

		public MediaType(string value)
		{
			Contract.Requires(value != null);

			Value = value;

			if(value == "")
			{
				Name = "";
				CharSet = "";
				Parameters = new List<NameValueHeaderValue>().AsReadOnly();
				Path = MediaPath.Unspecified;
			}
			else
			{
				InitializeFrom(new MediaTypeHeaderValue(value));
			}
		}

		public MediaType(MediaTypeHeaderValue headerValue)
		{
			Contract.Requires(headerValue != null);

			Value = headerValue.ToString();

			InitializeFrom(headerValue);
		}

		private void InitializeFrom(MediaTypeHeaderValue headerValue)
		{
			Name = headerValue.MediaType ?? "";
			CharSet = headerValue.CharSet ?? "";
			Parameters = headerValue.Parameters.ToList().AsReadOnly();

			Path = new MediaPath(Name);
		}

		public string Value { get; private set; }

		public string Name { get; private set; }

		public string CharSet { get; private set; }

		public ReadOnlyCollection<NameValueHeaderValue> Parameters { get; private set; }

		public MediaPath Path { get; private set; }

		public MediaTypeHeaderValue ToHeaderValue()
		{
			return new MediaTypeHeaderValue(Value);
		}

		public override string ToString()
		{
			return Value;
		}

		public override bool Equals(object other)
		{
			return Equals(other as MediaType);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public bool Equals(MediaType other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public int CompareTo(MediaType other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}
	}
}