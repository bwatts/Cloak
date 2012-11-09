using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Reflection;

namespace Cloak
{
	public static class ObjectConverter
	{
		public static object To(Type type, object value)
		{
			Contract.Requires(type != null);

			object convertedValue;

			if(value == null)
			{
				if(!type.IsAssignableNull())
				{
					throw new InvalidCastException(Resources.TypeNotAssignableNull.FormatInvariant(type));
				}

				convertedValue = type.GetDefaultValue();
			}
			else if(type.IsAssignableFrom(value.GetType()))
			{
				convertedValue = value;
			}
			else
			{
				var converter = TypeDescriptor.GetConverter(type);

				if(converter == null || !converter.CanConvertFrom(value.GetType()))
				{
					throw new InvalidCastException(Resources.CannotConvertValue.FormatInvariant(value.GetType(), type, converter == null ? "" : converter.ToString()));
				}

				convertedValue = converter.ConvertFrom(value);
			}

			return convertedValue;
		}

		public static object To(Type type, string value, CultureInfo culture)
		{
			Contract.Requires(culture != null);

			object convertedValue;

			if(value == null)
			{
				if(!type.IsAssignableNull())
				{
					throw new InvalidCastException(Resources.TypeNotAssignableNull.FormatInvariant(type));
				}

				convertedValue = type.GetDefaultValue();
			}
			else if(type == typeof(string))
			{
				convertedValue = value;
			}
			else
			{
				var converter = TypeDescriptor.GetConverter(type);

				if(converter == null || !converter.CanConvertFrom(typeof(string)))
				{
					throw new InvalidCastException(Resources.CannotConvertString.FormatInvariant(value, type));
				}

				convertedValue = converter.ConvertFromString(null, culture, value);
			}

			return convertedValue;
		}

		public static T To<T>(object value)
		{
			return (T) To(typeof(T), value);
		}

		public static T To<T>(string value, CultureInfo culture)
		{
			return (T) To(typeof(T), value, culture);
		}
	}
}