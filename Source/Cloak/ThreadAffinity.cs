using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cloak
{
	/// <summary>
	/// An association to a particular thread
	/// </summary>
	public sealed class ThreadAffinity
	{
		/// <summary>
		/// Initializes an affinity with the specified thread
		/// </summary>
		/// <param name="associatedThread">The associated thread</param>
		public ThreadAffinity(Thread associatedThread)
		{
			Contract.Requires(associatedThread != null);

			AssociatedThread = associatedThread;
		}

		/// <summary>
		/// Initializes an affinity with the current thread
		/// </summary>
		public ThreadAffinity() : this(Thread.CurrentThread)
		{}

		/// <summary>
		/// Gets the associated thread
		/// </summary>
		public Thread AssociatedThread { get; private set; }

		/// <summary>
		/// Determines if the current thread is the associated thread
		/// </summary>
		/// <returns>Whether the current thread is the associated thread</returns>
		[Pure]
		public bool Check()
		{
			return Thread.CurrentThread == AssociatedThread;
		}

		/// <summary>
		/// Throws an exception if the current thread is not the associated thread
		/// </summary>
		/// <exception cref="ThreadAffinityException">Thrown if the current thread is not the associated thread</exception>
		[Pure]
		public void Enforce()
		{
			if(!Check())
			{
				throw new ThreadAffinityException(this);
			}
		}
	}
}