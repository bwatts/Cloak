using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences with the ability to select elements at even or odd positions
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AlternatingSelection
	{
		/// <summary>
		/// Filters a sequence to values at even positions
		/// </summary>
		/// <param name="source">The sequence whose even items are selected</param>
		/// <returns>A sequence consisting of the items at even positions in <paramref name="source"/></returns>
		[Pure]
		public static IEnumerable<T> AtEvenPositions<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return AtAlternatePositions(source, true);
		}

		/// <summary>
		/// Filters a sequence to values at odd positions
		/// </summary>
		/// <param name="source">The sequence whose odd items are selected</param>
		/// <returns>A sequence consisting of the items at odd positions in <paramref name="source"/></returns>
		[Pure]
		public static IEnumerable<T> AtOddPositions<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return AtAlternatePositions(source, false);
		}

		private static IEnumerable<T> AtAlternatePositions<T>(IEnumerable<T> source, bool isEven)
		{
			foreach(var item in source)
			{
				if(isEven)
				{
					yield return item;
				}

				isEven = !isEven;
			}
		}
	}
}