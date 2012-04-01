using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends <see cref="LambdaExpression"/> objects with the ability to create strongly-typed lambda expressions
	/// </summary>
	public static class TypedLambdaExpressions
	{
		/// <summary>
		/// Gets a strongly-typed version of the specified lambda expression
		/// </summary>
		/// <typeparam name="TResult">The return type of the lambda expression</typeparam>
		/// <param name="lambda">The lambda expression for which the strongly-typed lambda expression is created</param>
		/// <returns>A strongly-typed version of the specified lambda expression</returns>
		public static Expression<Func<TResult>> AsFunc<TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return Expression.Lambda<Func<TResult>>(lambda.Body, lambda.Parameters);
		}

		/// <summary>
		/// Gets a strongly-typed version of the specified lambda expression
		/// </summary>
		/// <typeparam name="T">The type of the parameter to the lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the lambda expression</typeparam>
		/// <param name="lambda">The lambda expression for which the strongly-typed lambda expression is created</param>
		/// <returns>A strongly-typed version of the specified lambda expression</returns>
		public static Expression<Func<T, TResult>> AsFunc<T, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return Expression.Lambda<Func<T, TResult>>(lambda.Body, lambda.Parameters);
		}

		/// <summary>
		/// Gets a strongly-typed version of the specified lambda expression
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter to the lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second parameter to the lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the lambda expression</typeparam>
		/// <param name="lambda">The lambda expression for which the strongly-typed lambda expression is created</param>
		/// <returns>A strongly-typed version of the specified lambda expression</returns>
		public static Expression<Func<T1, T2, TResult>> AsFunc<T1, T2, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return Expression.Lambda<Func<T1, T2, TResult>>(lambda.Body, lambda.Parameters);
		}

		/// <summary>
		/// Gets a strongly-typed version of the specified lambda expression
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter to the lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second parameter to the lambda expression</typeparam>
		/// <typeparam name="T3">The type of the third parameter to the lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the lambda expression</typeparam>
		/// <param name="lambda">The lambda expression for which the strongly-typed lambda expression is created</param>
		/// <returns>A strongly-typed version of the specified lambda expression</returns>
		public static Expression<Func<T1, T2, T3, TResult>> AsFunc<T1, T2, T3, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return Expression.Lambda<Func<T1, T2, T3, TResult>>(lambda.Body, lambda.Parameters);
		}

		/// <summary>
		/// Gets a strongly-typed version of the specified lambda expression
		/// </summary>
		/// <typeparam name="T1">The type of the first parameter to the lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second parameter to the lambda expression</typeparam>
		/// <typeparam name="T3">The type of the third parameter to the lambda expression</typeparam>
		/// <typeparam name="T4">The type of the fourth parameter to the lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the lambda expression</typeparam>
		/// <param name="lambda">The lambda expression for which the strongly-typed lambda expression is created</param>
		/// <returns>A strongly-typed version of the specified lambda expression</returns>
		public static Expression<Func<T1, T2, T3, T4, TResult>> AsFunc<T1, T2, T3, T4, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return Expression.Lambda<Func<T1, T2, T3, T4, TResult>>(lambda.Body, lambda.Parameters);
		}
	}
}