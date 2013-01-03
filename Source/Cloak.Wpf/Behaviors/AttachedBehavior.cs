using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages the attachment, detachment, and synchronization of a behavior and host objects
	/// </summary>
	public sealed class AttachedBehavior
	{
		private static readonly PropertyGenerator _propertyGenerator = new PropertyGenerator();

		/// <summary>
		/// Registers an attached behavior with the specified instance factory
		/// </summary>
		/// <param name="behaviorFactory">The factory which creates instances of the behavior for host objects</param>
		/// <returns>An object which manages the attachment, detachment, and synchronization of a behavior and host objects</returns>
		public static AttachedBehavior Register(Func<DependencyObject, IBehavior> behaviorFactory)
		{
			Contract.Requires(behaviorFactory != null);

			return new AttachedBehavior(_propertyGenerator.RegisterNextProperty(), behaviorFactory);
		}

		private readonly DependencyProperty _property;
		private readonly Func<DependencyObject, IBehavior> _behaviorFactory;

		internal AttachedBehavior(DependencyProperty property, Func<DependencyObject, IBehavior> behaviorFactory)
		{
			_property = property;
			_behaviorFactory = behaviorFactory;
		}

		/// <summary>
		/// Attaches, detaches, and/or synchronizes this behavior with the specified host object
		/// </summary>
		/// <param name="host">The object with which this behavior will be attached, detached, and/or synchronized</param>
		public void Update(DependencyObject host)
		{
			Contract.Requires(host != null);

			var behavior = (IBehavior) host.GetValue(_property);

			if(behavior == null)
			{
				TryCreateBehavior(host);
			}
			else
			{
				UpdateBehavior(host, behavior);
			}
		}

		private void TryCreateBehavior(DependencyObject host)
		{
			var behavior = _behaviorFactory(host);

			if(behavior.IsApplicable())
			{
				behavior.Attach();

				host.SetValue(_property, behavior);

				behavior.Update();
			}
		}

		private void UpdateBehavior(DependencyObject host, IBehavior behavior)
		{
			if(behavior.IsApplicable())
			{
				behavior.Update();
			}
			else
			{
				host.ClearValue(_property);

				behavior.Detach();
			}
		}

		private sealed class PropertyGenerator
		{
			private readonly object _propertySync = new object();
			private int _propertyCount;

			internal DependencyProperty RegisterNextProperty()
			{
				lock(_propertySync)
				{
					var property = DependencyProperty.RegisterAttached(
						GetNextPropertyName(),
						typeof(IBehavior),
						typeof(AttachedBehavior));

					_propertyCount++;

					return property;
				}
			}

			private string GetNextPropertyName()
			{
				return "AttachedBehavior" + _propertyCount.ToString();
			}
		}
	}
}