using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends lambda expressions with the ability to get the members and methods referenced within them
	/// </summary>
	public static class LambdaBodies
	{
		/// <summary>
		/// Gets the <see cref="MemberInfo"/> accessed by the specified lambda expression
		/// </summary>
		/// <typeparam name="TOwner">The type of object which owns the member</typeparam>
		/// <typeparam name="TValue">The type of the member's value</typeparam>
		/// <param name="getMember">The lambda expression which accesses the member</param>
		/// <returns>The member accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if the lambda expression does not access a member</exception>
		[Pure]
		public static MemberInfo GetMemberInfo<TOwner, TValue>(this Expression<Func<TOwner, TValue>> getMember)
		{
			Contract.Requires(getMember != null);

			var memberExpression = getMember.Body as MemberExpression;
			var member = memberExpression == null ? null : memberExpression.Member;

			if(member == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotSelectMemberFormat.FormatInvariant(getMember), "getMember");
			}

			return member;
		}

		/// <summary>
		/// Gets the name of the member accessed by the specified lambda expression
		/// </summary>
		/// <typeparam name="TOwner">The type of object which owns the member</typeparam>
		/// <typeparam name="TValue">The type of the member's value</typeparam>
		/// <param name="getMember">The lambda expression which accesses the member</param>
		/// <returns>The name of the member accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if the lambda expression does not access a member</exception>
		[Pure]
		public static string GetMemberName<TOwner, TValue>(this Expression<Func<TOwner, TValue>> getMember)
		{
			Contract.Requires(getMember != null);

			return getMember.GetMemberInfo().Name;
		}

		/// <summary>
		/// Gets the <see cref="MethodInfo"/> called by the specified lambda expression
		/// </summary>
		/// <typeparam name="TOwner">The type of object which owns the method</typeparam>
		/// <typeparam name="TValue">The method's return type</typeparam>
		/// <param name="callMethod">The lambda expression which calls the method</param>
		/// <returns>The method called by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if the lambda expression does not call a method</exception>
		[Pure]
		public static MethodInfo GetMethodInfo<TOwner, TValue>(this Expression<Func<TOwner, TValue>> callMethod)
		{
			Contract.Requires(callMethod != null);

			var callExpression = callMethod.Body as MethodCallExpression;
			var method = callExpression == null ? null : callExpression.Method;

			if(method == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotCallMethodFormat.FormatInvariant(callMethod), "callMethod");
			}

			return method;
		}

		/// <summary>
		/// Gets the name of the method called by the specified lambda expression
		/// </summary>
		/// <typeparam name="TOwner">The type of object which owns the method</typeparam>
		/// <typeparam name="TValue">The method's return type</typeparam>
		/// <param name="callMethod">The lambda expression which calls the method</param>
		/// <returns>The name of the method called by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if the lambda expression does not call a method</exception>
		[Pure]
		public static string GetMethodName<TOwner, TValue>(this Expression<Func<TOwner, TValue>> callMethod)
		{
			Contract.Requires(callMethod != null);

			return callMethod.GetMethodInfo().Name;
		}
	}
}