namespace CodeTest.Game
{
	public class PlayerSpawnerSystemFactory : IWorldSystemFactory
	{
		private readonly PlayerSpawnerConfiguration configuration;

		public PlayerSpawnerSystemFactory(PlayerSpawnerConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IWorldSystem Build(World world)
		{
			return new PlayerSpawnerSystem(world, configuration);
		}
	}
}
