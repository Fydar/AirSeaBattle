using CodeTest.Game.Control;
using CodeTest.Game.Math;
using CodeTest.Game.Simulation.Models;

namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	public class PlayerControlSystem : IWorldSystem
	{
		private readonly World world;
		private readonly PlayerControlConfiguration configuration;

		public PlayerControlSystem(World world, PlayerControlConfiguration configuration)
		{
			this.world = world;
			this.configuration = configuration;
		}

		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
			// give the player a gun in the world
			var gun = new WorldGun(world, configuration.DefaultPosition);

			gun.PositionX.Value = world.Guns.Count == 0 ? ((Fixed)1) / 4 : ((Fixed)3) / 4;

			world.Guns.Add(gun.Identifier, gun);
			worldPlayer.ControlledGuns.Add(gun.Identifier, gun);
		}

		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
			foreach (var gunKvp in worldPlayer.ControlledGuns)
			{
				world.Guns.Remove(gunKvp.Key);
			}
		}

		public void OnUpdate(UpdateParameters parameters)
		{
			if (world.IsGameOver)
			{
				return;
			}

			foreach (var playerKvp in world.Players)
			{
				var player = playerKvp.Value;

				WorldGunPosition targetAngle;
				if (player.Input.Down.IsDown)
				{
					targetAngle = configuration.DownPosition;
				}
				else if (player.Input.Up.IsDown)
				{
					targetAngle = configuration.UpPosition;
				}
				else
				{
					targetAngle = configuration.DefaultPosition;
				}

				foreach (var gun in player.ControlledGuns)
				{
					if (gun.Value.Angle.Value != targetAngle)
					{
						gun.Value.Angle.Value = targetAngle;
					}

					if (player.Input.Fire.CurrentState == InputButtonState.Pressed)
					{
						// Limit the amount of projectiles in the game.
						// Don't create new projectiles if there are too many.
						if (world.Projectiles.Count >= world.MaximumProjectiles)
						{
							continue;
						}

						var velocityVector = FixedVector2.Rotate(FixedVector2.Right, gun.Value.Angle.Value.Inclination * Constants.Deg2Rad);

						var projectilePosition = gun.Value.Bounds.NormalizedToPoint(gun.Value.Angle.Value.BulletOffset);

						var projectile = new WorldProjectile(world);
						projectile.Position.Value = projectilePosition;
						projectile.Velocity.Value = velocityVector * configuration.BulletSpeed;

						world.Projectiles.Add(projectile.Identifier, projectile);
					}
				}
			}
		}
	}
}
