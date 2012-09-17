using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Xunit
{
	/// <summary>
	/// Arranges, acts, and asserts on a scenario in an example
	/// </summary>
	public sealed class ScenarioBuilder
	{
		/// <summary>
		/// Arranges the context of a scenario
		/// </summary>
		/// <typeparam name="TContext">The type of context arranged for the scenario</typeparam>
		/// <param name="context">The context arranged for the scenario</param>
		/// <returns>An API for acting on the context</returns>
		public static WhenBuilder<TContext> Given<TContext>(TContext context)
		{
			return new WhenBuilder<TContext>(context);
		}

		/// <summary>
		/// Acts on the context of a scenario
		/// </summary>
		/// <typeparam name="TResult">The type of result of the action</typeparam>
		/// <param name="result">The result of the action</param>
		/// <returns>An API for asserting on the result of the action</returns>
		public static ScenarioBuilder.ThenBuilder<TResult> When<TResult>(TResult result)
		{
			return new ThenBuilder<TResult>(result);
		}

		/// <summary>
		/// Acts on the context arranged for the scenario
		/// </summary>
		/// <typeparam name="TContext">The type of context arranged for the scenario</typeparam>
		public sealed class WhenBuilder<TContext>
		{
			private readonly TContext _context;

			/// <summary>
			/// Initializes a builder with the specified context
			/// </summary>
			/// <param name="context">The context arranged for the scenario</param>
			public WhenBuilder(TContext context)
			{
				_context = context;
			}

			/// <summary>
			/// Acts on the context arranged for the scenario
			/// </summary>
			/// <typeparam name="TResult">The type of result of the action</typeparam>
			/// <param name="action">The action which operates on the context</param>
			/// <returns>An API for making assertions on the result of the action</returns>
			public ThenBuilder<TResult> When<TResult>(Func<TContext, TResult> action)
			{
				var result = action(_context);

				return new ThenBuilder<TResult>(result);
			}
		}

		/// <summary>
		/// Asserts on the outcome of a scenario
		/// </summary>
		/// <typeparam name="TResult">The type of result of the action</typeparam>
		/// <returns>An API for asserting on the outcome of the action</returns>
		public sealed class ThenBuilder<TResult>
		{
			private readonly TResult _result;

			/// <summary>
			/// Initializes a builder with the specified result
			/// </summary>
			/// <param name="result">The result of the action</param>
			public ThenBuilder(TResult result)
			{
				_result = result;
			}

			/// <summary>
			/// Asserts on the result of an action
			/// </summary>
			/// <param name="assertion">The assertion made on the result of the action</param>
			/// <returns>An API for making further assertions on the result of the action </returns>
			public ThenBuilder<TResult> Then(Action<TResult> assertion)
			{
				assertion(_result);

				return this;
			}
		}
	}
}