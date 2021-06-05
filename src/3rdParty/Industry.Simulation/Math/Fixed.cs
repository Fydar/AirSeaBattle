using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Industry.Simulation.Math
{
	/// <summary>
	/// A deterministic, fixed-point decimal value.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct Fixed : IEquatable<Fixed>, IComparable<Fixed>
	{
		public class Comparer : IComparer<Fixed>
		{
			public static Comparer Instance { get; } = new();

			private Comparer()
			{
			}

			public int Compare(Fixed x, Fixed y)
			{
				return x.RawValue.CompareTo(y.RawValue);
			}
		}

		public class EqualityComparer : IEqualityComparer<Fixed>
		{
			public static EqualityComparer Instance { get; } = new();

			private EqualityComparer()
			{
			}

			/// <inheritdoc/>
			public bool Equals(Fixed x, Fixed y)
			{
				return x.RawValue == y.RawValue;
			}

			/// <inheritdoc/>
			public int GetHashCode(Fixed obj)
			{
				return obj.GetHashCode();
			}
		}

		public readonly long RawValue;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int AsInt
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)(RawValue >> 16);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public long AsLong
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return RawValue >> 16;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public float AsFloat
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return RawValue / 65536f;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public double AsDouble
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return RawValue / 65536.0;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return AsFloat.ToString("###,##0.0000", CultureInfo.InvariantCulture);
			}
		}

		internal Fixed(in long rawValue)
		{
			RawValue = rawValue;
		}

		public int CompareTo(Fixed other)
		{
			return RawValue.CompareTo(other.RawValue);
		}

		public bool Equals(Fixed other)
		{
			return RawValue == other.RawValue;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return -55426706 + RawValue.GetHashCode();
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			return obj is Fixed fixedObj && RawValue == fixedObj.RawValue;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return AsFloat.ToString("###,##0.0000", CultureInfo.InvariantCulture);
		}

		public static Fixed FromFloat(in float value)
		{
			return new Fixed(checked((long)(value * 65536.0f)));
		}

		public static Fixed FromDouble(in double value)
		{
			return new Fixed(checked((long)(value * 65536.0)));
		}

		public static Fixed FromRaw(in long rawValue)
		{
			return new Fixed(rawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator +(in Fixed a)
		{
			return new Fixed(a.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator -(in Fixed a)
		{
			return new Fixed(-a.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator +(in Fixed a, in Fixed b)
		{
			return new Fixed(a.RawValue + b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator +(in Fixed a, in int b)
		{
			long bVal = (long)b << 16;
			return new Fixed(a.RawValue + bVal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator +(in int a, in Fixed b)
		{
			long aVal = (long)a << 16;
			return new Fixed(aVal + b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator -(in Fixed a, in Fixed b)
		{
			return new Fixed(a.RawValue - b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator -(in Fixed a, in int b)
		{
			long bVal = (long)b << 16;
			return new Fixed(a.RawValue - bVal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator -(in int a, in Fixed b)
		{
			long aVal = (long)a << 16;
			return new Fixed(aVal + b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator *(in Fixed a, in Fixed b)
		{
			return new Fixed((a.RawValue * b.RawValue) >> 16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator *(in Fixed a, in int b)
		{
			return new Fixed(a.RawValue * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator *(in int a, in Fixed b)
		{
			return new Fixed(a * b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator /(in Fixed a, in Fixed b)
		{
			return new Fixed((a.RawValue << 16) / b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator /(in Fixed a, in int b)
		{
			return new Fixed(a.RawValue / b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator /(in int a, in Fixed b)
		{
			long aVal = (long)a << 32;
			return new Fixed(aVal / b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator %(in Fixed a, in Fixed b)
		{
			return new Fixed(a.RawValue % b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator %(in Fixed a, in int b)
		{
			long bVal = (long)b << 16;
			return new Fixed(a.RawValue % bVal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Fixed operator %(in int a, in Fixed b)
		{
			long aVal = ((long)a << 16);
			return new Fixed(aVal % b.RawValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(in Fixed a, in Fixed b)
		{
			return a.RawValue < b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(in Fixed a, in int b)
		{
			return a.RawValue < (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(in int a, in Fixed b)
		{
			return (long)a << 16 < b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(in Fixed a, in Fixed b)
		{
			return a.RawValue <= b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(in Fixed a, in int b)
		{
			return a.RawValue <= (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(in int a, in Fixed b)
		{
			return (long)a << 16 <= b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(in Fixed a, in Fixed b)
		{
			return a.RawValue > b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(in Fixed a, in int b)
		{
			return a.RawValue > (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(in int a, in Fixed b)
		{
			return (long)a << 16 > b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(in Fixed a, in Fixed b)
		{
			return a.RawValue >= b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(in Fixed a, in int b)
		{
			return a.RawValue >= (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(in int a, in Fixed b)
		{
			return (long)a << 16 >= b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Fixed a, in Fixed b)
		{
			return a.RawValue == b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in Fixed a, in int b)
		{
			return a.RawValue == (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in int a, in Fixed b)
		{
			return (long)a << 16 == b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Fixed a, in Fixed b)
		{
			return a.RawValue != b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in Fixed a, in int b)
		{
			return a.RawValue != (long)b << 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in int a, in Fixed b)
		{
			return (long)a << 16 != b.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Fixed(in int value)
		{
			return new Fixed((long)value << 16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Fixed(in long value)
		{
			return new Fixed(value << 16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int(in Fixed value)
		{
			return (int)(value.RawValue >> 16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator long(in Fixed value)
		{
			return value.RawValue >> 16;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float(in Fixed value)
		{
			return value.RawValue / 65536f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double(in Fixed value)
		{
			return value.RawValue / 65536.0;
		}
	}
}