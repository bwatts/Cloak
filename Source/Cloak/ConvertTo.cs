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
	public static class ConvertTo<T>
	{
		public static T From(object value)
		{
			T convertedValue;

			if(value == null)
			{
				if(!typeof(T).IsAssignableNull())
				{
					throw new InvalidCastException(Resources.TypeNotAssignableNull.FormatInvariant(typeof(T)));
				}

				convertedValue = default(T);
			}
			else if(typeof(T).IsAssignableFrom(value.GetType()))
			{
				convertedValue = (T) value;
			}
			else
			{
				var converter = TypeDescriptor.GetConverter(typeof(T));

				if(converter == null || !converter.CanConvertFrom(value.GetType()))
				{
					throw new InvalidCastException(Resources.CannotConvertValue.FormatInvariant(value.GetType(), typeof(T), converter == null ? "" : converter.ToString()));
				}

				convertedValue = (T) converter.ConvertFrom(value);
			}

			return convertedValue;
		}

		public static T From(string value, CultureInfo culture)
		{
			Contract.Requires(culture != null);

			T convertedValue;

			if(value == null)
			{
				if(!typeof(T).IsAssignableNull())
				{
					throw new InvalidCastException(Resources.TypeNotAssignableNull.FormatInvariant(typeof(T)));
				}

				convertedValue = default(T);
			}
			else if(typeof(T) == typeof(string))
			{
				convertedValue = (T) (object) value;
			}
			else
			{
				var converter = TypeDescriptor.GetConverter(typeof(T));

				if(converter == null || !converter.CanConvertFrom(typeof(string)))
				{
					throw new InvalidCastException(Resources.CannotConvertString.FormatInvariant(value, typeof(T)));
				}

				convertedValue = (T) converter.ConvertFromString(null, culture, value);
			}

			return convertedValue;
		}
	}
}