using CodeTest.Game.Math;
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

		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
		}

		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
		}

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

					var enemyBox = new FixedAABox(enemy.Position.Value,
						new FixedVector2(
							enemy.Template.Width,
							enemy.Template.Height
						)
					);

					if (enemyBox.Contains(projectile.Position.Value))
					{
						collided = true;
						world.Enemies.Remove(enemy.Identifier);
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
