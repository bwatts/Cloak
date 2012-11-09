using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Extends types with the ability to find all types in their inheritance chains
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class InheritanceChain
	{
		/// <summary>
		/// Gets all types in the inheritance chain of the specified type, starting with the type itself
		/// </summary>
		/// <param name="type">The type at the end of the inheritance chain to get</param>
		/// <returns>The types in the inheritance chain of the specified type, starting with the type itself</returns>
		[Pure]
		public static IEnumerable<Type> GetInheritanceChain(this Type type)
		{
			Contract.Requires(type != null);

			for(var currentType = type; currentType != null; currentType = currentType.BaseType)
			{
				yield return currentType;
			}
		}
	}
}