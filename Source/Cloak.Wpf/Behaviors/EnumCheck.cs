using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Checks whether an enumeration value matches text containing names of enumeration values
	/// </summary>
	public class EnumCheck
	{
		private readonly List<object> _parsedTargetValues = new List<object>();

		/// <summary>
		/// Initializes a check which has never been matched
		/// </summary>
		public EnumCheck()
		{
			ParsedTargetValues = _parsedTargetValues.AsReadOnly();
		}

		/// <summary>
		/// Gets the enumeration value against which the <see cref="TargetValue"/> property is matched
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Gets the text containing names of enumeration values against which the <see cref="Value"/> property is matched
		/// </summary>
		public string TargetValue { get; private set; }

		/// <summary>
		/// Gets the values parsed from the <see cref="TargetValue"/> property using the type of the <see cref="Value"/> property
		/// </summary>
		public ReadOnlyCollection<object> ParsedTargetValues { get; private set; }

		/// <summary>
		/// Gets the first value in the <see cref="ParsedTargetValues"/> collection, or null if empty
		/// </summary>
		public object ParsedTargetValue
		{
			get { return ParsedTargetValues.FirstOrDefault(); }
		}

		/// <summary>
		/// Gets whether the <see cref="Value"/> and <see cref="TargetValue"/> properties match
		/// </summary>
		public bool IsMatch { get; private set; }

		/// <summary>
		/// Matches the specified enumeration value against the specified target value
		/// </summary>
		/// <param name="value">The value to match against the target value</param>
		/// <param name="targetValue">The text containing names of enumeration values to match against the value</param>
		public void Update(object value, string targetValue)
		{
			var valueChanged = value != Value;
			var targetValueChanged = targetValue != TargetValue;

			if(valueChanged || targetValueChanged)
			{
				if(valueChanged)
				{
					Value = value;
				}

				if(targetValueChanged)
				{
					TargetValue = targetValue;
				}

				ParseTargetValues();

				MatchValueAndTargetValue();
			}
		}

		private void ParseTargetValues()
		{
			_parsedTargetValues.Clear();

			if(Value != null && Value is Enum && !String.IsNullOrEmpty(TargetValue))
			{
				var parsedValues =
					from targetValue in TargetValue.Split(',')
					let trimmedTargetValue = targetValue.Trim()
					where trimmedTargetValue.Length > 0
					select Enum.Parse(Value.GetType(), trimmedTargetValue, false);

				_parsedTargetValues.AddRange(parsedValues);
			}
		}

		private void MatchValueAndTargetValue()
		{
			IsMatch = Value == null || Value.Equals("")
				? String.IsNullOrEmpty(TargetValue)
				: _parsedTargetValues.Contains(Value);
		}
	}
}