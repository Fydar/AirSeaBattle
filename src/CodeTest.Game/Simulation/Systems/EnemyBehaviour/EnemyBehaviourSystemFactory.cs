namespace CodeTest.Game.Simulation.Systems.EnemyBehaviour
{
	public class EnemyBehaviourSystemFactory : IWorldSystemFactory
	{
		private readonly EnemyBehaviourConfiguration configuration;

		public EnemyBehaviourSystemFactory(EnemyBehaviourConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IWorldSystem Build(World world)
		{
			return new EnemyBehaviourSystem(world, configuration);
		}
	}
}
