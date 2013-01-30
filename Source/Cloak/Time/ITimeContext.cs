using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// Describes a context in which events happen on the same timeline
	/// </summary>
	public interface ITimeContext
	{
		/// <summary>
		/// Gets the current date and time in this context
		/// </summary>
		DateTime Now { get; }

		/// <summary>
		/// Gets the interval between <see cref="Now"/> and the specified reference point
		/// </summary>
		/// <param name="referencePoint">The time relative to <see cref="Now"/></param>
		/// <returns>The interval between <see cref="Now"/> and the specified reference point</returns>
		RelativeTime NowRelativeTo(DateTime referencePoint);
	}
}