using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Extends sequences of text with the ability to separate the elements with a separator
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StringJoin
	{
		/// <summary>
		/// Gets a string with the specified separator between the text items
		/// </summary>
		/// <param name="source">The sequence of text whose items are joined</param>
		/// <param name="separator">The text which appears between each item</param>
		/// <returns>Text consisting of the items of <paramref name="source"/> interspersed with <paramref name="separator"/></returns>
		[Pure]
		public static string SeparateWith(this IEnumerable<string> source, string separator)
		{
			Contract.Requires(source != null);
			Contract.Requires(separator != null);

			return String.Join(separator, source);
		}

		/// <summary>
		/// Gets a string with commas between the text items
		/// </summary>
		/// <param name="source">The sequence of text whose items are joined with commas</param>
		/// <param name="appendSpace">Whether to insert a space after each comma</param>
		/// <returns>Text consisting of the items of <paramref name="source"/> interspersed with commas</returns>
		[Pure]
		public static string SeparateWithCommas(this IEnumerable<string> source, bool appendSpace = true)
		{
			return source.SeparateWith(", ");
		}

		/// <summary>
		/// Gets a string with the specified separator between the text representations of the items
		/// </summary>
		/// <typeparam name="T">The type of item in the sequence</typeparam>
		/// <param name="source">The sequence whose items are joined</param>
		/// <param name="separator">The text which appears between each item</param>
		/// <returns>Text consisting of the items of <paramref name="source"/> interspersed with <paramref name="separator"/></returns>
		[Pure]
		public static string SeparateWith<T>(this IEnumerable<T> source, string separator)
		{
			Contract.Requires(source != null);
			Contract.Requires(separator != null);

			return String.Join(separator, source);
		}

		/// <summary>
		/// Gets a string with commas between the text representations of the items
		/// </summary>
		/// <typeparam name="T">The type of item in the sequence</typeparam>
		/// <param name="source">The sequence whose items are joined with commas</param>
		/// <param name="appendSpace">Whether to insert a space after each comma</param>
		/// <returns>Text consisting of the items of <paramref name="source"/> interspersed with commas</returns>
		[Pure]
		public static string SeparateWithCommas<T>(this IEnumerable<T> source, bool appendSpace = true)
		{
			return source.Select(item => item == null ? "" : item.ToString()).SeparateWithCommas(appendSpace);
		}
	}
}