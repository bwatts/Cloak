using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences with the ability to select distinct elements in order
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OrderedDistinctSequences
	{
		/// <summary>
		/// Returns distinct elements in order, using the default equality comparer
		/// </summary>
		/// <param name="source">The sequence from which to select distinct elements in order</param>
		/// <returns>A sequence consisting of the distinct items in their original order</returns>
		[Pure]
		public static IEnumerable<T> DistinctInOrder<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			// LINQ's Distinct operator does not preserve order. OrderedDictionary seems to be the only type which can represent an ordered set.

			var distinctItems = new OrderedDictionary();

			foreach(var item in source)
			{
				if(!distinctItems.Contains(item))
				{
					distinctItems.Add(item, null);
				}
			}

			return distinctItems.Keys.Cast<T>();
		}
	}
}