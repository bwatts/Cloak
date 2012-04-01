using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cloak.Linq
{
	/// <summary>
	/// Base class for implementations of <see cref="IQueryProvider"/>
	/// </summary>
	public abstract class QueryProvider : IQueryProvider
	{
		/// <summary>
		/// Initializes a query provider
		/// </summary>
		protected QueryProvider()
		{}

		#region IQueryProvider

		IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
		{
			return new Query<T>(this, expression);
		}

		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			var elementType = SequenceElementType.From(expression.Type);

			try
			{
				return (IQueryable) Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType), new object[] { this, expression });
			}
			catch(TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}

		T IQueryProvider.Execute<T>(Expression expression)
		{
			return Execute<T>(expression);
		}

		object IQueryProvider.Execute(Expression expression)
		{
			return Execute(expression);
		}

		/// <summary>
		/// When implemented in a derived class, execute the specified expression
		/// </summary>
		/// <typeparam name="T">The type to which to convert the expression's result</typeparam>
		/// <param name="expression">The expression to execute</param>
		/// <returns>The result of evaluating the expression</returns>
		public abstract T Execute<T>(Expression expression);

		/// <summary>
		/// When implemented in a derived class, execute the specified expression
		/// </summary>
		/// <param name="expression">The expression to execute</param>
		/// <returns>The result of evaluating the expression</returns>
		public abstract object Execute(Expression expression);

		#endregion

		/// <summary>
		/// When implemented in a derived class, gets text representing the specified expression
		/// </summary>
		/// <param name="expression">The expression for which to get text</param>
		/// <returns>Text representing the expression</returns>
		public abstract string GetQueryText(Expression expression);
	}
}