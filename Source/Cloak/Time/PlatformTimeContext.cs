﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// A context in which events happen on the timeline of the current operating system
	/// </summary>
	public sealed class PlatformTimeContext : TimeContext
	{
		/// <summary>
		/// Gets the current date and time of the operating system
		/// </summary>
		public override DateTime Now
		{
			get { return DateTime.Now; }
		}
	}
}