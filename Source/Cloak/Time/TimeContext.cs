using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Time
{
	/// <summary>
	/// Base implementation of a context in which events happen on the same timeline
	/// </summary>
	public abstract class TimeContext : ITimeContext
	{
		/// <summary>
		/// When implemented by a derived class, gets the current date and time in this context
		/// </summary>
		public abstract DateTime Now { get; }

		/// <summary>
		/// Gets the interval between <see cref="Now"/> and the specified reference point
		/// </summary>
		/// <param name="referencePoint">The time relative to <see cref="Now"/></param>
		/// <returns>The interval between <see cref="Now"/> and the specified reference point</returns>
		public virtual RelativeTime NowRelativeTo(DateTime referencePoint)
		{
			return new RelativeTime(Now, referencePoint);
		}
	}
}