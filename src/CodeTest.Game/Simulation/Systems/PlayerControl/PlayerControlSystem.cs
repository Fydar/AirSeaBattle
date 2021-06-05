using CodeTest.Game.Control;
using Industry.Simulation.Math;
using CodeTest.Game.Simulation.Models;

namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	public class PlayerControlSystem : IWorldSystem
	{
		private readonly World world;

		public PlayerControlSystem(World world)
		{
			this.world = world;
		}

		/// <inheritdoc/>
		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
			// give the player a gun in the world
			var gun = new WorldGun(worldPlayer, world.Configuration.PlayerControl.DefaultPosition);

			gun.PercentX.Value = world.Guns.Count == 0 ? ((Fixed)1) / 4 : ((Fixed)3) / 4;

			world.Guns.Add(gun.Identifier, gun);
			worldPlayer.ControlledGuns.Add(gun.Identifier, gun);
		}

		/// <inheritdoc/>
		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
			foreach (var gunKvp in worldPlayer.ControlledGuns)
			{
				world.Guns.Remove(gunKvp.Key);
			}
		}

		/// <inheritdoc/>
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
				if (player.Player.Input.Down.IsDown)
				{
					targetAngle = world.Configuration.PlayerControl.DownPosition;
				}
				else if (player.Player.Input.Up.IsDown)
				{
					targetAngle = world.Configuration.PlayerControl.UpPosition;
				}
				else
				{
					targetAngle = world.Configuration.PlayerControl.DefaultPosition;
				}

				foreach (var gun in player.ControlledGuns)
				{
					if (gun.Value.Angle.Value != targetAngle)
					{
						gun.Value.Angle.Value = targetAngle;
					}

					if (player.Player.Input.Fire.CurrentState == InputButtonState.Pressed)
					{
						// Limit the amount of projectiles in the game.
						// Don't create new projectiles if there are too many.
						if (world.Projectiles.Count >= world.MaximumProjectiles)
						{
							continue;
						}

						var velocityVector = FixedVector2.Rotate(FixedVector2.Right, gun.Value.Angle.Value.Inclination * Constants.Deg2Rad);

						var projectilePosition = gun.Value.Bounds.NormalizedToPoint(gun.Value.Angle.Value.BulletOffset);

						var projectile = new WorldProjectile(gun.Value);
						projectile.Position.Value = projectilePosition;
						projectile.Velocity.Value = velocityVector * world.Configuration.PlayerControl.BulletSpeed;

						world.Projectiles.Add(projectile.Identifier, projectile);
					}
				}
			}
		}
	}
}
