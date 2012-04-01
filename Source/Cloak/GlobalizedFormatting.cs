using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Formats text using <see cref="IFormatProvider"/>, <see cref="CultureInfo.CurrentCulture"/>, and <see cref="CultureInfo.InvariantCulture"/>
	/// </summary>
	public static class GlobalizedFormatting
	{
		/// <summary>
		/// Formats the text using the specified provider and arguments
		/// </summary>
		/// <param name="format">The formatting of the arguments</param>
		/// <param name="provider">Culture-specific formatting information</param>
		/// <param name="arguments">The formatted arguments</param>
		/// <returns>The result of formatting the specified text</returns>
		[Pure]
		public static string Format(this string format, IFormatProvider provider, params object[] arguments)
		{
			Contract.Requires(format != null);
			Contract.Requires(provider != null);
			Contract.Requires(arguments != null);

			return String.Format(provider, format, arguments);
		}

		/// <summary>
		/// Formats the text using <see cref="CultureInfo.CurrentCulture"/> and the specified arguments
		/// </summary>
		/// <param name="format">The formatting of the arguments</param>
		/// <param name="arguments">The formatted arguments</param>
		/// <returns>The result of formatting the specified text</returns>
		[Pure]
		public static string FormatCurrent(this string format, params object[] arguments)
		{
			return format.Format(CultureInfo.CurrentCulture, arguments);
		}

		/// <summary>
		/// Formats the text using <see cref="CultureInfo.InvariantCulture"/> and the specified arguments
		/// </summary>
		/// <param name="format">The formatting of the arguments</param>
		/// <param name="arguments">The formatted arguments</param>
		/// <returns>The result of formatting the specified text</returns>
		[Pure]
		public static string FormatInvariant(this string format, params object[] arguments)
		{
			return format.Format(CultureInfo.InvariantCulture, arguments);
		}
	}
}