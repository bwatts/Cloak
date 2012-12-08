using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Extends assemblies, types, and methods with the ability to determine if they are extensions defined by
	/// <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Extensions
	{
		/// <summary>
		/// Determines if the specified method is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
		/// </summary>
		/// <param name="method">The method to check</param>
		/// <returns>Whether the specified method is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/></returns>
		[Pure]
		public static bool IsExtension(this MethodInfo method)
		{
			Contract.Requires(method != null);

			return method.HasAttribute<ExtensionAttribute>();
		}

		/// <summary>
		/// Determines if the specified class is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
		/// </summary>
		/// <param name="classType">The class to check</param>
		/// <returns>Whether the specified class is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/></returns>
		[Pure]
		public static bool IsExtension(this Type classType)
		{
			Contract.Requires(classType != null);

			return classType.IsClass && classType.HasAttribute<ExtensionAttribute>();
		}

		/// <summary>
		/// Determines if the specified assembly is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
		/// </summary>
		/// <param name="assembly">The assembly to check</param>
		/// <returns>Whether the specified assembly is an extension defined by <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/></returns>
		[Pure]
		public static bool IsExtension(this Assembly assembly)
		{
			Contract.Requires(assembly != null);

			return assembly.HasAttribute<ExtensionAttribute>();
		}
	}
}