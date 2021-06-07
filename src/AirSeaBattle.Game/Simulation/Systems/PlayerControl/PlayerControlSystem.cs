using AirSeaBattle.Game.Control;
using AirSeaBattle.Game.Simulation.Models;
using Industry.Simulation.Math;

namespace AirSeaBattle.Game.Simulation.Systems.PlayerControl
{
	/// <summary>
	/// The system responcible for aiming the player gun and creating projectiles.
	/// </summary>
	public class PlayerControlSystem : IWorldSystem
	{
		private readonly World world;

		internal PlayerControlSystem(World world)
		{
			this.world = world;
		}

		/// <inheritdoc/>
		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
			// Give the player a gun in the world
			var gun = new WorldGun(worldPlayer, world.Configuration.PlayerControl.DefaultPosition);

			gun.PercentX.Value = world.Guns.Count == 0 ? ((Fixed)1) / 4 : ((Fixed)3) / 4;

			world.Guns.Add(gun.Identifier, gun);
			worldPlayer.ControlledGuns.Add(gun.Identifier, gun);
		}

		/// <inheritdoc/>
		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
			// Remove all guns that belonged to the player that left.
			foreach (var gunKvp in worldPlayer.ControlledGuns)
			{
				world.Guns.Remove(gunKvp.Key);
			}
		}

		/// <inheritdoc/>
		public void OnUpdate(UpdateParameters parameters)
		{
			// Do nothing if the game has ended.
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
					// Change the guns angle configuration
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
