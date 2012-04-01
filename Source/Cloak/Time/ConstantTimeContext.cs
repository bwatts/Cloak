using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// A context in which all events happen simultaneously
	/// </summary>
	public sealed class ConstantTimeContext : ITimeContext
	{
		/// <summary>
		/// Initializes a context with the specified date and time
		/// </summary>
		/// <param name="now">The current date and time</param>
		public ConstantTimeContext(DateTime now)
		{
			Now = now;
		}

		/// <summary>
		/// Gets the date and time specified when this context was created
		/// </summary>
		public DateTime Now { get; private set; }
	}
}