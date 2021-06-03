using System;

namespace CodeTest.Game
{
	public class PlayerSpawnerSystem : IWorldSystem
	{
		private readonly World world;
		private readonly PlayerSpawnerConfiguration configuration;

		public PlayerSpawnerSystem(World world, PlayerSpawnerConfiguration configuration)
		{
			this.world = world;
			this.configuration = configuration;
		}

		public void OnPlayerJoined(WorldPlayer worldPlayer)
		{
			// give the player a gun in the world
			var gun = new WorldGun()
			{
				PositionX = world.Guns.Count == 0 ? 0.25f : 0.75f
			};

			world.Guns.Add(gun);
			worldPlayer.ControlledGuns.Add(gun);
		}

		public void OnPlayerRemoved(WorldPlayer worldPlayer)
		{
			foreach (var gun in worldPlayer.ControlledGuns)
			{
				world.Guns.Remove(gun);
			}
		}

		public void OnUpdate(UpdateParameters parameters)
		{
		}
	}
}
