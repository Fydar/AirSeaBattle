using CodeTest.Game.Math;
using RPGCore.Events;
using System;

namespace CodeTest.Game.Simulation.Models
{
	public class WorldEnemy
	{
		public event Action OnDestroyed;
		public Guid Identifier { get; }
		public World World { get; }
		public WorldEnemyTemplate Template { get; }
		public EventField<FixedVector2> Position { get; } = new();
		public EventField<Fixed> VelocityX { get; } = new();

		public FixedAABox Bounds => new(Position.Value, new(Template.Width / 2, Template.Height / 2));

		public WorldEnemy(World world, WorldEnemyTemplate template)
		{
			Identifier = Guid.NewGuid();
			World = world;
			Template = template;
		}

		public void InvokeOnDestroyed()
		{
			OnDestroyed?.Invoke();
		}
	}
}
