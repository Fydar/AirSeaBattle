using System.Runtime.CompilerServices;

namespace CodeTest.Game.Math
{
	/// <summary>
	/// Represents a <see cref="Fixed"/> axis-aligned box.
	/// </summary>
	public struct FixedAABox
	{
		private enum Side
		{
			Top,
			Bottom,
			Left,
			Right
		}

		public FixedVector2 Center;

		public FixedVector2 Extents;

		public readonly FixedVector2 Max
		{
			get
			{
				return Center + Extents;
			}
		}

		public readonly FixedVector2 Min
		{
			get
			{
				return Center - Extents;
			}
		}

		public readonly Fixed Width
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Extents.X * 2;
			}
		}

		public readonly Fixed Height
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Extents.Y * 2;
			}
		}

		public FixedAABox(in FixedVector2 center, in FixedVector2 extents)
		{
			Center = center;
			Extents = extents;
		}

		public readonly bool Contains(in FixedVector2 point)
		{
			var min = Min;
			var max = Max;

			return min.X <= point.X
				&& max.X >= point.X
				&& min.Y <= point.Y
				&& max.Y >= point.Y;
		}

		public readonly bool Overlaps(in FixedAABox bounds)
		{
			var min = Min;
			var max = Max;
			var boundsMin = bounds.Min;
			var boundsMax = bounds.Max;

			return min.X <= boundsMax.X
				&& max.X >= boundsMin.X
				&& min.Y <= boundsMax.Y
				&& max.Y >= boundsMin.Y;
		}
	}
}
