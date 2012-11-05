using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// A context in which all events happen simultaneously
	/// </summary>
	public sealed class ConstantTimeContext : TimeContext
	{
		private readonly DateTime _now;

		/// <summary>
		/// Initializes a context with the specified date and time
		/// </summary>
		/// <param name="now">The current date and time</param>
		public ConstantTimeContext(DateTime now)
		{
			_now = now;
		}

		/// <summary>
		/// Gets the date and time specified when this context was created
		/// </summary>
		public override DateTime Now
		{
			get { return _now; }
		}
	}
}