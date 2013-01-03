using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Cloak.Wpf
{
	/// <summary>
	/// Extension methods for dispatching <see cref="System.Action"/> delegates
	/// </summary>
	public static class ActionDispatch
	{
		/// <summary>
		/// Dispatches the specified action to the dispatcher
		/// </summary>
		/// <param name="dispatcher">The dispatcher to which the specified action is dispatched</param>
		/// <param name="action">The action to be dispatched</param>
		/// <param name="priority">The priority with which the action is dispatched</param>
		/// <returns>Whether the dispatcher was already on the current thread</returns>
		public static bool Dispatch(this Dispatcher dispatcher, Action action, DispatcherPriority priority)
		{
			Contract.Requires(dispatcher != null);
			Contract.Requires(action != null);

			var hasAccess = dispatcher.CheckAccess();

			if(hasAccess)
			{
				action();
			}
			else
			{
				dispatcher.Invoke(priority, action);
			}

			return hasAccess;
		}

		/// <summary>
		/// Dispatches the specified action to the dispatcher with normal priority
		/// </summary>
		/// <param name="dispatcher">The dispatcher to which the specified action is dispatched</param>
		/// <param name="action">The action to be dispatched with normal priority</param>
		/// <returns>Whether the dispatcher was already on the current thread</returns>
		public static bool Dispatch(this Dispatcher dispatcher, Action action)
		{
			Contract.Requires(dispatcher != null);
			Contract.Requires(action != null);

			return dispatcher.Dispatch(action, DispatcherPriority.Normal);
		}

		/// <summary>
		/// Dispatches the specified action to the object's dispatcher
		/// </summary>
		/// <param name="dispatcherObject">The object associated with the dispatcher to which the specified action is dispatched</param>
		/// <param name="action">The action to be dispatched</param>
		/// <param name="priority">The priority with which the action is dispatched</param>
		/// <returns>Whether the object's dispatcher was already on the current thread</returns>
		public static bool Dispatch(this DispatcherObject dispatcherObject, Action action, DispatcherPriority priority)
		{
			Contract.Requires(dispatcherObject != null);
			Contract.Requires(action != null);

			return dispatcherObject.Dispatcher.Dispatch(action, priority);
		}

		/// <summary>
		/// Dispatches the specified action to the object's dispatcher with normal priority
		/// </summary>
		/// <param name="dispatcherObject">The object associated with the dispatcher to which the specified action is dispatched</param>
		/// <param name="action">The action to be dispatched with normal priority</param>
		/// <returns>Whether the object's dispatcher was already on the current thread</returns>
		public static bool Dispatch(this DispatcherObject dispatcherObject, Action action)
		{
			Contract.Requires(dispatcherObject != null);
			Contract.Requires(action != null);

			return dispatcherObject.Dispatcher.Dispatch(action);
		}
	}
}