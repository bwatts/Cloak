using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Cloak.Wpf.Mvvm
{
	/// <summary>
	/// Base implementation of a command without a parameter
	/// </summary>
	public abstract class Command : CommandBase, ICommand
	{
		#region ICommand

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}

		void ICommand.Execute(object parameter)
		{
			Execute();
		}
		#endregion

		/// <summary>
		/// When implemented in a derived class, determines whether this command can currently execute
		/// </summary>
		/// <returns>Whether this command can currently execute</returns>
		public abstract bool CanExecute();

		/// <summary>
		/// When implemented in a derived class, executes this command
		/// </summary>
		public abstract void Execute();
	}

	/// <summary>
	/// Base implementation of a command with a parameter
	/// </summary>
	/// <typeparam name="TParameter">The type of parameter used by the command</typeparam>
	public abstract class Command<TParameter> : CommandBase, ICommand
	{
		#region ICommand

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute((TParameter) parameter);
		}

		void ICommand.Execute(object parameter)
		{
			Execute((TParameter) parameter);
		}
		#endregion

		/// <summary>
		/// When implemented in a derived class, determines whether this command can currently execute
		/// with the specified parameter
		/// </summary>
		/// <param name="parameter">
		/// Data used by the command. If the command does not require data to be passed, this object can be set to null.
		/// </param>
		/// <returns>Whether this command can currently execute</returns>
		public abstract bool CanExecute(TParameter parameter);

		/// <summary>
		/// When implemented in a derived class, executes this command with the specified parameter
		/// </summary>
		/// <param name="parameter">
		/// Data used by the command. If the command does not require data to be passed, this object can be set to null.
		/// </param>
		public abstract void Execute(TParameter parameter);
	}
}