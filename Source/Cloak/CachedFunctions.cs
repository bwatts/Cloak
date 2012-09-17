using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Creates functions which cache values based on keys
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CachedFunctions
	{
		/// <summary>
		/// Creates a function which caches values based on keys, using the cache
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <param name="cache">The dictionary which holds the cached values</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> Cached<TKey, TValue>(this Func<TKey, TValue> valueFunction, IDictionary<TKey, TValue> cache)
		{
			Contract.Requires(valueFunction != null);
			Contract.Requires(cache != null);

			return key =>
			{
				TValue value;

				if(!cache.TryGetValue(key, out value))
				{
					value = valueFunction(key);

					cache[key] = value;
				}

				return value;
			};
		}

		/// <summary>
		/// Creates a function which caches values based on keys
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> Cached<TKey, TValue>(this Func<TKey, TValue> valueFunction)
		{
			Contract.Requires(valueFunction != null);

			return Cached(valueFunction, new Dictionary<TKey, TValue>());
		}

		/// <summary>
		/// Creates a function which caches values based on keys, using the specified comparer
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <param name="keyComparer">The comparer which specifies equality among keys</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> Cached<TKey, TValue>(this Func<TKey, TValue> valueFunction, IEqualityComparer<TKey> keyComparer)
		{
			Contract.Requires(valueFunction != null);
			Contract.Requires(keyComparer != null);

			return Cached(valueFunction, new Dictionary<TKey, TValue>(keyComparer));
		}

		/// <summary>
		/// Creates a concurrent (thread-safe) function which caches values based on keys
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <param name="cache">The dictionary which holds the cached values</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> CachedConcurrently<TKey, TValue>(this Func<TKey, TValue> valueFunction, IDictionary<TKey, TValue> cache)
		{
			Contract.Requires(valueFunction != null);
			Contract.Requires(cache != null);

			var sync = new object();

			return key =>
			{
				TValue value;

				lock(sync)
				{
					if(!cache.TryGetValue(key, out value))
					{
						value = valueFunction(key);

						cache[key] = value;
					}
				}

				return value;
			};
		}

		/// <summary>
		/// Creates a concurrent (thread-safe) function which caches values based on keys
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> CachedConcurrently<TKey, TValue>(this Func<TKey, TValue> valueFunction)
		{
			Contract.Requires(valueFunction != null);

			return CachedConcurrently(valueFunction, new Dictionary<TKey, TValue>());
		}

		/// <summary>
		/// Creates a concurrent (thread-safe) function which caches values based on keys, using the specified comparer
		/// </summary>
		/// <typeparam name="TKey">The type of key associated with each value</typeparam>
		/// <typeparam name="TValue">The type of value associated with each key</typeparam>
		/// <param name="valueFunction">The function whose results are cached</param>
		/// <param name="keyComparer">The comparer which specifies equality among inputs</param>
		/// <returns>A function which caches the results of <paramref name="valueFunction"/></returns>
		[Pure]
		public static Func<TKey, TValue> CachedConcurrently<TKey, TValue>(this Func<TKey, TValue> valueFunction, IEqualityComparer<TKey> keyComparer)
		{
			Contract.Requires(valueFunction != null);
			Contract.Requires(keyComparer != null);

			return CachedConcurrently(valueFunction, new Dictionary<TKey, TValue>(keyComparer));
		}
	}
}