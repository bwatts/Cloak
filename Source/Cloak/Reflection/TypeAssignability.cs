using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Extends types with the ability to check assignability
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TypeAssignability
	{
		/// <summary>
		/// Determines if the specified type can be assigned a null value
		/// </summary>
		/// <param name="type">The type to which null may be assigned</param>
		/// <returns>Whether the type can be assigned a null value</returns>
		[Pure]
		public static bool IsAssignableNull(this Type type)
		{
			Contract.Requires(type != null);

			return type.IsClass
				|| type.IsInterface
				|| (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		/// <summary>
		/// Determines if the specified generic type definition is the specified type
		/// </summary>
		/// <param name="type">The type which may be the specified generic type definition</param>
		/// <param name="genericDefinition">The generic definition to which may be the specified type</param>
		/// <returns>Whether the specified generic type definition is the specified type</returns>
		[Pure]
		public static bool IsGenericDefinition(this Type type, Type genericDefinition)
		{
			Contract.Requires(type != null);

			return type.IsGenericType && genericDefinition == type.GetGenericTypeDefinition();
		}

		/// <summary>
		/// Determines if the specified generic type definition is assignable from the specified type
		/// </summary>
		/// <param name="type">The type which may be assignable to the specified generic type definition</param>
		/// <param name="genericDefinition">The generic definition to which the specified type may be assignable</param>
		/// <returns>Whether the specified generic type definition is assignable from the specified type</returns>
		[Pure]
		public static bool IsAssignableFromGenericDefinition(this Type type, Type genericDefinition)
		{
			Contract.Requires(type != null);

			return type.IsGenericType && genericDefinition.IsAssignableFrom(type.GetGenericTypeDefinition());
		}
	}
}