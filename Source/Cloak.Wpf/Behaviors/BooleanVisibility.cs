using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages the <see cref="System.Windows.UIElement.Visibility"/> property using <see cref="System.Boolean"/> values
	/// </summary>
	public static class BooleanVisibility
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.Value"/> attached property
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(bool),
			typeof(BooleanVisibility),
			new PropertyMetadata(true, OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenTrue"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenTrueProperty = DependencyProperty.RegisterAttached(
			"WhenTrue",
			typeof(Visibility),
			typeof(BooleanVisibility),
			new PropertyMetadata(Visibility.Visible, OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenFalse"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenFalseProperty = DependencyProperty.RegisterAttached(
			"WhenFalse",
			typeof(Visibility),
			typeof(BooleanVisibility),
			new PropertyMetadata(Visibility.Collapsed, OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new BooleanVisibilityBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.Value"/> attached property</returns>
		public static bool GetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (bool) uiElement.GetValue(ValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.Value"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetValue(UIElement uiElement, bool value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenTrue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenTrue"/> attached property</returns>
		public static Visibility GetWhenTrue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenTrueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenTrue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenTrue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenTrue(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenTrueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenFalse"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenFalse"/> attached property</returns>
		public static Visibility GetWhenFalse(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenFalseProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenFalse"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.BooleanVisibility.WhenFalse"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenFalse(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenFalseProperty, value);
		}

		private sealed class BooleanVisibilityBehavior : Behavior<UIElement>
		{
			internal BooleanVisibilityBehavior(DependencyObject host) : base(host)
			{
				// Does not propagate external changes in visibility to the binding source
			}

			protected override void Update(UIElement host)
			{
				host.Visibility = GetValue(host) ? GetWhenTrue(host) : GetWhenFalse(host);
			}
		}
	}
}