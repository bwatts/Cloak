using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// A <see cref="ReadOnlyCollection{T}"/> which provides access to its inner items
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection</typeparam>
	public class PubliclyReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		/// <summary>
		/// Initializes a collection with the specified inner items
		/// </summary>
		/// <param name="innerItems">The items to put in this collection</param>
		public PubliclyReadOnlyCollection(IList<T> innerItems) : base(innerItems)
		{
			Contract.Requires(innerItems != null);

			InnerItems = innerItems;
		}

		/// <summary>
		/// Initializes a collection with no inner items
		/// </summary>
		public PubliclyReadOnlyCollection() : this(new List<T>().AsReadOnly())
		{}

		/// <summary>
		/// Gets this collection's inner items
		/// </summary>
		public IList<T> InnerItems { get; private set; }
	}
}