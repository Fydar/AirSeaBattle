using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;
using System.Linq;

namespace CodeTest.Game.Simulation.Systems.EnemyBehaviour
{
	public class EnemyBehaviourSystem : IWorldSystem
	{
		private readonly World world;

		public EnemyBehaviourSystem(World world)
		{
			this.world = world;
		}

		/// <inheritdoc/>
		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
		}

		/// <inheritdoc/>
		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
		}

		/// <inheritdoc/>
		public void OnUpdate(UpdateParameters parameters)
		{
			foreach (var enemyKvp in world.Enemies.ToList())
			{
				var enemy = enemyKvp.Value;

				enemy.Position.Value += FixedVector2.Right * enemy.VelocityX.Value * parameters.DeltaTime;

				// Has the enemy left the bounds of the world
				if (!world.Bounds.Overlaps(enemy.Bounds))
				{
					enemy.Position.Value = new FixedVector2(-enemy.Template.Width / 2, enemy.Position.Value.Y);
				}
			}
		}
	}
}
