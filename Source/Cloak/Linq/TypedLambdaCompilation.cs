using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends <see cref="LambdaExpression"/> objects with the ability to compile strongly-typed lambda expressions
	/// </summary>
	public static class TypedLambdaCompilation
	{
		/// <summary>
		/// Compiles the specified lambda expression to a function with the specified signature
		/// </summary>
		/// <typeparam name="TResult">The return type of the compiled lambda expression</typeparam>
		/// <param name="lambda">The lambda expression to be compiled</param>
		/// <returns>A function representing the executable form of the specified lambda expression</returns>
		public static Func<TResult> Compile<TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return lambda.AsFunc<TResult>().Compile();
		}

		/// <summary>
		/// Compiles the specified lambda expression to a function with the specified signature
		/// </summary>
		/// <typeparam name="T">The type of the parameter to the compiled lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the compiled lambda expression</typeparam>
		/// <param name="lambda">The lambda expression to be compiled</param>
		/// <returns>A function representing the executable form of the specified lambda expression</returns>
		public static Func<T, TResult> Compile<T, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return lambda.AsFunc<T, TResult>().Compile();
		}

		/// <summary>
		/// Compiles the specified lambda expression to a function with the specified signature
		/// </summary>
		/// <typeparam name="T1">The type of the first to the compiled lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second to the compiled lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the compiled lambda expression</typeparam>
		/// <param name="lambda">The lambda expression to be compiled</param>
		/// <returns>A function representing the executable form of the specified lambda expression</returns>
		public static Func<T1, T2, TResult> Compile<T1, T2, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return lambda.AsFunc<T1, T2, TResult>().Compile();
		}

		/// <summary>
		/// Compiles the specified lambda expression to a function with the specified signature
		/// </summary>
		/// <typeparam name="T1">The type of the first to the compiled lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second to the compiled lambda expression</typeparam>
		/// <typeparam name="T3">The type of the third to the compiled lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the compiled lambda expression</typeparam>
		/// <param name="lambda">The lambda expression to be compiled</param>
		/// <returns>A function representing the executable form of the specified lambda expression</returns>
		public static Func<T1, T2, T3, TResult> Compile<T1, T2, T3, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return lambda.AsFunc<T1, T2, T3, TResult>().Compile();
		}

		/// <summary>
		/// Compiles the specified lambda expression to a function with the specified signature
		/// </summary>
		/// <typeparam name="T1">The type of the first to the compiled lambda expression</typeparam>
		/// <typeparam name="T2">The type of the second to the compiled lambda expression</typeparam>
		/// <typeparam name="T3">The type of the third to the compiled lambda expression</typeparam>
		/// <typeparam name="T4">The type of the fourth to the compiled lambda expression</typeparam>
		/// <typeparam name="TResult">The return type of the compiled lambda expression</typeparam>
		/// <param name="lambda">The lambda expression to be compiled</param>
		/// <returns>A function representing the executable form of the specified lambda expression</returns>
		public static Func<T1, T2, T3, T4, TResult> Compile<T1, T2, T3, T4, TResult>(this LambdaExpression lambda)
		{
			Contract.Requires(lambda != null);

			return lambda.AsFunc<T1, T2, T3, T4, TResult>().Compile();
		}
	}
}