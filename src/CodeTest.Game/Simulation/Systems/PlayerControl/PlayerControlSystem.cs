using System;

namespace CodeTest.Game
{
	public class PlayerControlSystem : IWorldSystem
	{
		private readonly World world;
		private readonly PlayerSpawnerConfiguration configuration;

		public PlayerControlSystem(World world, PlayerSpawnerConfiguration configuration)
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
			foreach (var player in world.Players)
			{
				if (player.Input.Fire.CurrentState == InputButtonState.Pressed)
				{
					foreach (var gun in player.ControlledGuns)
					{

					}
				}
			}
		}
	}
}
