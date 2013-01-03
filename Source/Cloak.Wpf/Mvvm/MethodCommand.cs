using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Cloak.Wpf.Mvvm
{
	#region MethodCommand
	/// <summary>
	/// A command which implements <see cref="CanExecute"/> and <see cref="Execute"/> using references to external methods
	/// </summary>
	public sealed class MethodCommand : Command
	{
		private readonly Action _executeMethod;
		private readonly Func<bool> _canExecuteMethod;

		/// <summary>
		/// Initialize a command with the specified implementation of the <see cref="Execute"/> method
		/// </summary>
		/// <param name="executeMethod">The implementation of the <see cref="Execute"/> method</param>
		public MethodCommand(Action executeMethod)
		{
			Contract.Requires(executeMethod != null);

			_executeMethod = executeMethod;
		}

		/// <summary>
		/// Initialize a command with the specified implementation of the <see cref="Execute"/> method
		/// </summary>
		/// <param name="executeMethod">The implementation of the <see cref="Execute"/> method</param>
		/// <param name="canExecuteMethod">The implementation of the <see cref="CanExecute"/> method</param>
		public MethodCommand(Action executeMethod, Func<bool> canExecuteMethod)
			: this(executeMethod)
		{
			Contract.Requires(canExecuteMethod != null);

			_canExecuteMethod = canExecuteMethod;
		}

		/// <summary>
		/// Determines whether this command can currently execute
		/// </summary>
		/// <returns>Whether this command can currently execute</returns>
		public override bool CanExecute()
		{
			return _canExecuteMethod == null || _canExecuteMethod();
		}

		/// <summary>
		/// Executes this command
		/// </summary>
		public override void Execute()
		{
			_executeMethod();
		}
	}
	#endregion

	#region MethodCommand<TParameter>
	/// <summary>
	/// A parameterized command which implements <see cref="CanExecute"/> and <see cref="Execute"/> using references to external methods
	/// </summary>
	public sealed class MethodCommand<TParameter> : Command<TParameter>
	{
		private readonly Action<TParameter> _executeMethod;
		private readonly Func<TParameter, bool> _canExecuteMethod;

		/// <summary>
		/// Initialize a command with the specified implementation of the <see cref="Execute"/> method
		/// </summary>
		/// <param name="executeMethod">The implementation of the <see cref="Execute"/> method</param>
		public MethodCommand(Action<TParameter> executeMethod)
		{
			Contract.Requires(executeMethod != null);

			_executeMethod = executeMethod;
		}

		/// <summary>
		/// Initialize a command with the specified implementation of the <see cref="Execute"/> method
		/// </summary>
		/// <param name="executeMethod">The implementation of the <see cref="Execute"/> method</param>
		/// <param name="canExecuteMethod">The implementation of the <see cref="CanExecute"/> method</param>
		public MethodCommand(Action<TParameter> executeMethod, Func<TParameter, bool> canExecuteMethod)
			: this(executeMethod)
		{
			Contract.Requires(canExecuteMethod != null);

			_canExecuteMethod = canExecuteMethod;
		}

		/// <summary>
		/// Determines whether this command can currently execute with the specified parameter
		/// </summary>
		/// <param name="parameter">
		/// Data used by the command. If the command does not require data to be passed, this object can be set to null.
		/// </param>
		/// <returns>Whether this command can currently execute</returns>
		public override bool CanExecute(TParameter parameter)
		{
			return _canExecuteMethod == null || _canExecuteMethod(parameter);
		}

		/// <summary>
		/// Executes this command with the specified parameter
		/// </summary>
		/// <param name="parameter">
		/// Data used by the command. If the command does not require data to be passed, this object can be set to null.
		/// </param>
		public override void Execute(TParameter parameter)
		{
			_executeMethod(parameter);
		}
	}
	#endregion
}