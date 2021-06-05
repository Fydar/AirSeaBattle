namespace CodeTest.Game.Simulation.Systems.PlayerControl
{
	/// <summary>
	/// A <see cref="IWorldSystemFactory"/> that provides a <see cref="ProjectileMovementSystem"/>.
	/// </summary>
	public class PlayerControlSystemFactory : IWorldSystemFactory
	{
		/// <inheritdoc/>
		public IWorldSystem Build(World world)
		{
			return new PlayerControlSystem(world);
		}
	}
}
