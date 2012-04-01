using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cloak.Linq
{
	// http://blogs.msdn.com/b/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx

	/// <summary>
	///	Gets the types of elements in sequences
	/// </summary>
	public static class SequenceElementType
	{
		/// <summary>
		/// Gets the type of the elements in the specified sequence type
		/// </summary>
		/// <param name="sequenceType">The type of sequence</param>
		/// <returns>The type of elements in the sequence</returns>
		[Pure]
		public static Type From(Type sequenceType)
		{
			Contract.Requires(sequenceType != null);

			var enumerableType = GetEnumerableType(sequenceType);

			return enumerableType == null ? sequenceType : enumerableType.GetGenericArguments()[0];
		}

		private static Type GetEnumerableType(Type sequenceType)
		{
			return sequenceType == null || sequenceType == typeof(string) ? null : GetNonStringEnumerableType(sequenceType);
		}

		private static Type GetNonStringEnumerableType(Type sequenceType)
		{
			return GetArrayType(sequenceType) ?? GetGenericEnumerableType(sequenceType) ?? GetInterfaceEnumerableType(sequenceType);
		}

		private static Type GetArrayType(Type sequenceType)
		{
			return !sequenceType.IsArray ? null : typeof(IEnumerable<>).MakeGenericType(sequenceType.GetElementType());
		}

		private static Type GetGenericEnumerableType(Type sequenceType)
		{
			Type enumerableType = null;

			if(sequenceType.IsGenericType)
			{
				foreach(var genericArgument in sequenceType.GetGenericArguments())
				{
					enumerableType = typeof(IEnumerable<>).MakeGenericType(genericArgument);

					if(enumerableType.IsAssignableFrom(sequenceType))
					{
						break;
					}
				}
			}

			return enumerableType;
		}

		private static Type GetInterfaceEnumerableType(Type sequenceType)
		{
			Type enumerableType = null;

			var interfaces = sequenceType.GetInterfaces();

			if(interfaces != null && interfaces.Length > 0)
			{
				foreach(var @interface in interfaces)
				{
					enumerableType = GetEnumerableType(@interface);

					if(enumerableType != null)
					{
						break;
					}
				}
			}

			if(enumerableType == null && sequenceType.BaseType != null && sequenceType.BaseType != typeof(object))
			{
				enumerableType = GetEnumerableType(sequenceType.BaseType);
			}

			return enumerableType;
		}
	}
}