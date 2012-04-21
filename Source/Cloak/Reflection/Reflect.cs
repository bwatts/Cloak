using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cloak.Reflection
{
	/// <summary>
	/// Strongly-typed reflection using <see cref="System.Action"/> and <see cref="System.Func{T}"/> delegates and the <see cref="System.Linq.Expressions"/> namespace
	/// </summary>
	public static class Reflect
	{
		#region Action
		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action(Action action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T>(Action<T> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2>(Action<T1, T2> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3>(Action<T1, T2, T3> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="action">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
		{
			Contract.Requires(action != null);

			return GetMethod(action);
		}
		#endregion

		#region Func
		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<TResult>(Func<TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T, TResult>(Func<T, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, TResult>(Func<T1, T2, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}

		/// <summary>
		/// Gets the method encapsulated by the specified delegate
		/// </summary>
		/// <param name="func">The delegate which encapsulates the method</param>
		/// <returns>The method encapsulated by the specified delegate</returns>
		public static MethodInfo Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func)
		{
			Contract.Requires(func != null);

			return GetMethod(func);
		}
		#endregion

		#region Members
		/// <summary>
		/// Gets the property or field accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the property or field</param>
		/// <returns>The property or field accessed by the specified expression</returns>
		public static MemberInfo Member<TResult>(Expression<Func<TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember(expression);
		}

		/// <summary>
		/// Gets the property or field accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the property or field</param>
		/// <returns>The property or field accessed by the specified expression</returns>
		public static MemberInfo Member<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember(expression);
		}

		/// <summary>
		/// Gets the property accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the property</param>
		/// <returns>The property accessed by the specified expression</returns>
		public static PropertyInfo Property<TResult>(Expression<Func<TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember<PropertyInfo>(expression);
		}

		/// <summary>
		/// Gets the property accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the property</param>
		/// <returns>The property accessed by the specified expression</returns>
		public static PropertyInfo Property<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember<PropertyInfo>(expression);
		}

		/// <summary>
		/// Gets the field accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the field</param>
		/// <returns>The field accessed by the specified expression</returns>
		public static FieldInfo Field<TResult>(Expression<Func<TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember<FieldInfo>(expression);
		}

		/// <summary>
		/// Gets the field accessed by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which accesses the field</param>
		/// <returns>The field accessed by the specified expression</returns>
		public static FieldInfo Field<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetMember<FieldInfo>(expression);
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<TResult>(Expression<Func<TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}

		/// <summary>
		/// Gets the constructor invoked by the specified lambda expression
		/// </summary>
		/// <param name="expression">The expression which invokes the constructor</param>
		/// <returns>The constructor invoked by the specified expression</returns>
		public static ConstructorInfo Constructor<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> expression)
		{
			Contract.Requires(expression != null);

			return GetConstructor(expression);
		}
		#endregion

		private static TInfo GetInfo<TBody, TInfo>(LambdaExpression expression, Func<TBody, TInfo> getInfo) where TBody : Expression where TInfo : MemberInfo
		{
			var body = expression.Body as TBody;

			if(body == null)
			{
				throw new ArgumentException(Resources.InvalidLambdaExpressionBody.FormatInvariant(typeof(TBody), expression));
			}

			return getInfo(body);
		}

		private static MethodInfo GetMethod(Delegate wrapper)
		{
			return wrapper.Method;
		}

		private static MethodInfo GetMethod(LambdaExpression expression)
		{
			return GetInfo(expression, (MethodCallExpression call) => call.Method);
		}

		private static MemberInfo GetMember(LambdaExpression expression)
		{
			return GetInfo(expression, (MemberExpression access) => access.Member);
		}

		private static TInfo GetMember<TInfo>(LambdaExpression expression) where TInfo : MemberInfo
		{
			var info = GetMember(expression) as TInfo;

			if(info == null)
			{
				throw new ArgumentException(Resources.InvalidLambdaExpressionBody.FormatInvariant(typeof(TInfo), expression));
			}

			return info;
		}

		private static ConstructorInfo GetConstructor(LambdaExpression expression)
		{
			return GetInfo(expression, (NewExpression create) => create.Constructor);
		}
	}
}