using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace Cloak.Wpf.Mvvm
{
	/// <summary>
	/// Base class for generic and non-generic implementations of a command
	/// </summary>
	public abstract class CommandBase
	{
		private readonly Dispatcher _dispatcher = DispatcherContext.GetDispatcher();

		/// <summary>
		/// Occurs when this command changes whether it can currently execute
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		/// <summary>
		/// Raises the <see cref="CanExecuteChanged"/> event via the current dispatcher
		/// </summary>
		protected virtual void OnCanExecuteChanged()
		{
			if(_dispatcher.CheckAccess())
			{
				CommandManager.InvalidateRequerySuggested();
			}
			else
			{
				_dispatcher.Invoke((ThreadStart) OnCanExecuteChanged, DispatcherPriority.Normal);
			}
		}
	}
}