using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;

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
			foreach (var enemyKvp in world.Enemies)
			{
				var enemy = enemyKvp.Value;

				enemy.Position.Value += FixedVector2.Right * enemy.VelocityX.Value * parameters.DeltaTime;
			}
		}
	}
}
