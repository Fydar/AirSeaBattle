using AirSeaBattle.Game.Simulation.Models;
using Industry.Simulation.Math;
using System.Linq;

namespace AirSeaBattle.Game.Simulation.Systems.EnemyBehaviour
{
	/// <summary>
	/// The system responcible for moving enemies.
	/// </summary>
	public class EnemyBehaviourSystem : IWorldSystem
	{
		private readonly World world;

		internal EnemyBehaviourSystem(World world)
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
			foreach (var enemyKvp in world.Enemies)
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
