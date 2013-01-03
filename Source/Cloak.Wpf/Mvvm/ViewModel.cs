using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Cloak.Wpf.Mvvm
{
	/// <summary>
	/// Base class for objects which model the domain-specific behavior of a WPF view
	/// </summary>
	public abstract class ViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a view model with the effective dispatcher in the current context
		/// </summary>
		protected ViewModel()
		{
			Dispatcher = DispatcherContext.GetDispatcher();
		}

		#region INotifyPropertyChanged
		/// <summary>
		/// Occurs when a property value changes
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event
		/// </summary>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;

			if(handler != null)
			{
				handler(this, e);
			}
		}
		#endregion

		/// <summary>
		/// Gets the dispatcher which manages the queue of work items for this view model's thread
		/// </summary>
		protected Dispatcher Dispatcher { get; private set; }

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event for the property with the specified name
		/// </summary>
		/// <param name="name">The name of the property which changed</param>
		protected void RaisePropertyChanged(string name)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(name));
		}

		/// <summary>
		/// Sets the property with the specified name to the specified value
		/// </summary>
		/// <typeparam name="TValue">The type of the property's value</typeparam>
		/// <param name="name">The name of the property to set</param>
		/// <param name="member">The member variable which holds the property's value</param>
		/// <param name="newValue">The new value to be applied to the property</param>
		/// <param name="equalityComparer">The comparer which determines whether the member and new value are equal</param>
		/// <returns>Whether the property changed</returns>
		protected bool SetProperty<TValue>(string name, ref TValue member, TValue newValue, IEqualityComparer<TValue> equalityComparer)
		{
			Contract.Requires(equalityComparer != null);

			var changed = !equalityComparer.Equals(member, newValue);

			if(changed)
			{
				member = newValue;

				RaisePropertyChanged(name);
			}

			return changed;
		}

		/// <summary>
		/// Sets the property with the specified name to the specified value
		/// </summary>
		/// <typeparam name="TValue">The type of the property's value</typeparam>
		/// <param name="name">The name of the property to set</param>
		/// <param name="member">The member variable which holds the property's value</param>
		/// <param name="newValue">The new value to be applied to the property</param>
		/// <returns>Whether the property changed</returns>
		protected bool SetProperty<TValue>(string name, ref TValue member, TValue newValue)
		{
			return SetProperty(name, ref member, newValue, EqualityComparer<TValue>.Default);
		}

		/// <summary>
		/// Adds the specified action to the queue of work items for this view model's thread
		/// </summary>
		/// <param name="action">The action to execute on the view model's thread</param>
		/// <returns>Whether this call was already running on the view model's thread</returns>
		protected bool Dispatch(Action action)
		{
			return Dispatcher.Dispatch(action);
		}
	}
}