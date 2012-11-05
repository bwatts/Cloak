using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences with the ability to concatenate an arbitrary number of values
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConcatParams
	{
		/// <summary>
		/// Adds the specified values to the end of the sequence
		/// </summary>
		/// <param name="source">The sequence whose even items are selected</param>
		/// <param name="items">The items to add to the sequence</param>
		/// <returns>A sequence consisting of the source items and the specified items</returns>
		[Pure]
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, params T[] items)
		{
			Contract.Requires(source != null);

			return source.Concat(items as IEnumerable<T>);
		}
	}
}