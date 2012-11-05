using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Linq;

namespace Cloak.Reflection
{
	public static class AnonymousDictionary
	{
		public static IReadOnlyDictionary<string, object> Read(object values)
		{
			Contract.Requires(values != null);

			return TypeDescriptor
				.GetProperties(values)
				.Cast<PropertyDescriptor>()
				.ToDictionary(property => property.Name, property => property.GetValue(values))
				.AsReadOnly();
		}

		public static IReadOnlyDictionary<string, TValue> Read<TValue>(object values)
		{
			Contract.Requires(values != null);

			var dictionary = new Dictionary<string, TValue>();

			foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(values))
			{
				if(!typeof(TValue).IsAssignableFrom(property.PropertyType))
				{
					throw new ArgumentException(Resources.PropertyNotAssignableToValueType.FormatCurrent(property.Name, typeof(TValue)));
				}

				dictionary.Add(property.Name, (TValue) property.GetValue(values));
			}

			return dictionary.AsReadOnly();
		}
	}
}