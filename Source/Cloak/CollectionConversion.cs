using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Extends sequences with the ability to create observable collections from them
	/// </summary>
	public static class CollectionConversion
	{
		/// <summary>
		/// Creates an <see cref="ObservableCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A collection containing the items in the specified sequence</returns>
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return new ObservableCollection<T>(source);
		}

		/// <summary>
		/// Creates an <see cref="ReadOnlyCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A read-only collection containing the items in the specified sequence</returns>
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return source.ToList().AsReadOnly();
		}

		/// <summary>
		/// Creates an <see cref="ReadOnlyObservableCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A read-only collection containing the items in the specified sequence</returns>
		public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this ObservableCollection<T> source)
		{
			Contract.Requires(source != null);

			return new ReadOnlyObservableCollection<T>(source);
		}

		/// <summary>
		/// Creates an <see cref="ReadOnlyObservableCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A read-only collection containing the items in the specified sequence</returns>
		public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return source.ToObservableCollection().ToReadOnlyObservableCollection();
		}

		/// <summary>
		/// Creates an <see cref="PubliclyReadOnlyCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A read-only collection containing the items in the specified sequence</returns>
		public static PubliclyReadOnlyCollection<T> ToPubliclyReadOnlyCollection<T>(this IList<T> source)
		{
			Contract.Requires(source != null);

			return new PubliclyReadOnlyCollection<T>(source.ToList());
		}

		/// <summary>
		/// Creates an <see cref="PubliclyReadOnlyCollection{T}"/> from the specified sequence
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="source">The sequence containing the items to put in the collection</param>
		/// <returns>A publicly read-only collection containing the items in the specified sequence</returns>
		public static PubliclyReadOnlyCollection<T> ToPubliclyReadOnlyCollection<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return new PubliclyReadOnlyCollection<T>(source.ToList());
		}

		/// <summary>
		/// Adds the elements of the specified sequence to the collection
		/// </summary>
		/// <typeparam name="T">The type of elements in the sequence</typeparam>
		/// <param name="collection">The collection in which to put the items</param>
		/// <param name="items">The items to put in the collection</param>
		public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
		{
			Contract.Requires(collection != null);
			Contract.Requires(items != null);

			foreach(var item in items)
			{
				collection.Add(item);
			}
		}
	}
}