using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Time
{
	/// <summary>
	/// A context whose timeline is offset from another context
	/// </summary>
	public sealed class OffsetTimeContext : ITimeContext
	{
		private readonly ITimeContext _innerContext;
		private readonly TimeSpan _offset;

		/// <summary>
		/// Initializes a context with the specified inner context and offset
		/// </summary>
		/// <param name="innerContext">The context which provides the base date and time</param>
		/// <param name="offset">The amount to shift the base date and time</param>
		public OffsetTimeContext(ITimeContext innerContext, TimeSpan offset)
		{
			Contract.Requires(innerContext != null);

			_innerContext = innerContext;
			_offset = offset;
		}

		/// <summary>
		/// Gets the shifted date and time
		/// </summary>
		public DateTime Now
		{
			get { return _innerContext.Now + _offset; }
		}
	}
}