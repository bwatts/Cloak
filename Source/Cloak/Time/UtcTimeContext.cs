using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// A context in which events happen on the UTC timeline
	/// </summary>
	public sealed class UtcTimeContext : ITimeContext
	{
		/// <summary>
		/// Gets the current UTC date and time
		/// </summary>
		public DateTime Now
		{
			get { return DateTime.UtcNow; }
		}
	}
}