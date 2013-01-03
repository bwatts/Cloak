using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages the <see cref="System.Windows.UIElement.Visibility"/> property using enumeration values
	/// </summary>
	public static class EnumVisibility
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.Value"/> attached property
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(object),
			typeof(EnumVisibility),
			new PropertyMetadata(OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property
		/// </summary>
		public static readonly DependencyProperty TargetValueProperty = DependencyProperty.RegisterAttached(
			"TargetValue",
			typeof(string),
			typeof(EnumVisibility),
			new PropertyMetadata(OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenMatched"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenMatchedProperty = DependencyProperty.RegisterAttached(
			"WhenMatched",
			typeof(Visibility),
			typeof(EnumVisibility),
			new PropertyMetadata(Visibility.Visible, OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenNotMatched"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenNotMatchedProperty = DependencyProperty.RegisterAttached(
			"WhenNotMatched",
			typeof(Visibility),
			typeof(EnumVisibility),
			new PropertyMetadata(Visibility.Collapsed, OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new EnumVisibilityBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.Value"/> attached property</returns>
		public static object GetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return uiElement.GetValue(ValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.Value"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetValue(UIElement uiElement, object value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property</returns>
		public static string GetTargetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (string) uiElement.GetValue(TargetValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetTargetValue(UIElement uiElement, string value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(TargetValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenMatched"/> attached property</returns>
		public static Visibility GetWhenMatched(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenMatchedProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenMatched"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenMatched(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenMatchedProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenNotMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenNotMatched"/> attached property</returns>
		public static Visibility GetWhenNotMatched(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (Visibility) uiElement.GetValue(WhenNotMatchedProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenNotMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.WhenNotMatched"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenNotMatched(UIElement uiElement, Visibility value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenNotMatchedProperty, value);
		}

		private sealed class EnumVisibilityBehavior : Behavior<UIElement>
		{
			private readonly EnumCheck _enumCheck = new EnumCheck();

			internal EnumVisibilityBehavior(DependencyObject host) : base(host)
			{
				// Does not propagate external changes in visibility to the binding source
			}

			protected override void Update(UIElement host)
			{
				_enumCheck.Update(GetValue(host), GetTargetValue(host));

				host.Visibility = _enumCheck.IsMatch ? GetWhenMatched(host) : GetWhenNotMatched(host);
			}
		}
	}
}