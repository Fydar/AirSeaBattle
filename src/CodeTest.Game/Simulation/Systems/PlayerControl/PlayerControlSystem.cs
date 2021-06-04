using CodeTest.Game.Control;
using CodeTest.Game.Math;
using System;

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
			foreach (var playerKvp in world.Players)
			{
				var player = playerKvp.Value;

				if (player.Input.Fire.CurrentState == InputButtonState.Pressed)
				{
					var targetAngle = configuration.DefaultPosition;
					if (player.Input.Up.IsDown)
					{
						targetAngle = configuration.DownPosition;
					}
					else if (player.Input.Down.IsDown)
					{
						targetAngle = configuration.UpPosition;
					}

					foreach (var gun in player.ControlledGuns)
					{
						gun.Value.Angle.Value = targetAngle;
					}
				}
			}
		}
	}
}
