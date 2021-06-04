using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using System.Linq;

namespace CodeTest.Game.Simulation.Systems.EnemyBehaviour
{
	public class EnemyBehaviourSystem : IWorldSystem
	{
		private readonly World world;
		private readonly EnemyBehaviourConfiguration configuration;

		public EnemyBehaviourSystem(World world, EnemyBehaviourConfiguration configuration)
		{
			this.world = world;
			this.configuration = configuration;
		}

		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
		}

		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
		}

		public void OnUpdate(UpdateParameters parameters)
		{
			foreach (var enemyKvp in world.Enemies.ToList())
			{
				var enemy = enemyKvp.Value;

				enemy.Position.Value += FixedVector2.Right * enemy.VelocityX.Value * parameters.DeltaTime;

				// Has the enemy left the bounds of the world
				if (!world.Bounds.Overlaps(enemy.Bounds))
				{
					world.Enemies.Remove(enemy.Identifier);
				}
			}
		}
	}
}
