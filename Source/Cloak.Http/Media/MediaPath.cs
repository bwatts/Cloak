using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	public sealed class MediaPath : IEquatable<MediaPath>, IComparable<MediaPath>
	{
		#region Operators

		public static bool operator ==(MediaPath x, MediaPath y)
		{
			return Object.ReferenceEquals(x, y) || (!Object.ReferenceEquals(x, null) && x.Equals(y));
		}

		public static bool operator !=(MediaPath x, MediaPath y)
		{
			return !(x == y);
		}

		public static bool operator >(MediaPath x, MediaPath y)
		{
			return x.CompareTo(y) > 0;
		}

		public static bool operator <(MediaPath x, MediaPath y)
		{
			return x.CompareTo(y) < 0;
		}

		public static bool operator >=(MediaPath x, MediaPath y)
		{
			return x.CompareTo(y) >= 0;
		}

		public static bool operator <=(MediaPath x, MediaPath y)
		{
			return x.CompareTo(y) <= 0;
		}
		#endregion

		public const string VendorPrefix = "vnd";
		public const string Separator = ".";
		public const string FormatSeparator = "+";

		public const string HtmlFormat = "html";
		public const string JsonFormat = "json";
		public const string XmlFormat = "xml";

		public static readonly MediaPath Unspecified = new MediaPath();

		public MediaPath()
		{
			Value = "";
			Vendor = "";
			Media = "";
			Format = "";
		}

		public MediaPath(string mediaType)
		{
			Contract.Requires(mediaType != null);

			var pattern = new Pattern(mediaType);

			Vendor = pattern.GetVendor();
			Media = pattern.GetMedia();
			Format = pattern.GetFormat();

			var value = new StringBuilder();

			if(!String.IsNullOrEmpty(Vendor))
			{
				value.Append(VendorPrefix).Append(Separator).Append(Vendor);

				if(!String.IsNullOrEmpty(Media))
				{
					value.Append(Separator).Append(Media);
				}
			}

			if(!String.IsNullOrEmpty(Format))
			{
				value.Append(FormatSeparator).Append(Format);
			}

			Value = value.ToString();
		}

		public string Value { get; private set; }

		public string Vendor { get; private set; }

		public string Media { get; private set; }

		public string Format { get; private set; }

		public bool FormatIsHtml
		{
			get { return Format.Equals(HtmlFormat, StringComparison.OrdinalIgnoreCase); }
		}

		public bool FormatIsJson
		{
			get { return Format.Equals(JsonFormat, StringComparison.OrdinalIgnoreCase); }
		}

		public bool FormatIsXml
		{
			get { return Format.Equals(XmlFormat, StringComparison.OrdinalIgnoreCase); }
		}

		public override string ToString()
		{
			return Value;
		}

		public override bool Equals(object other)
		{
			return Equals(other as MediaPath);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public bool Equals(MediaPath other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public int CompareTo(MediaPath other)
		{
			return other == null ? 1 : Value.CompareTo(other.Value);
		}

		private sealed class Pattern
		{
			private static readonly Regex _regex = new Regex(
				@"^\s*"														// Match the input start and any leading spaces
				+ @"application/vnd\."						// Match the prefix signaling a custom media type
				+ @"(?<vendor>[\w-]*)"						// Capture the vendor name
				+ @"(\.(?<mediaPart>[\w-]+))*"		// Capture any number of parts of the referenced media
				+ @"(\+(?<format>[\w-]+))"				// Match the format separator and capture the format
				+ @"\s*$",												// Match trailing spaces and the input end
				RegexOptions.Compiled);

			private readonly Match _match;

			internal Pattern(string mediaType)
			{
				// If any part of the value is formatted incorrectly, the pattern won't match and we use defaults. Any path with defaults for vendor and resource
				// indicates that it could not parse the media type.

				_match = _regex.Match(mediaType);
			}

			internal string GetVendor()
			{
				return MatchFailed ? "" : GetCapturedVendor();
			}

			internal string GetMedia()
			{
				return MatchFailed ? "" : GetCapturedMedia();
			}

			internal string GetFormat()
			{
				return MatchFailed ? "" : GetCapturedFormat();
			}

			private bool MatchFailed
			{
				get { return !_match.Success; }
			}

			private string GetCapturedVendor()
			{
				return _match.Groups["vendor"].Captures[0].Value;
			}

			private string GetCapturedMedia()
			{
				var partCaptureValues = _match.Groups["mediaPart"].Captures.Cast<Capture>().Select(capture => capture.Value);

				return String.Join(".", partCaptureValues);
			}

			private string GetCapturedFormat()
			{
				return _match.Groups["format"].Captures[0].Value;
			}
		}
	}
}