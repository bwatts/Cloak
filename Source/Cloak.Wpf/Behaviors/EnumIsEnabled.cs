using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages the <see cref="System.Windows.UIElement.IsEnabled"/> property using enumeration values
	/// </summary>
	public static class EnumIsEnabled
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.Value"/> attached property
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(object),
			typeof(EnumIsEnabled),
			new PropertyMetadata(OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.TargetValue"/> attached property
		/// </summary>
		public static readonly DependencyProperty TargetValueProperty = DependencyProperty.RegisterAttached(
			"TargetValue",
			typeof(string),
			typeof(EnumIsEnabled),
			new PropertyMetadata(OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenMatched"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenMatchedProperty = DependencyProperty.RegisterAttached(
			"WhenMatched",
			typeof(Visibility),
			typeof(EnumIsEnabled),
			new PropertyMetadata(Visibility.Visible, OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenNotMatched"/> attached property
		/// </summary>
		public static readonly DependencyProperty WhenNotMatchedProperty = DependencyProperty.RegisterAttached(
			"WhenNotMatched",
			typeof(Visibility),
			typeof(EnumIsEnabled),
			new PropertyMetadata(Visibility.Collapsed, OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new EnumIsEnabledBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.Value"/> attached property</returns>
		public static object GetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return uiElement.GetValue(ValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.Value"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.Value"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetValue(UIElement uiElement, object value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.TargetValue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.TargetValue"/> attached property</returns>
		public static string GetTargetValue(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (string) uiElement.GetValue(TargetValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.TargetValue"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.TargetValue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetTargetValue(UIElement uiElement, string value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(TargetValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenMatched"/> attached property</returns>
		public static bool GetWhenMatched(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (bool) uiElement.GetValue(WhenMatchedProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenMatched"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenMatched(UIElement uiElement, bool value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenMatchedProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenNotMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenNotMatched"/> attached property</returns>
		public static bool GetWhenNotMatched(UIElement uiElement)
		{
			Contract.Requires(uiElement != null);

			return (bool) uiElement.GetValue(WhenNotMatchedProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenNotMatched"/> attached property
		/// </summary>
		/// <param name="uiElement">The element on which to set the <see cref="Cloak.Wpf.Behaviors.EnumIsEnabled.WhenNotMatched"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetWhenNotMatched(UIElement uiElement, bool value)
		{
			Contract.Requires(uiElement != null);

			uiElement.SetValue(WhenNotMatchedProperty, value);
		}

		private sealed class EnumIsEnabledBehavior : Behavior<UIElement>
		{
			private readonly EnumCheck _enumCheck = new EnumCheck();

			internal EnumIsEnabledBehavior(DependencyObject host) : base(host)
			{
				// Does not propagate external changes to the binding source
			}

			protected override void Update(UIElement host)
			{
				_enumCheck.Update(GetValue(host), GetTargetValue(host));

				host.IsEnabled = _enumCheck.IsMatch ? GetWhenMatched(host) : GetWhenNotMatched(host);
			}
		}
	}
}