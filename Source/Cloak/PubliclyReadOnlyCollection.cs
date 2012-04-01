using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	public class PubliclyReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		public PubliclyReadOnlyCollection(IList<T> innerItems) : base(innerItems)
		{
			Contract.Requires(innerItems != null);

			InnerItems = innerItems;
		}

		public PubliclyReadOnlyCollection() : this(new List<T>().AsReadOnly())
		{}

		public IList<T> InnerItems { get; private set; }
	}
}