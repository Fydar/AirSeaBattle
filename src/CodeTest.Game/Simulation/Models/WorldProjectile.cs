using CodeTest.Game.Math;
using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation.Models
{
	public class WorldProjectile
	{
		public Guid Identifier { get; }

		public WorldGun Owner { get; }
		public EventField<FixedVector2> Position { get; } = new();
		public EventField<FixedVector2> Velocity { get; } = new();

		public World World => Owner.World;

		public WorldProjectile(WorldGun owner)
		{
			Identifier = Guid.NewGuid();
			Owner = owner;
		}
	}
}
