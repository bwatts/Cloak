using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Extends types with the ability to get default values
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TypeDefaultValue
	{
		/// <summary>
		/// Gets the default value of the specified type
		/// </summary>
		/// <param name="type">The type whose default value is returned</param>
		/// <returns>The default value of the specified type</returns>
		[Pure]
		public static object GetDefaultValue(this Type type)
		{
			Contract.Requires(type != null);

			return type.IsValueType ? Activator.CreateInstance(type) : null;
		}
	}
}