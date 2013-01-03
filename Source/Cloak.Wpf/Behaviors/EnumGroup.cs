using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages an enumeration value based on the checked states of a group of radio buttons
	/// </summary>
	public static class EnumGroup
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumGroup.Value"/> attached property
		/// </summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
			"Value",
			typeof(object),
			typeof(EnumGroup),
			new PropertyMetadata(OnArgumentsChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumGroup.TargetValue"/> attached property
		/// </summary>
		public static readonly DependencyProperty TargetValueProperty = DependencyProperty.RegisterAttached(
			"TargetValue",
			typeof(string),
			typeof(EnumGroup),
			new PropertyMetadata(OnArgumentsChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new EnumGroupBehavior(host));

		private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumGroup.Value"/> attached property
		/// </summary>
		/// <param name="radioButton">The radio button from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumGroup.Value"/> attached property</returns>
		public static object GetValue(RadioButton radioButton)
		{
			Contract.Requires(radioButton != null);

			return radioButton.GetValue(ValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumGroup.Value"/> attached property
		/// </summary>
		/// <param name="radioButton">The radio button on which to set the <see cref="Cloak.Wpf.Behaviors.EnumGroup.Value"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetValue(RadioButton radioButton, object value)
		{
			Contract.Requires(radioButton != null);

			radioButton.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property
		/// </summary>
		/// <param name="radioButton">The radio button from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property</returns>
		public static string GetTargetValue(RadioButton radioButton)
		{
			Contract.Requires(radioButton != null);

			return (string) radioButton.GetValue(TargetValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property
		/// </summary>
		/// <param name="radioButton">The radio button on which to set the <see cref="Cloak.Wpf.Behaviors.EnumVisibility.TargetValue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetTargetValue(RadioButton radioButton, string value)
		{
			Contract.Requires(radioButton != null);

			radioButton.SetValue(TargetValueProperty, value);
		}

		private sealed class EnumGroupBehavior : Behavior<RadioButton>
		{
			private readonly EnumCheck _enumCheck = new EnumCheck();

			internal EnumGroupBehavior(DependencyObject host) : base(host)
			{}

			protected override void Attach(RadioButton host)
			{
				host.Checked += OnChecked;
			}

			protected override void Detach(RadioButton host)
			{
				host.Checked -= OnChecked;
			}

			protected override void Update(RadioButton host)
			{
				_enumCheck.Update(GetValue(host), GetTargetValue(host));

				host.IsChecked = _enumCheck.IsMatch;
			}

			private void OnChecked(object sender, RoutedEventArgs e)
			{
				TryUpdate(host => SetValue(host, _enumCheck.ParsedTargetValue));
			}
		}
	}
}