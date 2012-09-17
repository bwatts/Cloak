using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences with the ability to select sequences of <see cref="ConstantExpression"/> objects from them
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConstantSequence
	{
		/// <summary>
		/// Gets a sequence of <see cref="ConstantExpression"/> objects representing the items in the specified sequence
		/// </summary>
		/// <param name="source">The sequence from which <see cref="ConstantExpression"/> objects are created</param>
		/// <returns>A sequence of <see cref="ConstantExpression"/> objects representing the items in the specified sequence</returns>
		[Pure]
		public static IEnumerable<Expression> ToConstants<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return source.Select<T, Expression>(item => Expression.Constant(item));
		}
	}
}