using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Manages an enumeration value based on a selector's selected item
	/// </summary>
	public static class EnumSelector
	{
		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumSelector.SelectedValue"/> attached property
		/// </summary>
		public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.RegisterAttached(
			"SelectedValue",
			typeof(object),
			typeof(EnumSelector),
			new PropertyMetadata(OnSelectedValueChanged));

		/// <summary>
		/// Identifies the <see cref="Cloak.Wpf.Behaviors.EnumSelector.ItemValue"/> attached property
		/// </summary>
		public static readonly DependencyProperty ItemValueProperty = DependencyProperty.RegisterAttached(
			"ItemValue",
			typeof(string),
			typeof(EnumSelector),
			new PropertyMetadata(OnItemValueChanged));

		private static readonly AttachedBehavior Behavior =
			AttachedBehavior.Register(host => new EnumSelectorBehavior(host));

		private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Behavior.Update(d);
		}

		private static void OnItemValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var item = d as FrameworkElement;

			if(item != null)
			{
				var selector = item.Parent as Selector;

				if(selector != null)
				{
					Behavior.Update(selector);
				}
			}
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumSelector.SelectedValue"/> attached property
		/// </summary>
		/// <param name="selector">The selector from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumGroup.SelectedValue"/> attached property</returns>
		public static object GetSelectedValue(Selector selector)
		{
			Contract.Requires(selector != null);

			return selector.GetValue(SelectedValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumSelector.SelectedValue"/> attached property
		/// </summary>
		/// <param name="selector">The selector on which to set the <see cref="Cloak.Wpf.Behaviors.EnumSelector.SelectedValue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetSelectedValue(Selector selector, object value)
		{
			Contract.Requires(selector != null);

			selector.SetValue(SelectedValueProperty, value);
		}

		/// <summary>
		/// Gets the value of the <see cref="Cloak.Wpf.Behaviors.EnumSelector.ItemValue"/> attached property
		/// </summary>
		/// <param name="item">The selector item from which to read the property value</param>
		/// <returns>The value of the <see cref="Cloak.Wpf.Behaviors.EnumGroup.ItemValue"/> attached property</returns>
		public static string GetItemValue(DependencyObject item)
		{
			Contract.Requires(item != null);

			return (string) item.GetValue(ItemValueProperty);
		}

		/// <summary>
		/// Sets the value of the <see cref="Cloak.Wpf.Behaviors.EnumSelector.ItemValue"/> attached property
		/// </summary>
		/// <param name="item">The selector item on which to set the <see cref="Cloak.Wpf.Behaviors.EnumSelector.ItemValue"/> attached property</param>
		/// <param name="value">The property value to set</param>
		public static void SetItemValue(DependencyObject item, string value)
		{
			Contract.Requires(item != null);

			item.SetValue(ItemValueProperty, value);
		}

		private sealed class EnumSelectorBehavior : Behavior<Selector>
		{
			private readonly IDictionary<object, EnumCheck> _itemEnumChecks = new Dictionary<object, EnumCheck>();

			internal EnumSelectorBehavior(DependencyObject host) : base(host)
			{}

			protected override void Attach(Selector host)
			{
				host.SelectionChanged += OnSelectionChanged;
			}

			protected override void Detach(Selector host)
			{
				host.SelectionChanged -= OnSelectionChanged;

				_itemEnumChecks.Clear();
			}

			protected override void Update(Selector host)
			{
				var selectedValue = GetSelectedValue(host);

				for(var index = 0; index < host.Items.Count; index++)
				{
					var item = host.Items[index];

					var itemEnumCheck = GetItemEnumCheck(item);

					itemEnumCheck.Update(selectedValue, GetItemValue(item));

					if(itemEnumCheck.IsMatch)
					{
						host.SelectedIndex = index;

						break;
					}
				}
			}

			private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
			{
				TryUpdate(UpdateSelectedValue);
			}

			private void UpdateSelectedValue(Selector host)
			{
				var selectedValue = GetSelectedValue(host);

				var itemEnumCheck = GetItemEnumCheck(host.SelectedItem);

				itemEnumCheck.Update(selectedValue, GetItemValue(host.SelectedItem));

				var parsedTargetValue = itemEnumCheck.ParsedTargetValue;

				if(selectedValue != null && !selectedValue.Equals(parsedTargetValue))
				{
					SetSelectedValue(host, parsedTargetValue);
				}
			}

			private EnumCheck GetItemEnumCheck(object item)
			{
				EnumCheck itemEnumCheck;

				if(!_itemEnumChecks.TryGetValue(item, out itemEnumCheck))
				{
					itemEnumCheck = new EnumCheck();

					_itemEnumChecks[item] = itemEnumCheck;
				}

				return itemEnumCheck;
			}

			private static string GetItemValue(object item)
			{
				var dependencyObjectItem = item as DependencyObject;

				return dependencyObjectItem == null ? null : EnumSelector.GetItemValue(dependencyObjectItem);
			}
		}
	}
}