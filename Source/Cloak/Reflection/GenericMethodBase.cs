using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Creates generic methods from <see cref="MethodBase"/> objects
	/// </summary>
	public static class GenericMethodBase
	{
		/// <summary>
		/// Creates a generic method from the specified method base using the specified type arguments
		/// </summary>
		/// <param name="methodBase">The method which is made generic</param>
		/// <param name="typeArguments">The type arguments which close the generic method</param>
		/// <returns>The closed version of the specific generic method</returns>
		[Pure]
		public static MethodInfo MakeGenericMethod(this MethodBase methodBase, params Type[] typeArguments)
		{
			Contract.Requires(methodBase != null);
			Contract.Requires(methodBase is MethodInfo);
			Contract.Requires(typeArguments != null);
			
			return methodBase.MakeGenericMethod(typeArguments);
		}

		/// <summary>
		/// Creates a generic method from the specified method base using the specified type arguments
		/// </summary>
		/// <param name="methodBase">The method which is made generic</param>
		/// <param name="typeArguments">The type arguments which close the generic method</param>
		/// <returns>The closed version of the specific generic method</returns>
		[Pure]
		public static MethodInfo MakeGenericMethod(this MethodBase methodBase, IEnumerable<Type> typeArguments)
		{
			Contract.Requires(methodBase != null);
			Contract.Requires(methodBase is MethodInfo);
			Contract.Requires(typeArguments != null);

			return methodBase.MakeGenericMethod(typeArguments.ToArray());
		}
	}
}