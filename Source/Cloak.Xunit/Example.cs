using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cloak.Xunit
{
	/// <summary>
	/// An example comprised of several scenarios describing a particular problem
	/// </summary>
	public abstract class Example
	{
		/// <summary>
		/// Arranges the context of a scenario
		/// </summary>
		/// <typeparam name="TContext">The type of context arranged for the scenario</typeparam>
		/// <param name="context">The context arranged for the scenario</param>
		/// <returns>An API for acting on the context</returns>
		protected ScenarioBuilder.WhenBuilder<TContext> Given<TContext>(TContext context)
		{
			return ScenarioBuilder.Given(context);
		}

		/// <summary>
		/// Acts on the context of a scenario
		/// </summary>
		/// <typeparam name="TResult">The type of result of the action</typeparam>
		/// <param name="result">The result of the action</param>
		/// <returns>An API for asserting on the result of the action</returns>
		public ScenarioBuilder.ThenBuilder<TResult> When<TResult>(TResult result)
		{
			return ScenarioBuilder.When(result);
		}
	}
}