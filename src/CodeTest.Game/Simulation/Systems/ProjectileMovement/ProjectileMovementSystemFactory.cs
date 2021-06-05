namespace CodeTest.Game.Simulation.Systems.ProjectileMovement
{
	/// <summary>
	/// A <see cref="IWorldSystemFactory"/> that provides a <see cref="ProjectileMovementSystem"/>.
	/// </summary>
	public class ProjectileMovementSystemFactory : IWorldSystemFactory
	{
		/// <inheritdoc/>
		public IWorldSystem Build(World world)
		{
			return new ProjectileMovementSystem(world);
		}
	}
}
