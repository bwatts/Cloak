using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak
{
	/// <summary>
	/// An integer representing a zero-or-greater count of items
	/// </summary>
	[Serializable]
	public struct Count : IEquatable<Count>, IComparable<Count>
	{
		#region Operators

		public static bool operator ==(Count x, Count y)
		{
			return x.Value == y.Value;
		}

		public static bool operator !=(Count x, Count y)
		{
			return x.Value != y.Value;
		}

		public static bool operator >(Count x, Count y)
		{
			return x.Value > y.Value;
		}

		public static bool operator <(Count x, Count y)
		{
			return x.Value < y.Value;
		}

		public static bool operator >=(Count x, Count y)
		{
			return x.Value >= y.Value;
		}

		public static bool operator <=(Count x, Count y)
		{
			return x.Value <= y.Value;
		}

		public static Count operator +(Count x, Count y)
		{
			return new Count(x.Value + y.Value);
		}

		public static Count operator -(Count x, Count y)
		{
			return new Count(x.Value - y.Value);
		}

		public static Count operator +(Count x, int y)
		{
			return new Count(x.Value + y);
		}

		public static Count operator -(Count x, int y)
		{
			return new Count(x.Value - y);
		}
		#endregion

		public static readonly Count None = default(Count);

		public Count(int value) : this()
		{
			Contract.Requires(value >= 0);

			Value = value;
		}

		public int Value { get; private set; }

		public override bool Equals(object obj)
		{
			return obj is Count && Equals((Count) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		public bool Equals(Count other)
		{
			return Value == other.Value;
		}
		
		public int CompareTo(Count other)
		{
			return Value.CompareTo(other.Value);
		}

		public string ToString(IFormatProvider format)
		{
			return Value.ToString(format);
		}
	}
}