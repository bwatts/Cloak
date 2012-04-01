using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Cloak.Linq
{
	/// <summary>
	/// Gets <see cref="MethodCallExpression"/> objects representing invocations of the <see cref="Enumerable.Cast"/> method
	/// </summary>
	public static class CastSequenceExpression
	{
		/// <summary>
		/// Gets an invocation of the <see cref="Enumerable.Cast"/> method with the specified sequence as the parameter
		/// </summary>
		/// <param name="targetType">The type to which each item in the source is cast</param>
		/// <param name="source">An expression representing the sequence whose items are cast to <paramref name="targetType"/></param>
		/// <returns>
		/// An expression representing a call to the <see cref="Enumerable.Cast"/> method with the specified sequence
		/// expression as the parameter
		/// </returns>
		[Pure]
		public static MethodCallExpression To(Type targetType, Expression source)
		{
			Contract.Requires(targetType != null);
			Contract.Requires(source != null);

			return Expression.Call(typeof(Enumerable), "Cast", new[] { targetType }, source);
		}

		/// <summary>
		/// Gets an invocation of the <see cref="Enumerable.Cast"/> method with the specified sequence as the parameter
		/// </summary>
		/// <param name="targetType">The type to which each item in the source is cast</param>
		/// <param name="source">The sequence whose items are cast to <paramref name="targetType"/></param>
		/// <returns>
		/// An expression representing a call to the <see cref="Enumerable.Cast"/> method with the specified sequence
		/// as the parameter
		/// </returns>
		[Pure]
		public static MethodCallExpression To(Type targetType, IEnumerable source)
		{
			Contract.Requires(targetType != null);
			Contract.Requires(source != null);

			return To(targetType, Expression.Constant(source));
		}
	}
}