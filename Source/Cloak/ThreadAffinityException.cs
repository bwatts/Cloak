using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak
{
	/// <summary>
	/// Indicates a cross-thread access error
	/// </summary>
	public class ThreadAffinityException : Exception
	{
		/// <summary>
		/// Initializes an exception with the specified thread affinity
		/// </summary>
		/// <param name="threadAffinity">The thread affinity which caused the error</param>
		public ThreadAffinityException(ThreadAffinity threadAffinity)
		{
			Contract.Requires(threadAffinity != null);

			ThreadAffinity = threadAffinity;
		}

		/// <summary>
		/// Initializes an exception with the specified thread affinity
		/// </summary>
		/// <param name="threadAffinity">The thread affinity which caused the error</param>
		/// <param name="message">The exception that describes the error</param>
		public ThreadAffinityException(ThreadAffinity threadAffinity, string message) : base(message)
		{
			Contract.Requires(threadAffinity != null);

			ThreadAffinity = threadAffinity;
		}

		/// <summary>
		/// Initializes an exception with the specified thread affinity
		/// </summary>
		/// <param name="threadAffinity">The thread affinity which caused the error</param>
		/// <param name="message">The exception that describes the error</param>
		/// <param name="inner">The exception that is the cause of the current exception</param>
		public ThreadAffinityException(ThreadAffinity threadAffinity, string message, Exception inner) : base(message, inner)
		{
			Contract.Requires(threadAffinity != null);

			ThreadAffinity = threadAffinity;
		}

		/// <summary>
		/// Gets the thread affinity which caused the error
		/// </summary>
		public ThreadAffinity ThreadAffinity { get; private set; }
	}
}