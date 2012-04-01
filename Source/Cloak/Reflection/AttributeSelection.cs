using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Selects attributes from <see cref="ICustomAttributeProvider"/> objects
	/// </summary>
	public static class AttributeSelection
	{
		/// <summary>
		/// Gets the attributes of the specified type from the specified provider
		/// </summary>
		/// <typeparam name="T">The type of selected attribute</typeparam>
		/// <param name="provider">The object which declares the attributes</param>
		/// <returns>The attributes of the specified type declared by the specified provider</returns>
		[Pure]
		public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return GetAttributes<T>(provider, true);
		}

		/// <summary>
		/// Gets the first attribute of the specified type from the specified provider
		/// </summary>
		/// <typeparam name="T">The type of selected attribute</typeparam>
		/// <param name="provider">The object which declares the attribute</param>
		/// <returns>The first attribute of the specified type declared by the specified provider</returns>
		[Pure]
		public static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return GetAttribute<T>(provider, true);
		}

		/// <summary>
		/// Determines if the specified provider declares an attribute of the specified type
		/// </summary>
		/// <typeparam name="T">The type of declared attribute</typeparam>
		/// <param name="provider">The object which declares the attribute</param>
		/// <returns>Whether the specified provider declares an attribute of the specified type</returns>
		[Pure]
		public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return HasAttribute<T>(provider, true);
		}

		/// <summary>
		/// Gets the attributes of the specified type from the specified provider. Only attributes declared
		/// directly on the provider are considered.
		/// </summary>
		/// <typeparam name="T">The type of selected attribute</typeparam>
		/// <param name="provider">The object which declares the attributes</param>
		/// <returns>The attributes of the specified type declared by the specified provider</returns>
		[Pure]
		public static IEnumerable<T> GetAttributesWithoutInheriting<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return GetAttributes<T>(provider, false);
		}

		/// <summary>
		/// Gets the first attribute of the specified type from the specified provider. Only attributes declared
		/// directly on the provider are considered.
		/// </summary>
		/// <typeparam name="T">The type of selected attribute</typeparam>
		/// <param name="provider">The object which declares the attribute</param>
		/// <returns>The first attribute of the specified type declared by the specified provider</returns>
		[Pure]
		public static T GetAttributeWithoutInheriting<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return GetAttribute<T>(provider, false);
		}

		/// <summary>
		/// Determines if the specified provider declares an attribute of the specified type. Only attributes declared
		/// directly on the provider are considered.
		/// </summary>
		/// <typeparam name="T">The type of declared attribute</typeparam>
		/// <param name="provider">The object which declares the attribute</param>
		/// <returns>Whether the specified provider declares an attribute of the specified type</returns>
		[Pure]
		public static bool HasAttributeWithoutInheriting<T>(this ICustomAttributeProvider provider) where T : Attribute
		{
			Contract.Requires(provider != null);

			return HasAttribute<T>(provider, false);
		}

		private static IEnumerable<T> GetAttributes<T>(ICustomAttributeProvider provider, bool inherit) where T : Attribute
		{
			return provider.GetCustomAttributes(typeof(T), inherit).Cast<T>();
		}

		private static T GetAttribute<T>(ICustomAttributeProvider provider, bool inherit) where T : Attribute
		{
			return GetAttributes<T>(provider, inherit).FirstOrDefault();
		}

		private static bool HasAttribute<T>(ICustomAttributeProvider provider, bool inherit) where T : Attribute
		{
			return provider.IsDefined(typeof(T), inherit);
		}
	}
}