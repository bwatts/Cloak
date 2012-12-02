using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends sequences with the ability to select sequences of <see cref="Expression"/> objects
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ExpressionSequence
	{
		/// <summary>
		/// Gets a sequence of <see cref="Expression"/> objects representing the items in the specified sequence
		/// </summary>
		/// <param name="source">
		/// The sequence from which <see cref="Expression"/> objects are created. Items which are not instances of
		/// <see cref="Expression"/> are converted to instances of <see cref="ConstantExpression"/>.
		/// </param>
		/// <returns>A sequence of <see cref="Expression"/> objects representing the items in the specified sequence</returns>
		[Pure]
		public static IEnumerable<Expression> ToExpressions<T>(this IEnumerable<T> source)
		{
			Contract.Requires(source != null);

			return source.Select<T, Expression>(item => (item as Expression) ?? Expression.Constant(item));
		}
	}
}