namespace CodeTest.Game
{
	public class PlayerControlSystemFactory : IWorldSystemFactory
	{
		private readonly PlayerControlConfiguration configuration;

		public PlayerSpawnerSystemFactory(PlayerControlConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IWorldSystem Build(World world)
		{
			return new PlayerSpawnerSystem(world, configuration);
		}
	}
}
