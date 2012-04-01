using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Combines the hash codes of multiple objects
	/// </summary>
	public static class HashCode
	{
		/// <summary>
		/// Combines the hash codes of the objects in the specified sequence
		/// </summary>
		/// <param name="source">The sequence whose items' hash codes are combined</param>
		/// <returns>The hash code created from the items' hash codes</returns>
		public static int Combine(IEnumerable<object> source)
		{
			Contract.Requires(source != null);

			return Combine<object>(source);
		}

		/// <summary>
		/// Combines the hash codes of the objects in the specified sequence
		/// </summary>
		/// <param name="source">The sequence whose items' hash codes are combined</param>
		/// <returns>The hash code created from the items' hash codes</returns>
		public static int Combine(params object[] source)
		{
			Contract.Requires(source != null);

			return Combine<object>(source as IEnumerable<object>);
		}

		/// <summary>
		/// Combines the hash codes of the objects in the specified sequence
		/// </summary>
		/// <param name="source">The sequence whose items' hash codes are combined</param>
		/// <returns>The hash code created from the items' hash codes</returns>
		public static int Combine<T>(IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			int hash;

			var listSource = source as IList<T>;

			if(listSource != null && listSource.Count == 1)
			{
				hash = listSource[0].GetHashCode();
			}
			else
			{
				// Based on: http://blog.roblevine.co.uk/?cat=10

				hash = 23;	// Non-zero and prime

				foreach(var item in source)
				{
					hash = ((hash << 5) * 37) ^ (item == null ? 0 : item.GetHashCode());
				}
			}

			return hash;
		}

		/// <summary>
		/// Combines the hash codes of the objects in the specified sequence
		/// </summary>
		/// <param name="source">The sequence whose items' hash codes are combined</param>
		/// <returns>The hash code created from the items' hash codes</returns>
		public static int Combine<T>(params T[] source)
		{
			Contract.Requires(source != null);

			return Combine(source as IEnumerable<T>);
		}
	}
}