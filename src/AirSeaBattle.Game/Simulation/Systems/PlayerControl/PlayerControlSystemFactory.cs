namespace AirSeaBattle.Game.Simulation.Systems.PlayerControl
{
	/// <summary>
	/// A <see cref="IWorldSystemFactory"/> that provides a <see cref="PlayerControlSystem"/> implementation.
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
