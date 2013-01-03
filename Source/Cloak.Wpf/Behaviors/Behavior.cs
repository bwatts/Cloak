using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Base implementation of a behavior which can be attached to a host object
	/// </summary>
	/// <typeparam name="THost">The type of host object to which this behavior can be attached</typeparam>
	public abstract class Behavior<THost> : IBehavior where THost : DependencyObject
	{
		private readonly WeakReference _hostReference;

		/// <summary>
		/// Initializes a behavior with the specified host object
		/// </summary>
		/// <param name="host">The object with which this behavior is associated</param>
		protected Behavior(DependencyObject host)
		{
			Contract.Requires(host != null);
			Contract.Requires(host is THost);

			_hostReference = new WeakReference(host);
		}

		#region IBehavior
		/// <summary>
		/// Determines whether this behavior is applicable to the host object (if not garbage-collected)
		/// </summary>
		/// <returns>Whether this behavior is applicable to the host object</returns>
		public bool IsApplicable()
		{
			var host = GetHost();

			return host != null && IsApplicable(host);
		}

		/// <summary>
		/// Attaches this behavior to the host object (if not garbage-collected)
		/// </summary>
		public void Attach()
		{
			var host = GetHost();

			if(host != null)
			{
				Attach(host);
			}
		}

		/// <summary>
		/// Detaches this behavior from the host object (if not garbage-collected)
		/// </summary>
		public void Detach()
		{
			var host = GetHost();

			if(host != null)
			{
				Detach(host);
			}
		}

		/// <summary>
		/// Synchronizes the behavior with the current state of the host object (if not garbage-collected)
		/// </summary>
		public void Update()
		{
			var host = GetHost();

			if(host != null)
			{
				Update(host);
			}
		}
		#endregion

		/// <summary>
		/// Determines whether this behavior is applicable to the specified host object
		/// </summary>
		/// <returns>Whether this behavior is applicable to the host object</returns>
		protected virtual bool IsApplicable(THost host)
		{
			return true;
		}

		/// <summary>
		/// Attaches this behavior to the specified host object
		/// </summary>
		protected virtual void Attach(THost host)
		{}

		/// <summary>
		/// Detaches this behavior from the specified host object
		/// </summary>
		protected virtual void Detach(THost host)
		{}

		/// <summary>
		/// When implemented by a derived class, synchronizes the behavior with the current state of the
		/// specified host object
		/// </summary>
		protected abstract void Update(THost host);

		/// <summary>
		/// Executes the specified update on the host object (if not garbage-collected)
		/// </summary>
		/// <param name="update">The update to be execute on the host object if it has not been garbage-collected</param>
		protected void TryUpdate(Action<THost> update)
		{
			Contract.Requires(update != null);

			var host = GetHost();

			if(host != null)
			{
				update(host);
			}
		}

		private THost GetHost()
		{
			return (THost) _hostReference.Target;
		}
	}
}