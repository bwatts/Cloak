using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http
{
	public class ItemStack<T>
	{
		public ItemStack(T rootItem)
		{
			Item = rootItem;
		}

		private ItemStack(T item, ItemStack<T> parent) : this(item)
		{
			Parent = parent;
		}

		public T Item { get; private set; }

		public ItemStack<T> Parent { get; private set; }

		public virtual ItemStack<T> Append(T item)
		{
			return new ItemStack<T>(item, this);
		}

		public IEnumerable<T> GetAllItemsFromHere()
		{
			yield return Item;

			var parent = Parent;

			while(parent != null)
			{
				yield return parent.Item;

				parent = parent.Parent;
			}
		}

		public IEnumerable<T> GetAllItemsFromRoot()
		{
			return GetAllItemsFromHere().Reverse();
		}

		public override string ToString()
		{
			return ToString(" ");
		}

		public string ToString(string separator)
		{
			return ToString(separator, item => item.ToString());
		}

		public string ToString(Func<T, string> itemTextSelector)
		{
			return ToString(" ", itemTextSelector);
		}

		public string ToString(string separator, Func<T, string> itemTextSelector)
		{
			return String.Join(separator, GetAllItemsFromRoot().Select(itemTextSelector));
		}
	}
}