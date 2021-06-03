namespace CodeTest.Game.Simulation.Systems.EnemySpawning
{
	public class EnemySpawnerSystemFactory : IWorldSystemFactory
	{
		private readonly EnemySpawnerConfiguration configuration;

		public EnemySpawnerSystemFactory(EnemySpawnerConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IWorldSystem Build(World world)
		{
			return new EnemySpawnerSystem(world, configuration);
		}
	}
}
