using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Time
{
	/// <summary>
	/// The interval between now and a specified point in time
	/// </summary>
	public class RelativeTime
	{
		/// <summary>
		/// Initializes a relative time with the specified time, reference point, and natural text
		/// </summary>
		/// <param name="now">The start of the interval</param>
		/// <param name="referencePoint">The end of the interval</param>
		/// <param name="naturalText">Natural text describing the interval. Defaults to the text representation of the length of the interval.</param>
		public RelativeTime(DateTime now, DateTime referencePoint, string naturalText = null)
		{
			Contract.Requires(naturalText != null);

			Now = now;
			ReferencePoint = referencePoint;
			
			Length = referencePoint - now;

			if(Length < TimeSpan.Zero)
			{
				Length = new TimeSpan(Math.Abs(Length.Ticks));
			}

			NaturalText = naturalText ?? Length.ToString();
		}

		/// <summary>
		/// Gets natural text describing this interval
		/// </summary>
		public string NaturalText { get; private set; }

		/// <summary>
		/// Gets the start of this interval
		/// </summary>
		public DateTime Now { get; private set; }

		/// <summary>
		/// Gets the end of this interval
		/// </summary>
		public DateTime ReferencePoint { get; private set; }

		/// <summary>
		/// Gets the absolute value of the difference between <see cref="Now"/> and <see cref="ReferencePoint"/>
		/// </summary>
		public TimeSpan Length { get; private set; }

		/// <summary>
		/// Gets whether <see cref="ReferencePoint"/> occurs before <see cref="Now"/>
		/// </summary>
		public bool OccursInPast
		{
			get { return Length < TimeSpan.Zero; }
		}

		/// <summary>
		/// Gets whether <see cref="ReferencePoint"/> occurs after <see cref="Now"/>
		/// </summary>
		public bool OccursInFuture
		{
			get { return Length > TimeSpan.Zero; }
		}

		/// <summary>
		/// Gets whether <see cref="ReferencePoint"/> occurs at <see cref="Now"/>
		/// </summary>
		public bool OccursNow
		{
			get { return Length == TimeSpan.Zero; }
		}

		/// <summary>
		/// Gets the natural text describing this interval
		/// </summary>
		/// <returns>The natural text describing this interval</returns>
		public override string ToString()
		{
			return NaturalText;
		}
	}
}