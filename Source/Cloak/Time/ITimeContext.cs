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
	}
}