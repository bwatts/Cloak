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
	public static class XmlAttributeReading
	{
		public static void EnsureName(this XAttribute attribute, XName name)
		{
			Contract.Requires(attribute != null);
			Contract.Requires(name != null);

			if(attribute.Name != name)
			{
				throw new FormatException(Resources.ExpectedAttributeName.FormatInvariant(name, attribute.GetPath()));
			}
		}

		public static XAttribute RequiredAttribute(this XElement element, XName name)
		{
			Contract.Requires(element != null);
			Contract.Requires(name != null);

			var attribute = element.Attribute(name);

			if(attribute == null)
			{
				throw new FormatException(Resources.ExpectedNonEmptyAttribute.FormatInvariant(name, element.GetPath()));
			}

			return attribute;
		}

		public static string RequiredString(this XAttribute attribute)
		{
			Contract.Requires(attribute != null);

			var value = (string) attribute;

			if(String.IsNullOrEmpty(value))
			{
				throw new FormatException(Resources.ExpectedNonEmptyString.FormatInvariant(attribute.GetPath()));
			}

			return value;
		}
	}
}