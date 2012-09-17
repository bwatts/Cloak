using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Determines if types can be assigned null values
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TypeNullability
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
	}
}