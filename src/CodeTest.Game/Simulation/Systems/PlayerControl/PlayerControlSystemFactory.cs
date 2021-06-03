namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	public class PlayerControlSystemFactory : IWorldSystemFactory
	{
		private readonly PlayerControlConfiguration configuration;

		public PlayerControlSystemFactory(PlayerControlConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IWorldSystem Build(World world)
		{
			return new PlayerControlSystem(world, configuration);
		}
	}
}
