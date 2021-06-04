namespace CodeTest.Game.Simulation.Systems.ProjectileMovement
{
	public class ProjectileMovementSystemFactory : IWorldSystemFactory
	{
		public ProjectileMovementSystemFactory()
		{
		}

		public IWorldSystem Build(World world)
		{
			return new ProjectileMovementSystem(world);
		}
	}
}
