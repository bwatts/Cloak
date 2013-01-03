using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Cloak.Wpf
{
	/// <summary>
	/// Provides access to the effective dispatcher in the current context
	/// </summary>
	public static class DispatcherContext 
	{
		/// <summary>
		/// Gets the effective dispatcher in the current context
		/// </summary>
		/// <returns>The effective dispatcher in the current context</returns>
		public static Dispatcher GetDispatcher()
		{
			return Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher;
		}
	}
}