using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cloak.Xml
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XmlElementReading
	{
		public static void EnsureName(this XElement element, XName name)
		{
			Contract.Requires(element != null);
			Contract.Requires(name != null);

			if(element.Name != name)
			{
				throw new FormatException(Resources.ExpectedElementName.FormatInvariant(name, element.GetPath()));
			}
		}

		public static XElement RequiredElement(this XElement element, XName name)
		{
			Contract.Requires(element != null);
			Contract.Requires(name != null);

			var childElement = element.Element(name);

			if(childElement == null)
			{
				throw new FormatException(Resources.ExpectedChildElement.FormatInvariant(name, element.GetPath()));
			}

			return childElement;
		}

		public static string RequiredString(this XElement element)
		{
			Contract.Requires(element != null);

			var value = (string) element;

			if(String.IsNullOrEmpty(value))
			{
				throw new FormatException(Resources.ExpectedNonEmptyString.FormatInvariant(element.GetPath()));
			}

			return value;
		}
	}
}