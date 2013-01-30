using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cloak.Linq
{
	/// <summary>
	/// Extends lambda expressions with the ability to get the members and methods referenced within them
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class LambdaBodies
	{
		/// <summary>
		/// Gets the <see cref="MemberInfo"/> accessed by the specified lambda expression
		/// </summary>
		/// <param name="getter">The lambda expression which accesses the member</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a member; false to return null</param>
		/// <returns>The member accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a member</exception>
		[Pure]
		public static MemberInfo GetMemberInfo(this LambdaExpression getter, bool strict = true)
		{
			Contract.Requires(getter != null);

			var memberExpression = getter.Body as MemberExpression;

			var member = memberExpression == null ? null : memberExpression.Member;

			if(strict && member == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotAccessMember.FormatInvariant(getter), "getter");
			}

			return member;
		}

		/// <summary>
		/// Gets the <see cref="FieldInfo"/> accessed by the specified lambda expression
		/// </summary>
		/// <param name="fieldGetter">The lambda expression which accesses the field</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a field; false to return null</param>
		/// <returns>The field accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a field</exception>
		[Pure]
		public static FieldInfo GetFieldInfo(this LambdaExpression fieldGetter, bool strict = true)
		{
			Contract.Requires(fieldGetter != null);

			var field = fieldGetter.GetMemberInfo() as FieldInfo;

			if(strict && field == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotAccessField.FormatInvariant(fieldGetter), "fieldGetter");
			}

			return field;
		}

		/// <summary>
		/// Gets the <see cref="PropertyInfo"/> accessed by the specified lambda expression
		/// </summary>
		/// <param name="propertyGetter">The lambda expression which accesses the property</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a property; false to return null</param>
		/// <returns>The property accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a property</exception>
		[Pure]
		public static PropertyInfo GetPropertyInfo(this LambdaExpression propertyGetter, bool strict = true)
		{
			Contract.Requires(propertyGetter != null);

			var property = propertyGetter.GetMemberInfo() as PropertyInfo;

			if(strict && property == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotAccessProperty.FormatInvariant(propertyGetter), "propertyGetter");
			}

			return property;
		}

		/// <summary>
		/// Gets the <see cref="MethodInfo"/> called by the specified lambda expression
		/// </summary>
		/// <param name="methodCall">The lambda expression which calls the method</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a method; false to return null</param>
		/// <returns>The method called by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not call a method</exception>
		[Pure]
		public static MethodInfo GetMethodInfo(this LambdaExpression methodCall, bool strict = true)
		{
			Contract.Requires(methodCall != null);

			var callExpression = methodCall.Body as MethodCallExpression;

			var method = callExpression == null ? null : callExpression.Method;

			if(strict && method == null)
			{
				throw new ArgumentException(Resources.ExpressionDoesNotCallMethod.FormatInvariant(methodCall), "callMethod");
			}

			return method;
		}

		/// <summary>
		/// Gets the name of the member accessed by the specified lambda expression
		/// </summary>
		/// <param name="getter">The lambda expression which accesses the member</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a member; false to return null</param>
		/// <returns>The name of the member accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a member</exception>
		[Pure]
		public static string GetMemberName(this LambdaExpression getter, bool strict = true)
		{
			Contract.Requires(getter != null);

			var member = getter.GetMemberInfo(strict);

			return member == null ? null : member.Name;
		}

		/// <summary>
		/// Gets the name of the field accessed by the specified lambda expression
		/// </summary>
		/// <param name="getter">The lambda expression which accesses the field</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a field; false to return null</param>
		/// <returns>The name of the field accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a field</exception>
		[Pure]
		public static string GetFieldName(this LambdaExpression getter, bool strict = true)
		{
			Contract.Requires(getter != null);

			var field = getter.GetFieldInfo(strict);

			return field == null ? null : field.Name;
		}

		/// <summary>
		/// Gets the name of the property accessed by the specified lambda expression
		/// </summary>
		/// <param name="getter">The lambda expression which accesses the property</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a property; false to return null</param>
		/// <returns>The name of the property accessed by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not access a property</exception>
		[Pure]
		public static string GetPropertyName(this LambdaExpression getter, bool strict = true)
		{
			Contract.Requires(getter != null);

			var property = getter.GetPropertyInfo(strict);

			return property == null ? null : property.Name;
		}

		/// <summary>
		/// Gets the name of the method called by the specified lambda expression
		/// </summary>
		/// <param name="callMethod">The lambda expression which calls the method</param>
		/// <param name="strict">True to throw an exception if the lambda expression does not access a method; false to return null</param>
		/// <returns>The name of the method called by the lambda expression</returns>
		/// <exception cref="ArgumentException">Thrown if <paramref name="strict"/> is true and the lambda expression does not call a method</exception>
		[Pure]
		public static string GetMethodName(this LambdaExpression callMethod, bool strict = true)
		{
			Contract.Requires(callMethod != null);

			var method = callMethod.GetMethodInfo(strict);

			return method == null ? null : method.Name;
		}
	}
}