using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak
{
	public class CountConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(int) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string)
			{
				return new Count(Int32.Parse((string) value, culture));
			}
			else if(value is int)
			{
				return new Count((int) value);
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(value is Count)
			{
				var count = (Count) value;

				if(destinationType == typeof(string))
				{
					return count.ToString();
				}
				else
				{
					if(destinationType == typeof(int))
					{
						return count.Value;
					}
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}