using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cloak
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XmlPaths
	{
		public static string GetPath(this XElement element)
		{
			Contract.Requires(element != null);

			var ancestors = element.Ancestors().Select(ancestor => ancestor.GetRelativePath());

			return String.Concat(ancestors.Reverse()) + element.GetRelativePath();
		}

		public static string GetPath(this XAttribute attribute)
		{
			Contract.Requires(attribute != null);

			return attribute.Parent == null
					? attribute.Name.ToString()
					: Resources.QualifiedAttributePath.FormatInvariant(attribute.Parent.GetPath(), attribute.Parent.ExpandName(attribute.Name));
		}

		private static string GetRelativePath(this XElement element)
		{
			var index = element.GetIndexInParent();

			var currentNamespace = element.Name.Namespace;

			var expandedName = element.ExpandName(element.Name);

			return index == -1
				? Resources.ElementNonIndexedPath.FormatInvariant(expandedName)
				: Resources.ElementIndexedPath.FormatInvariant(expandedName, index);
		}

		private static int GetIndexInParent(this XElement element)
		{
			var indexInParent = -1;

			if(element.Parent != null)
			{
				indexInParent = 0;

				foreach(var sibling in element.Parent.Elements(element.Name))
				{
					if(sibling != element)
					{
						indexInParent++;
					}
				}
			}

			return indexInParent;
		}

		private static string ExpandName(this XElement element, XName name)
		{
			string expandedName;

			if(String.IsNullOrEmpty(name.Namespace.NamespaceName))
			{
				expandedName = name.LocalName;
			}
			else
			{
				var prefix = element.GetPrefixOfNamespace(name.Namespace);

				expandedName = Resources.ExpandedName.FormatInvariant(prefix, name.LocalName);
			}

			return expandedName;
		}
	}
}