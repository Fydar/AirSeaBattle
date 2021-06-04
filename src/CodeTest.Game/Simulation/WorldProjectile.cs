using CodeTest.Game.Math;
using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation
{
	public class WorldProjectile
	{
		public Guid Identifier { get; }
		public World World { get; }

		public EventField<FixedVector2> Position { get; } = new();
		public EventField<FixedVector2> Velocity { get; } = new();

		public WorldProjectile(World world)
		{
			Identifier = Guid.NewGuid();
			World = world;
		}
	}
}
