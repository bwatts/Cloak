using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Extends sequences of text with the ability to join the elements using a separator
	/// </summary>
	public static class StringJoin
	{
		/// <summary>
		/// Inserts the specified separator between the text elements
		/// </summary>
		/// <param name="source">The sequence of text whose items are joined</param>
		/// <param name="separator">The text which appears between each item</param>
		/// <returns>Text consisting of the elements of <paramref name="source"/> interspersed with <paramref name="separator"/></returns>
		public static string Join(this IEnumerable<string> source, string separator)
		{
			Contract.Requires(source != null);
			Contract.Requires(separator != null);

			return String.Join(separator, source.ToArray());
		}
	}
}