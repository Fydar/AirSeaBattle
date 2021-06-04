using CodeTest.Game.Math;
using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation
{
	public class WorldGun
	{
		public World World { get; }
		public Guid Identifier { get; }
		public EventField<WorldGunPosition> Angle { get; } = new();
		public EventField<Fixed> PositionX { get; } = new();
		public EventField<bool> IsFlipped { get; } = new();

		public FixedVector2 Position
		{
			get
			{
				return new FixedVector2(PositionX.Value * World.Width, World.GunHeightPercent * World.Height);
			}
		}

		public WorldGun(World world, WorldGunPosition angle)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Angle.Value = angle;
		}
	}
}
