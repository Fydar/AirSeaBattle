using CodeTest.Game.Simulation.Models;
using System.Linq;

namespace CodeTest.Game.Simulation.Systems.ProjectileMovement
{
	public class ProjectileMovementSystem : IWorldSystem
	{
		private readonly World world;

		public ProjectileMovementSystem(World world)
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
			foreach (var projectileKvp in world.Projectiles.ToList())
			{
				var projectile = projectileKvp.Value;

				projectile.Position.Value += projectile.Velocity.Value * parameters.DeltaTime;

				if (!world.Bounds.Contains(projectile.Position.Value))
				{
					world.Projectiles.Remove(projectile.Identifier);
					continue;
				}

				bool collided = false;

				foreach (var enemyKvp in world.Enemies)
				{
					var enemy = enemyKvp.Value;

					if (enemy.Bounds.Contains(projectile.Position.Value))
					{
						collided = true;
						enemy.InvokeOnDestroyed();
						world.Enemies.Remove(enemy.Identifier);

						projectile.Owner.Player.CurrentScore.Value += world.Configuration.PointsPerPlane;

						projectile.Owner.Player.Player.Highscore.Value = 
							System.Math.Max(
								projectile.Owner.Player.Player.Highscore.Value,
								projectile.Owner.Player.CurrentScore.Value);
						break;
					}
				}

				if (collided)
				{
					world.Projectiles.Remove(projectile.Identifier);
					continue;
				}
			}
		}
	}
}
