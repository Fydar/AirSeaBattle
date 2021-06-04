using CodeTest.Game.Math;
using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation
{
	public class WorldEnemy
	{
		public Guid Identifier { get; }
		public World World { get; }
		public WorldEnemyTemplate Template { get; }

		public EventField<FixedVector2> Position { get; } = new();
		public EventField<Fixed> VelocityX { get; } = new();

		public WorldEnemy(World world, WorldEnemyTemplate template)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Template = template;
		}
	}
}
