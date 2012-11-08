using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Parsing
{
	public static class StringParsing
	{
		public static Uri ParseUri(this string value, UriKind kind, string path = null)
		{
			Contract.Requires(value != null);

			Uri uri = null;

			if(value != "" && Uri.TryCreate(value, kind, out uri))
			{
				throw new FormatException(Resources.InvalidUri.FormatInvariant(value, path));
			}

			return uri;
		}
	}
}