using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences of key/value pairs with the ability to create read-only and writable dictionaries containing them
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class DictionarySequences
	{
		/// <summary>
		/// Creates a dictionary from the specified key/value pairs
		/// </summary>
		/// <param name="source">The key/value pairs in the dictionary</param>
		/// <returns>A dictionary containing the specified key/value pairs</returns>
		[Pure]
		public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			Contract.Requires(source != null);

			return source.ToDictionary(item => item.Key, item => item.Value);
		}

		/// <summary>
		/// Creates a read-only dictionary from the specified key/value pairs
		/// </summary>
		/// <param name="source">The key/value pairs in the read-only dictionary</param>
		/// <returns>A read-only dictionary containing the specified key/value pairs</returns>
		[Pure]
		public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
		{
			Contract.Requires(source != null);

			return source.ToDictionary().AsReadOnly();
		}

		/// <summary>
		/// Creates a read-only wrapper for the specified dictionary
		/// </summary>
		/// <param name="source">The dictionary backing the read-only dictionary</param>
		/// <returns>A read-only dictionary backed by the specified dictionary</returns>
		[Pure]
		public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
		{
			Contract.Requires(source != null);

			return new ReadOnlyDictionary<TKey, TValue>(source);
		}
	}
}