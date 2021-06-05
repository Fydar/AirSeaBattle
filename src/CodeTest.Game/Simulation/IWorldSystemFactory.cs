namespace CodeTest.Game.Simulation
{
	/// <summary>
	/// A factory for a system that drives gameplay and behaviour.
	/// </summary>
	public interface IWorldSystemFactory
	{
		/// <summary>
		/// Creates an <see cref="IWorldSystem"/> that drives gameplay and behaviour.
		/// </summary>
		/// <param name="world">The world to create a <see cref="IWorldSystem"/> for.</param>
		/// <returns>A <see cref="IWorldSystem"/> for the <see cref="World"/>.</returns>
		IWorldSystem Build(World world);
	}
}
