using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages the <see cref="System.Windows.UIElement.Visibility"/> property using nullable values
	/// </summary>
	public static class NullVisibility
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.NullVisibility.Value"/> attached property
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(object),
			typeof(NullVisibility),
			new PropertyMetadata(true, OnArgumentsChanged));

		// The default values are counter-intuitive. It initially made sense to equate null with collapsed and not null with visible,
		// but that does not align the default visibility (Visible) with the defaulkt nullity (null). In order to avoid unexpected
		// behavior, this is necessary for now.
		//
		// The workaround is to explicitly set these properties so there is no ambiguity.
		//
		// TODO: Determine if there is a way to use the intuitive defaults.

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNull"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenNullProperty = DependencyProperty.RegisterAttached(
			"WhenNull",
			typeof(Visibility),
			typeof(NullVisibility),
			new PropertyMetadata(Visibility.Visible, OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNotNull"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenNotNullProperty = DependencyProperty.RegisterAttached(
			"WhenNotNull",
			typeof(Visibility),
			typeof(NullVisibility),
			new PropertyMetadata(Visibility.Collapsed, OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new NullVisibilityBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.Value"/> attached property</returns>
		public static object GetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return uiElement.GetValue(ValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.NullVisibility.Value"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetValue(UIElement uiElement, object value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNull"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNull"/> attached property</returns>
		public static Visibility GetWhenNull(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenNullProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNull"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNull"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenNull(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenNullProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNotNull"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNotNull"/> attached property</returns>
		public static Visibility GetWhenNotNull(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenNotNullProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNotNull"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.NullVisibility.WhenNotNull"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenNotNull(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenNotNullProperty, value);
		}

		private sealed class NullVisibilityBehavior : Behavior<UIElement>
		{
			internal NullVisibilityBehavior(DependencyObject host) : base(host)
			{
				// Does not propagate external changes in visibility to the binding source
			}

			protected override void Update(UIElement host)
			{
				host.Visibility = GetValue(host) == null ? GetWhenNull(host) : GetWhenNotNull(host);
			}
		}
	}
}