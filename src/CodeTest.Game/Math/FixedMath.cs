// Photon.Deterministic.FixedMath
using CodeTest.Game.Math.Internal;
using System;
using System.Runtime.InteropServices;

namespace CodeTest.Game.Math
{
	/// <summary>
	/// A collection of maths for <see cref="Fixed"/> decimal values.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct FixedMath
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Fixed Sign(in Fixed value)
		{
			if (value >= 0)
			{
				return Constants.One;
			}
			return Constants.MinusOne;
		}

		public static Fixed SignZero(in Fixed value)
		{
			if (value < 0)
			{
				return Constants.MinusOne;
			}
			if (value > 0)
			{
				return Constants.One;
			}
			return Constants.Zero;
		}

		public static int SignInt(in Fixed value)
		{
			if (value >= 0)
			{
				return 1;
			}
			return -1;
		}

		public static int SignZeroInt(in Fixed value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>
		/// Returns the absolute value of a number.
		/// </summary>
		/// <param name="value">A number.</param>
		/// <returns>The absolute value of <paramref name="value"/></returns>
		public static Fixed Abs(in Fixed value)
		{
			long mask = value.RawValue >> 63;
			return new Fixed((value.RawValue + mask) ^ mask);
		}

		/// <summary>
		/// Rounds a value to the nearest integer. If the value is midway between two integers then it is rounded up.
		/// </summary>
		/// <param name="value">A number to be rounded.</param>
		/// <returns>The integer <paramref name="value"/> to the nearest integer value.</returns>
		public static Fixed Round(in Fixed value)
		{
			long fractionalPart = value.RawValue & 0xFFFF;
			var integralPart = Floor(value);
			if (fractionalPart < 32768)
			{
				return integralPart;
			}
			if (fractionalPart > 32768)
			{
				return integralPart + Constants.One;
			}
			if ((integralPart.RawValue & Constants.One.RawValue) != 0L)
			{
				return integralPart + Constants.One;
			}
			return integralPart;
		}

		/// <summary>
		/// Rounds a value to the nearest integer. If the value is midway between two integers then it is rounded up.
		/// </summary>
		/// <param name="value">A number to be rounded.</param>
		/// <returns>The integer <paramref name="value"/> to the nearest integer value as an <see cref="int"/>.</returns>
		public static int RoundToInt(in Fixed value)
		{
			if ((value.RawValue & 0xFFFF) >= Constants.Half.RawValue)
			{
				return (int)((value.RawValue >> 16) + 1);
			}
			return (int)(value.RawValue >> 16);
		}

		/// <summary>
		/// Returns the largest integral value that is less than or equal to <paramref name="value"/>.
		/// </summary>
		/// <param name="value">A decimal number.</param>
		/// <returns>The largest integral value that is less than or equal to <paramref name="value"/>.</returns>
		/// <seealso cref="FloorToInt(in Fixed)"/>
		public static Fixed Floor(in Fixed value)
		{
			return new Fixed(value.RawValue & -65536L);
		}

		/// <summary>
		/// Returns the largest integral value that is less than or equal to <paramref name="value"/>.
		/// </summary>
		/// <param name="value">A decimal number.</param>
		/// <returns>The largest integral value that is less than or equal to <paramref name="value"/> as an <see cref="int"/>.</returns>
		/// <seealso cref="Floor(in Fixed)"/>
		public static int FloorToInt(in Fixed value)
		{
			return (int)(value.RawValue >> 16);
		}

		/// <summary>
		/// Returns the smallest integral value that is greater than or equal to <paramref name="value"/>.
		/// </summary>
		/// <param name="value">A decimal number.</param>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="value"/>.</returns>
		/// <seealso cref="CeilToInt(in Fixed)"/>
		public static Fixed Ceiling(in Fixed value)
		{
			if ((value.RawValue & 0xFFFF) != 0L)
			{
				return new Fixed((value.RawValue & -65536) + 65536);
			}
			return value;
		}

		/// <summary>
		/// Returns the smallest integral value that is greater than or equal to <paramref name="value"/>.
		/// </summary>
		/// <param name="value">A decimal number.</param>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="value"/> as an <see cref="int"/>.</returns>
		/// <seealso cref="Ceiling(in Fixed)"/>
		public static int CeilToInt(in Fixed value)
		{
			if ((value.RawValue & 0xFFFF) >= 1)
			{
				return (int)((value.RawValue >> 16) + 1);
			}
			return (int)(value.RawValue >> 16);
		}

		/// <summary>
		/// Returns the larger of two numbers.
		/// </summary>
		/// <param name="val1">The first of two numbers to compare.</param>
		/// <param name="val2">The second of two numbers to compare.</param>
		/// <returns>Either <paramref name="val1"/> or <paramref name="val2"/>, whichever is largest.</returns>
		public static Fixed Max(in Fixed val1, in Fixed val2)
		{
			return val1 <= val2 ? val2 : val1;
		}

		/// <summary>
		/// Returns the smaller of two numbers.
		/// </summary>
		/// <param name="val1">The first of two numbers to compare.</param>
		/// <param name="val2">The second of two numbers to compare.</param>
		/// <returns>Either <paramref name="val1"/> or <paramref name="val2"/>, whichever is smallest.</returns>
		public static Fixed Min(in Fixed val1, in Fixed val2)
		{
			return val1 >= val2 ? val2 : val1;
		}

		/// <summary>
		/// Returns the smaller of supplied numbers.
		/// </summary>
		/// <param name="values">The supplied numbers.</param>
		/// <returns>Whichever one of <paramref name="values"/> is the smallest.</returns>
		public static Fixed Min(params Fixed[] values)
		{
			var min = values[0];
			for (int i = 1; i < values.Length; i++)
			{
				min = Min(min, values[i]);
			}
			return min;
		}

		/// <summary>
		/// Returns the largest of supplied numbers.
		/// </summary>
		/// <param name="values">The supplied numbers.</param>
		/// <returns>Whichever one of <paramref name="values"/> is the largest.</returns>
		public static Fixed Max(params Fixed[] values)
		{
			var max = values[0];
			for (int i = 1; i < values.Length; i++)
			{
				max = Max(max, values[i]);
			}
			return max;
		}

		/// <summary>
		/// Ensures the values are in order of minimim-to-maximum.
		/// </summary>
		/// <param name="a">The first of the two values to sort.</param>
		/// <param name="b">The second of the two values to sort.</param>
		/// <param name="min">Either <paramref name="a"/> or <paramref name="b"/>, whichever is smallest.</param>
		/// <param name="max">Either <paramref name="a"/> or <paramref name="b"/>, whichever is largest.</param>
		public static void MinMax(in Fixed a, in Fixed b, out Fixed min, out Fixed max)
		{
			if (a < b)
			{
				min = a;
				max = b;
			}
			else
			{
				min = b;
				max = a;
			}
		}

		public static Fixed Clamp(in Fixed value, in Fixed min, in Fixed max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		public static Fixed Clamp01(in Fixed value)
		{
			if (value < 0)
			{
				return 0;
			}
			if (value > 1)
			{
				return 1;
			}
			return value;
		}

		public static Fixed ClampSafe(in Fixed value)
		{
			long safeRawMin = int.MinValue;
			if (value.RawValue < safeRawMin)
			{
				return Fixed.FromRaw(safeRawMin);
			}
			long safeRawMax = int.MaxValue;
			if (value.RawValue > safeRawMax)
			{
				return Fixed.FromRaw(safeRawMax);
			}
			return value;
		}

		public static Fixed Fraction(in Fixed value)
		{
			return new Fixed(value.RawValue & 65535L);
		}

		public static Fixed Repeat(in Fixed t, in Fixed length)
		{
			return new Fixed(RepeatRaw(t.RawValue, length.RawValue));
		}

		public static Fixed LerpRadians(in Fixed a, in Fixed b, in Fixed time)
		{
			var i = Repeat(b - a, Constants.PiTimes2);
			if (i > Constants.Pi)
			{
				i -= Constants.PiTimes2;
			}

			return a + (i * Clamp01(time));
		}

		public static Fixed Lerp(in Fixed start, in Fixed end, in Fixed time)
		{
			return start + ((end - start) * Clamp01(time));
		}

		public static Fixed LerpUnclamped(in Fixed start, in Fixed end, in Fixed time)
		{
			return start + ((end - start) * time);
		}

		public static Fixed InverseLerp(in Fixed min, in Fixed max, in Fixed value)
		{
			return (value - min) / (max - min);
		}

		public static Fixed SmoothStep(in Fixed start, in Fixed end, in Fixed time)
		{
			return Hermite(start, Constants.Zero, end, Constants.Zero, Clamp01(time));
		}

		public static Fixed Sqrt(in Fixed x)
		{
			long absolute = x.RawValue < 0 ? -x.RawValue : x.RawValue;
			if (absolute == 0L)
			{
				return 0L;
			}
			bool isLessThanOne = absolute < 65536;
			long iterationValue = absolute;
			if (isLessThanOne)
			{
				iterationValue <<= 16;
			}

			int timedReduced = 0;
			while (iterationValue > 262144)
			{
				timedReduced++;
				iterationValue >>= 2;
			}

			long lutIndex = (iterationValue - 65536) / 3;
			if (lutIndex >= 65536)
			{
				lutIndex = 65535L;
			}
			else if (lutIndex < 0)
			{
				lutIndex = 0L;
			}

			iterationValue = MathEngine.sqrt[lutIndex] << timedReduced;
			if (isLessThanOne)
			{
				iterationValue >>= 8;
			}
			return new Fixed(iterationValue);
		}

		public static Fixed Barycentric(in Fixed value1, in Fixed value2, in Fixed value3, in Fixed amount1, in Fixed amount2)
		{
			return value1
				+ ((value2 - value1) * amount1)
				+ ((value3 - value1) * amount2);
		}

		public static Fixed CatmullRom(in Fixed value1, in Fixed value2, in Fixed value3, in Fixed value4, in Fixed amount)
		{
			var squared = amount * amount;
			var cubed = squared * amount;

			return Constants.Half
				* ((Constants.Two * value2)
				+ ((value3 - value1) * amount)
				+ (((Constants.Two * value1) - (Constants.Five * value2) + Constants.Four * value3 - value4) * squared)
				+ (((Constants.Three * value2) - value1 - (Constants.Three * value3) + value4) * cubed));
		}

		public static Fixed Hermite(in Fixed value1, in Fixed tangent1, in Fixed value2, in Fixed tangent2, in Fixed amount)
		{
			var squared = amount * amount;
			var cubed = squared * amount;

			if (amount == Constants.Zero)
			{
				return value1;
			}
			return (((Constants.Two * value1) - Constants.Two * value2 + tangent2 + tangent1) * cubed)
				+ (((Constants.Three * value2) - (Constants.Three * value1) - (Constants.Two * tangent1) - tangent2) * squared)
				+ (tangent1 * amount) + value1;
		}

		/// <summary>
		/// Returns the sine of the specified angle.
		/// </summary>
		/// <param name="a">An angle, measured in radians.</param>
		/// <returns>The sine of <paramref name="a"/>.</returns>
		public static Fixed Sin(in Fixed a)
		{
			long length = MathEngine.sin.Length - 1;
			long halfLength = length / 2;

			long index = a.RawValue % length;

			if (index < -halfLength)
			{
				index = -halfLength - index;
			}
			else if (a.RawValue > halfLength)
			{
				index = halfLength - index;
			}
			return new Fixed(MathEngine.sin[index + halfLength]);
		}

		/// <summary>
		/// Returns the cosine of the specified angle.
		/// </summary>
		/// <param name="d">An angle, measured in radians.</param>
		/// <returns>The cosine of <paramref name="d"/>.</returns>
		public static Fixed Cos(in Fixed d)
		{
			return Sin(d + Constants.PiOver2);
		}

		/// <summary>
		/// Returns the tangent of the specified angle.
		/// </summary>
		/// <param name="a">An angle, measured in radians.</param>
		/// <returns>The tangent of <paramref name="a"/>.</returns>
		public static Fixed Tan(in Fixed a)
		{
			long index = a.RawValue;
			if (index < -205887)
			{
				index %= -205887L;
			}
			if (index > 205887)
			{
				index %= 205887L;
			}
			return new Fixed(MathEngine.tan[index + 205887]);
		}

		/// <summary>
		/// Returns the angle whose sine is the specified number.
		/// </summary>
		/// <param name="d">A number representing a sine, where d must be greater than or equal to -1, but less than or equal to 1.
		/// </param>
		/// <returns></returns>
		public static Fixed Asin(in Fixed d)
		{
			if (d < -1 || d > 1)
			{
				return Constants.MinValue;
			}
			return new Fixed(MathEngine.asin[d.RawValue + 65536]);
		}

		/// <summary>
		/// Returns the angle whose cosine is the specified number.
		/// </summary>
		/// <param name="d">A number representing a cosine, where d must be greater than or equal to -1, but less than or equal to 1.
		/// </param>
		/// <returns>An angle, θ, measured in radians.</returns>
		public static Fixed Acos(in Fixed d)
		{
			if (d < -1 || d > 1)
			{
				return Constants.MinValue;
			}
			return new Fixed(MathEngine.acos[d.RawValue + 65536]);
		}

		/// <summary>
		/// Returns the angle whose tangent is the specified number.
		/// </summary>
		/// <param name="d">A number representing a tangent.</param>
		/// <returns>An angle, θ, measured in radians.</returns>
		public static Fixed Atan(in Fixed d)
		{
			long mask = d.RawValue >> 63;
			long absoluteValue = (d.RawValue + mask) ^ mask;

			long index;
			if (absoluteValue <= 262144L)
			{
				index = absoluteValue;
			}
			else if (absoluteValue <= 1310720L)
			{
				index = 262144L + ((absoluteValue - 262144L) >> 8);
			}
			else if (absoluteValue <= 68419584L)
			{
				index = 266240L + ((absoluteValue - 1310720L) >> 20);
			}
			else
			{
				index = MathEngine.atan.Length - 1;
			}
			return new Fixed((MathEngine.atan[index] + mask) ^ mask);
		}

		/// <remarks>
		/// Implementation based off of Clay. S. Turner's fast binary logarithm
		/// </remarks>
		public static Fixed Log2(in Fixed x)
		{
			if (x <= 0)
			{
				throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", nameof(x));
			}

			long b = 1U << 15;
			long y = 0;

			long rawX = x.RawValue;
			while (rawX < Constants.One.RawValue)
			{
				rawX <<= 1;
				y -= Constants.One.RawValue;
			}

			while (rawX >= (Constants.One.RawValue << 1))
			{
				rawX >>= 1;
				y += Constants.One.RawValue;
			}

			var z = Fixed.FromRaw(rawX);

			for (int i = 0; i < 16; i++)
			{
				z *= z;
				if (z.RawValue >= (Constants.One.RawValue << 1))
				{
					z = Fixed.FromRaw(z.RawValue >> 1);
					y += b;
				}
				b >>= 1;
			}

			return Fixed.FromRaw(y);
		}

		/// <summary>
		/// Returns the angle whose tangent is the quotient of two specified numbers.
		/// </summary>
		/// <param name="y">The <paramref name="y"/> coordinate of a point.</param>
		/// <param name="x">The <paramref name="x"/> coordinate of a point.</param>
		/// <returns>An angle, θ, measured in radians.</returns>
		public static Fixed Atan2(in Fixed y, in Fixed x)
		{
			if (x > 0)
			{
				return Atan(y / x);
			}
			if (x < 0)
			{
				if (y >= 0)
				{
					return Atan(y / x) + Constants.Pi;
				}
				else
				{
					return Atan(y / x) - Constants.Pi;
				}
			}
			if (y > 0)
			{
				return Constants.PiOver2;
			}
			if (y == 0)
			{
				return Constants.Zero;
			}
			return -Constants.PiOver2;
		}

		public static int NextPowerOfTwo(in int value)
		{
			if (value <= 0)
			{
				throw new ArgumentException("must be positive", nameof(value));
			}
			int v1 = value - 1;
			int v2 = v1 | (int)((uint)v1 >> 1);
			int v3 = v2 | (int)((uint)v2 >> 2);
			int v4 = v3 | (int)((uint)v3 >> 4);
			int v5 = v4 | (int)((uint)v4 >> 8);
			return (int)(((uint)v5 | ((uint)v5 >> 16)) + 1);
		}

		internal static long RepeatRaw(in long t, in long length)
		{
			return t - (FloorRaw((t << 16) / length) * length >> 16);
		}

		internal static long FloorRaw(in long value)
		{
			return value & -65536;
		}

		internal static int Clamp(in int value, in int min, in int max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		internal static long Clamp(in long value, in long min, in long max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}
	}
}
