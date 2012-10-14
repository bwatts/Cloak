using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http
{
	public sealed class UriPath : IEquatable<UriPath>, IComparable<UriPath>
	{
		#region Operators

		public static bool operator ==(UriPath x, UriPath y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		public static bool operator !=(UriPath x, UriPath y)
		{
			return !(x == y);
		}

		public static bool operator >(UriPath x, UriPath y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(UriPath x, UriPath y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(UriPath x, UriPath y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(UriPath x, UriPath y)
		{
			return x.CompareTo(y) <= 0;
		}
		#endregion

		public const string Separator = "/";

		public static readonly UriPath Root = new UriPath();

		public UriPath(UriPath basePath, string value)
		{
			Contract.Requires(basePath != null);
			Contract.Requires(!String.IsNullOrEmpty(value));

			BasePath = basePath;
			Value = value;
		}

		private UriPath(UriPath basePath)
		{
			BasePath = basePath;
			Value = "";
		}

		private UriPath()
		{
			Value = "";
		}

		public UriPath BasePath { get; private set; }

		public string Value { get; private set; }

		public override string ToString()
		{
			var uri = new StringBuilder();

			if(BasePath != null)
			{
				var basePathText = BasePath.ToString();

				uri.Append(basePathText);

				if(!String.IsNullOrEmpty(basePathText) && !basePathText.EndsWith(Separator))
				{
					uri.Append(Separator);
				}
			}

			uri.Append(Value);

			return uri.ToString();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as UriPath);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public bool Equals(UriPath other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public int CompareTo(UriPath other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		public UriPath Then(string nextPart)
		{
			return nextPart == Separator ? new UriPath(this) : new UriPath(this, nextPart);
		}

		public UriPath Then(object nextPart)
		{
			Contract.Requires(nextPart != null);

			return Then(nextPart.ToString());
		}

		public UriPath Then(object nextPart, IFormatProvider formatProvider)
		{
			Contract.Requires(nextPart != null);
			Contract.Requires(formatProvider != null);

			return Then(String.Format(formatProvider, "{0}", nextPart));
		}

		public UriPath ThenParameter(string parameterName)
		{
			return new UriPath(this, "{" + parameterName + "}");
		}

		public bool TryGetParameterName(out string name)
		{
			var isParameter = Value.StartsWith("{") && Value.EndsWith("}");

			name = !isParameter ? null : Value.Substring(1, Value.Length - 2);

			return isParameter;
		}

		public Uri ToAbsoluteUri(Uri baseUri)
		{
			Contract.Requires(baseUri != null);

			return new Uri(baseUri, ToString());
		}
	}
}