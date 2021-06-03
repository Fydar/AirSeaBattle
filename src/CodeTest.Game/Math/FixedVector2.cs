// Photon.Deterministic.FixedVector2
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CodeTest.Game.Math
{
	/// <summary>
	/// Represents a <see cref="Fixed"/> 2-value vector.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct FixedVector2 : IEquatable<FixedVector2>
	{
		public class EqualityComparer : IEqualityComparer<FixedVector2>
		{
			public static readonly EqualityComparer Instance = new();

			private EqualityComparer()
			{
			}

			public bool Equals(FixedVector2 x, FixedVector2 y)
			{
				return x == y;
			}

			public int GetHashCode(FixedVector2 obj)
			{
				return obj.GetHashCode();
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 Zero = new(0, 0);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 One = new(1, 1);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 Right = new(1, 0);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 Left = new(-1, 0);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 Up = new(0, 1);

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static readonly FixedVector2 Down = new(0, -1);

		public readonly Fixed X;
		public readonly Fixed Y;

		public readonly Fixed Magnitude
		{
			
			get
			{
				return FixedMath.Sqrt(SqrMagnitude);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public readonly Fixed SqrMagnitude
		{
			
			get
			{
				return (X * X) + (Y * Y);
			}
		}

		public readonly FixedVector2 Normalized
		{
			
			get
			{
				return Normalize(this);
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string DebuggerDisplay
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", X.AsFloat, Y.AsFloat);
			}
		}

		public FixedVector2(in int x, in int y)
		{
			X = x;
			Y = y;
		}

		public FixedVector2(in Fixed x, in Fixed y)
		{
			X = x;
			Y = y;
		}

		public FixedVector2(in Fixed value)
		{
			X = value;
			Y = value;
		}

		public readonly bool IsRightOf(in FixedVector2 vector)
		{
			return Dot(RotateRight(vector), this) > 0;
		}

		public readonly bool IsLeftOf(in FixedVector2 vector)
		{
			return Dot(RotateLeft(vector), this) > 0;
		}

		public readonly bool Equals(FixedVector2 other)
		{
			return this == other;
		}

		/// <inheritdoc/>
		public override readonly bool Equals(object obj)
		{
			return obj is FixedVector2 vector && this == vector;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int hashCode = 1861411795;
			hashCode = hashCode * -1521134295 + X.GetHashCode();
			hashCode = hashCode * -1521134295 + Y.GetHashCode();
			return hashCode;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}, {1})", X, Y);
		}

		public static Fixed Distance(in FixedVector2 a, in FixedVector2 b)
		{
			return (a - b).Magnitude;
		}

		public static Fixed DistanceSquared(in FixedVector2 a, in FixedVector2 b)
		{
			return (a - b).SqrMagnitude;
		}

		public static Fixed Dot(in FixedVector2 a, in FixedVector2 b)
		{
			return a.X * b.X + a.Y * b.Y;
		}

		public static FixedVector2 ClampMagnitude(in FixedVector2 vector, in Fixed length)
		{
			var normalised = Normalize(vector);
			return new FixedVector2(
				normalised.X * length,
				normalised.Y * length
			);
		}

		public static void Rotate(FixedVector2[] vectors, in Fixed rotation)
		{
			for (int i = 0; i < vectors.Length; i++)
			{
				vectors[i] = Rotate(vectors[i], rotation);
			}
		}

		public static FixedVector2 Rotate(in FixedVector2 vector, in Fixed rotation)
		{
			var cs = FixedMath.Cos(rotation);
			var sn = FixedMath.Sin(rotation);
			var px = (vector.X * cs) - (vector.Y * sn);
			var pz = (vector.X * sn) + (vector.Y * cs);

			return new FixedVector2(px, pz);
		}

		public static FixedVector2 Normalize(in FixedVector2 v)
		{
			return Normalize(v, out _);
		}

		public static FixedVector2 Normalize(in FixedVector2 v, out Fixed magnitude)
		{
			magnitude = v.Magnitude;

			if (magnitude <= 0)
			{
				return default;
			}

			return new FixedVector2(
				v.X / magnitude,
				v.Y / magnitude
			);
		}

		public static Fixed Cross(in FixedVector2 a, in FixedVector2 b)
		{
			return (a.X * b.Y) - (a.Y * b.X);
		}

		public static FixedVector2 Cross(in Fixed a, in FixedVector2 b)
		{
			return new FixedVector2(-a * b.Y, a * b.X);
		}

		public static FixedVector2 Cross(in FixedVector2 b, in Fixed a)
		{
			return new FixedVector2(-a * b.Y, a * b.X);
		}

		public static FixedVector2 Reflect(in FixedVector2 vector, in FixedVector2 normal)
		{
			var dot = Dot(vector, normal);

			return new FixedVector2(
				vector.X - 2 * dot * normal.X,
				vector.Y - 2 * dot * normal.Y
			);
		}

		public static FixedVector2 Clamp(in FixedVector2 vector, in FixedVector2 min, in FixedVector2 max)
		{
			return new FixedVector2(
				FixedMath.Clamp(vector.X, min.X, max.X),
				FixedMath.Clamp(vector.Y, min.Y, max.Y));
		}

		public static FixedVector2 MoveTowards(in FixedVector2 from, in FixedVector2 to, in Fixed maxDelta)
		{
			var vector = to - from;
			var magnitude = vector.Magnitude;
			if (magnitude <= maxDelta || magnitude == 0)
			{
				return to;
			}
			return from + vector / magnitude * maxDelta;
		}

		public static FixedVector2 Lerp(in FixedVector2 start, in FixedVector2 end, in Fixed time)
		{
			var clampedTime = FixedMath.Clamp01(time);
			return new FixedVector2(
				FixedMath.LerpUnclamped(start.X, end.X, clampedTime),
				FixedMath.LerpUnclamped(start.Y, end.Y, clampedTime));
		}

		public static FixedVector2 LerpUnclamped(in FixedVector2 start, in FixedVector2 end, in Fixed time)
		{
			return new FixedVector2(
				FixedMath.LerpUnclamped(start.X, end.X, time),
				FixedMath.LerpUnclamped(start.Y, end.Y, time));
		}

		public static FixedVector2 Max(in FixedVector2 value1, in FixedVector2 value2)
		{
			return new FixedVector2(
				FixedMath.Max(value1.X, value2.X),
				FixedMath.Max(value1.Y, value2.Y));
		}

		public static FixedVector2 Max(params FixedVector2[] values)
		{
			if (values == null || values.Length == 0)
			{
				return default;
			}
			var max = values[0];
			for (int i = 1; i < values.Length; i++)
			{
				max = Max(max, values[i]);
			}
			return max;
		}

		public static FixedVector2 Min(in FixedVector2 value1, in FixedVector2 value2)
		{
			return new FixedVector2(
				FixedMath.Min(value1.X, value2.X),
				FixedMath.Min(value1.Y, value2.Y));
		}

		public static FixedVector2 Min(params FixedVector2[] values)
		{
			if (values == null || values.Length == 0)
			{
				return default;
			}
			var min = values[0];
			for (int i = 1; i < values.Length; i++)
			{
				min = Min(min, values[i]);
			}
			return min;
		}

		public static FixedVector2 Scale(in FixedVector2 vector, in FixedVector2 scaler)
		{
			return new FixedVector2(
				vector.X * scaler.X,
				vector.Y * scaler.Y);
		}

		public static FixedVector2 Scale(in FixedVector2 vector, in Fixed scaler)
		{
			return new FixedVector2(
				vector.X * scaler,
				vector.Y * scaler);
		}

		public static Fixed Angle(in FixedVector2 a, in FixedVector2 b)
		{
			return FixedMath.Acos(Dot(a.Normalized, b.Normalized)) * Constants.Rad2Deg;
		}

		public static FixedVector2 RotateRight(in FixedVector2 vector)
		{
			return new FixedVector2(vector.Y, -vector.X);
		}

		public static FixedVector2 RotateLeft(in FixedVector2 vector)
		{
			return new FixedVector2(-vector.Y, vector.X);
		}

		public static Fixed Radians(in FixedVector2 from, in FixedVector2 to)
		{
			return NormalRadians(Normalize(from), Normalize(to));
		}

		public static Fixed NormalRadians(in FixedVector2 from, in FixedVector2 to)
		{
			return FixedMath.Acos(FixedMath.Clamp(Dot(from, to), -1, 1));
		}

		public static Fixed RadiansSigned(in FixedVector2 v1, in FixedVector2 v2)
		{
			var radians = Radians(v1, v2);
			var sign = FixedMath.Sign(v1.X * v2.Y - v1.Y * v2.X);
			return radians * sign;
		}

		public static Fixed RadiansSignedSkipNormalize(in FixedVector2 v1, in FixedVector2 v2)
		{
			var radians = NormalRadians(v1, v2);
			var sign = FixedMath.Sign(v1.X * v2.Y - v1.Y * v2.X);
			return radians * sign;
		}

		public static FixedVector2 SmoothStep(in FixedVector2 start, in FixedVector2 end, in Fixed time)
		{
			return new FixedVector2(
				FixedMath.SmoothStep(start.X, end.X, time),
				FixedMath.SmoothStep(start.Y, end.Y, time)
			);
		}

		public static FixedVector2 Hermite(in FixedVector2 value1, in FixedVector2 tangent1, in FixedVector2 value2, in FixedVector2 tangent2, in Fixed amount)
		{
			return new FixedVector2(
				FixedMath.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
				FixedMath.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount)
			);
		}

		public static FixedVector2 Barycentric(in FixedVector2 value1, in FixedVector2 value2, in FixedVector2 value3, in Fixed amount1, in Fixed amount2)
		{
			return new FixedVector2(
				FixedMath.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
				FixedMath.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2)
			);
		}

		public static FixedVector2 CatmullRom(in FixedVector2 value1, in FixedVector2 value2, in FixedVector2 value3, in FixedVector2 value4, in Fixed amount)
		{
			return new FixedVector2(
				FixedMath.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
				FixedMath.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount)
			);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator +(in FixedVector2 vector)
		{
			return new FixedVector2(vector.X, vector.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator -(in FixedVector2 vector)
		{
			return new FixedVector2(-vector.X, -vector.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator +(in FixedVector2 a, in FixedVector2 b)
		{
			return new FixedVector2(a.X + b.X, a.Y + b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator -(in FixedVector2 a, in FixedVector2 b)
		{
			return new FixedVector2(a.X - b.X, a.Y - b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator *(in FixedVector2 a, in FixedVector2 b)
		{
			return new FixedVector2(a.X * b.X, a.Y * b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator *(in FixedVector2 a, in Fixed b)
		{
			return new FixedVector2(a.X * b, a.Y * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator *(in Fixed a, in FixedVector2 b)
		{
			return new FixedVector2(a * b.X, a * b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator *(in FixedVector2 a, in int b)
		{
			return new FixedVector2(a.X * b, a.Y * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator *(in int a, in FixedVector2 b)
		{
			return new FixedVector2(a * b.X, a * b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator /(in FixedVector2 a, in FixedVector2 b)
		{
			return new FixedVector2(a.X / b.X, a.Y / b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator /(in FixedVector2 a, in Fixed b)
		{
			return new FixedVector2(a.X / b, a.Y / b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator /(in Fixed a, in FixedVector2 b)
		{
			return new FixedVector2(a / b.X, a / b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator /(in FixedVector2 a, in int b)
		{
			return new FixedVector2(a.X / b, a.Y / b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FixedVector2 operator /(in int a, in FixedVector2 b)
		{
			return new FixedVector2(a / b.X, a / b.Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(in FixedVector2 a, in FixedVector2 b)
		{
			return a.X.RawValue == b.X.RawValue
				&& a.Y.RawValue == b.Y.RawValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(in FixedVector2 a, in FixedVector2 b)
		{
			return a.X.RawValue != b.X.RawValue
				|| a.Y.RawValue != b.Y.RawValue;
		}
	}
}
